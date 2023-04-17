using System;
using AutoMapper;
using GestorAlquilerApi.BussinessLogicLayer.Interfaces;
using GestorAlquilerApi.DataAccessLayer.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> PutClient(string email)
        => await _setUserAdminService.PutClient(email);
        
        private bool ClientExists(int id)
        => _setUserAdminService.ClientExists(id);
    }
}
