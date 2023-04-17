namespace GestorAlquilerApi.BussinessLogicLayer.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string? Registration { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmationPassword { get; set; }
        public int PhoneNumber { get; set; }
        public string? BankAccount { get; set; }
        public string? Rol { get; set; } = "User";
        public virtual ICollection<Reservation>? Reservations { get; set; }
    }
}
