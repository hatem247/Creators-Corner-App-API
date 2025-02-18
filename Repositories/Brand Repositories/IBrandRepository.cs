using Creators_Corner_App_API.DTOs;
using Creators_Corner_App_API.Models;

namespace Creators_Corner_App_API.Repositories.Brand_Repositories
{
    public interface IBrandRepository
    {
        Task<Brand> LoginAsync(LoginDTO loginDTO);
        Task FillApplicationAsync(BrandApplicationDTO applicationDto);
        Task UploadProductAsync(ProductDTO productDto);
        Task<List<ProductDTO>> GetProductsByBrandAsync(int brandId);
        Task ForgetPasswordAsync(ForgetPasswordDTO forgetPasswordDTO);
    }
}
