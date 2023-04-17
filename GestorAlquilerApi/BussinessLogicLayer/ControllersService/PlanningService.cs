using AutoMapper;
using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Interfaces;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using GestorAlquilerApi.DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.BussinessLogicLayer.ControllersService
{
    public class PlanningService : ControllerBase, IPlanningService
    {
        private readonly IQueryPlanning _repository;
        private readonly IMapper _mapper;
        private readonly DbSet<Planning> _planning;

        public PlanningService(IQueryPlanning repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _planning = _repository.GetDataPlanning();
        }

        public async Task<ActionResult<IEnumerable<PlanningDTO>>> GetPlanning()
        {
            if (_planning == null)
            {
                return NotFound();
            }
            var planning = _planning.Select(p => _mapper.Map<PlanningDTO>(p));
            return await planning.ToListAsync();
        }

        public async Task<ActionResult<PlanningDTO>> GetPlanning(int id)
        {
            if (_planning == null)
            {
                return NotFound();
            }
            var planning = await _planning.FindAsync(id);

            if (planning == null)
            {
                return Problem($"There are no Planning with id:{id}");
            }
            var planningDTO = _mapper.Map<PlanningDTO>(planning);

            return planningDTO;
        }

        public async Task<IActionResult> PutPlanning(int id, PlanningDTO planningDTO)
        {
            var planning = _mapper.Map<Planning>(planningDTO);

            if (id != planning.Id)
            {
                return BadRequest();
            }

            _repository.ModifiedState(planning);

            try
            {
                await _repository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanningExists(id))
                {
                    return NotFound($"There are no planning with id: {id}");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        public async Task<ActionResult<Planning>> PostPlanning(PlanningDTO planningDTO)
        {
            if (_planning == null)
            {
                return Problem("Entity set 'ApiContext.Planning'  is null.");
            }

            var planning = _mapper.Map<Planning>(planningDTO);

            _repository.AddPlanning(planning);
            await _repository.SaveChangesAsync();

            return CreatedAtAction("GetPlanning", new { id = planning.Id }, planning);
        }

        public async Task<IActionResult> DeletePlanning(int id)
        {
            if (_planning == null)
            {
                return NotFound();
            }
            var planning = await _planning.FindAsync(id);
            if (planning == null)
            {
                return NotFound();
            }

            _repository.Remove(planning);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

        private bool PlanningExists(int id)
        {
            return (_planning?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
