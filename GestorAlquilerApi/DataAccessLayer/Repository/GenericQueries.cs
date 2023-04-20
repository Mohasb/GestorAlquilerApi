
using GestorAlquilerApi.DataAccessLayer.Data;
using GestorAlquilerApi.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.DataAccessLayer.Repository
{
    public class GenericQueries<T> : IGenericQuery<T>
    {
        private readonly ApiContext _context;

        public GenericQueries(ApiContext context)
        {
            _context = context;
        }
        public T AddElement(T element)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAllElements()
        {
            var type = typeof(T);
            Console.WriteLine(type);
            throw new NotImplementedException();
            //return _context.Set<typeof(T)>();
            //return _context.GetType(T);
        }

        public List<T> GetdataById(int id)
        {
            throw new NotImplementedException();
        }

        public T RemoveElement(T element)
        {
            throw new NotImplementedException();
        }
    }
}
