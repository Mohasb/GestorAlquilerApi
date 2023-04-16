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
        public DbSet<Client> GetDataClients()
        {
            return _context.Client;
        }
        public void ModifiedState(Client client)
        {
            _context.Entry(client).State = EntityState.Modified;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void AddClient(Client client)
        {
            _context.Add(client);
        }
        public void Remove(Client client)
        {
            _context.Remove(client);
        }
    }
}
