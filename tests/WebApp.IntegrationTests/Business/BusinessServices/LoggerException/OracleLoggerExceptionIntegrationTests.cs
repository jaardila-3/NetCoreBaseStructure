using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Business.Interfaces;
using WebApp.Common.Constants;
using WebApp.Data.Context;
using WebApp.Data.Models.Entities;
using WebApp.IntegrationTests.Fixtures;

namespace WebApp.IntegrationTests.Business.BusinessServices.LoggerException;

public class OracleLoggerExceptionIntegrationTests : IClassFixture<DatabaseFixture>
{
    private readonly IServiceProvider _serviceProvider;
    private readonly DatabaseFixture _fixture;

    public OracleLoggerExceptionIntegrationTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
        // Reset the database before each test to isolate the state
        _fixture.ResetDatabase();
        _serviceProvider = _fixture.ServiceProvider;
    }

    [Fact]
    public async Task CreateLogErrorAsync_Should_SaveLogToDatabase()
    {
        // Arrange
        using var scope = _serviceProvider.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILoggerException>();
        var dbContext = scope.ServiceProvider.GetRequiredService<WebAppDbContext>();

        string message = "Integration test error";
        string exception = "System.Exception: Test";
        string details = "StackTrace info";

        // Act
        await logger.CreateLogErrorAsync(message, exception, details);

        // Assert
        var logEntry = await dbContext.Set<Log>().FirstOrDefaultAsync(l => l.Message == message);
        Assert.NotNull(logEntry);
        Assert.Equal(GlobalConstants.ERROR, logEntry.Level);
        Assert.Equal(exception, logEntry.Exception);
        Assert.Equal(details, logEntry.Properties);
    }

    [Fact]
    public async Task CreateLogErrorAsync_Should_NotSaveLog_When_MessageIsEmpty()
    {
        // Arrange
        using var scope = _serviceProvider.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILoggerException>();
        var dbContext = scope.ServiceProvider.GetRequiredService<WebAppDbContext>();

        // Act
        await logger.CreateLogErrorAsync("", "Test exception");

        // Assert
        var logCount = await dbContext.Set<Log>().CountAsync();
        Assert.Equal(0, logCount); // No logs saved
    }
}