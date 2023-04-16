using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GestorAlquilerApi.BussinessLogicLayer.Interfaces
{
    public interface IReservationService
    {
        Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservation();
        Task<ActionResult<ReservationDTO>> GetReservation(int id);
        Task<IActionResult> PutReservation(int id, ReservationDTO reservationDTO);
        Task<ActionResult<ReservationDTO>> PostReservation(ReservationDTO reservationDTO);
        Task<IActionResult> DeleteReservation(int id);

    }
}
