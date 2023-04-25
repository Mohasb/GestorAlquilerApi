using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using GestorAlquilerApi.DataAccessLayer.Data;
using GestorAlquilerApi.DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace GestorAlquilerApi.DataAccessLayer.Repository
{
    public class PlanningQueries : IQueryPlanning
    {
        private readonly ApiContext _context;

        public PlanningQueries(ApiContext context)
        {
            _context = context;
        }

        public DbSet<Planning> GetDataPlanning() => _context.Planning;

        public void AddPlanning(Planning planning) => _context.Add(planning);

        public void Remove(Planning planning) => _context.Remove(planning);

        public IQueryable<Planning> PlanningCarCategory(Car carDTO) =>
            from p in _context.Planning
            where p.CarCategory == carDTO.Category && p.BranchId == carDTO.BranchId
            select p;

        public List<Car> GetCarsAvailables(
            int branchId,
            DateTime startDate,
            DateTime endDate,
            int age
        )
        {


            var data = from p in _context.Planning
                       where p.Day >= startDate
                       && p.Day <= endDate
                       && p.BranchId == branchId
                       group p by p.CarCategory;

            List<Car> cars = new();

            foreach (var group in data)
            {
                if (group.All(d => d.CarsAvailables > 0))
                {
                    // Obtiene los coches del branch correspondiente
                    var branchCars = _context.Car.Where(c => c.BranchId == branchId);

                    // Filtra los coches por categoría
                    var filteredCars = branchCars.Where(c => c.Category == group.Key);

                    // Agrega los coches filtrados a la lista de coches
                    cars.AddRange(filteredCars);
                }
            }


            return cars;

            //var cars = (
            //    from p in _context.Planning
            //    from b in _context.Branch
            //    from c in _context.Car
            //    where
            //        p.BranchId == b.Id
            //        && p.BranchId == branchId
            //        && p.Day >= startDate
            //        && p.Day <= endDate
            //        && c.BranchId == branchId
            //    select c
            //).GroupBy(c => c.Category).Select(c => c.First());


            //return cars;
        }
    }
}
