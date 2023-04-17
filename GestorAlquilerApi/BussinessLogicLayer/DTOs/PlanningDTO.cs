using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GestorAlquilerApi.BussinessLogicLayer.DTOs
{
    public class PlanningDTO
    {
        //[JsonIgnore]
        [Required(ErrorMessage = "The field 'Id' is required")]
        public int Id { get; set; }
        public string? Day { get; set; }
        public int CarsAvailables { get; set; }
        public string? CarCategory { get; set; }
        public int BranchId { get; set; }
    }
}
