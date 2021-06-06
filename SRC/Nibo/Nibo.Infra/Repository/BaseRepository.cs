using Microsoft.EntityFrameworkCore;
using Nibo.Domain.Interfaces;
using Nibo.Infra.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nibo.Infra.Repository
{
    public class BaseRepository<TEntity> : IEntityBaseRepository<TEntity> where TEntity : class
    {
        protected readonly NiboContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public BaseRepository(NiboContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public async Task AddRange(IEnumerable<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities);
            await Db.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
