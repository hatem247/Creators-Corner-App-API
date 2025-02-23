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

        [HttpPost("apply")]
        public async Task<IActionResult> FillApplication([FromBody] BrandApplicationDTO applicationDto)
        {
            try
            {
                int applicationNumber = await _brandRepository.FillApplicationAsync(applicationDto);
                return Ok(Response<object>.Success("Application submitted successfully", applicationNumber));
            }
            catch (Exception ex)
            {
                return BadRequest(Response<object>.Fail(ex.Message));
            }
        }

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

        [HttpGet("products")]
        public async Task<IActionResult> GetProductsByBrand([FromQuery] int brandId)
        {
            try
            {
                var products = await _brandRepository.GetProductsByBrandAsync(brandId);
                return Ok(Response<List<Product>>.Success("Products retrieved successfully", products));
            }
            catch (Exception ex)
            {
                return BadRequest(Response<object>.Fail(ex.Message));
            }
        }
        [HttpGet("orders")]
        public async Task<IActionResult> GetOrdersByBrand([FromQuery] int brandId)
        {
            try
            {
                var products = await _brandRepository.GetOrdersByBrandAsync(brandId);
                return Ok(Response<List<Product>>.Success("Orders retrieved successfully", products));
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

        [HttpPost("update-data")]
        public async Task<IActionResult> UpdateData([FromBody] BrandDTO brandDTO, [FromQuery] int brandId)
        {
            try
            {
                await _brandRepository.UpdateData(brandDTO, brandId);
                return Ok(Response<object>.Success("Data Updated successfully", brandDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(Response<object>.Fail(ex.Message));
            }
        }

        [HttpPost("application-status")]
        public async Task<IActionResult> GetApplicationStatus([FromQuery] string brandEmail)
        {
            try
            {
                var application = await _brandRepository.GetApplicationStatus(brandEmail);
                return Ok(Response<object>.Success("Application retrieved successfully", application));
            }
            catch (Exception ex)
            {
                return BadRequest(Response<object>.Fail(ex.Message));
            }
        }
        [HttpPost("update-application-status")]
        public async Task<IActionResult> UpdateApplicationStatus([FromQuery] int applicationId, [FromBody] bool status)
        {
            try
            {
                await _brandRepository.UpdateApplicationStatus(applicationId, status);
                return Ok(Response<object>.Success("Application retrieved successfully", applicationId));
            }
            catch (Exception ex)
            {
                return BadRequest(Response<object>.Fail(ex.Message));
            }
        }

        [HttpPost("update-login-status")]
        public async Task<IActionResult> UpdateLoginStatus([FromQuery] int brandId)
        {
            try
            {
                await _brandRepository.UpdateLoginStatus(brandId);
                return Ok(Response<object>.Success("Login successful", brandId));
            }
            catch (Exception ex)
            {
                return BadRequest(Response<object>.Fail(ex.Message));
            }
        }
    }
}
