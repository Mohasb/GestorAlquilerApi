namespace GestorAlquilerApi.BussinessLogicLayer.Models
{
    public class Planning
    {
        public int Id { get; set; }
        public DateTime? Day { get; set; }
        public int CarsAvailables { get; set; }
        public string? CarCategory { get; set; }
        public int BranchId { get; set; }
        public Branch? Branch { get; set; }
    }
}
