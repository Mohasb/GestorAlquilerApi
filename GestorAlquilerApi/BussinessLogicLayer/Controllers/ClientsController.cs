using Microsoft.AspNetCore.Mvc;
using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Interfaces;

namespace GestorAlquilerApi.BussinessLogicLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController
    {
        private readonly IGenericService<ClientDTO> _clientService;

        public ClientsController(IGenericService<ClientDTO> clientService)
        {
            _clientService = clientService;
        }

        // GET: api/Clients
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetClient() =>
            await _clientService.GetAllElements();

        // GET: api/Clients/5
        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<ClientDTO>> GetClient(int id) =>
            await _clientService.GetElementById(id);

        // PUT: api/Clients/5
        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutClient(int id, ClientDTO clientDTO) =>
            await _clientService.EditElement(id, clientDTO);

        // POST: api/Clients
        [HttpPost]
        /*[Authorize(Roles = "Admin")]*/
        public async Task<ActionResult<ClientDTO>> PostClient(ClientDTO clientDTO) =>
            await _clientService.AddElement(clientDTO);

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteClient(int id) =>
            await _clientService.RemoveElement(id);
    }
}
