using System;
using AutoMapper;
using GestorAlquilerApi.DataAccessLayer.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.BussinessLogicLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class SetUserAdminController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApiContext _context;
        public SetUserAdminController(IMapper mapper, ApiContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        [HttpPut("{email}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutClient(string email)
        {
            var client = _context.Client.FirstOrDefault(x => x.Email == email);

            if (client == null)
            {
                return NotFound("User not found");
            }
            client.Rol = "Admin";
            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok("User updated successfully");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(client.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


            /* if (id != client.Id)
            {
                return BadRequest();
            }
            client.Rol = "Admin";
            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); */

        }
        private bool ClientExists(int id)
        {
            return (_context.Client?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
