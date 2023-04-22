using AutoMapper;
using GestorAlquilerApi.BussinessLogicLayer.Interfaces;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using GestorAlquilerApi.DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                return Problem($"There are no Client with id:{id}");
            }
            var clientDto = _mapper.Map<ClientDTO>(client);

            return clientDto;
        }

        public async Task<IActionResult> EditElement(int id, ClientDTO clientDTO)
        {
            var client = _mapper.Map<Client>(clientDTO);
            client.Id = id;

            if (id != client.Id)
            {
                return BadRequest();
            }
            _saveData.ModifiedState(client);

            try
            {
                await _saveData.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        public async Task<ActionResult<ClientDTO>> AddElement(ClientDTO clientDTO)
        {
            var client = _mapper.Map<Client>(clientDTO);
            client.Password = BCrypt.Net.BCrypt.HashPassword(client.Password);
            client.ConfirmationPassword = BCrypt.Net.BCrypt.HashPassword(client.ConfirmationPassword);

            if (_clients == null)
            {
                return Problem("Entity set 'ApiContext.Client'  is null.");
            }

            _repository.AddClient(client);

            await _saveData.SaveChangesAsync();

            return CreatedAtAction("GetClient", new { id = client.Id }, client);
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
