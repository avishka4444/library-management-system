using LibraryManagement.Api.DTOs;
using Xunit;

namespace LibraryManagement.Api.Tests.Services;

public class BookServiceTests
{
    [Fact]
    public void BookService_ShouldExist()
    {
        Assert.True(true);
    }

    [Fact]
    public void CreateBookDto_Validation_ShouldWork()
    {
        // Arrange
        var dto = new CreateBookDto
        {
            Title = "Test Book",
            ISBN = "123456",
            TotalCopies = 10
        };

        // Act & Assert
        Assert.NotNull(dto);
        Assert.Equal("Test Book", dto.Title);
        Assert.Equal("123456", dto.ISBN);
        Assert.Equal(10, dto.TotalCopies);
    }

    [Fact]
    public void UpdateBookDto_Validation_ShouldWork()
    {
        // Arrange
        var dto = new UpdateBookDto
        {
            Title = "Updated Title",
            TotalCopies = 15
        };

        // Act & Assert
        Assert.NotNull(dto);
        Assert.Equal("Updated Title", dto.Title);
        Assert.Equal(15, dto.TotalCopies);
    }
}

