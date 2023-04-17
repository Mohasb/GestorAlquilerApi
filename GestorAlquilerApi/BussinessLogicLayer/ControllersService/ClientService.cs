﻿using AutoMapper;
using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Interfaces;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using GestorAlquilerApi.DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.BussinessLogicLayer.ControllersService
{
    public class ClientService : ControllerBase, IClientService
    {
        private readonly IQueryClient _repository;
        private readonly IMapper _mapper;
        private readonly DbSet<Client> _clients;

        public ClientService(IQueryClient repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _clients = _repository.GetDataClients();
        }

        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetClients()
        {
            if (_clients == null)
            {
                return NotFound();
            }

            var countClients = (from c in _clients select c).Count();

            if (!Convert.ToBoolean(countClients))
                return NotFound("There are no Clients");

            var data = _clients;
            var clients = data.Select(c => _mapper.Map<ClientDTO>(c));

            return await clients.ToListAsync();
        }

        public async Task<ActionResult<ClientDTO>> GetClient(int id)
        {
            if (_clients == null)
            {
                return NotFound();
            }
            var client = await _clients.FindAsync(id);

            if (client == null)
            {
                return Problem($"There are no Client with id:{id}");
            }
            var clientDto = _mapper.Map<ClientDTO>(client);

            return clientDto;
        }

        public async Task<IActionResult> PutClient(int id, ClientDTO clientDTO)
        {
            var client = _mapper.Map<Client>(clientDTO);
            client.Id = id;

            if (id != client.Id)
            {
                return BadRequest();
            }
            _repository.ModifiedState(client);

            try
            {
                await _repository.SaveChangesAsync();
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

            return NoContent();
        }

        public async Task<ActionResult<ClientDTO>> PostClient(ClientDTO clientDTO)
        {
            clientDTO.Password = BCrypt.Net.BCrypt.HashPassword(clientDTO.Password);

            var client = _mapper.Map<Client>(clientDTO);

            if (_clients == null)
            {
                return Problem("Entity set 'ApiContext.Client'  is null.");
            }

            _repository.AddClient(client);

            await _repository.SaveChangesAsync();

            return CreatedAtAction("GetClient", new { id = client.Id }, client);
        }

        public async Task<IActionResult> DeleteClient(int id)
        {
            if (_clients == null)
            {
                return NotFound();
            }
            var client = await _clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _repository.Remove(client);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientExists(int id)
        {
            return (_clients?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public ClientDTO? AuthenticateUser(UserDTO user)
        {
            var usuario = _repository.GetClientByEmail(user);

            if (usuario != null && BCrypt.Net.BCrypt.Verify(user.Password, usuario.Password))
            {
                return _mapper.Map<ClientDTO>(usuario);
            }
            return null;
        }
    }
}