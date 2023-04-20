using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GestorAlquilerApi.BussinessLogicLayer.Interfaces
{
    public interface IConsultasService
    {
        Task<ActionResult<IEnumerable<ICollection<CarDTO>>>> GetCarsByBranchId(int id);
        Task<ActionResult<IEnumerable<ICollection<CarDTO>>>> GetCarsByBranch(
            DateTime date,
            int branchId,
            string carCategory
        );
    }
}
