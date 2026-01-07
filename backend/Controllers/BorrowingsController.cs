using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Api.Data;
using LibraryManagement.Api.Models;
using LibraryManagement.Api.DTOs;
using LinqToDB;
using LibraryManagement.Api.Controllers;

namespace LibraryManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BorrowingsController : ControllerBase
{
    private readonly LibraryDbContext _db;

    public BorrowingsController(LibraryDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BorrowingDto>>> GetBorrowings()
    {
        var borrowings = await (from b in _db.Borrowings
                                join book in _db.Books on b.BookId equals book.Id
                                join member in _db.Members on b.MemberId equals member.Id
                                select new BorrowingDto
                                {
                                    Id = b.Id,
                                    BookId = b.BookId,
                                    BookTitle = book.Title,
                                    MemberId = b.MemberId,
                                    MemberName = member.FullName,
                                    BorrowedDate = b.BorrowedDate,
                                    ReturnedDate = b.ReturnedDate,
                                    DueDate = b.DueDate,
                                    Status = b.Status ?? "Borrowed",
                                    CreatedAt = b.CreatedAt
                                }).ToListAsync();

        return Ok(borrowings);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BorrowingDto>> GetBorrowing(int id)
    {
        var borrowing = await (from b in _db.Borrowings
                               where b.Id == id
                               join book in _db.Books on b.BookId equals book.Id
                               join member in _db.Members on b.MemberId equals member.Id
                               select new BorrowingDto
                               {
                                   Id = b.Id,
                                   BookId = b.BookId,
                                   BookTitle = book.Title,
                                   MemberId = b.MemberId,
                                   MemberName = member.FullName,
                                   BorrowedDate = b.BorrowedDate,
                                   ReturnedDate = b.ReturnedDate,
                                   DueDate = b.DueDate,
                                   Status = b.Status ?? "Borrowed",
                                   CreatedAt = b.CreatedAt
                               }).FirstOrDefaultAsync();

        if (borrowing == null)
        {
            return NotFound();
        }

        return Ok(borrowing);
    }

    [HttpGet("member/{memberId}")]
    public async Task<ActionResult<IEnumerable<BorrowingDto>>> GetBorrowingsByMember(int memberId)
    {
        var borrowings = await (from b in _db.Borrowings
                                where b.MemberId == memberId
                                join book in _db.Books on b.BookId equals book.Id
                                join member in _db.Members on b.MemberId equals member.Id
                                select new BorrowingDto
                                {
                                    Id = b.Id,
                                    BookId = b.BookId,
                                    BookTitle = book.Title,
                                    MemberId = b.MemberId,
                                    MemberName = member.FullName,
                                    BorrowedDate = b.BorrowedDate,
                                    ReturnedDate = b.ReturnedDate,
                                    DueDate = b.DueDate,
                                    Status = b.Status ?? "Borrowed",
                                    CreatedAt = b.CreatedAt
                                }).ToListAsync();

        return Ok(borrowings);
    }

    [HttpPost]
    public async Task<ActionResult<BorrowingDto>> CreateBorrowing(CreateBorrowingDto dto)
    {
        // Check if book exists and has available copies
        var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == dto.BookId);
        if (book == null)
        {
            return NotFound("Book not found.");
        }

        if (book.AvailableCopies <= 0)
        {
            return BadRequest("No available copies of this book.");
        }

        // Check if member exists
        var member = await _db.Members.FirstOrDefaultAsync(m => m.Id == dto.MemberId);
        if (member == null)
        {
            return NotFound("Member not found.");
        }

        var borrowing = new Borrowing
        {
            BookId = dto.BookId,
            MemberId = dto.MemberId,
            BorrowedDate = DateTime.UtcNow,
            DueDate = dto.DueDate,
            Status = "Borrowed",
            CreatedAt = DateTime.UtcNow
        };

        borrowing.Id = _db.InsertWithInt32Identity(borrowing);

        // Update book available copies
        book.AvailableCopies--;
        book.UpdatedAt = DateTime.UtcNow;
        _db.Update(book);

        var borrowingDto = new BorrowingDto
        {
            Id = borrowing.Id,
            BookId = borrowing.BookId,
            BookTitle = book.Title,
            MemberId = borrowing.MemberId,
            MemberName = member.FullName,
            BorrowedDate = borrowing.BorrowedDate,
            ReturnedDate = borrowing.ReturnedDate,
            DueDate = borrowing.DueDate,
            Status = borrowing.Status ?? "Borrowed",
            CreatedAt = borrowing.CreatedAt
        };

        return CreatedAtAction(nameof(GetBorrowing), new { id = borrowing.Id }, borrowingDto);
    }

    [HttpPost("return")]
    public async Task<IActionResult> ReturnBook(ReturnBookDto dto)
    {
        var borrowing = await _db.Borrowings.FirstOrDefaultAsync(b => b.Id == dto.BorrowingId);

        if (borrowing == null)
        {
            return NotFound("Borrowing record not found.");
        }

        if (borrowing.Status == "Returned")
        {
            return BadRequest("This book has already been returned.");
        }

        borrowing.ReturnedDate = DateTime.UtcNow;
        borrowing.Status = "Returned";
        borrowing.UpdatedAt = DateTime.UtcNow;

        _db.Update(borrowing);

        // Update book available copies
        var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == borrowing.BookId);
        if (book != null)
        {
            book.AvailableCopies++;
            book.UpdatedAt = DateTime.UtcNow;
            _db.Update(book);
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBorrowing(int id)
    {
        var borrowing = await _db.Borrowings.FirstOrDefaultAsync(b => b.Id == id);

        if (borrowing == null)
        {
            return NotFound();
        }

        // If not returned, update book available copies
        if (borrowing.Status != "Returned")
        {
            var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == borrowing.BookId);
            if (book != null)
            {
                book.AvailableCopies++;
                book.UpdatedAt = DateTime.UtcNow;
                _db.Update(book);
            }
        }

        _db.Delete(borrowing);

        return NoContent();
    }
}

