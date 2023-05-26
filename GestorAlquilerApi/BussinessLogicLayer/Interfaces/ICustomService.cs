using System;
using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.BussinessLogicLayer.Interfaces
{
    public interface ICustomService
    {
        List<Car> GetAvailablesCars(int branchId, DateTime startDate, DateTime endDate, int age);
        public List<CarDTO> GetCarsByBranch(int branchId);
        IActionResult GetReservationByClient(int id);
    }
}
