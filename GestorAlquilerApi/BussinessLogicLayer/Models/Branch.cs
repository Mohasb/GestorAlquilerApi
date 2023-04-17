namespace GestorAlquilerApi.BussinessLogicLayer.Models
{
    public class Branch
    {
        public int Id { get; set; }
        public string? Cif { get; set; }
        public string? Name { get; set; }
        public string? Population { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<Car>? Cars { get; set; }
        public virtual ICollection<Reservation>? Reservations { get; set; }
    }
}
