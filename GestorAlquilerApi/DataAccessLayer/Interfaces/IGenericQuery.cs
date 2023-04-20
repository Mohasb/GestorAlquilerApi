
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.DataAccessLayer.Interfaces
{
    public interface IGenericQuery<T>
    {
        public IQueryable<T> GetAllElements();
        public List<T> GetdataById(int id);
        public T AddElement(T element);
        public T RemoveElement(T element);
    }
}
