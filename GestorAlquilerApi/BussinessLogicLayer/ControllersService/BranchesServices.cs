using AutoMapper;
using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Interfaces;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using GestorAlquilerApi.DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.BussinessLogicLayer.ControllersService
{
    public class BranchesServices<BranchDTO> : IGenericService<BranchDTO>
    {
        private readonly IQueryBranch _repository;
        private readonly IMapper _mapper;
        private readonly DbSet<Branch> _branches;
        public BranchesServices(IQueryBranch repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _branches = _repository.GetDataBranches();
        }

        public async Task<IEnumerable<BranchDTO>> GetAllElements()
        {
            if (_branches == null)
            {
                //return NotFound();
                //TODO:Custom Response
                //Empiezo de nuevo
            }

            var countBranches = (from b in _branches select b).Count();

            /* if (!Convert.ToBoolean(countBranches))
                return NotFound("There are no Branches"); */

            var data = _branches;
            var branchesDTO = data.Select(b => _mapper.Map<BranchDTO>(b));

            return await branchesDTO.ToListAsync();
        }

        public async Task<BranchDTO> GetElementById(int id)
        {
            /* if (_branches == null)
            {
                return NotFound();
            } */
            var branch = await _branches.FindAsync(id);

            /* if (branch == null)
            {
                return NotFound($"There are no branch with id: {id}");
            } */

            var branchDTO = _mapper.Map<BranchDTO>(branch);

            return branchDTO;
        }

        public async void EditElement(int id, BranchDTO branchDTO)
        {
            var branch = _mapper.Map<Branch>(branchDTO);
            branch.Id = id;

            /* if (id != branch.Id)
            {
                return BadRequest();
            } */

            _repository.ModifiedState(branch);

                await _repository.SaveChangesAsync();
            /* try
            {
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchExists(id))
                {
                    return NotFound($"There are no branch with id: {id}");
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); */
        }

        public async Task<BranchDTO> AddElement(BranchDTO branchDTO)
        {
            var branch = _mapper.Map<Branch>(branchDTO);

            /* if (_branches == null)
            {
                return Problem("Entity set 'ApiContext.Branch'  is null.");
            } */

            _repository.AddBranch(branch);

            await _repository.SaveChangesAsync();
            //Here is added all the planning from this branch(365 days for categories(Car))
            AddPlanningBranch(branch);

            //return CreatedAtAction("GetBranch", new { id = branch.Id }, branch);
            return _mapper.Map<BranchDTO>(branch);
        }

        public async void RemoveElement(int id)
        {
            var branch = await _branches.FindAsync(id);
            /* if (_branches == null)
            {
                return NotFound();
            }
            if (branch == null)
            {
                return NotFound();
            } */

            _repository.Remove(branch);
            await _repository.SaveChangesAsync();

            //return NoContent();
        }

        //////////////////////////////////////////////////////////Helpers///////////////////////////////////////////////
        private bool BranchExists(int id)
        {
            return (_branches?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async void AddPlanningBranch(Branch branch)
        {
            var categories = Enum.GetValues(typeof(Car.Categories));

            for (int i = 0; i < 365; i++)
            {
                foreach (var categori in categories)
                {
                    var plan = _mapper.Map<Planning>(
                        new PlanningDTO
                        {
                            Day = DateTime.Now.AddDays(i).ToString(),
                            CarsAvailables = 0,
                            CarCategory = categori.ToString(),
                            BranchId = branch.Id
                        }
                    );
                    _repository.AddPlanning(plan);

                    await _repository.SaveChangesAsync();
                }
            }
        }
    }
}
