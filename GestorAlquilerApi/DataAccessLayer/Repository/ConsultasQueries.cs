using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using GestorAlquilerApi.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.DataAccessLayer.Repository
{
    public class ConsultasQueries : IQueryConsultas
    {
        public IQueryable<Car> GetCarsByBranchId(int id, DbSet<Car> cars, DbSet<Branch> branches)
            => from b in branches
               from c in cars
               where b.Id == id && c.BranchId == b.Id
               select c;

        public IQueryable<Planning> GetPlanningCars(DbSet<Planning> planning, DateTime date, int branchId, string carCategory)
           => from p in planning
              where p.Day == date && p.BranchId == branchId && p.CarCategory == carCategory
              select p;
    }
}
