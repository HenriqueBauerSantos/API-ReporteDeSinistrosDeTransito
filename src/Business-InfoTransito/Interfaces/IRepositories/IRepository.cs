using System.Linq.Expressions;

namespace Business_InfoTransito.Interfaces.IRepositories;

public interface IRepository<TEntity> : IDisposable where TEntity : class
{
    Task Add(TEntity entity);
    Task<TEntity> GetById(Guid id);
    Task<List<TEntity>> GetAll();
    Task Update(TEntity entity);
    Task Delete(Guid id);
    Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
    Task<int> SaveChanges();
    Task DetachEntity(TEntity entity);
}
