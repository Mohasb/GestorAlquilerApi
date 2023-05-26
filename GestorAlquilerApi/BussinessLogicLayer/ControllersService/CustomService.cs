using System;
using System.Net;
using AutoMapper;
using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Interfaces;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using GestorAlquilerApi.DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.BussinessLogicLayer.ControllersService
{
    public class CustomService : ICustomService
    {
        private readonly IQueryPlanning _planning;
        private readonly IQueryCar _car;
        private readonly IQueryReservation _reservation;
        private readonly IMapper _mapper;

        public CustomService(
            IMapper mapper,
            IQueryPlanning planning,
            IQueryCar car,
            IQueryReservation reservation
        )
        {
            _planning = planning;
            _car = car;
            _reservation = reservation;
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

        public IActionResult GetReservationByClient(int id)
        {
            var reservations = _reservation.GetDataReservationByClient(id).ToList();

            if (reservations.Count() == 0)
            {
                return new JsonResult(
                    new
                    {
                        statusCode = (int)HttpStatusCode.NotFound,
                        isOk = false,
                        responseText = $"There are no reservations for user with id:{id}"
                    }
                );
            }

            return new JsonResult(
                new
                {
                    statusCode = (int)HttpStatusCode.OK,
                    isOk = true,
                    reservations
                }
            );
        }
    }
}
