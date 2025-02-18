using Creators_Corner_App_API.DTOs;
using Creators_Corner_App_API.Models;

namespace Creators_Corner_App_API.Repositories.Customer_Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> LoginAsync(LoginDTO loginDTO);
        Task RegisterAsync(CustomerDTO customerDto);
        Task CheckoutAsync(int customerId);
        Task<List<Product>> GetAllProductsAsync();
        Task<string> CompareProductsAsync(int productId1, int productId2);
        Task ForgetPasswordAsync(ForgetPasswordDTO forgetPasswordDTO);
        Task<Cart> GetCustomerCartAsync(int customerId);
        Task AddProductToCartAsync(int productId, int customerId);
    }
}
