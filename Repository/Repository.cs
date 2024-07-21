﻿using Microsoft.EntityFrameworkCore;
using RepositoryPatternCrudEFCore.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryPatternCrudEFCore.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbSet<T> _dbSet;

        private readonly MyDbContext _myDbContext;  
        public Repository(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext; 
            _dbSet = _myDbContext.Set<T>(); 
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _myDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _myDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _myDbContext.Entry(entity).State = EntityState.Modified;
            await _myDbContext.SaveChangesAsync();
        }
    }
}
