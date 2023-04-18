using System.Collections.Generic;
namespace GestorAlquilerApi.BussinessLogicLayer.Interfaces
{
    public interface IGenericService<T>
    {
        Task<IEnumerable<T>> GetAllElements();
        Task<T> GetElementById(int id);
        void EditElement(int id, T element);
        Task<T> AddElement(T element);
        void RemoveElement(int id);
    }
}
