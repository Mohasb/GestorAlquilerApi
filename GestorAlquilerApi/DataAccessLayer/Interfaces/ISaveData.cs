using System;

namespace GestorAlquilerApi.DataAccessLayer.Interfaces
{
    public interface ISaveData<T>
    {
        public void ModifiedState(T branch);
        public Task SaveChangesAsync();
    }
}
