using System;
using Microsoft.AspNetCore.Mvc;

namespace GestorAlquilerApi.BussinessLogicLayer.Interfaces
{
    public interface ICarStoreService<CarDTO>
    {
        Task<ActionResult<IEnumerable<CarDTO>>> GetAllElements();
        Task<ActionResult<CarDTO>> GetElementById(int id);
        Task<IActionResult> EditElement(int id, CarDTO carDTO);
        Task<ActionResult<CarDTO>> AddElement(CarDTO carDTO);
        Task<IActionResult> RemoveElement(int id);
    }
}
