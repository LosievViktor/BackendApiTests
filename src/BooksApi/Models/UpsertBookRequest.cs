using System.ComponentModel.DataAnnotations;

namespace BooksApi.Models;

/// <summary>
/// The payload accepted by the create (POST) and replace (PUT) endpoints.
/// Deliberately separate from <see cref="Book"/> so clients never set the Id themselves.
/// </summary>
public class UpsertBookRequest
{
    [Required(ErrorMessage = "Title is required.")]
    [MinLength(1)]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Author is required.")]
    [MinLength(1)]
    [MaxLength(200)]
    public string Author { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Genre { get; set; } = string.Empty;

    [Range(1, 2100, ErrorMessage = "PublishedYear must be a realistic year.")]
    public int PublishedYear { get; set; }

    [Required(ErrorMessage = "Isbn is required.")]
    [RegularExpression(@"^[0-9\-Xx]{10,17}$", ErrorMessage = "Isbn does not look like a valid ISBN-10/13.")]
    public string Isbn { get; set; } = string.Empty;
}
