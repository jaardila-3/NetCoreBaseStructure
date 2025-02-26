using Moq;
using WebApp.Business.BusinessServices.LoggerException;
using WebApp.Common.Constants;
using WebApp.Data.Interfaces;
using WebApp.Data.Models.Entities;

namespace WebApp.UnitTests.Business.BusinessServices.LoggerException;

public class OracleLoggerExceptionTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IRepository<Log>> _mockLogRepository;
    private readonly OracleLoggerException _logger;

    public OracleLoggerExceptionTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockLogRepository = new Mock<IRepository<Log>>();

        _mockUnitOfWork.Setup(u => u.Repository<Log>()).Returns(_mockLogRepository.Object);
        _logger = new OracleLoggerException(_mockUnitOfWork.Object);
    }

    [Fact]
    public async Task CreateLogErrorAsync_Should_SaveLogOnce_When_Called()
    {
        // Arrange
        var message = "Test message";
        var exception = "Test exception";
        var details = "Test details";

        // Act
        await _logger.CreateLogErrorAsync(message, exception, details);

        // Assert
        _mockLogRepository.Verify(r => r.AddAsync(It.IsAny<Log>()), Times.Once, "Log should be saved exactly once.");
    }

    [Fact]
    public async Task CreateLogErrorAsync_Should_NotThrowException_When_DatabaseFails()
    {
        // Arrange
        _mockLogRepository.Setup(r => r.AddAsync(It.IsAny<Log>())).ThrowsAsync(new Exception("DB Error"));

        // Act
        var exception = await Record.ExceptionAsync(() => _logger.CreateLogErrorAsync("Test message", "Test exception"));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public async Task Should_NotSaveLog_When_MessageIsNullOrEmpty()
    {
        // Act
        await _logger.CreateLogErrorAsync("", "Test exception");

        // Assert
        _mockLogRepository.Verify(r => r.AddAsync(It.IsAny<Log>()), Times.Never,
            "Log should NOT be saved when message is null or empty.");
    }

    [Fact]
    public async Task Should_SaveCorrectLogData()
    {
        // Arrange
        var message = "Error occurred";
        var exception = "System.Exception: Test";
        var details = "StackTrace info";

        Log? capturedLog = null;
        _mockLogRepository.Setup(r => r.AddAsync(It.IsAny<Log>()))
            .Callback<Log>(log => capturedLog = log);

        // Act
        await _logger.CreateLogErrorAsync(message, exception, details);

        // Assert
        Assert.NotNull(capturedLog);
        Assert.Equal(GlobalConstants.ERROR, capturedLog.Level);
        Assert.Equal(message, capturedLog.Message);
        Assert.Equal(exception, capturedLog.Exception);
        Assert.Equal(details, capturedLog.Properties);
    }

    [Fact]
    public async Task Should_SaveLog_When_DetailsIsNull()
    {
        // Arrange
        var message = "Error occurred";
        var exception = "System.Exception: Test";

        // Act
        await _logger.CreateLogErrorAsync(message, exception, null);

        // Assert
        _mockLogRepository.Verify(r => r.AddAsync(It.IsAny<Log>()), Times.Once,
            "Log should be saved even if details is null.");
    }

}