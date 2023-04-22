using AutoMapper;
using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Interfaces;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using GestorAlquilerApi.DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.BussinessLogicLayer.ControllersService
{
    public class ReservationService<ReservationDTO> : ControllerBase, IGenericService<ReservationDTO>
    {
        private readonly IQueryReservation _repository;
        private readonly IMapper _mapper;
        private readonly DbSet<Reservation> _reservations;
        private readonly ISaveData<Reservation> _saveData;

        public ReservationService(IQueryReservation repository, IMapper mapper, ISaveData<Reservation> saveData)
        {
            _repository = repository;
            _mapper = mapper;
            _reservations = _repository.GetDataReservation();
            _saveData = saveData;
        }

        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetAllElements()
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

        public async Task<ActionResult<ReservationDTO>> GetElementById(int id)
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

        public async Task<IActionResult> EditElement(int id, ReservationDTO reservationDTO)
        {
            var reservation = _mapper.Map<Reservation>(reservationDTO);

            if (id != reservation.Id)
            {
                return BadRequest();
            }

            _saveData.ModifiedState(reservation);

            try
            {
                await _saveData.SaveChangesAsync();
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

        public async Task<ActionResult<ReservationDTO>> AddElement(
            ReservationDTO reservationDTO
        )
        {
            if (_reservations == null)
            {
                return Problem("Entity set 'ApiContext.Reservation'  is null.");
            }

            var reservation = _mapper.Map<Reservation>(reservationDTO);

            _repository.AddReservation(reservation);
            await _saveData.SaveChangesAsync();

            RemoveCarFromAvailable(reservation);

            return CreatedAtAction("GetReservation", new { id = reservation.Id }, reservation);
        }

        public async Task<IActionResult> RemoveElement(int id)
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
            await _saveData.SaveChangesAsync();

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
            await _saveData.SaveChangesAsync();
        }
    }
}
