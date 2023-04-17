using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestorAlquilerApi.BussinessLogicLayer.Interfaces
{
    public interface IPlanningService
    {
        Task<ActionResult<IEnumerable<PlanningDTO>>> GetPlanning();
        Task<ActionResult<PlanningDTO>> GetPlanning(int id);
        Task<IActionResult> PutPlanning(int id, PlanningDTO planningDTO);
        Task<ActionResult<Planning>> PostPlanning(PlanningDTO planningDTO);
        Task<IActionResult> DeletePlanning(int id);
    }
}
