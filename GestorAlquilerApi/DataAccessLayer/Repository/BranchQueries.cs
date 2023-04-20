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

        public DbSet<Branch> GetDataBranches() => _context.Branch;

        public DbSet<Planning> GetDataPlanning() => _context.Planning;

        public void AddBranch(Branch branch) => _context.Add(branch);

        public void Remove(Branch branch) => _context.Remove(branch);

        public void AddPlanning(Planning planning) => _context.Add(planning);
    }
}
