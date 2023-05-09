using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestorAlquilerApi.BussinessLogicLayer.Interfaces
{
    public interface ILoginService
    {
        ClientDTO? AuthenticateUser(UserDTO user);
        IActionResult Login(UserDTO user);
        ClientDTO? CheckUserEmailPassword(UserDTO user);
        string GenerateToken(ClientDTO user);
    }
}
