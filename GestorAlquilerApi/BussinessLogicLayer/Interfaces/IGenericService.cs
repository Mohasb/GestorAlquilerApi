using Microsoft.AspNetCore.Mvc;

namespace GestorAlquilerApi.BussinessLogicLayer.Interfaces
{
    public interface IGenericService<T>
    {
        Task<ActionResult<IEnumerable<T>>> GetAllElements();
        Task<ActionResult<T>> GetElementById(int id);
        Task<IActionResult> EditElement(int id, T element);
        Task<ActionResult<T>> AddElement(T element);
        Task<IActionResult> RemoveElement(int id);
    }
}
