using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.DataAccessLayer.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace GestorAlquilerApi.BussinessLogicLayer.Controllers
{
    [Route("api/[controller]")]
    [Tags("!Planning")]
    [ApiController]

    public class ConsultasController : ControllerBase
    {

        private readonly ApiContext _context;

        public ConsultasController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Branches
        [HttpGet("carsByBranchId")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<ICollection<CarDTO>>>> GetCarsByBranchId(int id)
        {
            if (_context.Branch == null || _context.Car == null)
            {
                return NotFound();
            }

            var countCars = (from b in _context.Car select b).Count();
            var countBranches = (from b in _context.Branch select b).Count();

            if (!Convert.ToBoolean(countBranches)) return Problem("Theres no Branches", statusCode: 404);
            if (!Convert.ToBoolean(countCars)) return Problem("Theres no Cars", statusCode: 404);


            var cars = from b in _context.Branch
                       from c in _context.Car
                       where b.Id == id && c.BranchId == b.Id
                       select new CarDTO
                       {
                           Id = c.Id,
                           Brand = c.Brand,
                           Model = c.Model,
                           FuelType = c.FuelType,
                           GearShiftType = c.GearShiftType,
                           Image = c.Image,
                           Category = c.Category,
                           BranchId = c.BranchId
                       };



            return Ok(await cars.ToListAsync());


        }

        // GET: api/Branches
        [HttpGet("carsAvailablesByDate/{date}/{branchId}/{carCategory}")]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<ICollection<CarDTO>>>> GetCarsByBranch(DateTime date, int branchId, string carCategory)
        {
            if (_context.Reservation == null || _context.Branch == null || _context.Car == null)
            {
                return NotFound();
            }


            var data = from p in _context.Planning
                       where p.Day == date && p.BranchId == branchId && p.CarCategory == carCategory
                       select p;

            return Ok(await data.ToListAsync());
        }
    }
}
