using GestorAlquilerApi.BussinessLogicLayer.Models;
using GestorAlquilerApi.DataAccessLayer.Data;
using GestorAlquilerApi.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace GestorAlquilerApi.DataAccessLayer.Repository
{
    public class PlanningQueries : IQueryPlanning
    {
        private readonly ApiContext _context;

        public PlanningQueries(ApiContext context)
        {
            _context = context;
        }

        public DbSet<Planning> GetDataPlanning() => _context.Planning;

        public void ModifiedState(Planning planning) =>
            _context.Entry(planning).State = EntityState.Modified;

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public void AddPlanning(Planning planning) => _context.Add(planning);

        public void Remove(Planning planning) => _context.Remove(planning);
    }
}
