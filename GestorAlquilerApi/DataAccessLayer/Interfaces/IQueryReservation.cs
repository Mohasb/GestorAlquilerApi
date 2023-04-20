using GestorAlquilerApi.BussinessLogicLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.DataAccessLayer.Interfaces
{
    public interface IQueryReservation
    {
        public DbSet<Reservation> GetDataReservation();
        public void Remove(Reservation reservation);
        public void AddReservation(Reservation reservation);
        public IQueryable<Planning> GetReservationCars(Reservation reservation);
    }
}
