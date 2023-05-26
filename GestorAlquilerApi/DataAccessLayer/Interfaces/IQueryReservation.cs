using GestorAlquilerApi.BussinessLogicLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.DataAccessLayer.Interfaces
{
    public interface IQueryReservation
    {
        public DbSet<Reservation> GetDataReservation();
        public void Remove(Reservation reservation);
        public void AddReservation(Reservation reservation);

        public bool CheckAvailabilityCars(Reservation reservation);
        public IQueryable<Planning> GetReservationData(Reservation reservation);
        public IQueryable<Planning> GetReservationDataReturn(Reservation reservation);
        public IQueryable<Planning> GetReservationDataBranch(Reservation reservation);
        public IQueryable<Reservation> GetDataReservationByClient(int id);
    }
}
