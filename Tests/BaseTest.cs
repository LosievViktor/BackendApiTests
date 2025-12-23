using BackendApiTests.Microservices;
using BackendApiTests.Utilities;
using Refit;

namespace BackendApiTests.Tests
{
    [TestFixture]
    public class BaseTest
    {
        public IObjects _api;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var httpClient = new HttpClient( new HttpLoggingHandler
            {
               InnerHandler = new HttpClientHandler()
            })
            {
                BaseAddress = new System.Uri(TestContext.Parameters["Environment"])
            };

            _api = RestService.For<IObjects>(httpClient);
          
        }
    }
}
