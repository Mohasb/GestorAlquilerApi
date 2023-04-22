using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using GestorAlquilerApi.DataAccessLayer.Data;
using GestorAlquilerApi.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.DataAccessLayer.Repository
{
    public class CarQueries : IQueryCar
    {
        private readonly ApiContext _context;

        public CarQueries(ApiContext context)
        {
            _context = context;
        }

        public DbSet<Car> GetDataCars() => _context.Car;
        public void AddCar(Car car) => _context.Add(car);
        public void Remove(Car car) => _context.Remove(car);

    }
}
