using System.Net;
using BooksApi.Tests.Contracts;
using BooksApi.Tests.Infrastructure;
using NUnit.Framework;

namespace BooksApi.Tests.Tests;

[TestFixture]
public class CreateBookTests : BooksApiTestBase
{
    [Test]
    public async Task CreateBook_ValidPayload_ReturnsCreatedWithBook()
    {
        var request = new UpsertBookRequestDto
        {
            Title = "Refactoring",
            Author = "Martin Fowler",
            Genre = "Software Engineering",
            PublishedYear = 1999,
            Isbn = "978-0201485677"
        };

        var response = await Client.CreateBookAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created),
            "Check that Status Code is Created 201.");
        
        Assert.That(response.Content, Is.Not.Null,
            "Check that response is not Null.");
        
        Assert.That(response.Content!.Id, Is.GreaterThan(0),
            "Check that Id is greater than zero.");
        
        Assert.That(response.Content!.Title, Is.EqualTo(request.Title),
            "Check  that  request Title is equal to response Title.");
        
        Assert.That(response.Content!.Isbn, Is.EqualTo(request.Isbn),
            "Check that request ISBN is equal to response ISBN.");
        
        Assert.That(response.Headers.Location, Is.Not.Null,
            "Check that headers Location is not null.");
    }

    [Test]
    public async Task CreateBook_MissingTitle_ReturnsBadRequest()
    {
        var request = new UpsertBookRequestDto
        {
            Title = "",
            Author = "Someone",
            Genre = "Fiction",
            PublishedYear = 2020,
            Isbn = "978-0000000000"
        };

        var response = await Client.CreateBookAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest),
            "Check that server can handle request without Title with BadRequest 400 status code.");
    }

    [Test]
    public async Task CreateBook_InvalidPublishedYear_ReturnsBadRequest()
    {
        var request = new UpsertBookRequestDto
        {
            Title = "A Book From The Future",
            Author = "Someone",
            Genre = "Fiction",
            PublishedYear = -5,
            Isbn = "978-0000000001"
        };

        var response = await Client.CreateBookAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest),
            "Check that server can handle request with negative Published Year with BadRequest 400 status code.");
    }

    [Test]
    public async Task CreateBook_AddsBookThatIsThenRetrievable()
    {
        var request = new UpsertBookRequestDto
        {
            Title = "Domain-Driven Design",
            Author = "Eric Evans",
            Genre = "Software Engineering",
            PublishedYear = 2003,
            Isbn = "978-0321125217"
        };

        var createResponse = await Client.CreateBookAsync(request);
        var newId = createResponse.Content!.Id;

        var getResponse = await Client.GetBookByIdAsync(newId);

        Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK),
            "Check that response for Create book resuqest have 200 OK Status Code.");
        
        Assert.That(getResponse.Content!.Title, Is.EqualTo(request.Title),
            "Check that Title in request is equal to response Title.");
    }
}