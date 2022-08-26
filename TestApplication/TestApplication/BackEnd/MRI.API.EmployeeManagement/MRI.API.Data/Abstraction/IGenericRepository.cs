using System.Collections.Generic;
using System.Threading.Tasks;

namespace MRI.API.Data.Abstraction
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Get(object id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Delete(object id);
        Task<TEntity> Update(TEntity entity);
    }
}
