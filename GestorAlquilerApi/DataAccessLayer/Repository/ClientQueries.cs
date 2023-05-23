using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using GestorAlquilerApi.DataAccessLayer.Data;
using GestorAlquilerApi.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.DataAccessLayer.Repository
{
    public class ClientQueries : IQueryClient
    {
        private readonly ApiContext _context;

        public ClientQueries(ApiContext context)
        {
            _context = context;
        }

        public DbSet<Client> GetDataClients() => _context.Client;

        public void AddClient(Client client) => _context.Add(client);

        public void Remove(Client client) => _context.Remove(client);

        public Client? GetClientByEmail(UserDTO user) =>
            (
                from c in _context.Client
                where c.Email!.ToLower() == user.Email!.ToLower()
                select c
            ).FirstOrDefault();
    }
}
