using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GestorAlquilerApi.BussinessLogicLayer.Models;

namespace GestorAlquilerApi.DataAccessLayer.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

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
        }

    }
}
