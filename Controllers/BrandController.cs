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
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            try
            {
                var brand = await _brandRepository.LoginAsync(loginDto);
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
                return Ok(Response<object>.Success("Application submitted successfully", applicationDto));
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
                return Ok(Response<object>.Success("Product uploaded successfully", productDto));
            }
            catch (Exception ex)
            {
                return BadRequest(Response<object>.Fail(ex.Message));
            }
        }

        // Get Products by Brand
        [HttpGet("products")]
        public async Task<IActionResult> GetProductsByBrand([FromQuery] int brandId)
        {
            try
            {
                var products = await _brandRepository.GetProductsByBrandAsync(brandId);
                return Ok(Response<List<ProductDTO>>.Success("Products retrieved successfully", products));
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
                await _brandRepository.ForgetPasswordAsync(forgetPasswordDto);
                return Ok(Response<object>.Success("Temporary password sent to your email", forgetPasswordDto));
            }
            catch (Exception ex)
            {
                return BadRequest(Response<object>.Fail(ex.Message));
            }
        }
    }
}
