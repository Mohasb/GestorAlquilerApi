using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using Microsoft.AspNetCore.Mvc;
using GestorAlquilerApi.BussinessLogicLayer.Interfaces;


namespace GestorAlquilerApi.BussinessLogicLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarStoreController
    {
        private readonly ICarStoreService<CarDTO> _carStoreService;
        public CarStoreController(ICarStoreService<CarDTO> carStoreService)
        {
            _carStoreService = carStoreService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDTO>>> GetCars() =>
        await _carStoreService.GetAllElements();

        [HttpGet("{id}")]
        public async Task<ActionResult<CarDTO>> GetCarById(int id) =>
        await _carStoreService.GetElementById(id);

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarStore(int id, CarDTO carDTO) =>
            await _carStoreService.EditElement(id, carDTO);

        [HttpPost]
        public async Task<ActionResult<CarDTO>> PostCarStore(CarDTO carDTO) =>
            await _carStoreService.AddElement(carDTO);

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarStore(int id) => await _carStoreService.RemoveElement(id);
    }
}
