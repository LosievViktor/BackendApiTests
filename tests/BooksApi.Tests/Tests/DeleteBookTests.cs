using System.Net;
using BooksApi.Tests.Infrastructure;
using NUnit.Framework;

namespace BooksApi.Tests.Tests;

[TestFixture]
public class DeleteBookTests : BooksApiTestBase
{
    [Test]
    public async Task DeleteBook_ExistingId_ReturnsNoContentThenNotFoundOnGet()
    {
        var deleteResponse = await Client.DeleteBookAsync(2);
        Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent),
            "Check that Delete response status is NoContent 204.");

        var getResponse = await Client.GetBookByIdAsync(2);
        Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound),
            "Check that Get response status for deleted item is Not Found 404.");
    }

    [Test]
    public async Task DeleteBook_UnknownId_ReturnsNotFound()
    {
        var response = await Client.DeleteBookAsync(9999);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound),
            "Check that deleting a book with a non-existent id returns 404 Not Found.");
    }

    [Test]
    public async Task DeleteBook_DoesNotAffectOtherBooks()
    {
        await Client.DeleteBookAsync(2);

        var remaining = await Client.GetAllBooksAsync();

        var ids = remaining.Content!.Select(b => b.Id);

        Assert.That(
            ids,
            Is.EquivalentTo(new[] { 1, 3 }),
            "Check that the remaining book ids are exactly 1 and 3.");
    }
}
