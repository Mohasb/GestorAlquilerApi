using GestorAlquilerApi.BussinessLogicLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.DataAccessLayer.Interfaces
{
    public interface IQueryCarStore 
    {
        public DbSet<CarStore> GetDataCars();
        public void Remove(CarStore car);
        public void AddCar(CarStore car);
    }
}
