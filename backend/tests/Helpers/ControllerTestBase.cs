using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace LibraryManagement.Api.Tests.Helpers;

public abstract class ControllerTestBase
{
    protected Mock<ILogger<TController>> CreateMockLogger<TController>()
        where TController : class
    {
        return new Mock<ILogger<TController>>();
    }

    protected void VerifyLogging<TController>(
        Mock<ILogger<TController>> logger,
        LogLevel level,
        string message)
        where TController : class
    {
        logger.Verify(
            x => x.Log(
                level,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains(message)),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.AtLeastOnce);
    }

    protected T? GetValueFromActionResult<T>(IActionResult? result)
    {
        if (result is OkObjectResult okResult)
        {
            return (T?)okResult.Value;
        }
        if (result is CreatedAtActionResult createdResult)
        {
            return (T?)createdResult.Value;
        }
        return default;
    }
}

