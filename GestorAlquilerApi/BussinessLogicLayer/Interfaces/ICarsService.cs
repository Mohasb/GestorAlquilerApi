using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GestorAlquilerApi.BussinessLogicLayer.Interfaces
{
    public interface ICarsService
    {
        Task<ActionResult<IEnumerable<CarDTO>>> GetCars();
        Task<ActionResult<CarDTO>> GetCar(int id);
        Task<IActionResult> PutCar(int id, CarDTO carDTO);
        Task<ActionResult<CarDTO>> PostCar(CarDTO carDTO);
        Task<IActionResult> DeleteCar(int id);
    }
}
