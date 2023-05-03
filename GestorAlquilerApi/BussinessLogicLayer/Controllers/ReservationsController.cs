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
        private readonly IGenericService<ReservationDTO> _repository;

        public ReservationsController(IGenericService<ReservationDTO> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Returns a List of all Reservations
        /// </summary>
        // GET: api/Reservations
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservation() =>
            await _repository.GetAllElements();

        /// <summary>
        /// Returns a Reservation by its Id
        /// </summary>
        /// <param name="id">The id of the Reservation.</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/Reservation/5
        ///
        /// </remarks>
        /// <response code="200">Returns the Reservation</response>
        /// <response code="400">If there are no Reservation with that id</response>
        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<ReservationDTO>> GetReservation(int id) =>
            await _repository.GetElementById(id);

        /// <summary>
        /// Edit a Reservation by its Id
        /// </summary>
        /// <param name="id">The id of the Reservation.</param>
        /// <param name="reservationDTO">Body of the new Reservation</param>
        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutReservation(int id, ReservationDTO reservationDTO) =>
            await _repository.EditElement(id, reservationDTO);

        /// <summary>
        /// Add a new Reservation
        /// </summary>
        /// <param name="reservationDTO">Body of the Reservation </param>
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<ReservationDTO>> PostReservation(
            ReservationDTO reservationDTO
        ) => await _repository.AddElement(reservationDTO);

        /// <summary>
        /// Delete a Reservation
        /// </summary>
        /// <param name="id">id of the Reservation to delete</param>
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteReservation(int id) =>
            await _repository.RemoveElement(id);
    }
}
