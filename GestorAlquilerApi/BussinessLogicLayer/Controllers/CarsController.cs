using Microsoft.AspNetCore.Mvc;
using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Interfaces;

namespace GestorAlquilerApi.BussinessLogicLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController
    {
        private readonly ICarsService _carsService;

        public CarsController(ICarsService carsService)
        {
            _carsService = carsService;
        }

        // GET: api/Cars
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<CarDTO>>> GetCar()
        => await _carsService.GetCars();

        // GET: api/Cars/{id}
        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<CarDTO>> GetCar(int id)
        => await _carsService.GetCar(id);

        // PUT: api/Cars/{id}
        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutCar(int id, CarDTO carDTO)
        => await _carsService.PutCar(id, carDTO);

        // POST: api/Cars
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<CarDTO>> PostCar(CarDTO carDTO)
        => await _carsService.PostCar(carDTO);

        // DELETE: api/Cars/{id}
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCar(int id)
        => await _carsService.DeleteCar(id);
    }
}
