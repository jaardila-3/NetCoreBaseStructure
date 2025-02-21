using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApp.Data.Context;
using WebApp.Data.Interfaces;

namespace WebApp.Data.Repositories;

public class Repository<T>(WebAppDbContext context) : IRepository<T> where T : class
{
    private readonly WebAppDbContext _context = context;

    public async Task<IReadOnlyList<T>> GetAllAsync() => await _context.Set<T>().AsNoTracking().ToListAsync();

    public async Task<IReadOnlyList<T>> GetListAsync(Expression<Func<T, bool>> predicate) => await _context.Set<T>().Where(predicate).AsNoTracking().ToListAsync();

    public async Task<IReadOnlyList<T>> GetListAsync(Expression<Func<T, bool>>? predicate, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<Expression<Func<T, object>>>? includes = null, bool disableTracking = true)
    {
        IQueryable<T> query = _context.Set<T>();
        if (disableTracking) query = query.AsNoTracking();

        if (includes is not null) query = includes.Aggregate(query, (current, include) => current.Include(include));

        if (predicate is not null) query = query.Where(predicate);

        if (orderBy is not null) return await orderBy(query).ToListAsync();

        return await query.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id) => (await _context.Set<T>().FindAsync(id))!;

    public async Task<T> GetFirstAsync(Expression<Func<T, bool>>? predicate, List<Expression<Func<T, object>>>? includes = null, bool disableTracking = true)
    {
        IQueryable<T> query = _context.Set<T>();
        if (disableTracking) query = query.AsNoTracking();

        if (includes is not null) query = includes.Aggregate(query, (current, include) => current.Include(include));

        if (predicate is not null) query = query.Where(predicate);

        return (await query.FirstOrDefaultAsync())!;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _context.Set<T>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> AddAsync(T entity)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public void UpdateEntity(T entity)
    {
        _context.Set<T>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void AddEntity(T entity) => _context.Set<T>().Add(entity);

    public void AddRange(List<T> entities) => _context.Set<T>().AddRange(entities);    

    public void DeleteEntity(T entity) => _context.Set<T>().Remove(entity);

    public void DeleteRange(IReadOnlyList<T> entities) => _context.Set<T>().RemoveRange(entities);
}