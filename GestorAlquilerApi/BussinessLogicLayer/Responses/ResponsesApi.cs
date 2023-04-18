using System.Collections.Generic;
using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GestorAlquilerApi.BussinessLogicLayer.Responses
{
    public class ResponsesApi : IResponsesApi
    {
        public string? message { get; set; }
        public int errorCode { get; set; }
        public List<Branch>? data { get; set; }

        public ResponsesApi() { }
        public ResponsesApi(List<Branch> data) { 
            this.data = data;
         }

        public ResponsesApi MessageResponse() => new ResponsesApi();

        public ResponsesApi DataResponse(List<Branch> data) => new ResponsesApi(data);
    }
}
