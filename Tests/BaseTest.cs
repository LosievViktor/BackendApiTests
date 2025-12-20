using BackendApiTests.Microservices;
using Refit;

namespace BackendApiTests.Tests
{
    [TestFixture]
    public class BaseTest
    {
        public IObjects _api;
        public string _baseUrl = "https://api.restful-api.dev";

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var settings = new RefitSettings
            {
                ContentSerializer = new SystemTextJsonContentSerializer()
            };

            _api = RestService.For<IObjects>(TestContext.Parameters["Environment"] ?? _baseUrl, settings);
        }
    }
}