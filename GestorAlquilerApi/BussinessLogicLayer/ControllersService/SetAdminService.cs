using GestorAlquilerApi.BussinessLogicLayer.Interfaces;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using GestorAlquilerApi.DataAccessLayer.Interfaces;
using GestorAlquilerApi.DataAccessLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.BussinessLogicLayer.ControllersService
{
    public class SetAdminService : ControllerBase, ISetAdminService
    {
        private readonly IQueryClient _repository;
        private readonly DbSet<Client> _users;
        private readonly ISaveData<Client> _saveData;

        public SetAdminService(IQueryClient repository, ISaveData<Client> saveData)
        {
            _repository = repository;
            _users = _repository.GetDataClients();
            _saveData = saveData;
        }

        public async Task<IActionResult> EditUserRol(string email)
        {
            var client = _users.FirstOrDefault(x => x.Email == email);

            if (client == null)
            {
                return NotFound("User not found");
            }
            client.Rol = "Admin";
            _saveData.ModifiedState(client);

            try
            {
                await _saveData.SaveChangesAsync();
                return Ok("User updated successfully");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckClientExists(client.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        public bool CheckClientExists(int id)
        {
            return (_users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    
}
}
