using BooksApi.Tests.Contracts;
using Refit;

namespace BooksApi.Tests.Api;

/// <summary>
/// Refit-generated typed client for the Books API. The interface shape mirrors
/// BooksController's routes exactly; Refit builds the HttpClient calls for us
/// from these attributes at runtime.
/// </summary>
public interface IBooksApiClient
{
    [Get("/api/books")]
    Task<ApiResponse<List<BookDto>>> GetAllBooksAsync();

    [Get("/api/books/{id}")]
    Task<ApiResponse<BookDto>> GetBookByIdAsync(int id);

    [Post("/api/books")]
    Task<ApiResponse<BookDto>> CreateBookAsync([Body] UpsertBookRequestDto book);

    [Put("/api/books/{id}")]
    Task<ApiResponse<BookDto>> UpdateBookAsync(int id, [Body] UpsertBookRequestDto book);

    [Delete("/api/books/{id}")]
    Task<IApiResponse> DeleteBookAsync(int id);
}
