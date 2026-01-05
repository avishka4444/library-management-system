using LinqToDB.Mapping;

namespace LibraryManagement.Api.Models;

[Table("Books")]
public class Book
{
    [PrimaryKey, Identity]
    public int Id { get; set; }

    [Column, NotNull]
    public string Title { get; set; } = string.Empty;

    [Column, NotNull]
    public string ISBN { get; set; } = string.Empty;

    [Column]
    public int? AuthorId { get; set; }

    [Column]
    public DateTime? PublishedDate { get; set; }

    [Column]
    public int TotalCopies { get; set; }

    [Column]
    public int AvailableCopies { get; set; }

    [Column]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column]
    public DateTime? UpdatedAt { get; set; }

    // Navigation property
    [Association(ThisKey = nameof(AuthorId), OtherKey = nameof(Author.Id), CanBeNull = true)]
    public Author? Author { get; set; }
}


