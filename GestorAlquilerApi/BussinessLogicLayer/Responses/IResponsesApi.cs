using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Models;

namespace GestorAlquilerApi.BussinessLogicLayer.Responses
{
    public interface IResponsesApi
    {
        ResponsesApi MessageResponse();
        ResponsesApi DataResponse(List<Branch> data);
    }
}
