using Microsoft.AspNetCore.Mvc;

namespace GestorAlquilerApi.BussinessLogicLayer.Interfaces
{
    public interface ISetAdminService
    {
        Task<IActionResult> EditUserRol(string email);
    }
}
