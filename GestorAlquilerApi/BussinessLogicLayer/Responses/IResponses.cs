namespace GestorAlquilerApi.BussinessLogicLayer.Responses
{
    public interface IResponses
    {

        string DataResponse<Tdata>(List<Tdata> data);
        string ComunicateResponse(string message, int errorCode);


    }
}
