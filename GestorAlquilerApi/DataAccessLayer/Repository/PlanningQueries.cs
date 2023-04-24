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

        public IQueryable<Car> GetCarsAvailables(
            int branchId,
            DateTime startDate,
            DateTime endDate,
            int age
        )
        {




            var cars = (
                from p in _context.Planning
                from b in _context.Branch
                from c in _context.Car
                where
                    p.BranchId == b.Id
                    && p.BranchId == branchId
                    && p.Day >= startDate
                    && p.Day <= endDate
                    && c.BranchId == branchId
                select c
            ).GroupBy(c => c.Category).Select(c => c.First());


            return cars;
        }
    }
}
