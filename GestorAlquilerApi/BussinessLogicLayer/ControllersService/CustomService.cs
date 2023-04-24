using System;
using GestorAlquilerApi.BussinessLogicLayer.Interfaces;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using GestorAlquilerApi.DataAccessLayer.Interfaces;

namespace GestorAlquilerApi.BussinessLogicLayer.ControllersService
{
    public class CustomService : ICustomService
    {
        private readonly IQueryPlanning _planning;

        public CustomService(IQueryPlanning planning)
        {
            _planning = planning;
        }

        public List<Car> GetAvailablesCars(int branchId, DateTime startDate, DateTime endDate, int age) { 
            return _planning.GetCarsAvailables(branchId, startDate, endDate, age);
        }
    }
}
