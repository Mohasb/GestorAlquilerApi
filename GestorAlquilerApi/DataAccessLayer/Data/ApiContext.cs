using Microsoft.EntityFrameworkCore;
using GestorAlquilerApi.BussinessLogicLayer.Models;

namespace GestorAlquilerApi.DataAccessLayer.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options) { }

        public DbSet<Planning> Planning { get; set; } = default!;

        public DbSet<Branch> Branch { get; set; } = default!;

        public DbSet<Car> Car { get; set; } = default!;

        public DbSet<Client> Client { get; set; } = default!;

        public DbSet<Reservation> Reservation { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Branch>().HasIndex(b => b.Cif).IsUnique();
            modelBuilder.Entity<Car>().HasIndex(c => c.Registration).IsUnique();
            modelBuilder.Entity<Client>().HasIndex(c => c.Registration).IsUnique();
            modelBuilder.Entity<Client>().HasIndex(c => c.Email).IsUnique();


            //Sedder db
            /* modelBuilder.Entity<Branch>().HasData(
                new Branch { Id = 1, Cif = "C-124582", Name = "Centauro Car hire Benidorm", Population = "Benidorm", Country = "Spain", Address = "C. de Berlin, 2, 03503 Benidorm, Alicante" },
                new Branch { Id = 2, Cif = "C-124625", Name = "Centauro Car hire Alicante airport", Population = "Alicante", Country = "Spain", Address = "Alicante Airport, 03195 Elx, Alicante" },
                new Branch { Id = 3, Cif = "C-126385", Name = "Centauro Car Rental Torrevieja", Population = "Torrevieja", Country = "Spain", Address = "Av. de las Cortes Valencianas, 6, 03183 Torrevieja, Alicante" },
                new Branch { Id = 4, Cif = "C-124758", Name = "Centauro Location voiture Lisbonne", Population = "Lisboa", Country = "Portugal", Address = "Av. Severiano Falcao 2, Prior Velho, 2685-378 Lisboa, Portugal" },
                new Branch { Id = 5, Cif = "C-123485", Name = "Centauro Rent a Car Rome Airport Fiumicino", Population = "Roma", Country = "Italia", Address = "Via Portuense, 2483, 00054 Fiumicino RM, Italia" }

                );
            modelBuilder.Entity<Car>().HasData(
                new Car { Id = 1,  Registration = "3993-YLH", Brand = "Fiat", Model = "500", FuelType = "Gasoline", GearShiftType = "Manual", Image = "Fiat500Image", Category = "A", BranchId = 1 },
                new Car { Id = 2, Registration = "2519-YES", Brand = "Fiat", Model = "500", FuelType = "Gasoline", GearShiftType = "Manual", Image = "Fiat500Image", Category = "A", BranchId = 2 },
                new Car { Id = 3, Registration = "8324-TJS", Brand = "Fiat", Model = "500", FuelType = "Diesel", GearShiftType = "Manual", Image = "Fiat500Image", Category = "A", BranchId = 3 },
                new Car { Id = 4, Registration = "0035-LQJ", Brand = "Fiat", Model = "500", FuelType = "Gasoline", GearShiftType = "Automatic", Image = "Fiat500Image", Category = "A", BranchId = 4 },
                new Car { Id = 5, Registration = "4596-TCK", Brand = "Fiat", Model = "500", FuelType = "Gasoline", GearShiftType = "Manual", Image = "Fiat500Image", Category = "A", BranchId = 5 },
                //
                new Car { Id = 6, Registration = "3960-XXN", Brand = "Kia", Model = "Ceed", FuelType = "Diesel", GearShiftType = "Manual", Image = "KiaCeedImage", Category = "C", BranchId = 1 },
                new Car { Id = 7, Registration = "3063-GIR", Brand = "Kia", Model = "Ceed", FuelType = "Gasoline", GearShiftType = "Manual", Image = "KiaCeedImage", Category = "C", BranchId = 2 },
                new Car { Id = 8, Registration = "1551-DLN", Brand = "Kia", Model = "Ceed", FuelType = "Gasoline", GearShiftType = "Automatic", Image = "KiaCeedImage", Category = "C", BranchId = 3 },
                new Car { Id = 9, Registration = "1547-SPJ", Brand = "Kia", Model = "Ceed", FuelType = "Gasoline", GearShiftType = "Manual", Image = "KiaCeedImage", Category = "C", BranchId = 4 },
                new Car { Id = 10, Registration = "0725-TMW", Brand = "Kia", Model = "Ceed", FuelType = "Diesel", GearShiftType = "Automatic", Image = "KiaCeedImage", Category = "C", BranchId = 5 },
                //
                new Car { Id = 11, Registration = "3967-QPV", Brand = "Volkswagen", Model = "T-ROC", FuelType = "Diesel", GearShiftType = "Automatic", Image = "VolkswagenTRocImage", Category = "D", BranchId = 1 },
                new Car { Id = 12, Registration = "9125-NUJ", Brand = "Volkswagen", Model = "T-ROC", FuelType = "Gasoline", GearShiftType = "Manual", Image = "VolkswagenTRocImage", Category = "D", BranchId = 2 },
                new Car { Id = 13, Registration = "6997-QVO", Brand = "Volkswagen", Model = "T-ROC", FuelType = "Diesel", GearShiftType = "Automatic", Image = "VolkswagenTRocImage", Category = "D", BranchId = 3 },
                new Car { Id = 14, Registration = "3315-QUI", Brand = "Volkswagen", Model = "T-ROC", FuelType = "Gasoline", GearShiftType = "Manual", Image = "VolkswagenTRocImage", Category = "D", BranchId = 4 },
                new Car { Id = 15, Registration = "9243-IAC", Brand = "Volkswagen", Model = "T-ROC", FuelType = "Diesel", GearShiftType = "Automatic", Image = "VolkswagenTRocImage", Category = "D", BranchId = 5 },
                //
                new Car { Id = 16, Registration = "3013-EKU", Brand = "Hyundai", Model = "Kona", FuelType = "Diesel", GearShiftType = "Automatic", Image = "HyundaiKonaImage", Category = "D", BranchId = 1 },
                new Car { Id = 17, Registration = "8640-AZH", Brand = "Hyundai", Model = "Kona", FuelType = "Diesel", GearShiftType = "Manual", Image = "HyundaiKonaImage", Category = "D", BranchId = 2 },
                new Car { Id = 18, Registration = "1540-QQM", Brand = "Hyundai", Model = "Kona", FuelType = "Diesel", GearShiftType = "Automatic", Image = "HyundaiKonaImage", Category = "D", BranchId = 3 },
                new Car { Id = 19, Registration = "9947-WES", Brand = "Hyundai", Model = "Kona", FuelType = "Gasoline", GearShiftType = "Manual", Image = "HyundaiKonaImage", Category = "D", BranchId = 4 },
                new Car { Id = 20, Registration = "7365-BZL", Brand = "Hyundai", Model = "Kona", FuelType = "Diesel", GearShiftType = "Automatic", Image = "HyundaiKonaImage", Category = "D", BranchId = 5 },
                //
                new Car { Id = 21, Registration = "5752-PLO", Brand = "Ford", Model = "Transit ", FuelType = "Diesel", GearShiftType = "Manual", Image = "FordTransitImage", Category = "B", BranchId = 1 },
                new Car { Id = 22, Registration = "5755-FIO", Brand = "Ford", Model = "Transit ", FuelType = "Diesel", GearShiftType = "Manual", Image = "FordTransitImage", Category = "B", BranchId = 2 },
                new Car { Id = 23, Registration = "8673-QLF", Brand = "Ford", Model = "Transit ", FuelType = "Diesel", GearShiftType = "Manual", Image = "FordTransitImage", Category = "B", BranchId = 3 },
                new Car { Id = 24, Registration = "0332-GYO", Brand = "Ford", Model = "Transit ", FuelType = "Diesel", GearShiftType = "Manual", Image = "FordTransitImage", Category = "B", BranchId = 4 },
                new Car { Id = 25, Registration = "3145-WEU", Brand = "Ford", Model = "Transit ", FuelType = "Diesel", GearShiftType = "Manual", Image = "FordTransitImage", Category = "B", BranchId = 5 },
                //ADMIN CAR
                new Car { Id = 26, Registration = "4609-KFD", Brand = "Nissan", Model = "Skyline ", FuelType = "Gasoline", GearShiftType = "Manual", Image = "NissanSkylineImage", Category = "A", BranchId = 1 }
                );
            modelBuilder.Entity<Client>().HasData(
                new Client { Id = 1, Registration = "45113560A", Name="Muhammad", LastName = "Hicho Haidor", Email = "mh.haidor@gmail.com", Password = BCrypt.Net.BCrypt.HashPassword("12345"), ConfirmationPassword = BCrypt.Net.BCrypt.HashPassword("12345"), PhoneNumber = 686601702, BankAccount = "1234Bank", Rol = "Admin" },
                new Client { Id = 2, Registration = "45223659F", Name = "Jhon", LastName = "Doe", Email = "jhon.doe@gmail.com", Password = BCrypt.Net.BCrypt.HashPassword("12345"), ConfirmationPassword = BCrypt.Net.BCrypt.HashPassword("12345"), PhoneNumber = 695632458, BankAccount = "1234Bank", Rol = "User" },
                new Client { Id = 3, Registration = "45369875G", Name = "Jane", LastName = "Doe", Email = "jane.doe@gmail.com", Password = BCrypt.Net.BCrypt.HashPassword("12345"), ConfirmationPassword = BCrypt.Net.BCrypt.HashPassword("12345"), PhoneNumber = 695632458, BankAccount = "1234Bank", Rol = "User" }
                ); */





        }
    }
}
