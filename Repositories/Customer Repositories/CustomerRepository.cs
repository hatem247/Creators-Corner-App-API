using Creators_Corner_App_API.Data;
using Creators_Corner_App_API.DTOs;
using Creators_Corner_App_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Creators_Corner_App_API.Repositories.Customer_Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> LoginAsync(string username, string password)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Username == username && c.Password == password);

            if (customer == null)
                throw new Exception("Invalid username or password");

            return customer;
        }

        public async Task RegisterAsync(CustomerDTO customerDto)
        {
            var customer = new Customer
            {
                Username = customerDto.username,
                Name = customerDto.name,
                Email = customerDto.email,
                Password = customerDto.password,
                Address = customerDto.address,
                PhoneNumber = customerDto.phoneNumber
            };

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task BuyProductAsync(int productId, string customerUsername)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Username == customerUsername);

            if (customer == null)
                throw new Exception("Customer not found");

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
                throw new Exception("Product not found");

            if (product.StockQuantity <= 0)
                throw new Exception("Product out of stock");

            product.StockQuantity--;

            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                TotalAmount = product.Price,
                CustomerId = customer.Id,
                Products = new List<Product> { product }
            };

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<string> CompareProductsAsync(int productId1, int productId2)
        {
            var product1 = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productId1);

            var product2 = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productId2);

            if (product1 == null || product2 == null)
                throw new Exception("One or both products not found");

            var comparison = new StringBuilder();
            comparison.AppendLine($"Comparing {product1.Name} and {product2.Name}:");
            comparison.AppendLine($"- Price: {product1.Price} vs {product2.Price}");
            comparison.AppendLine($"- Stock Quantity: {product1.StockQuantity} vs {product2.StockQuantity}");
            comparison.AppendLine($"- Description: {product1.Description} vs {product2.Description}");

            return comparison.ToString();
        }

        public async Task ForgetPasswordAsync(string email)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Email == email);

            if (customer == null)
                throw new Exception("Customer not found");

            var temporaryPassword = Guid.NewGuid().ToString().Substring(0, 8);
            customer.Password = temporaryPassword;
            await _context.SaveChangesAsync();
        }

        public async Task<Cart> GetCustomerCartAsync(string customerUsername)
        {
            var customer = await _context.Customers
                .Include(c => c.Cart)
                .ThenInclude(cart => cart.Products)
                .FirstOrDefaultAsync(c => c.Username == customerUsername);

            if (customer == null)
                throw new Exception("Customer not found");

            return customer.Cart;
        }

        public async Task AddProductToCartAsync(int productId, string customerUsername)
        {
            var customer = await _context.Customers
                .Include(c => c.Cart)
                .FirstOrDefaultAsync(c => c.Username == customerUsername);

            if (customer == null)
                throw new Exception("Customer not found");

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
                throw new Exception("Product not found");

            if (customer.Cart == null)
            {
                customer.Cart = new Cart { CustomerId = customer.Id };
                await _context.Carts.AddAsync(customer.Cart);
            }

            customer.Cart.Products.Add(product);
            await _context.SaveChangesAsync();
        }
    }
}
