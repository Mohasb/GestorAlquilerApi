using Microsoft.AspNetCore.Mvc;
using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using GestorAlquilerApi.BussinessLogicLayer.ControllersService;

namespace GestorAlquilerApi.BussinessLogicLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController
    {
        private readonly IGenericService<BranchDTO> _branchService;

        public BranchesController(IGenericService<BranchDTO> branchService)
        {
            _branchService = branchService;
        }

        // GET: api/Branches
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IEnumerable<BranchDTO>> GetBranch() =>
            await _branchService.GetAllElements();

        // GET: api/Branches/{id}
        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<BranchDTO> GetBranch(int id) => await _branchService.GetElementById(id);

        // PUT: api/Branches/{id}
        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public void PutBranch(int id, BranchDTO branchDTO) =>
            _branchService.EditElement(id, branchDTO);

        // POST: api/Branches
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<BranchDTO> PostBranch(BranchDTO branchDTO) =>
            await _branchService.AddElement(branchDTO);

        // DELETE: api/Branches/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public void DeleteBranch(int id) => _branchService.RemoveElement(id);
    }
}
