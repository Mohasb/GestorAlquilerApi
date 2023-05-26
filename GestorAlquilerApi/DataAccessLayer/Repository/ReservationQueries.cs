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

        public void AddReservation(Reservation reservation) => _context.Add(reservation);

        public void Remove(Reservation reservation) => _context.Remove(reservation);

        public bool CheckAvailabilityCars(Reservation reservation)
        {
            var data =
                from p in _context.Planning
                where
                    p.BranchId == reservation.BranchId
                    && p.CarCategory == reservation.CarCategory
                    && p.Day >= reservation.StartDate.Date
                    && p.Day <= reservation.EndDate.Date
                select p;

            bool isAvailableAllDays = data.All(p => p.CarsAvailables > 0);
            return isAvailableAllDays;
        }

        public IQueryable<Planning> GetReservationData(Reservation reservation)
        {
            var data =
                from p in _context.Planning
                where
                    p.BranchId == reservation.BranchId
                    && p.CarCategory == reservation.CarCategory
                    && p.Day >= reservation.StartDate.Date
                    && p.Day <= reservation.EndDate.Date
                select p;
            return data;
        }

        public IQueryable<Planning> GetReservationDataReturn(Reservation reservation)
        {
            var data =
                from p in _context.Planning
                where
                    p.BranchId == reservation.ReturnBranchId
                    && p.CarCategory == reservation.CarCategory
                    && p.Day > reservation.EndDate.Date
                select p;
            return data;
        }

        public IQueryable<Planning> GetReservationDataBranch(Reservation reservation)
        {
            var data =
                from p in _context.Planning
                where
                    p.BranchId == reservation.BranchId
                    && p.CarCategory == reservation.CarCategory
                    && p.Day >= reservation.StartDate.Date
                select p;
            return data;
        }

        public IQueryable<Reservation> GetDataReservationByClient(int id)
        {
            var data =
                from r in _context.Reservation
                where r.ClientId == id
                from b in _context.Branch
                where r.BranchId == b.Id
                from rb in _context.Branch
                where r.ReturnBranchId == b.Id

                select new
                {
                    r,
                    b.Name,
                    returnBranch = rb.Name
                };
            //Aqui obtener los nombre sde las sucursales tambien y solo mostrar las reservas del dia date now para adelante
            return data;
        }
    }
}
