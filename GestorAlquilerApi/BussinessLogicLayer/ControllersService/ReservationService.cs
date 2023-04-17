using AutoMapper;
using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Interfaces;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using GestorAlquilerApi.DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.BussinessLogicLayer.ControllersService
{
    public class ReservationService : ControllerBase, IReservationService
    {
        private readonly IQueryReservation _repository;
        private readonly IMapper _mapper;
        private readonly DbSet<Reservation> _reservations;

        public ReservationService(IQueryReservation repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _reservations = _repository.GetDataReservation();
        }

        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservation()
        {
            if (_reservations == null)
            {
                return NotFound();
            }

            if (!Convert.ToBoolean(_reservations.Count()))
                return Problem("There are no Reservations", statusCode: 404);

            var reservations = _reservations.Select(r => _mapper.Map<ReservationDTO>(r));

            return await reservations.ToListAsync();
        }

        public async Task<ActionResult<ReservationDTO>> GetReservation(int id)
        {
            if (_reservations == null)
            {
                return NotFound();
            }
            var reservation = await _reservations.FindAsync(id);

            if (reservation == null)
            {
                return Problem($"There are no Reservation with id:{id}");
            }
            var reservationDTO = _mapper.Map<ReservationDTO>(reservation);

            return reservationDTO;
        }

        public async Task<IActionResult> PutReservation(int id, ReservationDTO reservationDTO)
        {
            var reservation = _mapper.Map<Reservation>(reservationDTO);

            if (id != reservation.Id)
            {
                return BadRequest();
            }

            _repository.ModifiedState(reservation);

            try
            {
                await _repository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
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

        public async Task<ActionResult<ReservationDTO>> PostReservation(
            ReservationDTO reservationDTO
        )
        {
            if (_reservations == null)
            {
                return Problem("Entity set 'ApiContext.Reservation'  is null.");
            }

            var reservation = _mapper.Map<Reservation>(reservationDTO);

            _repository.AddReservation(reservation);
            await _repository.SaveChangesAsync();

            RemoveCarFromAvailable(reservation);

            return CreatedAtAction("GetReservation", new { id = reservation.Id }, reservation);
        }

        public async Task<IActionResult> DeleteReservation(int id)
        {
            if (_reservations == null)
            {
                return NotFound();
            }
            var reservation = await _reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _reservations.Remove(reservation);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservationExists(int id)
        {
            return (_reservations?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async void RemoveCarFromAvailable(Reservation reservation)
        {
            var planning = _repository.GetReservationCars(reservation);

            foreach (var day in planning)
            {
                day.CarsAvailables--;
            }
            await _repository.SaveChangesAsync();
        }
    }
}
