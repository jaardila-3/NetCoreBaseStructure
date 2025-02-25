namespace WebApp.Business.Interfaces;

public interface ILoggerException
{
    Task CreateLogErrorAsync(string message, string exception, string? details = null);
}