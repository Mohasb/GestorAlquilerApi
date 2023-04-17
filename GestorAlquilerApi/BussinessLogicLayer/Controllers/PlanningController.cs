using Microsoft.AspNetCore.Mvc;
using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using GestorAlquilerApi.BussinessLogicLayer.Interfaces;

namespace GestorAlquilerApi.BussinessLogicLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanningController
    {
        private readonly IPlanningService _planningService;

        public PlanningController(IPlanningService planningService)
        {
            _planningService = planningService;
        }

        // GET: api/Planning
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<PlanningDTO>>> GetPlanning() =>
            await _planningService.GetAllPlanning();

        // GET: api/Planning/5
        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<PlanningDTO>> GetPlanning(int id) =>
            await _planningService.GetPlanningById(id);

        // PUT: api/Planning/5
        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutPlanning(int id, PlanningDTO planningDTO) =>
            await _planningService.EditPlanning(id, planningDTO);

        // POST: api/Planning
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<Planning>> PostPlanning(PlanningDTO planningDTO) =>
            await _planningService.AddPlanning(planningDTO);

        // DELETE: api/Planning/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePlanning(int id) =>
            await _planningService.RemovePlanning(id);
    }
}
