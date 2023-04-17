using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestorAlquilerApi.BussinessLogicLayer.Controllers
{
    [Route("api/[controller]")]
    [Tags("!Planning")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {
        private readonly IConsultasService _consultasService;

        public ConsultasController(IConsultasService consultasService)
        {
            _consultasService = consultasService;
        }

        // GET: api/Branches
        [HttpGet("carsByBranchId")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<ICollection<CarDTO>>>> GetCarsByBranchId(
            int id
        ) => await _consultasService.GetCarsByBranchId(id);

        // GET: api/Branches
        [HttpGet("carsAvailablesByDate/{date}/{branchId}/{carCategory}")]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<ICollection<CarDTO>>>> GetCarsByBranch(
            DateTime date,
            int branchId,
            string carCategory
        ) => Ok(await _consultasService.GetCarsByBranch(date, branchId, carCategory));
    }
}
