using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Api.Data;
using LibraryManagement.Api.Models;
using LibraryManagement.Api.DTOs;
using LinqToDB;
using LibraryManagement.Api.Controllers;

namespace LibraryManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly LibraryDbContext _db;

    public MembersController(LibraryDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetMembers()
    {
        var members = await _db.Members.ToListAsync();

        var memberDtos = members.Select(m => new MemberDto
        {
            Id = m.Id,
            FirstName = m.FirstName,
            LastName = m.LastName,
            FullName = m.FullName,
            Email = m.Email,
            PhoneNumber = m.PhoneNumber,
            Address = m.Address,
            CreatedAt = m.CreatedAt
        }).ToList();

        return Ok(memberDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MemberDto>> GetMember(int id)
    {
        var member = await _db.Members.FirstOrDefaultAsync(m => m.Id == id);
        if (member == null)
        {
            return NotFound();
        }

        var memberDto = new MemberDto
        {
            Id = member.Id,
            FirstName = member.FirstName,
            LastName = member.LastName,
            FullName = member.FullName,
            Email = member.Email,
            PhoneNumber = member.PhoneNumber,
            Address = member.Address,
            CreatedAt = member.CreatedAt
        };

        return Ok(memberDto);
    }

    [HttpPost]
    public async Task<ActionResult<MemberDto>> CreateMember(CreateMemberDto dto)
    {
        // Check if email already exists
        var existingMember = await _db.Members.FirstOrDefaultAsync(m => m.Email == dto.Email);
        if (existingMember != null)
        {
            return BadRequest("A member with this email already exists.");
        }

        var member = new Member
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            Address = dto.Address,
            CreatedAt = DateTime.UtcNow
        };

        member.Id = _db.InsertWithInt32Identity(member);

        var memberDto = new MemberDto
        {
            Id = member.Id,
            FirstName = member.FirstName,
            LastName = member.LastName,
            FullName = member.FullName,
            Email = member.Email,
            PhoneNumber = member.PhoneNumber,
            Address = member.Address,
            CreatedAt = member.CreatedAt
        };

        return CreatedAtAction(nameof(GetMember), new { id = member.Id }, memberDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMember(int id, UpdateMemberDto dto)
    {
        var member = await _db.Members.FirstOrDefaultAsync(m => m.Id == id);
        if (member == null)
        {
            return NotFound();
        }

        // Check if email is being changed and if it already exists
        if (dto.Email != null && dto.Email != member.Email)
        {
            var existingMember = await _db.Members.FirstOrDefaultAsync(m => m.Email == dto.Email);
            if (existingMember != null)
            {
                return BadRequest("A member with this email already exists.");
            }
        }

        if (dto.FirstName != null) member.FirstName = dto.FirstName;
        if (dto.LastName != null) member.LastName = dto.LastName;
        if (dto.Email != null) member.Email = dto.Email;
        if (dto.PhoneNumber != null) member.PhoneNumber = dto.PhoneNumber;
        if (dto.Address != null) member.Address = dto.Address;

        member.UpdatedAt = DateTime.UtcNow;

        _db.Update(member);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMember(int id)
    {
        var member = await _db.Members.FirstOrDefaultAsync(m => m.Id == id);
        if (member == null)
        {
            return NotFound();
        }

        // Check if member has active borrowings
        var activeBorrowings = await _db.Borrowings
            .Where(br => br.MemberId == id && br.Status == "Borrowed")
            .AnyAsync();

        if (activeBorrowings)
        {
            return BadRequest("Cannot delete member with active borrowings.");
        }

        _db.Delete(member);

        return NoContent();
    }
}

