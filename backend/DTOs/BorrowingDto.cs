namespace LibraryManagement.Api.DTOs;

public class BorrowingDto
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public string BookTitle { get; set; } = string.Empty;
    public int MemberId { get; set; }
    public string MemberName { get; set; } = string.Empty;
    public DateTime BorrowedDate { get; set; }
    public DateTime? ReturnedDate { get; set; }
    public DateTime DueDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class CreateBorrowingDto
{
    public int BookId { get; set; }
    public int MemberId { get; set; }
    public DateTime DueDate { get; set; }
}

public class ReturnBookDto
{
    public int BorrowingId { get; set; }
}


