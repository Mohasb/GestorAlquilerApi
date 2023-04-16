using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace GestorAlquilerApi.BussinessLogicLayer.DTOs
{
    public enum Categoria
    {
        A, B, C, D, E, F
    }
    public class CarDTO
    {
        [JsonIgnore]
        [Required(ErrorMessage = "The field 'Id' is required")]
        public int Id { get; set; }
        [Required(ErrorMessage = "The field 'Registration' is required")]
        [MaxLength(100)]
        public string? Registration { get; set; }
        [Required(ErrorMessage = "The field 'Brand' is required")]
        [MaxLength(100)]
        public string? Brand { get; set; }
        [Required(ErrorMessage = "The field 'Model' is required")]
        [MaxLength(100)]
        public string? Model { get; set; }
        [Required(ErrorMessage = "The field 'Fuel' is required")]
        [MaxLength(100)]
        public string? FuelType { get; set; }
        [Required(ErrorMessage = "The field 'GearShifType' is required")]
        [MaxLength(100)]
        public string? GearShiftType { get; set; }
        [Required(ErrorMessage = "The field 'Image' is required")]
        [MaxLength(100)]
        public string? Image { get; set; }
        [Required(ErrorMessage = "The field 'Category' is required")]
        [MaxLength(100)]
        public string? Category { get; set; }
        [Required(ErrorMessage = "The field 'BranchId' is required")]
        public int BranchId { get; set; }
    }
}
