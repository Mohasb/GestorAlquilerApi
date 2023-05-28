using System;

namespace GestorAlquilerApi.BussinessLogicLayer.DTOs
{
    public class ReservationClientDTO
    {
        public int Id { get; set; }
        public string? PickUpBranch { get; set; }
        public string? StartDate { get; set; }
        public string? ReturnBranch { get; set; }
        public string? EndDate { get; set; }
    }
}
