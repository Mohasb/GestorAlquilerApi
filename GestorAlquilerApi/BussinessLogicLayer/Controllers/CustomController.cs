using GestorAlquilerApi.BussinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestorAlquilerApi.BussinessLogicLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomController
    {
        private readonly ISetAdminService _setUserAdminService;

        public CustomController(ISetAdminService setUserAdminService)
        {
            _setUserAdminService = setUserAdminService;
        }

        [HttpPut("setAdmin/{email}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetAdmin(string email) =>
            await _setUserAdminService.EditUserRol(email);

    }
}
