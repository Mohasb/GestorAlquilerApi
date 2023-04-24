namespace GestorAlquilerApi.BussinessLogicLayer.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? CarCategory { get; set; }
        public int BranchId { get; set; }
        public Branch? Branch { get; set; }
        public int ClientId { get; set; }
        public Client? Client { get; set; }
    }
}
