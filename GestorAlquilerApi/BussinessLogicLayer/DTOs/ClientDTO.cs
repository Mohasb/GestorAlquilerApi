﻿using System.Reflection.Metadata;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text;

namespace GestorAlquilerApi.BussinessLogicLayer.DTOs
{
    public class ClientDTO
    {
        //[JsonIgnore]
        [Required(ErrorMessage = "The field 'Id' is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "The field 'registration' is required")]
        [MaxLength(100)]
        public string? Registration { get; set; }

        [Required(ErrorMessage = "The field 'Name' is required")]
        [MaxLength(100)]
        public string? Name { get; set; }

        [MaxLength(100)]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "The field 'Email' is required")]
        [MaxLength(100)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "The field 'Password' is required")]
        [MaxLength(100)]
        public string? Password { get; set; }

        public int PhoneNumber { get; set; }

        [Required(ErrorMessage = "The field 'BankAccount' is required")]
        [MaxLength(100)]
        public string? BankAccount { get; set; }

        public string? Rol { get; set; } = "User";
        public string? Image { get; set; } = "";
    }
}
