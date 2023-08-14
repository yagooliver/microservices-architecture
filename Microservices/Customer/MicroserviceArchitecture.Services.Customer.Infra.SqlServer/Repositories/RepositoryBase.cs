using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroserviceArchitecture.Common.Models.Entities;
using MicroserviceArchitecture.Common.Models.Interfaces.Repositories;
using MicroserviceArchitecture.Services.Customer.Infra.SqlServer.ContextDb;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceArchitecture.Services.Customer.Infra.SqlServer.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        protected readonly CustomerDbContext Db;
        protected readonly DbSet<T> DbSet;

        public RepositoryBase(CustomerDbContext db)
        {
            Db = db;
            DbSet = Db.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await DbSet.AddAsync(entity);
            await Db.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            DbSet.Remove(entity);
            await Db.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>?> GetAllAsync()
        {
            return await DbSet.Where(x => !x.IsRemoved).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            DbSet.Entry(entity).State = EntityState.Modified;
            await Db.SaveChangesAsync();
            return entity;
        }
    }
}