using Microsoft.AspNetCore.Mvc;

namespace GestorAlquilerApi.BussinessLogicLayer.Interfaces
{
    public interface ISetUserAdminService
    {
        Task<IActionResult> PutClient(string email);
        bool ClientExists(int id);
    }
}
