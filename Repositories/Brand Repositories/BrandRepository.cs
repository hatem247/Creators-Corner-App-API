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

        public async Task<Brand> LoginAsync(string username, string password)
        {
            var brand = await _context.Brands
                .FirstOrDefaultAsync(b => b.Username == username && b.Password == password);

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
                ApplicationDate = DateTime.UtcNow,
                IsApproved = false
            };

            await _context.BrandApplications.AddAsync(application);
            await _context.SaveChangesAsync();
        }

        public async Task UploadProductAsync(ProductDTO productDto)
        {
            var brand = await _context.Brands
                .FirstOrDefaultAsync(b => b.Username == productDto.brandUsername);

            if (brand == null)
                throw new Exception("Brand not found");

            var product = new Product
            {
                Name = productDto.name,
                Description = productDto.description,
                Price = productDto.price,
                StockQuantity = productDto.stockQuantity,
                BrandId = brand.Id
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetProductsByBrandAsync(string brandUsername)
        {
            var brand = await _context.Brands
                .FirstOrDefaultAsync(b => b.Username == brandUsername);

            if (brand == null)
                throw new Exception("Brand not found");

            return await _context.Products
                .Where(p => p.BrandId == brand.Id)
                .ToListAsync();
        }

        public async Task ForgetPasswordAsync(string email)
        {
            var brand = await _context.Brands
                .FirstOrDefaultAsync(c => c.Email == email);

            if (brand == null)
                throw new Exception("Customer not found");

            var temporaryPassword = Guid.NewGuid().ToString().Substring(0, 8);
            brand.Password = temporaryPassword;
            await _context.SaveChangesAsync();
        }
    }
}
