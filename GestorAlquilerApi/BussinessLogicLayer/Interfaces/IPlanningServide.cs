using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GestorAlquilerApi.BussinessLogicLayer.Interfaces
{
    public interface IPlanningServide
    {
        Task<ActionResult<IEnumerable<PlanningDTO>>> GetAllElements();
    }
}