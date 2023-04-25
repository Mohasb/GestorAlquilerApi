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

        /// <summary>
        /// Returns a List of all Clients/users
        /// </summary>
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetClient() =>
            await _clientService.GetAllElements();

        /// <summary>
        /// Returns a Client/User by its Id
        /// </summary>
        /// <param name="id">The id of the Client/User.</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/client/5
        ///
        /// </remarks>
        /// <response code="200">Returns the Client/User</response>
        /// <response code="400">If there are no Client/User with that id</response>
        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<ClientDTO>> GetClient(int id) =>
            await _clientService.GetElementById(id);

        /// <summary>
        /// Edit a Client/User by its Id
        /// </summary>
        /// <param name="id">The id of the Client/User.</param>
        /// <param name="clientDTO">Body of the new Client/User</param>
        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutClient(int id, ClientDTO clientDTO) =>
            await _clientService.EditElement(id, clientDTO);

        /// <summary>
        /// Add a new Client/User
        /// </summary>
        /// <param name="clientDTO">Body of the Client/User (REGISTRATION UNIQUE, PASSWORDS MUST MATCH)</param>
        [HttpPost]
        /*[Authorize(Roles = "Admin")]*/
        public async Task<ActionResult<ClientDTO>> PostClient(ClientDTO clientDTO) =>
            await _clientService.AddElement(clientDTO);

        /// <summary>
        /// Delete a Client/User
        /// </summary>
        /// <param name="id">id of the Client/User to delete</param>
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteClient(int id) =>
            await _clientService.RemoveElement(id);
    }
}
