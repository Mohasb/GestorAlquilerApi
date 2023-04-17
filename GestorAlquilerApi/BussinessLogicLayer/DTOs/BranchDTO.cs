using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GestorAlquilerApi.BussinessLogicLayer.DTOs
{
    public class BranchDTO
    {
        //[JsonIgnore]
        [Required(ErrorMessage = "The field 'Id' is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "The field 'Name' is required")]
        [MaxLength(100)]
        public string? Cif { get; set; }

        [Required(ErrorMessage = "The field 'Name' is required")]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "The field 'Population' is required")]
        [MaxLength(100)]
        public string? Population { get; set; }

        [Required(ErrorMessage = "The field 'Country' is required")]
        [MaxLength(100)]
        public string? Country { get; set; }

        [Required(ErrorMessage = "The field 'address' is required")]
        [MaxLength(100)]
        public string? Address { get; set; }
    }
}
