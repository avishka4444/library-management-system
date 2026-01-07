using LinqToDB.Mapping;

namespace LibraryManagement.Api.Models;

[Table("Members")]
public class Member
{
    [PrimaryKey, Identity]
    public int Id { get; set; }

    [Column, NotNull]
    public string FirstName { get; set; } = string.Empty;

    [Column, NotNull]
    public string LastName { get; set; } = string.Empty;

    [Column, NotNull]
    public string Email { get; set; } = string.Empty;

    [Column]
    public string? PhoneNumber { get; set; }

    [Column]
    public string? Address { get; set; }

    [Column]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column]
    public DateTime? UpdatedAt { get; set; }

    public string FullName => $"{FirstName} {LastName}";
}


