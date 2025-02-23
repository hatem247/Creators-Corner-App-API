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

        public async Task<int> FillApplicationAsync(BrandApplicationDTO applicationDto)
        {
            if (applicationDto == null)
            {
                throw new ArgumentNullException(nameof(applicationDto), "Brand Application data cannot be null.");
            }

            bool emailExists = await _context.Customers.AnyAsync(c => c.Email == applicationDto.email);

            if (emailExists)
            {
                throw new InvalidOperationException("Email is already registered.");
            }

            var application = new BrandApplication
            {
                BrandName = applicationDto.brandName,
                Email = applicationDto.email,
                InstagramAccount = applicationDto.instagramAccount,
            };

            await _context.BrandApplications.AddAsync(application);
            await _context.SaveChangesAsync();

            return application.Id;
        }

        public async Task UploadProductAsync(ProductDTO productDto)
        {
            var brand = await _context.Brands
                .FirstOrDefaultAsync(b => b.Id == productDto.brandId);

            if (brand == null)
                throw new Exception("Brand not found");
            List<byte[]> imagesBytes = productDto.images.Select(image => Convert.FromBase64String(image)).ToList();
            var product = new Product
            {
                Name = productDto.name,
                Description = productDto.description,
                Images = imagesBytes,
                Price = productDto.price,
                StockQuantity = productDto.stockQuantity,
                BrandId = brand.Id
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            brand.Products.Add(product.Id);
            _context.Brands.Update(brand);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetProductsByBrandAsync(int brandId)
        {
            var brand = await _context.Brands
                .FirstOrDefaultAsync(b => b.Id == brandId);

            if (brand == null)
                throw new Exception("Brand not found");

            return await _context.Products
                .Where(p => p.BrandId == brand.Id)
                .ToListAsync();
        }
        
        public async Task<List<Product>> GetOrdersByBrandAsync(int brandId)
        {
            var brand = await _context.Brands
                .FirstOrDefaultAsync(b => b.Id == brandId);

            if (brand == null)
                throw new Exception("Brand not found");

            return await _context.Products
                .Where(p => p.BrandId == brand.Id && p.Orders != null)
                .ToListAsync();
        }

        public async Task ForgetPasswordAsync(ForgetPasswordDTO forgetPasswordDTO)
        {
            var brand = await _context.Brands
                .FirstOrDefaultAsync(c => c.Email == forgetPasswordDTO.email);

            if (brand == null)
                throw new Exception("Brand not found");

            brand.Password = forgetPasswordDTO.newPassword;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateData(BrandDTO brandDTO, int brandId)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(c => c.Id == brandId);
            if (brand == null)
                throw new Exception("Brand not found");
            brand.Name = brandDTO.name;
            brand.Username = brandDTO.username;
            brand.Password = brandDTO.password;
            brand.Email = brandDTO.email;
            brand.FirstLogin = false;
            brand.Bio = brandDTO.bio;
            brand.Image = Convert.FromBase64String(brandDTO.image);
            _context.Brands.Update(brand);
            await _context.SaveChangesAsync();
        }

        public async Task<BrandApplication> GetApplicationStatus(string brandEmail)
        {
            return await _context.BrandApplications.FirstOrDefaultAsync(x => x.Email == brandEmail);
        }

        public async Task UpdateApplicationStatus(int applicationId, bool status)
        {
            var application = _context.BrandApplications.FirstOrDefault(x => x.Id == applicationId);

            if(application == null)
            {
                throw new Exception("Application not found");
            }
            else
            {
                if (status == true)
                {
                    application.Status = "approved";
                    _context.BrandApplications.Update(application);
                    await _context.SaveChangesAsync();
                    Brand brand = new Brand
                    {
                        Name = application.BrandName,
                        Email = application.Email,
                        Bio = "",
                        Username = application.Email.Split('@').First(),
                        Password = GenerateRandomPassword(),
                        Image = new byte[0],
                        Products = []
                    };
                    await _context.Brands.AddAsync(brand);
                    await _context.SaveChangesAsync();
                }
                else if (status == false)
                {
                    application.Status = "rejected";
                    _context.BrandApplications.Update(application);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Status can't be null");
                }
            }
        }

        private static string GenerateRandomPassword()
        {
            var random = new Random();
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            string numbers = "0123456789";
            string symbols = "!@#$%^&*()_+{}|:.";
            string basePassword = "Brand";
            int remainingLength = random.Next(3, 12);

            char[] password = new char[remainingLength];

            password[0] = numbers[random.Next(numbers.Length)];
            password[1] = symbols[random.Next(symbols.Length)];

            string allChars = letters + numbers + symbols;
            for (int i = 2; i < remainingLength; i++)
            {
                password[i] = allChars[random.Next(allChars.Length)];
            }

            string shuffledPart = new string(password.OrderBy(x => random.Next()).ToArray());

            return basePassword + shuffledPart;
        }

        public async Task UpdateLoginStatus(int brandId)
        {
            var brand = await _context.Brands
               .FirstOrDefaultAsync(b => b.Id == brandId);

            if (brand == null)
                throw new Exception("Invalid username or password");
            brand.FirstLogin = false;
            _context.Brands.Update(brand);
            await _context.SaveChangesAsync();
        }

    }
}
