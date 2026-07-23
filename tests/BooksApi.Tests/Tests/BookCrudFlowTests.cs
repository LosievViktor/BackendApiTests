using System.Net;
using BooksApi.Tests.Contracts;
using BooksApi.Tests.Infrastructure;
using NUnit.Framework;

namespace BooksApi.Tests.Tests;

[TestFixture]
public class BookCrudFlowTests : BooksApiTestBase
{
    [Test]
    public async Task FullCrudLifecycle_CreateReadUpdateDelete_BehavesAsExpected()
    {
        var createRequest = new UpsertBookRequestDto
        {
            Title = "Working Effectively with Legacy Code",
            Author = "Michael Feathers",
            Genre = "Software Engineering",
            PublishedYear = 2004,
            Isbn = "978-0131177055"
        };

        var createResponse = await Client.CreateBookAsync(createRequest);
        Assert.That(createResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        var createdId = createResponse.Content!.Id;

        var getAfterCreate = await Client.GetBookByIdAsync(createdId);
        Assert.That(getAfterCreate.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(getAfterCreate.Content!.Title, Is.EqualTo(createRequest.Title));

        var updateRequest = new UpsertBookRequestDto
        {
            Title = createRequest.Title,
            Author = createRequest.Author,
            Genre = createRequest.Genre,
            PublishedYear = 2013,
            Isbn = createRequest.Isbn
        };
        var updateResponse = await Client.UpdateBookAsync(createdId, updateRequest);
        Assert.That(updateResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK),
            "Check that Status Code is 200 OK.");
        
        Assert.That(updateResponse.Content!.PublishedYear, Is.EqualTo(2013),
            "Check that Published Year is 2013 in Update response.");

        var getAfterUpdate = await Client.GetBookByIdAsync(createdId);
        
        Assert.That(getAfterUpdate.Content!.PublishedYear, Is.EqualTo(2013),
            "Check that Published Year is 2013 in Get response after update.");

        var deleteResponse = await Client.DeleteBookAsync(createdId);
        Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent),
            "Check that Status Code is 204 NoContent after delete request.");

        var getAfterDelete = await Client.GetBookByIdAsync(createdId);
        Assert.That(getAfterDelete.StatusCode, Is.EqualTo(HttpStatusCode.NotFound),
            "Check that Status Code is 404 NotFound in Get response after delete.");
    }
}
