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

        public DbSet<Car> GetDataCars()
        {
            return _context.Car;
        }
        public DbSet<Planning> GetDataPlanning(CarDTO carDTO)
        {
            var data = from p in _context.Planning
            where p.CarCategory == carDTO.Category && p.BranchId == carDTO.BranchId
                       select p;

            return _context.Planning;
        }

        public void AddCar(Car car)
        {
            _context.Add(car);
        }

        public void ModifiedState(Car car)
        {
            _context.Entry(car).State = EntityState.Modified;
        }

        public void Remove(Car car)
        {
            _context.Remove(car);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
