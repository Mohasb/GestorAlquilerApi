using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.DataAccessLayer.Interfaces
{
    public interface IQueryPlanning
    {
        public DbSet<Planning> GetDataPlanning();
        public void Remove(Planning planning);
        public void AddPlanning(Planning planning);
        public IQueryable<Planning> PlanningCarCategory(Car car);
        public IQueryable<Car> GetCarsAvailables(int branchId, DateTime startDate, DateTime endDate, int age);
    }
}
