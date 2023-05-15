using System.ComponentModel.DataAnnotations;

namespace GestorAlquilerApi.BussinessLogicLayer.DTOs
{
    public class ReservationDTO
    {
        //[JsonIgnore]
        [Required(ErrorMessage = "The field 'Id' is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "The field 'DateStart' is required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "The field 'DateEnd' is required")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "The field 'BranchId' is required")]
        public int BranchId { get; set; }

        [Required(ErrorMessage = "The field 'ReturnBranchId' is required")]
        public int ReturnBranchId { get; set; }

        [Required(ErrorMessage = "The field 'ClientId' is required")]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "The field 'CarId' is required")]
        public string? CarCategory { get; set; }
        public int CarId { get; set; }
    }
}
