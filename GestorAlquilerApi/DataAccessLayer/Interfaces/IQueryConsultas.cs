using GestorAlquilerApi.BussinessLogicLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.DataAccessLayer.Interfaces
{
    public interface IQueryConsultas
    {
        public IQueryable<Car> GetCarsByBranchId(int id, DbSet<Car> cars, DbSet<Branch> branches);
    }
}
