
using GestorAlquilerApi.BussinessLogicLayer.Models;

namespace GestorAlquilerApi.DataAccessLayer.Interfaces
{
    public interface IQueryUser
    {
        public void ModifiedState(Client user);
        public Task SaveChangesAsync();
    }
}
