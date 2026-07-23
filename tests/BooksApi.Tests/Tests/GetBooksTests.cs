using System.Net;
using BooksApi.Tests.Infrastructure;
using NUnit.Framework;

namespace BooksApi.Tests.Tests;

[TestFixture]
public class GetBooksTests : BooksApiTestBase
{
    [Test]
    public async Task GetAllBooks_ReturnsOk_WithSeededBooks()
    {
        var response = await Client.GetAllBooksAsync();

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK),
            "Check that the response status code is 200 OK.");
        
        Assert.That(response.Content, Is.Not.Null,
            "Check that the response content is not null.");
        
        Assert.That(response.Content!.Count, Is.GreaterThanOrEqualTo(3),
            "Check that the response contains at least 3 books.");
        
        Assert.That(response.Content!.Select(b => b.Title), Does.Contain("Dune"),
            "Check that the response contains a book with the title 'Dune'.");
    }

    [Test]
    public async Task GetBookById_ExistingId_ReturnsOkWithBook()
    {
        var response = await Client.GetBookByIdAsync(1);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK),
            "Check that the response status code is 200 OK.");
        
        Assert.That(response.Content, Is.Not.Null,
            "Check that the response content is not null.");
        
        Assert.That(response.Content!.Id, Is.EqualTo(1),
            "Check that the book ID is 1.");
        
        Assert.That(response.Content!.Title, Is.EqualTo("Clean Code"),
            "Check that the book title is 'Clean Code'.");
        
        Assert.That(response.Content!.Author, Is.EqualTo("Robert C. Martin"),
            "Check that the book author is 'Robert C. Martin'.");
    }

    [Test]
    public async Task GetBookById_UnknownId_ReturnsNotFound()
    {
        var response = await Client.GetBookByIdAsync(9999);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound),
            "Check that for request with unreal book id, response will be with NotFound 404 Status Code.");
    }
}
