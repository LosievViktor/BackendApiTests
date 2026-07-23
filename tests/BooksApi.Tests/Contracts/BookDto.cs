namespace BooksApi.Tests.Contracts;

/// <summary>
/// Test-side representation of the JSON the API returns for a book. Kept
/// independent from BooksApi's own <c>Book</c> model on purpose: these tests
/// exercise the API as a black box, over HTTP, the same way any real client would.
/// </summary>
public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public int PublishedYear { get; set; }
    public string Isbn { get; set; } = string.Empty;
}
