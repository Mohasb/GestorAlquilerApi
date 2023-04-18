namespace GestorAlquilerApi.BussinessLogicLayer.Responses
{
    public interface IResponsesApi<T>
    {
        ResponsesApi<T> MessageResponse(string message, int errorCode);
        //ResponsesApi<T> DataResponse(string message, int errorCode, List<T> data);
    }
}
