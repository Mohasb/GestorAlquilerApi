using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GestorAlquilerApi.BussinessLogicLayer.Interfaces
{
    public interface IBranchService
    {
        Task<ActionResult<IEnumerable<BranchDTO>>> GetAllBranches();
        Task<ActionResult<BranchDTO>> GetBranchById(int id);
        Task<IActionResult> EditBranch(int id, BranchDTO branchDTO);
        Task<ActionResult<BranchDTO>> AddBranch(BranchDTO branchDTO);
        Task<IActionResult> RemoveBranch(int id);
    }
}
