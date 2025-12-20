using BackendApiTests.Models;
using FluentAssertions;
using FluentAssertions.Execution;
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
                    ["price"] = 100,

                }
            };

            var response = await _api.CreateObject(request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Should().NotBeNull();
            response.Content.Id.Should().NotBeNullOrEmpty();

            response.Content.Name.Should().Be(request.Name);
            response.Content.Data.Should().BeEquivalentTo(request.Data);
            
            var response1 = await _api.DeleteObject(response.Content.Id);

            response1.IsSuccessStatusCode.Should().BeTrue();
            response1.StatusCode.Should().Be(HttpStatusCode.OK);

            using (new AssertionScope())
            {
                response1.Content.Should().NotBeNull();
                response1.Content.Message.Should()
                    .Contain(response.Content.Id)
                    .And.Contain("deleted");
            }
        }
    }
}