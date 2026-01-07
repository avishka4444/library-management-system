using LibraryManagement.Api.DTOs;

namespace LibraryManagement.Api.Tests.Helpers;

public static class TestDataBuilder
{
    public static CreateBookDto CreateBookDto(
        string? title = null,
        string? isbn = null,
        int? authorId = null,
        DateTime? publishedDate = null,
        int? totalCopies = null)
    {
        return new CreateBookDto
        {
            Title = title ?? "Test Book",
            ISBN = isbn ?? "978-0-123456-78-9",
            AuthorId = authorId,
            PublishedDate = publishedDate,
            TotalCopies = totalCopies ?? 5
        };
    }

    public static UpdateBookDto UpdateBookDto(
        string? title = null,
        string? isbn = null,
        int? authorId = null,
        DateTime? publishedDate = null,
        int? totalCopies = null)
    {
        return new UpdateBookDto
        {
            Title = title,
            ISBN = isbn,
            AuthorId = authorId,
            PublishedDate = publishedDate,
            TotalCopies = totalCopies
        };
    }

    public static BookDto BookDto(
        int id = 1,
        string? title = null,
        string? isbn = null,
        int? authorId = null,
        string? authorName = null,
        int? totalCopies = null,
        int? availableCopies = null)
    {
        return new BookDto
        {
            Id = id,
            Title = title ?? "Test Book",
            ISBN = isbn ?? "978-0-123456-78-9",
            AuthorId = authorId,
            AuthorName = authorName,
            TotalCopies = totalCopies ?? 5,
            AvailableCopies = availableCopies ?? 5,
            CreatedAt = DateTime.UtcNow
        };
    }
}

