using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.DataAccessLayer.Interfaces
{
    public interface IQueryClient
    {
        public DbSet<Client> GetDataClients();
        public void AddClient(Client client);
        public void Remove(Client Client);
        public Client? GetClientByEmail(UserDTO user);
    }
}
