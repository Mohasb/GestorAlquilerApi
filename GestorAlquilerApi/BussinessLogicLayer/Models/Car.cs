namespace GestorAlquilerApi.BussinessLogicLayer.Models
{
    public class Car
    {
        public enum Categories
        {
            A, B, C, D
        }
        public int Id { get; set; }
        public string? Registration { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? FuelType { get; set; }
        public string? GearShiftType { get; set; }
        public string? Image { get; set; }
        public string? Category { get; set; }

        public int BranchId { get; set; }
        public Branch? Branch { get; set; }
    }
}
