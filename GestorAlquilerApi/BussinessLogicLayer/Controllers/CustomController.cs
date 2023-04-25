using AutoMapper;
using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Interfaces;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public CustomController(
            ISetAdminService setUserAdminService,
            ILoginService loginService,
            ICustomService customService
        )
        {
            _setUserAdminService = setUserAdminService;
            _loginService = loginService;
            _customService = customService;
        }

        /// <summary>
        /// Returns a confirmation dialog or error
        /// </summary>
        /// <param name="email">The email of the User.</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/custom/setAdmin/abc123@gmail.com
        ///
        /// </remarks>
        /// <response code="200">Returns the confirmation</response>
        /// <response code="400">If there are no user with that email</response>
        [HttpPut("setAdmin/{email}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetAdmin(string email) =>
            await _setUserAdminService.EditUserRol(email);

        /// <summary>
        /// Login user to get the JWT token
        /// </summary>
        /// <param name="user">Body of the User (email and password)</param>
        /// <response code="200">Returns the token</response>
        /// <response code="400">if there are no user qith that email or the password doesnt match with the encripted in db</response>
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult LoginUser(UserDTO user)
        {
            return _loginService.Login(user);
        }

        /// <summary>
        /// Returns a Cars of a brnach by its id
        /// </summary>
        [HttpGet("carsByBranch/{branchId}")]
        public List<CarDTO> GetCarsByBranch(int branchId)
        {
            return _customService.GetCarsByBranch(branchId);
        }
        /// <summary>
        /// Returns a list of car availables in the dates and branch id given
        /// </summary>
        [HttpGet("getCarsAvailables/{branchId}/{startDate}/{endDate}/{ageUser}")]
        public List<Car> GetAvailablesCars(
            int branchId,
            DateTime startDate,
            DateTime endDate,
            int ageUser
        )
        {
            var cars = _customService.GetAvailablesCars(branchId, startDate, endDate, ageUser);
            return cars;
        }
    }
}
