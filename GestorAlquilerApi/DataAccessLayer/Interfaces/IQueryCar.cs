using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.DataAccessLayer.Interfaces
{
    public interface IQueryCar
    {
        public DbSet<Car> GetDataCars();
        public void Remove(Car car);
        public void AddCar(Car car);
    }
}
