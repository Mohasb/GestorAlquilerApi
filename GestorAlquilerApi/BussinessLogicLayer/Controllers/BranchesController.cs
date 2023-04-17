using Microsoft.AspNetCore.Mvc;
using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace GestorAlquilerApi.BussinessLogicLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController
    {
        private readonly IBranchService _branchService;

        public BranchesController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        // GET: api/Branches
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BranchDTO>>> GetBranch() =>
            await _branchService.GetBranch();

        // GET: api/Branches/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BranchDTO>> GetBranch(int id) =>
            await _branchService.GetBranch(id);

        // PUT: api/Branches/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutBranch(int id, BranchDTO branchDTO) =>
            await _branchService.PutBranch(id, branchDTO);

        // POST: api/Branches
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BranchDTO>> PostBranch(BranchDTO branchDTO) =>
            await _branchService.PostBranch(branchDTO);

        // DELETE: api/Branches/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBranch(int id) =>
            await _branchService.DeleteBranch(id);
    }
}
