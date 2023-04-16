using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GestorAlquilerApi.BussinessLogicLayer.Interfaces
{
    public interface IClientService
    {
        Task<ActionResult<IEnumerable<ClientDTO>>> GetClients();
        Task<ActionResult<ClientDTO>> GetClient(int id);
        Task<IActionResult> PutClient(int id, ClientDTO clientDTO);
        Task<ActionResult<ClientDTO>> PostClient(ClientDTO clientDTO);
        Task<IActionResult> DeleteClient(int id);
    }
}
