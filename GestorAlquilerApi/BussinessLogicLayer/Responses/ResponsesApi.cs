namespace GestorAlquilerApi.BussinessLogicLayer.Responses
{
    public class ResponsesApi<T> : IResponsesApi<T>
    {
        public string? Message { get; set; }
        public int ErrorCode { get; set; }
        //public List<T>? Data { get; set; }

        public ResponsesApi(string message, int errorCode) 
        { 
            Message = message;
            ErrorCode = errorCode;
         }
        /* public ResponsesApi(string message, int errorCode, List<T> data) { 
            Message = message;
            ErrorCode = errorCode;
            Data = data;
         } */

        public ResponsesApi<T> MessageResponse(string message, int errorCode) => new ResponsesApi<T>(message, errorCode);

        /* public ResponsesApi<T> DataResponse(string message, int errorCode, List<T> data) => new ResponsesApi<T>(message, errorCode, data); */
    }
}
