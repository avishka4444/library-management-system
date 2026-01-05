using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Api.DTOs;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public int? AuthorId { get; set; }
    public string? AuthorName { get; set; }
    public DateTime? PublishedDate { get; set; }
    public int TotalCopies { get; set; }
    public int AvailableCopies { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateBookDto
{
    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 200 characters")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "ISBN is required")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "ISBN must be between 1 and 50 characters")]
    public string ISBN { get; set; } = string.Empty;

    public int? AuthorId { get; set; }

    public DateTime? PublishedDate { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Total copies must be at least 1")]
    public int TotalCopies { get; set; }
}

public class UpdateBookDto
{
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 200 characters")]
    public string? Title { get; set; }

    [StringLength(50, MinimumLength = 1, ErrorMessage = "ISBN must be between 1 and 50 characters")]
    public string? ISBN { get; set; }

    public int? AuthorId { get; set; }

    public DateTime? PublishedDate { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Total copies must be at least 1")]
    public int? TotalCopies { get; set; }
}


