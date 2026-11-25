namespace Library;

public class Book
{
    public int Id { get; }
    public string Title { get; }
    public string Author { get; }
    public string? BorrowedBy { get; private set; }
    public int? DueDay { get; private set; }
    public Waitlist Waitlist { get; private set; } = new();

    public Book(int id, string title, string author)
    {
        Id = id;
        Title = title;
        Author = author;
    }
}
