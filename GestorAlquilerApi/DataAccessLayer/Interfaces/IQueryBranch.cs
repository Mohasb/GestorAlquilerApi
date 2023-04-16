using GestorAlquilerApi.BussinessLogicLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.DataAccessLayer.Interfaces
{
    public interface IQueryBranch
    {
        public DbSet<Branch> GetDataBranches();
        public DbSet<Planning> GetDataPlanning();
        public void ModifiedState(Branch branch);
        public Task SaveChangesAsync();
        public void Remove(Branch branch);
        public void AddBranch(Branch branch);
        public void AddPlanning(Planning planning);
    }
}
