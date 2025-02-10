using Creators_Corner_App_API.Data;
using Creators_Corner_App_API.DTOs;
using Creators_Corner_App_API.Models;
using Creators_Corner_App_API.Repositories.Brand_Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Creators_Corner_App_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandRepository _brandRepository;

        public BrandController(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        // Brand Login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] BrandLoginDTO loginDto)
        {
            try
            {
                var brand = await _brandRepository.LoginAsync(loginDto.username, loginDto.password);
                return Ok(Response<Brand>.Success("Login successful", brand));
            }
            catch (Exception ex)
            {
                return BadRequest(Response<object>.Fail(ex.Message));
            }
        }

        // Fill Brand Application
        [HttpPost("apply")]
        public async Task<IActionResult> FillApplication([FromBody] BrandApplicationDTO applicationDto)
        {
            try
            {
                await _brandRepository.FillApplicationAsync(applicationDto);
                return Ok(Response<object>.Success("Application submitted successfully", null));
            }
            catch (Exception ex)
            {
                return BadRequest(Response<object>.Fail(ex.Message));
            }
        }

        // Upload Product
        [HttpPost("upload-product")]
        public async Task<IActionResult> UploadProduct([FromBody] ProductDTO productDto)
        {
            try
            {
                await _brandRepository.UploadProductAsync(productDto);
                return Ok(Response<object>.Success("Product uploaded successfully", null));
            }
            catch (Exception ex)
            {
                return BadRequest(Response<object>.Fail(ex.Message));
            }
        }

        // Get Products by Brand
        [HttpGet("products")]
        public async Task<IActionResult> GetProductsByBrand([FromQuery] string brandUsername)
        {
            try
            {
                var products = await _brandRepository.GetProductsByBrandAsync(brandUsername);
                return Ok(Response<List<Product>>.Success("Products retrieved successfully", products));
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
                await _brandRepository.ForgetPasswordAsync(forgetPasswordDto.email);
                return Ok(Response<object>.Success("Temporary password sent to your email", null));
            }
            catch (Exception ex)
            {
                return BadRequest(Response<object>.Fail(ex.Message));
            }
        }
    }
}
