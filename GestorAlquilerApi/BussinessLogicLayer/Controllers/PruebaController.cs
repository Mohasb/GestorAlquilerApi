using GestorAlquilerApi.BussinessLogicLayer.Responses;
using Microsoft.AspNetCore.Mvc;
using GestorAlquilerApi.DataAccessLayer.Data;
using GestorAlquilerApi.BussinessLogicLayer.Models;

namespace GestorAlquilerApi.BussinessLogicLayer.Controllers
{
    [Route("api/[controller]")]
    [Tags("!Prueba")]
    [ApiController]
    public class PruebaController : ControllerBase
    {
        /* private readonly IResponsesApi _response;
        private readonly ApiContext _context;

        public PruebaController(IResponsesApi response, ApiContext context)
        {
            _response = response;
            _context = context;
        }

        [HttpGet]
        public ResponsesApi Get()
        {
            return _response.MessageResponse();
        }

        [HttpGet("{id}")]
        public ResponsesApi GetData(int id)
        {
            return _response.DataResponse(_context.Branch.ToList());
        } */
    }
}
