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
    public class CarStoreService<CarDTO> : Controller, ICarStoreService<CarDTO>
    {
        private readonly IQueryCarStore _repository;
        private readonly IMapper _mapper;
        private readonly DbSet<CarStore> _cars;
        private readonly ISaveData<CarStore> _saveData;

        public CarStoreService(IQueryCarStore repository, IMapper mapper, DbSet<CarStore> cars, ISaveData<CarStore> saveData)
        {
            _repository = repository;
            _mapper = mapper;
            _cars = cars;
            _saveData = saveData;
        }

        public async Task<ActionResult<IEnumerable<CarDTO>>> GetAllElements()
        {
            if (_cars == null)
            {
                return NotFound();
            }

            if (!Convert.ToBoolean(_cars.Count()))
                return NotFound("There are no Cars");

            var cars = _cars.Select(c => _mapper.Map<CarDTO>(c));

            return await cars.ToListAsync();
        }

        public async Task<ActionResult<CarDTO>> GetElementById(int id)
        {
            if (_cars == null)
            {
                return NotFound();
            }
            var car = await _cars.FindAsync(id);

            if (car == null)
            {
                return Problem($"There are no Car with id:{id}");
            }

            var carDTO = _mapper.Map<CarDTO>(car);

            return carDTO;
        }

        public async Task<IActionResult> EditElement(int id, CarDTO carDTO)
        {
            var car = _mapper.Map<CarStore>(carDTO);
            car.Id = id;

            if (id != car.Id)
            {
                return BadRequest();
            }

            _saveData.ModifiedState(car);

            try
            {
                await _saveData.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        public async Task<ActionResult<CarDTO>> AddElement(CarDTO carDTO)
        {
            var car = _mapper.Map<CarStore>(carDTO);

            if (_cars == null)
            {
                return Problem("Entity set 'ApiContext.Car'  is null.");
            }

            try
            {
                var valuesAsArray = Enum.GetNames(typeof(CarStore.Categories));
                if (!valuesAsArray.Contains(car.Category))
                {
                    return BadRequest(
                        $"Category '{car.Category}' is invalid. It has to be in: {string.Join(", ", valuesAsArray.SkipLast(1))} or {valuesAsArray[^1]}"
                    );
                }

                if (car.Category != null)
                {
                    CarStore.Prices price = (CarStore.Prices)Enum.Parse(typeof(CarStore.Prices), car.Category);
                    car.Price = (decimal?)price;
                }

                _repository.AddCar(car);

                await _saveData.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("UNIQUE"))
                {
                    return BadRequest(
                        $"Problem adding car. There is already a car with Registration = {car.Registration}."
                    );
                }
                else if (ex.InnerException != null && ex.InnerException.Message.Contains("FOREIGN"))
                {
                    return BadRequest(
                        $"Problem adding car. The branch = {car.BranchId} is not correct."
                    );
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        public async Task<IActionResult> RemoveElement(int id)
        {
            if (_cars == null)
            {
                return NotFound();
            }
            var car = await _cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _repository.Remove(car);
            //Delete car from availables planning
            await _saveData.SaveChangesAsync();

            return NoContent();
        }

        /////////////////////////////////////////////HELPERS///////////////////////////////////
        private bool CarExists(int id)
        {
            return (_cars?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }

}
