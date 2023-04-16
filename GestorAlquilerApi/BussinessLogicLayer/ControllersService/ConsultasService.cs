using AutoMapper;
using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Interfaces;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using GestorAlquilerApi.DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace GestorAlquilerApi.BussinessLogicLayer.ControllersService
{
    public class ConsultasService : ControllerBase, IConsultasService
    {
        private readonly IQueryConsultas _repository;
        private readonly IQueryBranch _branchRepository;
        private readonly IQueryCar _carsRepository;
        private readonly IMapper _mapper;
        private readonly DbSet<Branch> _branches;
        private readonly DbSet<Car> _cars;

        public ConsultasService(IMapper mapper, IQueryConsultas repository, IQueryBranch branchRepository, IQueryCar carsRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _branchRepository = branchRepository;
            _carsRepository = carsRepository;
            /////
            _branches = branchRepository.GetDataBranches();
            _cars = carsRepository.GetDataCars();
        }
        public async Task<ActionResult<IEnumerable<ICollection<CarDTO>>>> GetCarsByBranchId(int id)
        {
            if (_branches == null || _cars == null)
            {
                return NotFound();
            }

            var countCars = _cars.Count();
            var countBranches = _branches.Count();

            if (!Convert.ToBoolean(countBranches)) return Problem("Theres no Branches", statusCode: 404);
            if (!Convert.ToBoolean(countCars)) return Problem("Theres no Cars", statusCode: 404);


            var cars = _repository.GetCarsByBranchId(id, _cars, _branches);



            return Ok(await cars.ToListAsync());
        }
        public async Task<ActionResult<IEnumerable<ICollection<CarDTO>>>> GetCarsByBranch(DateTime date, int branchId, string carCategory)
        {
            /*if (_context.Reservation == null || _branches == null || _cars == null)
            {
                return NotFound();
            }


            var data = from p in _context.Planning
                       where p.Day == date && p.BranchId == branchId && p.CarCategory == carCategory
                       select p;

            return Ok(await data.ToListAsync());*/
            return Ok();
        }
    }
}
