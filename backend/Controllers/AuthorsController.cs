using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Api.Data;
using LibraryManagement.Api.Models;
using LibraryManagement.Api.DTOs;
using LinqToDB;
using LibraryManagement.Api.Controllers;

namespace LibraryManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly LibraryDbContext _db;

    public AuthorsController(LibraryDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors()
    {
        var authors = await _db.Authors.ToListAsync();

        var authorDtos = authors.Select(a => new AuthorDto
        {
            Id = a.Id,
            FirstName = a.FirstName,
            LastName = a.LastName,
            FullName = a.FullName,
            DateOfBirth = a.DateOfBirth,
            Biography = a.Biography,
            CreatedAt = a.CreatedAt
        }).ToList();

        return Ok(authorDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorDto>> GetAuthor(int id)
    {
        var author = await _db.Authors.FirstOrDefaultAsync(a => a.Id == id);
        if (author == null)
        {
            return NotFound();
        }

        var authorDto = new AuthorDto
        {
            Id = author.Id,
            FirstName = author.FirstName,
            LastName = author.LastName,
            FullName = author.FullName,
            DateOfBirth = author.DateOfBirth,
            Biography = author.Biography,
            CreatedAt = author.CreatedAt
        };

        return Ok(authorDto);
    }

    [HttpPost]
    public Task<ActionResult<AuthorDto>> CreateAuthor(CreateAuthorDto dto)
    {
        var author = new Author
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            DateOfBirth = dto.DateOfBirth,
            Biography = dto.Biography,
            CreatedAt = DateTime.UtcNow
        };

        author.Id = _db.InsertWithInt32Identity(author);

        var authorDto = new AuthorDto
        {
            Id = author.Id,
            FirstName = author.FirstName,
            LastName = author.LastName,
            FullName = author.FullName,
            DateOfBirth = author.DateOfBirth,
            Biography = author.Biography,
            CreatedAt = author.CreatedAt
        };

        return Task.FromResult<ActionResult<AuthorDto>>(
            CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, authorDto));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthor(int id, UpdateAuthorDto dto)
    {
        var author = await _db.Authors.FirstOrDefaultAsync(a => a.Id == id);
        if (author == null)
        {
            return NotFound();
        }

        if (dto.FirstName != null) author.FirstName = dto.FirstName;
        if (dto.LastName != null) author.LastName = dto.LastName;
        if (dto.DateOfBirth.HasValue) author.DateOfBirth = dto.DateOfBirth;
        if (dto.Biography != null) author.Biography = dto.Biography;

        author.UpdatedAt = DateTime.UtcNow;

        _db.Update(author);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        var author = await _db.Authors.FirstOrDefaultAsync(a => a.Id == id);
        if (author == null)
        {
            return NotFound();
        }

        // Check if author has books
        var hasBooks = await _db.Books.AnyAsync(b => b.AuthorId == id);
        if (hasBooks)
        {
            return BadRequest("Cannot delete author with associated books.");
        }

        _db.Delete(author);

        return NoContent();
    }
}

