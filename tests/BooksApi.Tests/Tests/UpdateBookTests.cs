using System.Net;
using BooksApi.Tests.Contracts;
using BooksApi.Tests.Infrastructure;
using NUnit.Framework;

namespace BooksApi.Tests.Tests;

[TestFixture]
public class UpdateBookTests : BooksApiTestBase
{
    [Test]
    public async Task UpdateBook_ExistingId_ReturnsOkWithUpdatedBook()
    {
        var request = new UpsertBookRequestDto
        {
            Title = "Clean Code (2nd Edition)",
            Author = "Robert C. Martin",
            Genre = "Software Engineering",
            PublishedYear = 2020,
            Isbn = "978-0132350884"
        };

        var response = await Client.UpdateBookAsync(1, request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK),
            "Check that the response status code is 200 OK.");
        
        Assert.That(response.Content, Is.Not.Null,
            "Check that the response content is not null.");
        
        Assert.That(response.Content!.Title, Is.EqualTo(request.Title),
            "Check that the book title matches the requested title.");
        
        Assert.That(response.Content!.PublishedYear, Is.EqualTo(2020),
            "Check that the book published year is 2020.");

        var getResponse = await Client.GetBookByIdAsync(1);
        
        Assert.That(getResponse.Content!.Title, Is.EqualTo(request.Title),
            "Check that the updated book title is persisted and returned by the API.");
    }

    [Test]
    public async Task UpdateBook_UnknownId_ReturnsNotFound()
    {
        var request = new UpsertBookRequestDto
        {
            Title = "Ghost Book",
            Author = "No One",
            Genre = "Mystery",
            PublishedYear = 2021,
            Isbn = "978-0000000002"
        };

        var response = await Client.UpdateBookAsync(9999, request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound),
            "Check that request with unreal id e will have Not Found 404 Status Code.");
    }

    [Test]
    public async Task UpdateBook_InvalidPayload_ReturnsBadRequest()
    {
        var request = new UpsertBookRequestDto
        {
            Title = "",
            Author = "Someone",
            Genre = "Fiction",
            PublishedYear = 2020,
            Isbn = "978-0000000003"
        };

        var response = await Client.UpdateBookAsync(1, request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest),
            "Check that update request with null tittle will have Bad Request 400 Status Code.");
    }
}
