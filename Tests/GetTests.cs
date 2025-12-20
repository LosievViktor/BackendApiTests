using FluentAssertions;
using System.Net;

namespace BackendApiTests.Tests
{
    [TestFixture]
    public class GetTests : BaseTest
    {
        [Test]
        [Description("Check that /objects api returns list of objects.")]
        public async Task GetAllObjectsTest()
        {
            var response = await _api.GetObjects();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Should().NotBeNull();

            response.Content.Should().NotBeEmpty();
            response.Content.Should().OnlyContain(x => !string.IsNullOrWhiteSpace(x.Id) && !string.IsNullOrWhiteSpace(x.Name));
        }

        [Test]
        [Description("Check that /objects api returns list objects by ids.")]
        public async Task GetObjectsByIdsTest()
        {
            var ids = new[] { "3", "5", "10" };

            var response = await _api.GetObjectsByIds(ids);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Should().NotBeNull();

            response.Content.Should().NotBeEmpty();
            response.Content.Should().OnlyContain(x => !string.IsNullOrWhiteSpace(x.Id) && !string.IsNullOrWhiteSpace(x.Name));

            response.Content.Select(x => x.Id).Should().BeEquivalentTo(ids);
        }


        [Test]
        [Description("Check that /objects api returns object by id.")]
        public async Task GetObjectsByIdTest()
        {
            var id = "3"; ;

            var response = await _api.GetObjectById(id);

            response.IsSuccessStatusCode.Should().BeTrue();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Id.Should().Be(id);
        }

        [Test]
        [Description("Check that /objects api returns 404 error for blablabla id.")]
        public async Task Return404Test()
        {
            var response = await _api.GetObjectById("blablalba");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}