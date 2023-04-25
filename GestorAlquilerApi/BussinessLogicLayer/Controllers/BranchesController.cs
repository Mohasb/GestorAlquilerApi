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

        /// <summary>
        /// Returns a List of all Branches
        /// </summary>
        // GET: api/Branches
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BranchDTO>>> GetBranch() =>
            await _branchService.GetAllElements();

        /// <summary>
        /// Returns a Branch by its Id
        /// </summary>
        /// <param name="id">The id of the Branch.</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/branches/5
        ///
        /// </remarks>
        /// <response code="200">Returns the branch</response>
        /// <response code="400">If there are no branch with that id</response>
        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<BranchDTO>> GetBranch(int id) =>
            await _branchService.GetElementById(id);

        /// <summary>
        /// Edit a Branch by its Id
        /// </summary>
        /// <param name="id">The id of the Branch.</param>
        /// <param name="branchDTO">Body of the new branch</param>
        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public Task<IActionResult> PutBranch(int id, BranchDTO branchDTO) =>
            _branchService.EditElement(id, branchDTO);

        /// <summary>
        /// Add a new Branch
        /// </summary>
        /// <param name="branchDTO">Body of the branch(CIF UNIQUE)</param>
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<BranchDTO>> PostBranch(BranchDTO branchDTO) =>
            await _branchService.AddElement(branchDTO);

        /// <summary>
        /// Delete a branch
        /// </summary>
        /// <param name="id">id of the branch to delete</param>
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public Task<IActionResult> DeleteBranch(int id) => _branchService.RemoveElement(id);
    }
}
