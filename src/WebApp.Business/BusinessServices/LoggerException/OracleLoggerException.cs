using WebApp.Business.Interfaces;
using WebApp.Data.Interfaces;
using WebApp.Data.Models.Entities;

namespace WebApp.Business.BusinessServices.LoggerException;

public class OracleLoggerException(IUnitOfWork unitOfWork): ILoggerException
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task CreateLogErrorAsync(string message, string exception, string? details = null)
    {
        try
        {
            await _unitOfWork.Repository<Log>().AddAsync(new Log
            {
                Timestamp = DateTime.Now,
                Level = "Error",
                Message = message,
                Exception = exception,
                Properties = details
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al registrar log en la base de datos. {ex.Message}");       
        }
    }
}