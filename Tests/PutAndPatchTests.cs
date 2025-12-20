using BackendApiTests.Models;
using System.Net;

namespace BackendApiTests.Tests
{
    [TestFixture]
    public class PutAndPatchTests : BaseTest
    {
        private string? _id;

        [SetUp]
        public async Task Setup()
        {
            var request = new PostObjectRequest
            {
                Name = "Apple Apple",
                Data = new()
                {
                    ["year"] = 2022,
                    ["price"] = 11
                }
            };

            var response = await _api.CreateObject(request);

            _id = response.Content.Id;
        }

        [Test]
        [Description("Check PUT request on /objects api.")]
        public async Task PutObjectTest()
        {
            var request = new PutObjectRequest
            {
                Name = "Apple Apple Apple Apple",
                Data = new()
                {
                    ["year"] = 2222,
                    ["price"] = 2000
                }
            };

            var response = await _api.UpdateObject(_id, request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK),
                "Check that response Status Code is 200 OK.");

            Assert.That(response.Content.Id, Is.EqualTo(_id),
                "Check that response Id is correct.");

            Assert.That(response.Content.Name, Is.EqualTo(request.Name),
                "Check that response Name is correct.");

            Assert.That(response.Content.Data["year"], Is.EqualTo(2222),
                 "Response year data is correct.");

            Assert.That(response.Content.Data["price"], Is.EqualTo(2000),
                 "Response price data is correct.");

        }

        [Test]
        [Description("Check PATCH request on /objects api.")]
        public async Task PatchObjectTest()
        {
            var newName = "Apple (Updated Name)";

            var request = new PatchObjectRequest
            {
                Name = newName
            };

            var response = await _api.PatchObject(_id, request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK),
                "Check that response Status Code is 200 OK.");

            Assert.That(response.Content.Id, Is.EqualTo(_id),
                "Check that response Id is correct.");

            Assert.That(response.Content.Name, Is.EqualTo(newName),
                "Check that response Name is correct.");
        }

        [TearDown]
        public async Task TearDown()
        {
            await _api.DeleteObject(_id);
        }
    }
}