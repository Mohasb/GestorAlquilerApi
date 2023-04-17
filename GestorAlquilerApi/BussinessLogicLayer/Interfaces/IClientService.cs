using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GestorAlquilerApi.BussinessLogicLayer.Interfaces
{
    public interface IClientService
    {
        Task<ActionResult<IEnumerable<ClientDTO>>> GetAllClients();
        Task<ActionResult<ClientDTO>> GetClientById(int id);
        Task<IActionResult> EditClient(int id, ClientDTO clientDTO);
        Task<ActionResult<ClientDTO>> AddClient(ClientDTO clientDTO);
        Task<IActionResult> RemoveClient(int id);
        ClientDTO? AuthenticateUser(UserDTO user);
    }
}
