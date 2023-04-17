using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GestorAlquilerApi.BussinessLogicLayer.Interfaces
{
    public interface ICarsService
    {
        Task<ActionResult<IEnumerable<CarDTO>>> GetAllCars();
        Task<ActionResult<CarDTO>> GetCarById(int id);
        Task<IActionResult> EditCar(int id, CarDTO carDTO);
        Task<ActionResult<CarDTO>> AddCar(CarDTO carDTO);
        Task<IActionResult> RemoveCar(int id);
    }
}
