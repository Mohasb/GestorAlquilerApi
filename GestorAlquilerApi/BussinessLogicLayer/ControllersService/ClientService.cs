using System.Net;
using AutoMapper;
using GestorAlquilerApi.BussinessLogicLayer.Interfaces;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using GestorAlquilerApi.DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace GestorAlquilerApi.BussinessLogicLayer.ControllersService
{
    public class ClientService<ClientDTO> : ControllerBase, IGenericService<ClientDTO>
    {
        private readonly IQueryClient _repository;
        private readonly IMapper _mapper;
        private readonly DbSet<Client> _clients;
        private readonly ISaveData<Client> _saveData;

        public ClientService(IQueryClient repository, IMapper mapper, ISaveData<Client> saveData)
        {
            _repository = repository;
            _mapper = mapper;
            _clients = _repository.GetDataClients();
            _saveData = saveData;
        }

        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetAllElements()
        {
            if (_clients == null)
            {
                return NotFound();
            }

            var countClients = (from c in _clients select c).Count();

            if (!Convert.ToBoolean(countClients))
                return NotFound("There are no Clients");

            var data = _clients;
            var clients = data.Select(c => _mapper.Map<ClientDTO>(c));

            return await clients.ToListAsync();
        }

        public async Task<ActionResult<ClientDTO>> GetElementById(int id)
        {
            if (_clients == null)
            {
                return NotFound();
            }
            var client = await _clients.FindAsync(id);

            if (client == null)
            {
                return Problem($"There is no Client with id:{id}");
            }

            string firstDigits = client!.BankAccount!.Substring(0, client.BankAccount.Length - 4);
            string asteriscos = new String('*', firstDigits.Length);

            string lastFourDigits = client.BankAccount!.Substring(client.BankAccount.Length - 4);

            client.BankAccount = asteriscos + lastFourDigits;

            var clientDto = _mapper.Map<ClientDTO>(client);

            return clientDto;
        }

        public async Task<IActionResult> EditElement(int id, ClientDTO clientDTO)
        {
            //Obtener cliente antes de modificar
            var clientBefore = _clients.AsNoTracking().FirstOrDefault(c => c.Id == id);
            //Mapeo de el DTO al modelo
            var client = _mapper.Map<Client>(clientDTO);
            //establecer el id del cliente con el id pasado por parámetro
            client.Id = id;
            //El password encriptado se sobreescribe de lo recibido del front ("editedByBackend") por que es requerido en el DTO
            client.Password = clientBefore!.Password;

            /*Si la contraseña no se ha modificado en el front puede ser que llegue aqui con los asteriscos(***********253) si es asi se obtiene la del clientBefore que no esta modificada. Si se modifica en el front no tendra * y por lo tanto no entrará y se actualizará*/

            if (client.BankAccount!.Contains("*"))
            {
                client.BankAccount = clientBefore!.BankAccount;
            }

            //si el id psado no es igual al id del cliente
            if (id != client.Id)
            {
                return new JsonResult(
                    new { statusCode = (int)HttpStatusCode.BadRequest, isOk = false, }
                );
            }

            //GAdvierte que se ha modificado los datos del cliente
            _saveData.ModifiedState(client);

            try
            {
                //Intenta guardar cambios
                await _saveData.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //Si el cliente no exist
                if (!ClientExists(id))
                {
                    return new JsonResult(
                        new { statusCode = (int)HttpStatusCode.NotFound, isOk = false, }
                    );
                }
                else
                {
                    return new JsonResult(
                        new { statusCode = (int)HttpStatusCode.BadRequest, isOk = false, }
                    );
                }
            }

            //Retorna al front un string modificado con ****** la cuenta bancária
            string firstDigits = client.BankAccount!.Substring(0, client.BankAccount.Length - 4);
            string asteriscos = new String('*', firstDigits.Length);
            string lastFourDigits = client.BankAccount!.Substring(client.BankAccount.Length - 4);
            client.BankAccount = asteriscos + lastFourDigits;

            return new JsonResult(
                new
                {
                    statusCode = (int)HttpStatusCode.OK,
                    isOk = true,
                    client = client,
                }
            );
        }

        public async Task<ActionResult<ClientDTO>> AddElement(ClientDTO clientDTO)
        {
            var client = _mapper.Map<Client>(clientDTO);
            client.Password = BCrypt.Net.BCrypt.HashPassword(client.Password);

            if (_clients == null)
            {
                return Problem("Entity set 'ApiContext.Client'  is null.");
            }

            try
            {
                _repository.AddClient(client);
                await _saveData.SaveChangesAsync();

                return new JsonResult(
                    new
                    {
                        statusCode = (int)HttpStatusCode.OK,
                        isOk = true,
                        client = client
                    }
                );
            }
            catch (Exception err)
            {
                var errorMessage = err.InnerException!.Message;

                if (errorMessage.Contains("UNIQUE") && errorMessage.Contains("Registration"))
                {
                    return new JsonResult(
                        new
                        {
                            statusCode = (int)HttpStatusCode.BadRequest,
                            isOk = false,
                            responseText = "Registration not unique"
                        }
                    );
                }
                else if (errorMessage.Contains("UNIQUE") && errorMessage.Contains("Email"))
                {
                    return new JsonResult(
                        new
                        {
                            statusCode = (int)HttpStatusCode.OK,
                            isOk = false,
                            responseText = "Email not unique"
                        }
                    );
                }
                else
                {
                    return new JsonResult(
                        new
                        {
                            statusCode = (int)HttpStatusCode.OK,
                            isOk = false,
                            responseText = errorMessage
                        }
                    );
                }
            }
        }

        public async Task<IActionResult> RemoveElement(int id)
        {
            if (_clients == null)
            {
                return NotFound();
            }
            var client = await _clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _repository.Remove(client);
            await _saveData.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientExists(int id)
        {
            return (_clients?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
