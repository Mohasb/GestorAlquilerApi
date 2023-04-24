using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestorAlquilerApi.BussinessLogicLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("!Custom")]
    public class CustomController
    {
        private readonly ISetAdminService _setUserAdminService;
        private readonly ILoginService _loginService;
        private readonly ICustomService _customService;

        public CustomController(ISetAdminService setUserAdminService, ILoginService loginService, ICustomService customService)
        {
            _setUserAdminService = setUserAdminService;
            _loginService = loginService;
            _customService = customService;//Aqui em`pieza las custom request----->
        }

        [HttpPut("setAdmin/{email}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetAdmin(string email) =>
            await _setUserAdminService.EditUserRol(email);

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult LoginUser(UserDTO user)
        {
            return _loginService.Login(user);
        }
    }
}
