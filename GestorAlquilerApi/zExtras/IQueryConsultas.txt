using GestorAlquilerApi.BussinessLogicLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.DataAccessLayer.Interfaces
{
    public interface IQueryConsultas
    {
        public IQueryable<Car> GetCarsByBranchId(int id, DbSet<Car> cars, DbSet<Branch> branches);
        public IQueryable<Planning> GetPlanningCars(
            DbSet<Planning> planning,
            DateTime date,
            int branchId,
            string carCategory
        );
    }
}
