using Creators_Corner_App_API.Data;
using Creators_Corner_App_API.DTOs;
using Creators_Corner_App_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Creators_Corner_App_API.Repositories.Brand_Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly AppDbContext _context;

        public BrandRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Brand> LoginAsync(LoginDTO loginDTO)
        {
            var brand = await _context.Brands
                .FirstOrDefaultAsync(b => b.Username == loginDTO.username && b.Password == loginDTO.password);

            if (brand == null)
                throw new Exception("Invalid username or password");

            return brand;
        }

        public async Task FillApplicationAsync(BrandApplicationDTO applicationDto)
        {
            var application = new BrandApplication
            {
                BrandName = applicationDto.brandName,
                Email = applicationDto.email,
                InstagramAccount = applicationDto.instagramAccount,
            };

            await _context.BrandApplications.AddAsync(application);
            await _context.SaveChangesAsync();
        }

        public async Task UploadProductAsync(ProductDTO productDto)
        {
            var brand = await _context.Brands
                .FirstOrDefaultAsync(b => b.Id == productDto.brandId);

            if (brand == null)
                throw new Exception("Brand not found");
            byte[] imageBytes = Convert.FromBase64String(productDto.image);
            var product = new Product
            {
                Name = productDto.name,
                Description = productDto.description,
                Image = imageBytes,
                Price = productDto.price,
                StockQuantity = productDto.stockQuantity,
                BrandId = brand.Id
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductDTO>> GetProductsByBrandAsync(int brandId)
        {
            var brand = await _context.Brands
                .FirstOrDefaultAsync(b => b.Id == brandId);

            if (brand == null)
                throw new Exception("Brand not found");

            return await _context.Products
                .Where(p => p.BrandId == brand.Id).Select(x => new ProductDTO
                {
                    name = x.Name,
                    description = x.Description,
                    image = Convert.ToBase64String(x.Image),
                    price = x.Price,
                    stockQuantity = x.StockQuantity,
                    brandId = x.BrandId
                })
                .ToListAsync();
        }

        public async Task ForgetPasswordAsync(ForgetPasswordDTO forgetPasswordDTO)
        {
            var brand = await _context.Brands
                .FirstOrDefaultAsync(c => c.Email == forgetPasswordDTO.email);

            if (brand == null)
                throw new Exception("Customer not found");

            brand.Password = forgetPasswordDTO.newPassword;
            await _context.SaveChangesAsync();
        }
    }
}
