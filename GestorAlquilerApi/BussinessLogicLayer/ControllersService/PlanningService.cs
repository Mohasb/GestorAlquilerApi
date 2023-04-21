using AutoMapper;
using GestorAlquilerApi.BussinessLogicLayer.Interfaces;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using GestorAlquilerApi.DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.BussinessLogicLayer.ControllersService
{
    public class PlanningService<PlanningDTO> : ControllerBase, IGenericService<PlanningDTO>
    {
        private readonly IQueryPlanning _repository;
        private readonly IMapper _mapper;
        private readonly DbSet<Planning> _planning;
        private readonly ISaveData<Planning> _saveData;

        public PlanningService(IQueryPlanning repository, IMapper mapper, ISaveData<Planning> saveData)
        {
            _repository = repository;
            _mapper = mapper;
            _planning = _repository.GetDataPlanning();
            _saveData = saveData;
        }

        public async Task<ActionResult<IEnumerable<PlanningDTO>>> GetAllElements()
        {
            if (_planning == null)
            {
                return NotFound();
            }
            var planning = _planning.Select(p => _mapper.Map<PlanningDTO>(p));
            return await planning.ToListAsync();
        }

        public async Task<ActionResult<PlanningDTO>> GetElementById(int id)
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

        public async Task<IActionResult> EditElement(int id, PlanningDTO planningDTO)
        {
            var planning = _mapper.Map<Planning>(planningDTO);

            if (id != planning.Id)
            {
                return BadRequest();
            }

            _saveData.ModifiedState(planning);

            try
            {
                await _saveData.SaveChangesAsync();
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
        public async Task<ActionResult<PlanningDTO>> AddElement(PlanningDTO planningDTO)
        {
            if (_planning == null)
            {
                return Problem("Entity set 'ApiContext.Planning'  is null.");
            }

            var planning = _mapper.Map<Planning>(planningDTO);

            _repository.AddPlanning(planning);
            await _saveData.SaveChangesAsync();

            return CreatedAtAction("GetPlanning", new { id = planning.Id }, planning);
        }

        public async Task<IActionResult> RemoveElement(int id)
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
            await _saveData.SaveChangesAsync();

            return NoContent();
        }

        private bool PlanningExists(int id)
        {
            return (_planning?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
