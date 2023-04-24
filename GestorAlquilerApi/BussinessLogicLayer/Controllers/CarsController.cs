using Microsoft.AspNetCore.Mvc;
using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace GestorAlquilerApi.BussinessLogicLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController
    {
        private readonly IGenericService<CarDTO> _carsService;

        public CarsController(IGenericService<CarDTO> carsService)
        {
            _carsService = carsService;
        }

        // GET: api/Cars
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<CarDTO>>> GetCar() =>
            await _carsService.GetAllElements();

        // GET: api/Cars/{id}
        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<CarDTO>> GetCar(int id) => await _carsService.GetElementById(id);

        // PUT: api/Cars/{id}
        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutCar(int id, CarDTO carDTO) =>
            await _carsService.EditElement(id, carDTO);

        // POST: api/Cars
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<CarDTO>> PostCar(CarDTO carDTO) =>
            await _carsService.AddElement(carDTO);

        // DELETE: api/Cars/{id}
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCar(int id) => await _carsService.RemoveElement(id);
    }
}
