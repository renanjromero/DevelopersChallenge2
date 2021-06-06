using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nibo.Domain.Interfaces
{
    public interface IEntityBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();

        Task AddRange(IEnumerable<TEntity> entities);

    }
}
