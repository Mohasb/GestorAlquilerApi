using GestorAlquilerApi.BussinessLogicLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.DataAccessLayer.Interfaces
{
    public interface IQueryPlanning
    {
        public DbSet<Planning> GetDataPlanning();
        public void ModifiedState(Planning planning);
        public Task SaveChangesAsync();
        public void Remove(Planning planning);
        public void AddPlanning(Planning planning);
    }
}
