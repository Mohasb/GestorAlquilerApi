using System;
using System.Text.Json.Serialization;

namespace GestorAlquilerApi.BussinessLogicLayer.DTOs
{
    public class UserDTO
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
