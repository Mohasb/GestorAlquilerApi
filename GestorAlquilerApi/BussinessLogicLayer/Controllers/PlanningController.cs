using Microsoft.AspNetCore.Mvc;
using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Interfaces;

namespace GestorAlquilerApi.BussinessLogicLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanningController
    {
        private readonly IGenericService<PlanningDTO> _planningService;

        public PlanningController(IGenericService<PlanningDTO> planningService)
        {
            _planningService = planningService;
        }

        /// <summary>
        /// Returns a List of all Planning
        /// </summary>
        // GET: api/Planning
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<PlanningDTO>>> GetPlanning() =>
            await _planningService.GetAllElements();

        /// <summary>
        /// Returns a Planning by its Id
        /// </summary>
        /// <param name="id">The id of the Planning.</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/Planning/5
        ///
        /// </remarks>
        /// <response code="200">Returns the Planning</response>
        /// <response code="400">If there are no Planning with that id</response>
        //[HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        /* public async Task<ActionResult<PlanningDTO>> GetPlanning(int id) =>
            await _planningService.GetElementById(id); */

        /// <summary>
        /// Edit a Planning by its Id
        /// </summary>
        /// <param name="id">The id of the Planning.</param>
        /// <param name="planningDTO">Body of the new Planning</param>
        //[HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        /* public async Task<IActionResult> PutPlanning(int id, PlanningDTO planningDTO) =>
            await _planningService.EditElement(id, planningDTO); */

        /// <summary>
        /// When added a branch 365 days with all cars categories is added.Normaly i dont use this endpoint because is managed by post Branches, cars and registrations.
        /// </summary>
        /// <param name="planningDTO">Body of the Planning</param>
        //[HttpPost]
        //[Authorize(Roles = "Admin")]
        /* public async Task<ActionResult<PlanningDTO>> PostPlanning(PlanningDTO planningDTO) =>
            await _planningService.AddElement(planningDTO); */

        /// <summary>
        /// Delete a Planning
        /// </summary>
        /// <param name="id">id of the Planning to delete</param>
        //[HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        /* public async Task<IActionResult> DeletePlanning(int id) =>
            await _planningService.RemoveElement(id); */
    }
}
