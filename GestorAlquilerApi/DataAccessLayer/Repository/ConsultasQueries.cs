using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using GestorAlquilerApi.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace GestorAlquilerApi.DataAccessLayer.Repository
{
    public class ConsultasQueries : IQueryConsultas
    {
        public IQueryable<Car> GetCarsByBranchId(int id, DbSet<Car> cars, DbSet<Branch> branches)
        {
            var carsByBranch = from b in branches
                       from c in cars
                       where b.Id == id && c.BranchId == b.Id
                       select new Car
                       {
                           Id = c.Id,
                           Brand = c.Brand,
                           Model = c.Model,
                           FuelType = c.FuelType,
                           GearShiftType = c.GearShiftType,
                           Image = c.Image,
                           Category = c.Category,
                           BranchId = c.BranchId
                       };

            return carsByBranch;
        }
    }
}
