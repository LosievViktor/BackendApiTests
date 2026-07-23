namespace BooksApi.Models;

/// <summary>
/// A book stored in the catalog. This is the shape returned by every endpoint.
/// </summary>
public class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public string Genre { get; set; } = string.Empty;

    public int PublishedYear { get; set; }

    public string Isbn { get; set; } = string.Empty;
}
