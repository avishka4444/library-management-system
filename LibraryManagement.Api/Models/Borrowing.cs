using LinqToDB.Mapping;

namespace LibraryManagement.Api.Models;

[Table("Borrowings")]
public class Borrowing
{
    [PrimaryKey, Identity]
    public int Id { get; set; }

    [Column, NotNull]
    public int BookId { get; set; }

    [Column, NotNull]
    public int MemberId { get; set; }

    [Column, NotNull]
    public DateTime BorrowedDate { get; set; } = DateTime.UtcNow;

    [Column]
    public DateTime? ReturnedDate { get; set; }

    [Column]
    public DateTime DueDate { get; set; }

    [Column]
    public string? Status { get; set; } = "Borrowed"; // Borrowed, Returned, Overdue

    [Column]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column]
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    [Association(ThisKey = nameof(BookId), OtherKey = nameof(Book.Id), CanBeNull = false)]
    public Book Book { get; set; } = null!;

    [Association(ThisKey = nameof(MemberId), OtherKey = nameof(Member.Id), CanBeNull = false)]
    public Member Member { get; set; } = null!;
}


