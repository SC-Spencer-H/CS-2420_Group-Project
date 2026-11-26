using Library;
namespace Tests;

public static class BookCatalogTests
{
    [Fact]
    public static void AddBook()
    {
        List<Book> inOrder = new()
        {
            new Book(1012, "Holes", "Louis Sachar"),
            new Book(1045, "Charlotte's Web", "E. B. White"),
            new Book(1101, "Wonder", "R. J. Palacio"),
        };

        BookCatalog.AddBook(new Book(1012, "Holes", "Louis Sachar"));
        BookCatalog.AddBook(new Book(1045, "Charlotte's Web", "E. B. White"));
        BookCatalog.AddBook(new Book(1101, "Wonder", "R. J. Palacio"));

        List<Book> fullCatalog = BookCatalog.GetFullCatalog();
        Assert.Equal(inOrder.Count, fullCatalog.Count);
        // Assert.Equal(inOrder, fullCatalog);

        for (int i = 0; i < inOrder.Count; i++)
        {
            Assert.Equal(inOrder[i].Id, fullCatalog[i].Id);
        }
    }
}