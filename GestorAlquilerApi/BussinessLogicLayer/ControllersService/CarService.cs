﻿using AutoMapper;
using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Interfaces;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using GestorAlquilerApi.DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.BussinessLogicLayer.ControllersService
{
    public class CarService<CarDTO> : ControllerBase, IGenericService<CarDTO>
    {
        private readonly IQueryCar _repository;
        private readonly IMapper _mapper;
        private readonly DbSet<Car> _cars;
        private readonly IPermuteData<Car> _permuteData;

        public CarService(IQueryCar repository, IMapper mapper, IPermuteData<Car> permuteData)
        {
            _repository = repository;
            _mapper = mapper;
            _cars = _repository.GetDataCars();
            _permuteData = permuteData;
        }

        public async Task<ActionResult<IEnumerable<CarDTO>>> GetAllElements()
        {
            if (_cars == null)
            {
                return NotFound();
            }

            if (!Convert.ToBoolean(_cars.Count()))
                return NotFound("There are no Cars");

            var data = _cars;
            var cars = data.Select(c => _mapper.Map<CarDTO>(c));

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
            var car = _mapper.Map<Car>(carDTO);
            car.Id = id;

            if (id != car.Id)
            {
                return BadRequest();
            }

            _permuteData.ModifiedState(car);

            try
            {
                await _permuteData.SaveChangesAsync();
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
            var car = _mapper.Map<Car>(carDTO);

            if (_cars == null)
            {
                return Problem("Entity set 'ApiContext.Car'  is null.");
            }
            var valuesAsArray = Enum.GetNames(typeof(Car.Categories));

            if (!valuesAsArray.Contains(car.Category))
            {
                return Problem(
                    $"Category '{car.Category}' is invalid. It has to be in: '{string.Join(", ", valuesAsArray.SkipLast(1))} or {valuesAsArray[valuesAsArray.Length - 1]}'"
                );
            }

            _repository.AddCar(car);

            await _permuteData.SaveChangesAsync();

            //Here is addedd a this car to availables in planning
            //TODO: This is not working
            //AddCarToAvaildables(carDTO);

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
            //TODO: This is not working
            //RemoveCarFromAvaildables(_mapper.Map<CarDTO>(car));
            await _permuteData.SaveChangesAsync();

            return NoContent();
        }

        /////////////////////////////////////////////HELPERS///////////////////////////////////
        private bool CarExists(int id)
        {
            return (_cars?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        //TODO: add this method
        /* private async void AddCarToAvaildables(CarDTO carDTO)
        {
            var planning = _repository.GetDataPlanning(carDTO);

            foreach (var plan in planning)
            {
                plan.CarsAvailables++;
            }
            await _repository.SaveChangesAsync();
        }

        private async void RemoveCarFromAvaildables(CarDTO carDTO)
        {
            var planning = _repository.GetDataPlanning(carDTO);
            foreach (var plan in planning)
            {
                plan.CarsAvailables--;
            }
            await _repository.SaveChangesAsync();
        } */
    }
}
