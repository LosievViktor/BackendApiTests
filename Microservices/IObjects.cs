using Refit;
using BackendApiTests.Models;

namespace BackendApiTests.Microservices
{
    public interface IObjects
    {
        [Get("/objects")]
        Task<ApiResponse<List<GetObjectResponse>>> GetObjects();

        [Get("/objects/{id}")]
        Task<ApiResponse<GetObjectResponse>> GetObjectById(string id);

        [Get("/objects")]
        Task<ApiResponse<List<GetObjectResponse>>> GetObjectsByIds([Query(CollectionFormat.Multi)] IEnumerable<string> id);

        [Post("/objects")]
        Task<ApiResponse<PostObjectResponse>> CreateObject([Body] PostObjectRequest request);

        [Put("/objects/{id}")]
        Task<ApiResponse<PutObjectResponse>> UpdateObject(string id,[Body] PutObjectRequest request);

        [Patch("/objects/{id}")]
        Task<ApiResponse<PatchObjectResponse>> PatchObject(string id, [Body] PatchObjectRequest request);

        [Delete("/objects/{id}")]
        Task<ApiResponse<DeleteObjectResponse>> DeleteObject(string id);

    }
}