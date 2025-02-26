using System.Collections;
using WebApp.Data.Context;
using WebApp.Data.Interfaces;

namespace WebApp.Data.Repositories;
public class UnitOfWork(WebAppDbContext context) : IUnitOfWork
{
    private Hashtable? _repositories;
    private readonly WebAppDbContext _context = context;
    private bool _disposed;

    public IRepository<T> Repository<T>() where T : class
    {
        _repositories ??= [];
        string keyName = typeof(T).Name;

        if (!_repositories.ContainsKey(keyName))
        {
            Type repositoryType = typeof(Repository<>);
            object? repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
            _repositories.Add(keyName, repositoryInstance);
        }

        return (IRepository<T>)_repositories[keyName]!;
    }

    public async Task<int> CommitAsync()
    {
        try
        {
            return await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error saving changes in UnitOfWork: {ex.Message}");
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }
}
