using BackendApiTests.Models;
using FluentAssertions;
using FluentAssertions.Execution;
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
                    ["price"] = 11,

                }
            };

            var response = await _api.CreateObject(request);

            _id = response.Content.Id;
        }

        [Test]
        [Description("Check PUT request on /objects api.")]
        public async Task PutObjectTest()
        {

            var objectId = _id;

            var request = new PutObjectRequest
            {
                Name = "Apple Apple Apple Apple",
                Data = new()
                {
                    ["year"] = 2222,
                    ["price"] = 2000,
                }
            };


            var response = await _api.UpdateObject(objectId, request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            using (new AssertionScope())
            {
                response.Content.Id.Should().Be(objectId);
                response.Content.Name.Should().Be(request.Name);
                response.Content.Data.Should().BeEquivalentTo(request.Data);

            }
        }

        [Test]
        [Description("Check PATCH request on /objects api.")]
        public async Task PatchObjectTest()
        {
            var objectId = _id;
            var newName = "Apple (Updated Name)";

            var request = new PatchObjectRequest
            {
                Name = newName
            };


            var response = await _api.PatchObject(objectId, request);

            response.IsSuccessStatusCode.Should().BeTrue();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            using (new AssertionScope())
            {
                response.Content.Id.Should().Be(objectId);
                response.Content.Name.Should().Be(newName);

            }
        }


        [TearDown]
        public async Task TearDown()
        {
            var response1 = await _api.DeleteObject(_id);
        }
    }
}