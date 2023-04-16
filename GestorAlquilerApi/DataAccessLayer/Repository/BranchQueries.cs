using GestorAlquilerApi.BussinessLogicLayer.Models;
using GestorAlquilerApi.DataAccessLayer.Data;
using GestorAlquilerApi.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.DataAccessLayer.Repository
{
    public class BranchQueries : IQueryBranch
    {
        private readonly ApiContext _context;
        public BranchQueries(ApiContext context)
        {
            _context = context;
        }
        public DbSet<Branch> GetDataBranches()
        {
            return _context.Branch;
        }
        public DbSet<Planning> GetDataPlanning()
        {
            return _context.Planning;
        }

        public void ModifiedState(Branch branch)
        {
             _context.Entry(branch).State = EntityState.Modified;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void AddBranch(Branch branch)
        {
            _context.Add(branch);
        }
        public void Remove(Branch branch)
        {
            _context.Remove(branch);
        }


        public void AddPlanning(Planning planning)
        {
            _context.Add(planning);
        }
    }
}
