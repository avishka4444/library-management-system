using LinqToDB.Mapping;

namespace LibraryManagement.Api.Models;

[Table("Authors")]
public class Author
{
    [PrimaryKey, Identity]
    public int Id { get; set; }

    [Column, NotNull]
    public string FirstName { get; set; } = string.Empty;

    [Column, NotNull]
    public string LastName { get; set; } = string.Empty;

    [Column]
    public DateTime? DateOfBirth { get; set; }

    [Column]
    public string? Biography { get; set; }

    [Column]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column]
    public DateTime? UpdatedAt { get; set; }

    public string FullName => $"{FirstName} {LastName}";
}


