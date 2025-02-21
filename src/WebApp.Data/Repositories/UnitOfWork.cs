using System.Collections;
using WebApp.Data.Context;
using WebApp.Data.Interfaces;

namespace WebApp.Data.Repositories;
public class UnitOfWork: IUnitOfWork
{
    private Hashtable? _repositories;
    private readonly WebAppDbContext _context;

    public UnitOfWork(WebAppDbContext context) => _context = context;

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

    public async Task<int> SaveChangesAsync()
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

    public void Dispose() => _context.Dispose();
}
