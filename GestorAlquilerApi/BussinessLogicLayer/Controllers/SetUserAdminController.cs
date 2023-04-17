using GestorAlquilerApi.BussinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestorAlquilerApi.BussinessLogicLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SetUserAdminController
    {
        private readonly ISetUserAdminService _setUserAdminService;

        public SetUserAdminController(ISetUserAdminService setUserAdminService)
        {
            _setUserAdminService = setUserAdminService;
        }

        [HttpPut("{email}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutClient(string email) =>
            await _setUserAdminService.EditClient(email);

        private bool ClientExists(int id) => _setUserAdminService.CheckClientExists(id);
    }
}
