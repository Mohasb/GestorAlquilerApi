using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GestorAlquilerApi.BussinessLogicLayer.Interfaces
{
    public interface IBranchService
    {
        Task<ActionResult<IEnumerable<BranchDTO>>> GetBranch();
        Task<ActionResult<BranchDTO>> GetBranch(int id);
        Task<IActionResult> PutBranch(int id, BranchDTO branchDTO);
        Task<ActionResult<BranchDTO>> PostBranch(BranchDTO branchDTO);
        Task<IActionResult> DeleteBranch(int id);
    }
}
