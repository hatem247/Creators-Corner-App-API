using Creators_Corner_App_API.DTOs;
using Creators_Corner_App_API.Models;

namespace Creators_Corner_App_API.Repositories.Brand_Repositories
{
    public interface IBrandRepository
    {
        Task<Brand> LoginAsync(LoginDTO loginDTO);
        Task<int> FillApplicationAsync(BrandApplicationDTO applicationDto);
        Task UploadProductAsync(ProductDTO productDto);
        Task<List<Product>> GetProductsByBrandAsync(int brandId);
        Task ForgetPasswordAsync(ForgetPasswordDTO forgetPasswordDTO);
        Task UpdateData(BrandDTO brandDTO, int brandId);
        Task<BrandApplication> GetApplicationStatus(string brandEmail);
        Task UpdateApplicationStatus(int applicationId, bool status);
        Task<List<Product>> GetOrdersByBrandAsync(int brandId);
        Task UpdateLoginStatus(int brandId);
    }
}
