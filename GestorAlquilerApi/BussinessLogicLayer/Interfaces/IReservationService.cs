using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GestorAlquilerApi.BussinessLogicLayer.Interfaces
{
    public interface IReservationService
    {
        Task<ActionResult<IEnumerable<ReservationDTO>>> GetAllReservations();
        Task<ActionResult<ReservationDTO>> GetReservationById(int id);
        Task<IActionResult> EditReservation(int id, ReservationDTO reservationDTO);
        Task<ActionResult<ReservationDTO>> AddReservationSameBranch(ReservationDTO reservationDTO);
        Task<IActionResult> RemoveReservation(int id);
    }
}
