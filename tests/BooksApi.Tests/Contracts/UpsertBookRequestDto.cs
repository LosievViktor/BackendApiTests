namespace BooksApi.Tests.Contracts;

/// <summary>Test-side representation of the create/update request body.</summary>
public class UpsertBookRequestDto
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public int PublishedYear { get; set; }
    public string Isbn { get; set; } = string.Empty;
}
