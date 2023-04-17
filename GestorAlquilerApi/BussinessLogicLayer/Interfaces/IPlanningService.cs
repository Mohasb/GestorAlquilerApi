using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestorAlquilerApi.BussinessLogicLayer.Interfaces
{
    public interface IPlanningService
    {
        Task<ActionResult<IEnumerable<PlanningDTO>>> GetAllPlanning();
        Task<ActionResult<PlanningDTO>> GetPlanningById(int id);
        Task<IActionResult> EditPlanning(int id, PlanningDTO planningDTO);
        Task<ActionResult<Planning>> AddPlanning(PlanningDTO planningDTO);
        Task<IActionResult> RemovePlanning(int id);
    }
}
