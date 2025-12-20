using BackendApiTests.Models;
using System.Net;

namespace BackendApiTests.Tests
{
    [TestFixture]
    public class PostAndDeleteTests : BaseTest
    {
        [Test]
        [Description("Check creating and deleting item on /objects api.")]
        public async Task CreateAndDeleteObjectTest()
        {
            var request = new PostObjectRequest
            {
                Name = "Apple Apple Apple",
                Data = new()
                {
                    ["year"] = 2020,
                    ["price"] = 100
                }
            };

            var createResponse = await _api.CreateObject(request);

            Assert.That(createResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK),
                "Check that response Status Code is 200 OK.");

            var deleteResponse = await _api.DeleteObject(createResponse.Content.Id);

            Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK),
                "Check that response Status Code is 200 OK.");

            Assert.That(deleteResponse.Content.Message.Contains(createResponse.Content.Id), Is.True,
                "Check that delete message should contain deleted object id.");

            Assert.That(deleteResponse.Content.Message.ToLower().Contains("deleted"), Is.True,
                "Check that delete message should confirm deletion.");
        }
    }
}