using System;
using GestorAlquilerApi.DataAccessLayer.Data;
using GestorAlquilerApi.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.DataAccessLayer.Repository
{
    public class PermuteData<T> : IPermuteData<T>
    {
        private readonly ApiContext _context;

        public PermuteData(ApiContext context)
        {
            _context = context;
        }
        
        public void ModifiedState(T data) =>
            _context.Entry(data).State = EntityState.Modified;

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
