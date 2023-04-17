using GestorAlquilerApi.BussinessLogicLayer.Interfaces;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using GestorAlquilerApi.DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.BussinessLogicLayer.ControllersService
{
    public class SetUserAdminService : ControllerBase, ISetUserAdminService
    {
        private readonly IQueryClient _repository;
        private readonly DbSet<Client> _users;

        public SetUserAdminService(IQueryClient repository)
        {
            _repository = repository;
            _users = _repository.GetDataClients();
        }

        public async Task<IActionResult> PutClient(string email)
        {
            var client = _users.FirstOrDefault(x => x.Email == email);

            if (client == null)
            {
                return NotFound("User not found");
            }
            client.Rol = "Admin";
            _repository.ModifiedState(client);

            try
            {
                await _repository.SaveChangesAsync();
                return Ok("User updated successfully");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(client.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        public bool ClientExists(int id)
        {
            return (_users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
