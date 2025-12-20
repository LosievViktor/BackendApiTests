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

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK),
                "Check that response Status Code is 200 OK.");

            var result = response.Content.All(x => !string.IsNullOrWhiteSpace(x.Id) && !string.IsNullOrWhiteSpace(x.Name));

            Assert.That(result, Is.True,
                "Check that all objects should have non-empty Id and Name.");
        }

        [Test]
        [Description("Check that /objects api returns list objects by ids.")]
        public async Task GetObjectsByIdsTest()
        {
            var ids = new[] { "3", "5", "10" };

            var response = await _api.GetObjectsByIds(ids);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK),
                "Check that response Status Code is 200 OK.");

            var result = response.Content.All(x => !string.IsNullOrWhiteSpace(x.Id) && !string.IsNullOrWhiteSpace(x.Name));

            Assert.That(result, Is.True,
                "Check that all objects should have non-empty Id and Name.");

            var returnedIds = response.Content.Select(x => x.Id).ToArray();

            Assert.That(returnedIds, Is.EquivalentTo(ids),
                "Check that response contain items with id 3, 5, 10.");
        }

        [Test]
        [Description("Check that /objects api returns object by id.")]
        public async Task GetObjectsByIdTest()
        {
            var id = "3";

            var response = await _api.GetObjectById(id);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK),
                "Check that responce Status Code is 200 OK.");

            Assert.That(response.Content.Id, Is.EqualTo(id),
                $"Check that response contain item with id {id}.");
        }

        [Test]
        [Description("Check that /objects api returns 404 error for blablabla id.")]
        public async Task Return404Test()
        {
            var response = await _api.GetObjectById("blablalba");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound),
                "Check that responce Status Code is 404 NotFound.");
        }
    }
}