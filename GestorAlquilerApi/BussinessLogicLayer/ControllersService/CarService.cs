using AutoMapper;
using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Interfaces;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using GestorAlquilerApi.DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.BussinessLogicLayer.ControllersService
{
    public class CarService : ControllerBase, ICarsService
    {
        private readonly IQueryCar _repository;
        private readonly IMapper _mapper;
        private readonly DbSet<Car> _cars;

        public CarService(IQueryCar repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _cars = _repository.GetDataCars();
        }

        public async Task<ActionResult<IEnumerable<CarDTO>>> GetCars()
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

        public async Task<ActionResult<CarDTO>> GetCar(int id)
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

        public async Task<IActionResult> PutCar(int id, CarDTO carDTO)
        {
            var car = _mapper.Map<Car>(carDTO);
            car.Id = id;

            if (id != car.Id)
            {
                return BadRequest();
            }

            _repository.ModifiedState(car);

            try
            {
                await _repository.SaveChangesAsync();
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

        public async Task<ActionResult<CarDTO>> PostCar(CarDTO carDTO)
        {
            var car = _mapper.Map<Car>(carDTO);

            if (_cars == null)
            {
                return Problem("Entity set 'ApiContext.Car'  is null.");
            }

            _repository.AddCar(car);

            await _repository.SaveChangesAsync();

            //Here is addedd a this car to availables in planning
            AddCarToAvaildables(carDTO);

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        public async Task<IActionResult> DeleteCar(int id)
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
            await _repository.SaveChangesAsync();

            return NoContent();
        }

        /////////////////////////////////////////////HELPERS///////////////////////////////////
        private bool CarExists(int id)
        {
            return (_cars?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async void AddCarToAvaildables(CarDTO carDTO)
        {
            var planning = _repository.GetDataPlanning(carDTO);

            foreach (var plan in planning)
            {
                plan.CarsAvailables++;
            }
            await _repository.SaveChangesAsync();
        }
    }
}
