using Microsoft.AspNetCore.Mvc;

namespace GestorAlquilerApi.BussinessLogicLayer.Interfaces
{
    public interface ISetUserAdminService
    {
        Task<IActionResult> EditClient(string email);
        bool CheckClientExists(int id);
    }
}
