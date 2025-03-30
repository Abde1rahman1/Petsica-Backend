using Petsica.Shared.Contracts.Marketplace.Request;
using Petsica.Shared.Contracts.Marketplace.Response;

namespace Petsica.Service.Abstractions.Marketplace
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync();
        Task<CategoryResponse> GetCategoryByIdAsync(int id);
        Task<CategoryResponse> CreateCategoryAsync(CategoryRequest request);
        Task<CategoryResponse> UpdateCategoryAsync(int id, CategoryRequest request);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
