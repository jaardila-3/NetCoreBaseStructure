using System.Linq.Expressions;

namespace WebApp.Data.Interfaces;

public interface IRepository<T> where T : class
{
    Task<IReadOnlyList<T>> GetAllAsync();

  Task<IReadOnlyList<T>> GetListAsync(Expression<Func<T, bool>> predicate);

  Task<IReadOnlyList<T>> GetListAsync(Expression<Func<T, bool>>? predicate,
                                 Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                 List<Expression<Func<T, object>>>? includes = null,
                                 bool disableTracking = true);


  Task<T> GetFirstAsync(Expression<Func<T, bool>>? predicate,
                                   List<Expression<Func<T, object>>>? includes = null,
                                 bool disableTracking = true);


  Task<T> GetByIdAsync(int id);

  Task<T> AddAsync(T entity);



  Task<T> UpdateAsync(T entity);

  Task DeleteAsync(T entity);


  void AddEntity(T entity);

  void UpdateEntity(T entity);

  void DeleteEntity(T entity);

  void AddRange(List<T> entities);

  void DeleteRange(IReadOnlyList<T> entities);  
}