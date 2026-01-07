using FluentAssertions;
using LibraryManagement.Api.Controllers;
using LibraryManagement.Api.DTOs;
using LibraryManagement.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace LibraryManagement.Api.Tests.Controllers;

public class BooksControllerTests
{
    private readonly Mock<IBookService> _mockBookService;
    private readonly Mock<ILogger<BooksController>> _mockLogger;
    private readonly BooksController _controller;

    public BooksControllerTests()
    {
        _mockBookService = new Mock<IBookService>();
        _mockLogger = new Mock<ILogger<BooksController>>();
        _controller = new BooksController(_mockBookService.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetBooks_ShouldReturnOkResultWithBooks()
    {
        // Arrange
        var expectedBooks = new List<BookDto>
        {
            new BookDto
            {
                Id = 1,
                Title = "Test Book 1",
                ISBN = "123456",
                TotalCopies = 5,
                AvailableCopies = 3,
                CreatedAt = DateTime.UtcNow
            },
            new BookDto
            {
                Id = 2,
                Title = "Test Book 2",
                ISBN = "789012",
                TotalCopies = 3,
                AvailableCopies = 1,
                CreatedAt = DateTime.UtcNow
            }
        };

        _mockBookService.Setup(s => s.GetAllBooksAsync())
            .ReturnsAsync(expectedBooks);

        // Act
        var result = await _controller.GetBooks();

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        okResult!.Value.Should().BeEquivalentTo(expectedBooks);
        _mockBookService.Verify(s => s.GetAllBooksAsync(), Times.Once);
    }

    [Fact]
    public async Task GetBook_WhenBookExists_ShouldReturnOkResult()
    {
        // Arrange
        var bookId = 1;
        var expectedBook = new BookDto
        {
            Id = bookId,
            Title = "Test Book",
            ISBN = "123456",
            TotalCopies = 5,
            AvailableCopies = 3,
            CreatedAt = DateTime.UtcNow
        };

        _mockBookService.Setup(s => s.GetBookByIdAsync(bookId))
            .ReturnsAsync(expectedBook);

        // Act
        var result = await _controller.GetBook(bookId);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        okResult!.Value.Should().BeEquivalentTo(expectedBook);
        _mockBookService.Verify(s => s.GetBookByIdAsync(bookId), Times.Once);
    }

    [Fact]
    public async Task GetBook_WhenBookDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var bookId = 999;
        _mockBookService.Setup(s => s.GetBookByIdAsync(bookId))
            .ReturnsAsync((BookDto?)null);

        // Act
        var result = await _controller.GetBook(bookId);

        // Assert
        result.Result.Should().BeOfType<NotFoundObjectResult>();
        var notFoundResult = result.Result as NotFoundObjectResult;
        notFoundResult!.Value.Should().NotBeNull();
        _mockBookService.Verify(s => s.GetBookByIdAsync(bookId), Times.Once);
    }

    [Fact]
    public async Task CreateBook_WithValidDto_ShouldReturnCreatedResult()
    {
        // Arrange
        var createDto = new CreateBookDto
        {
            Title = "New Book",
            ISBN = "987654",
            TotalCopies = 10
        };

        var createdBook = new BookDto
        {
            Id = 1,
            Title = createDto.Title,
            ISBN = createDto.ISBN,
            TotalCopies = createDto.TotalCopies,
            AvailableCopies = createDto.TotalCopies,
            CreatedAt = DateTime.UtcNow
        };

        _mockBookService.Setup(s => s.CreateBookAsync(createDto))
            .ReturnsAsync(createdBook);

        // Act
        var result = await _controller.CreateBook(createDto);

        // Assert
        result.Result.Should().BeOfType<CreatedAtActionResult>();
        var createdResult = result.Result as CreatedAtActionResult;
        createdResult!.Value.Should().BeEquivalentTo(createdBook);
        createdResult.ActionName.Should().Be(nameof(BooksController.GetBook));
        _mockBookService.Verify(s => s.CreateBookAsync(createDto), Times.Once);
    }

    [Fact]
    public async Task CreateBook_WithInvalidModelState_ShouldReturnBadRequest()
    {
        // Arrange
        var createDto = new CreateBookDto
        {
            Title = "", // Invalid - empty title
            ISBN = "987654",
            TotalCopies = 10
        };

        _controller.ModelState.AddModelError("Title", "Title is required");

        // Act
        var result = await _controller.CreateBook(createDto);

        // Assert
        result.Result.Should().BeOfType<BadRequestObjectResult>();
        _mockBookService.Verify(s => s.CreateBookAsync(It.IsAny<CreateBookDto>()), Times.Never);
    }

    [Fact]
    public async Task CreateBook_WithDuplicateISBN_ShouldReturnBadRequest()
    {
        // Arrange
        var createDto = new CreateBookDto
        {
            Title = "New Book",
            ISBN = "123456", // Duplicate ISBN
            TotalCopies = 10
        };

        _mockBookService.Setup(s => s.CreateBookAsync(createDto))
            .ThrowsAsync(new InvalidOperationException("A book with ISBN 123456 already exists."));

        // Act
        var result = await _controller.CreateBook(createDto);

        // Assert
        result.Result.Should().BeOfType<BadRequestObjectResult>();
        var badRequestResult = result.Result as BadRequestObjectResult;
        badRequestResult!.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateBook_WhenBookExists_ShouldReturnNoContent()
    {
        // Arrange
        var bookId = 1;
        var updateDto = new UpdateBookDto
        {
            Title = "Updated Title",
            TotalCopies = 15
        };

        _mockBookService.Setup(s => s.UpdateBookAsync(bookId, updateDto))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.UpdateBook(bookId, updateDto);

        // Assert
        result.Should().BeOfType<NoContentResult>();
        _mockBookService.Verify(s => s.UpdateBookAsync(bookId, updateDto), Times.Once);
    }

    [Fact]
    public async Task UpdateBook_WhenBookDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var bookId = 999;
        var updateDto = new UpdateBookDto { Title = "Updated Title" };

        _mockBookService.Setup(s => s.UpdateBookAsync(bookId, updateDto))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.UpdateBook(bookId, updateDto);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        _mockBookService.Verify(s => s.UpdateBookAsync(bookId, updateDto), Times.Once);
    }

    [Fact]
    public async Task UpdateBook_WithInvalidModelState_ShouldReturnBadRequest()
    {
        // Arrange
        var bookId = 1;
        var updateDto = new UpdateBookDto { Title = "" }; // Invalid

        _controller.ModelState.AddModelError("Title", "Title must be between 1 and 200 characters");

        // Act
        var result = await _controller.UpdateBook(bookId, updateDto);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
        _mockBookService.Verify(s => s.UpdateBookAsync(It.IsAny<int>(), It.IsAny<UpdateBookDto>()), Times.Never);
    }

    [Fact]
    public async Task DeleteBook_WhenBookExists_ShouldReturnNoContent()
    {
        // Arrange
        var bookId = 1;
        _mockBookService.Setup(s => s.DeleteBookAsync(bookId))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteBook(bookId);

        // Assert
        result.Should().BeOfType<NoContentResult>();
        _mockBookService.Verify(s => s.DeleteBookAsync(bookId), Times.Once);
    }

    [Fact]
    public async Task DeleteBook_WhenBookDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var bookId = 999;
        _mockBookService.Setup(s => s.DeleteBookAsync(bookId))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteBook(bookId);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        _mockBookService.Verify(s => s.DeleteBookAsync(bookId), Times.Once);
    }

    [Fact]
    public async Task DeleteBook_WhenBookHasActiveBorrowings_ShouldReturnBadRequest()
    {
        // Arrange
        var bookId = 1;
        _mockBookService.Setup(s => s.DeleteBookAsync(bookId))
            .ThrowsAsync(new InvalidOperationException("Cannot delete book with active borrowings."));

        // Act
        var result = await _controller.DeleteBook(bookId);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
        var badRequestResult = result as BadRequestObjectResult;
        badRequestResult!.Value.Should().NotBeNull();
    }
}

