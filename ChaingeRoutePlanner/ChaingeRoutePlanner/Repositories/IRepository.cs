using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ChaingeRoutePlanner.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);
    }
}