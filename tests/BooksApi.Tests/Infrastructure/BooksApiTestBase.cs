using System.Text.Json;
using BooksApi.Tests.Api;
using NUnit.Framework;
using Refit;

namespace BooksApi.Tests.Infrastructure;

/// <summary>
/// Base fixture shared by every test class. A brand new
/// <see cref="CustomWebApplicationFactory"/> (and therefore a brand new
/// in-memory book catalog, reseeded from scratch) is created before each
/// individual test and torn down right after, so tests never leak state
/// into one another regardless of execution order.
/// </summary>
[TestFixture]
public abstract class BooksApiTestBase
{
    private CustomWebApplicationFactory _factory = null!;
    protected IBooksApiClient Client { get; private set; } = null!;

    [SetUp]
    public void BaseSetUp()
    {
        _factory = new CustomWebApplicationFactory();
        var httpClient = _factory.CreateClient();

        // ASP.NET Core serializes with camelCase JSON by default; make sure the
        // Refit client deserializes the same way instead of relying on exact case.
        var refitSettings = new RefitSettings(
            new SystemTextJsonContentSerializer(
                new JsonSerializerOptions(JsonSerializerDefaults.Web)));

        Client = RestService.For<IBooksApiClient>(httpClient, refitSettings);
    }

    [TearDown]
    public void BaseTearDown()
    {
        _factory.Dispose();
    }
}
