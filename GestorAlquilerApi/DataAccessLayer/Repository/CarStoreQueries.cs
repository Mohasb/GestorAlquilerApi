using System;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using GestorAlquilerApi.DataAccessLayer.Data;
using GestorAlquilerApi.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.DataAccessLayer.Repository
{
    public class CarStoreQueries : IQueryCarStore
    {
        private readonly ApiContext _context;
        public CarStoreQueries(ApiContext context)
        {
            _context = context;
        }
        public DbSet<CarStore> GetDataCars() => _context.CarStore;
        public void AddCar(CarStore car) => _context.Add(car);
        public void Remove(CarStore car) => _context.Remove(car);
    }
}
