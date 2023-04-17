using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Interfaces;

namespace GestorAlquilerApi.BussinessLogicLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController
    {
        private readonly IReservationService _repository;

        public ReservationsController(IReservationService repository)
        {
            _repository = repository;
        }

        // GET: api/Reservations
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservation() =>
            await _repository.GetAllReservations();

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<ReservationDTO>> GetReservation(int id) =>
            await _repository.GetReservationById(id);

        // PUT: api/Reservations/5
        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutReservation(int id, ReservationDTO reservationDTO) =>
            await _repository.EditReservation(id, reservationDTO);

        // POST: api/Reservations
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<ReservationDTO>> PostReservation(
            ReservationDTO reservationDTO
        ) => await _repository.AddReservationSameBranch(reservationDTO);

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteReservation(int id) =>
            await _repository.RemoveReservation(id);
    }
}
