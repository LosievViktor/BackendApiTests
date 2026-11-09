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

        Assert.That(createResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created),
            "Check that the create response status code is 201 Created.");

        var createdId = createResponse.Content!.Id;

        var getAfterCreate = await Client.GetBookByIdAsync(createdId);

        Assert.That(getAfterCreate.StatusCode, Is.EqualTo(HttpStatusCode.OK),
            "Check that GET of the created book returns 200 OK.");

        Assert.That(getAfterCreate.Content!.Title, Is.EqualTo(createRequest.Title),
            "Check that the title returned after create equals the requested title.");

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
            "Check that the update response status code is 200 OK.");
        
        Assert.That(updateResponse.Content!.PublishedYear, Is.EqualTo(2013),
            "Check that the published year in the update response is 2013.");

        var getAfterUpdate = await Client.GetBookByIdAsync(createdId);
        
        Assert.That(getAfterUpdate.Content!.PublishedYear, Is.EqualTo(2013),
            "Check that the published year returned by GET after update is 2013.");

        var deleteResponse = await Client.DeleteBookAsync(createdId);

        Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent),
            "Check that the delete response status code is 204 No Content.");

        var getAfterDelete = await Client.GetBookByIdAsync(createdId);

        Assert.That(getAfterDelete.StatusCode, Is.EqualTo(HttpStatusCode.NotFound),
            "Check that GET after delete returns 404 Not Found.");
    }
}
