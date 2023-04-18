using Microsoft.DotNet.MSIdentity.Shared;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GestorAlquilerApi.BussinessLogicLayer.Responses
{
    public class Response : IResponses
    {
        public string DataResponse<Tdata>(List<Tdata> data)

            => JsonConvert.SerializeObject(new
            {
                result = new
                {
                    data
                }
            });

            
        

        public string ComunicateResponse(string message, int errorCode)

        => JsonConvert.SerializeObject(new
        {
            result = new
            {
                errorCode,
                message
            }
        });
    }
}
