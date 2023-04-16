using AutoMapper;
using GestorAlquilerApi.BussinessLogicLayer.Models;

namespace GestorAlquilerApi.BussinessLogicLayer.DTOs
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Branch, BranchDTO>(); //GET
            CreateMap<BranchDTO, Branch>(); //POST-PUT
            //
            CreateMap<Car, CarDTO>();
            CreateMap<CarDTO, Car>();
            //
            CreateMap<Client, ClientDTO>();
            CreateMap<ClientDTO, Client>();
            //
            CreateMap<Reservation, ReservationDTO>();
            CreateMap<ReservationDTO, Reservation>();
            //
            CreateMap<Planning, PlanningDTO>();
            CreateMap<PlanningDTO, Planning>();

        }
    }
}
