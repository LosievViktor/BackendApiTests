using System.Diagnostics;
using System.Text;

namespace BackendApiTests.Utilities
{
    public class HttpLoggingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();

            TestContext.Progress.WriteLine("----- HTTP REQUEST -----");
            TestContext.Progress.WriteLine($"{request.Method} {request.RequestUri}");

            if (request.Content != null)
            {
                var requestBody = await request.Content.ReadAsStringAsync();
                TestContext.Progress.WriteLine($"Request Body: {requestBody}");
            }

            var response = await base.SendAsync(request, cancellationToken);

            stopwatch.Stop();

            TestContext.Progress.WriteLine("----- HTTP RESPONSE -----");
            TestContext.Progress.WriteLine($"Status: {(int)response.StatusCode} {response.StatusCode}");
            TestContext.Progress.WriteLine($"Duration: {stopwatch.ElapsedMilliseconds} ms");

            if (response.Content != null)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                TestContext.Progress.WriteLine($"Response Body: {responseBody}");

                response.Content = new StringContent(
                    responseBody,
                    Encoding.UTF8,
                    response.Content.Headers.ContentType?.MediaType);
            }

            TestContext.Progress.WriteLine("------------------------");

            return response;
        }
    }
}