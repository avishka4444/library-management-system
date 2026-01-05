using LibraryManagement.Api.Data;
using LibraryManagement.Api.DTOs;
using LibraryManagement.Api.Models;
using LibraryManagement.Api.Controllers;
using LinqToDB;

namespace LibraryManagement.Api.Services;

public class BookService : IBookService
{
    private readonly LibraryDbContext _db;
    private readonly ILogger<BookService> _logger;

    public BookService(LibraryDbContext db, ILogger<BookService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
    {
        try
        {
            var books = await (from b in _db.Books
                              join a in _db.Authors on b.AuthorId equals a.Id into authorGroup
                              from author in authorGroup.DefaultIfEmpty()
                              select new BookDto
                              {
                                  Id = b.Id,
                                  Title = b.Title,
                                  ISBN = b.ISBN,
                                  AuthorId = b.AuthorId,
                                  AuthorName = author != null ? author.FullName : null,
                                  PublishedDate = b.PublishedDate,
                                  TotalCopies = b.TotalCopies,
                                  AvailableCopies = b.AvailableCopies,
                                  CreatedAt = b.CreatedAt
                              }).ToListAsync();

            return books;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all books");
            throw;
        }
    }

    public async Task<BookDto?> GetBookByIdAsync(int id)
    {
        try
        {
            var book = await (from b in _db.Books
                             where b.Id == id
                             join a in _db.Authors on b.AuthorId equals a.Id into authorGroup
                             from author in authorGroup.DefaultIfEmpty()
                             select new BookDto
                             {
                                 Id = b.Id,
                                 Title = b.Title,
                                 ISBN = b.ISBN,
                                 AuthorId = b.AuthorId,
                                 AuthorName = author != null ? author.FullName : null,
                                 PublishedDate = b.PublishedDate,
                                 TotalCopies = b.TotalCopies,
                                 AvailableCopies = b.AvailableCopies,
                                 CreatedAt = b.CreatedAt
                             }).FirstOrDefaultAsync();

            return book;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving book with id {BookId}", id);
            throw;
        }
    }

    public async Task<BookDto> CreateBookAsync(CreateBookDto dto)
    {
        try
        {
            // Validate ISBN uniqueness
            var existingBook = await _db.Books.FirstOrDefaultAsync(b => b.ISBN == dto.ISBN);
            if (existingBook != null)
            {
                throw new InvalidOperationException($"A book with ISBN {dto.ISBN} already exists.");
            }

            var book = new Book
            {
                Title = dto.Title,
                ISBN = dto.ISBN,
                AuthorId = dto.AuthorId,
                PublishedDate = dto.PublishedDate,
                TotalCopies = dto.TotalCopies,
                AvailableCopies = dto.TotalCopies,
                CreatedAt = DateTime.UtcNow
            };

            book.Id = _db.InsertWithInt32Identity(book);

            var author = book.AuthorId.HasValue
                ? await _db.Authors.FirstOrDefaultAsync(a => a.Id == book.AuthorId.Value)
                : null;

            var bookDto = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                AuthorId = book.AuthorId,
                AuthorName = author?.FullName,
                PublishedDate = book.PublishedDate,
                TotalCopies = book.TotalCopies,
                AvailableCopies = book.AvailableCopies,
                CreatedAt = book.CreatedAt
            };

            _logger.LogInformation("Book created with id {BookId}", book.Id);
            return bookDto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating book");
            throw;
        }
    }

    public async Task<bool> UpdateBookAsync(int id, UpdateBookDto dto)
    {
        try
        {
            var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
            {
                return false;
            }

            // Validate ISBN uniqueness if changed
            if (dto.ISBN != null && dto.ISBN != book.ISBN)
            {
                var existingBook = await _db.Books.FirstOrDefaultAsync(b => b.ISBN == dto.ISBN);
                if (existingBook != null)
                {
                    throw new InvalidOperationException($"A book with ISBN {dto.ISBN} already exists.");
                }
            }

            if (dto.Title != null) book.Title = dto.Title;
            if (dto.ISBN != null) book.ISBN = dto.ISBN;
            if (dto.AuthorId.HasValue) book.AuthorId = dto.AuthorId;
            if (dto.PublishedDate.HasValue) book.PublishedDate = dto.PublishedDate;
            if (dto.TotalCopies.HasValue)
            {
                var difference = dto.TotalCopies.Value - book.TotalCopies;
                book.TotalCopies = dto.TotalCopies.Value;
                book.AvailableCopies = Math.Max(0, book.AvailableCopies + difference);
            }

            book.UpdatedAt = DateTime.UtcNow;
            _db.Update(book);

            _logger.LogInformation("Book updated with id {BookId}", id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating book with id {BookId}", id);
            throw;
        }
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        try
        {
            var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
            {
                return false;
            }

            // Check if book has active borrowings
            var activeBorrowings = await _db.Borrowings
                .Where(br => br.BookId == id && br.Status == "Borrowed")
                .AnyAsync();

            if (activeBorrowings)
            {
                throw new InvalidOperationException("Cannot delete book with active borrowings.");
            }

            _db.Delete(book);
            _logger.LogInformation("Book deleted with id {BookId}", id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting book with id {BookId}", id);
            throw;
        }
    }
}

