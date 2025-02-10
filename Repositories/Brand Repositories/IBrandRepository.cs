using Creators_Corner_App_API.DTOs;
using Creators_Corner_App_API.Models;

namespace Creators_Corner_App_API.Repositories.Brand_Repositories
{
    public interface IBrandRepository
    {
        Task<Brand> LoginAsync(string username, string password);
        Task FillApplicationAsync(BrandApplicationDTO applicationDto);
        Task UploadProductAsync(ProductDTO productDto);
        Task<List<Product>> GetProductsByBrandAsync(string brandUsername);
        Task ForgetPasswordAsync(string email);
    }
}
