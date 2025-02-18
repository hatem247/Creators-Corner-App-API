using Creators_Corner_App_API.Data;
using Creators_Corner_App_API.DTOs;
using Creators_Corner_App_API.Models;
using Creators_Corner_App_API.Repositories.Customer_Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Creators_Corner_App_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            try
            {
                var customer = await _customerRepository.LoginAsync(loginDto);
                return Ok(Response<Customer>.Success("Login successful", customer));
            }
            catch (Exception ex)
            {
                return BadRequest(Response<object>.Fail(ex.Message));
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CustomerDTO customerDto)
        {
            try
            {
                await _customerRepository.RegisterAsync(customerDto);
                return Ok(Response<object>.Success("Customer registered successfully", null));
            }
            catch (Exception ex)
            {
                return BadRequest(Response<object>.Fail(ex.Message));
            }
        }

        [HttpPost("buy-product")]
        public async Task<IActionResult> BuyProduct([FromQuery] int productId, [FromQuery] int customerId)
        {
            try
            {
                await _customerRepository.AddProductToCartAsync(productId, customerId);
                return Ok(Response<object>.Success("Product purchased successfully", null));
            }
            catch (Exception ex)
            {
                return BadRequest(Response<object>.Fail(ex.Message));
            }
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _customerRepository.GetAllProductsAsync();
                return Ok(Response<List<Product>>.Success("Products retrieved successfully", products));
            }
            catch (Exception ex)
            {
                return BadRequest(Response<object>.Fail(ex.Message));
            }
        }

        [HttpGet("compare-products")]
        public async Task<IActionResult> CompareProducts([FromQuery] int productId1, [FromQuery] int productId2)
        {
            try
            {
                var comparison = await _customerRepository.CompareProductsAsync(productId1, productId2);
                return Ok(Response<string>.Success("Products compared successfully", comparison));
            }
            catch (Exception ex)
            {
                return BadRequest(Response<object>.Fail(ex.Message));
            }
        }

        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordDTO forgetPasswordDto)
        {
            try
            {
                await _customerRepository.ForgetPasswordAsync(forgetPasswordDto);
                return Ok(Response<object>.Success("Temporary password sent to your email", null));
            }
            catch (Exception ex)
            {
                return BadRequest(Response<object>.Fail(ex.Message));
            }
        }

        [HttpGet("cart")]
        public async Task<IActionResult> GetCustomerCart([FromQuery] int customerId)
        {
            try
            {
                var cart = await _customerRepository.GetCustomerCartAsync(customerId);
                return Ok(Response<Cart>.Success("Cart retrieved successfully", cart));
            }
            catch (Exception ex)
            {
                return BadRequest(Response<object>.Fail(ex.Message));
            }
        }

        [HttpPost("add-to-cart")]
        public async Task<IActionResult> AddProductToCart([FromQuery] int productId, [FromQuery] int customerId)
        {
            try
            {
                await _customerRepository.AddProductToCartAsync(productId, customerId);
                return Ok(Response<object>.Success("Product added to cart successfully", productId));
            }
            catch (Exception ex)
            {
                return BadRequest(Response<object>.Fail(ex.Message));
            }
        }
    }
}
