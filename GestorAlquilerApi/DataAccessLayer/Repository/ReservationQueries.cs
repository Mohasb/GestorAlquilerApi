using GestorAlquilerApi.BussinessLogicLayer.Models;
using GestorAlquilerApi.DataAccessLayer.Data;
using GestorAlquilerApi.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.DataAccessLayer.Repository
{
    public class ReservationQueries : IQueryReservation
    {
        private readonly ApiContext _context;

        public ReservationQueries(ApiContext context)
        {
            _context = context;
        }

        public DbSet<Reservation> GetDataReservation() => _context.Reservation;

        public void ModifiedState(Reservation reservation) =>
            _context.Entry(reservation).State = EntityState.Modified;

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public void AddReservation(Reservation reservation) => _context.Add(reservation);

        public void Remove(Reservation reservation) => _context.Remove(reservation);

        public IQueryable<Planning> GetReservationCars(Reservation reservation) =>
            (
                from p in _context.Planning
                from b in _context.Branch
                from c in _context.Car
                where
                    p.BranchId == b.Id
                    && p.BranchId == reservation.BranchId
                    && p.CarCategory == c.Category
                    && p.Day >= reservation.StartDate.Date
                    && p.Day < reservation.EndDate
                select p
            ).Distinct();
    }
}
