using AutoMapper;
using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Interfaces;
using GestorAlquilerApi.BussinessLogicLayer.Models;
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
        private readonly IMapper _mapper;

        public CustomController(
            ISetAdminService setUserAdminService,
            ILoginService loginService,
            ICustomService customService,
            IMapper mapper
        )
        {
            _setUserAdminService = setUserAdminService;
            _loginService = loginService;
            _customService = customService;
            _mapper = mapper;
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

        [HttpGet("getCarsAvailables/{branchId}/{startDate}/{endDate}/{age}")]
        public List<Car> GetAvailablesCars(
            int branchId,
            DateTime startDate,
            DateTime endDate,
            int age
        ) 
        {
            var cars = _customService.GetAvailablesCars(branchId, startDate, endDate, age);
            //return cars.Select(c => _mapper.Map<CarDTO>(cars));
            return cars;
        }
    }
}
