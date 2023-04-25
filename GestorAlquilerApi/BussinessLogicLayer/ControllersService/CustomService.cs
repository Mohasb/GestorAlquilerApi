using System;
using AutoMapper;
using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Interfaces;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using GestorAlquilerApi.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.BussinessLogicLayer.ControllersService
{
    public class CustomService : ICustomService
    {
        private readonly IQueryPlanning _planning;
        private readonly IQueryCar _car;
        private readonly IMapper _mapper;

        public CustomService(IMapper mapper, IQueryPlanning planning, IQueryCar car)
        {
            _planning = planning;
            _car = car;
            _mapper = mapper;
        }

        public List<Car> GetAvailablesCars(
            int branchId,
            DateTime startDate,
            DateTime endDate,
            int age
        )
        {
            return _planning.GetCarsAvailables(branchId, startDate, endDate, age);
        }

        public List<CarDTO> GetCarsByBranch(int branchId) 
        { 
            var cars = _car.GetDataCars().Where(c => c.BranchId == branchId).ToList();
            return _mapper.Map<List<CarDTO>>(cars);
        }
    }
}
