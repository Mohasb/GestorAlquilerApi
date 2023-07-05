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
    public class CustomService : Controller, ICustomService
    {
        private readonly IQueryPlanning _planning;
        private readonly IQueryCar _car;
        private readonly DbSet<Client> _clients;
        private readonly ISaveData<Client> _saveData;
        private readonly IQueryClient _repository;
        private readonly IQueryReservation _reservation;
        private readonly IMapper _mapper;

        public CustomService(
            IMapper mapper,
            IQueryPlanning planning,
            IQueryCar car,
            IQueryReservation reservation,
            IQueryClient clients,
            IQueryClient repositoryClient,
            ISaveData<Client> saveData,
            IQueryClient repository

        )
        {
            _planning = planning;
            _car = car;
            _reservation = reservation;
            _mapper = mapper;
            _repository = repository;
            _clients = _repository.GetDataClients();
            _saveData = saveData;
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
        public IActionResult updatePwd(ClientDTO clientDTO, int id)
        {

            var client = _mapper.Map<Client>(clientDTO);
            client.Password = BCrypt.Net.BCrypt.HashPassword(client.Password);

            //si el id psado no es igual al id del cliente
            if (id != client.Id)
            {
                return new JsonResult(
                    new { statusCode = (int)HttpStatusCode.BadRequest, isOk = false, }
                );
            }

            //Advierte que se ha modificado los datos del cliente
            _saveData.ModifiedState(client);

            try
            {
                _saveData.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //Si el cliente no exist
                if (!ClientExists(id))
                {
                    return new JsonResult(
                        new { statusCode = (int)HttpStatusCode.NotFound, isOk = false, }
                    );
                }
                else
                {
                    return new JsonResult(
                        new { statusCode = (int)HttpStatusCode.BadRequest, isOk = false, }
                    );
                }
            }


            return new JsonResult(
                new
                {
                    statusCode = (int)HttpStatusCode.OK,
                    isOk = true,
                    client = client,
                }
            );

        }
        private bool ClientExists(int id)
        {
            return (_clients?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
