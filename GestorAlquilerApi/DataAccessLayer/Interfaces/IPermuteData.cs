using System;

namespace GestorAlquilerApi.DataAccessLayer.Interfaces
{
    public interface IPermuteData<T>
    {
        public void ModifiedState(T branch);
        public Task SaveChangesAsync();
    }
}
