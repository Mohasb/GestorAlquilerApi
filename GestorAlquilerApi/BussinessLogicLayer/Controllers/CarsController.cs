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

        /// <summary>
        /// Returns a List of all Cars
        /// </summary>
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<CarDTO>>> GetCar() =>
            await _carsService.GetAllElements();

        /// <summary>
        /// Returns a Car by its Id
        /// </summary>
        /// <param name="id">The id of the Car.</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/car/5
        ///
        /// </remarks>
        /// <response code="200">Returns the car</response>
        /// <response code="400">If there are no car with that id</response>
        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<CarDTO>> GetCar(int id) =>
            await _carsService.GetElementById(id);

        /// <summary>
        /// Edit a car by its Id
        /// </summary>
        /// <param name="id">The id of the Car.</param>
        /// <param name="carDTO">Body of the new Car</param>
        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutCar(int id, CarDTO carDTO) =>
            await _carsService.EditElement(id, carDTO);

        /// <summary>
        /// Add a new Car
        /// </summary>
        /// <param name="carDTO">Body of the Car (REGISTRATION UNIQUE, BRANCHID FOREIGN KEY -> BRANCHES ID)</param>
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<CarDTO>> PostCar(CarDTO carDTO) =>
            await _carsService.AddElement(carDTO);

        /// <summary>
        /// Delete a Car
        /// </summary>
        /// <param name="id">id of the car to delete</param>
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCar(int id) => await _carsService.RemoveElement(id);
    }
}
