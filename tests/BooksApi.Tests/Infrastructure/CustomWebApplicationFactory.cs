using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BooksApi.Tests.Infrastructure;

/// <summary>
/// Boots the real BooksApi application in-process (via TestServer) so the
/// tests hit real ASP.NET Core routing, model binding, and validation —
/// without needing a separately running server, a port, or any network hop.
/// </summary>
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
    }
}
