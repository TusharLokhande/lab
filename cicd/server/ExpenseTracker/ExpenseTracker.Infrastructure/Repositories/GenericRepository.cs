using ExpenseTracker.Application.Interfaces.Repository;
using ExpenseTracker.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _db;

        public GenericRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _db.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
             _db.Set<T>().Update(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            _db.Set<T>().Remove(entity);
        }
    }

}
