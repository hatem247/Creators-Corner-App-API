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

        public async Task<Customer> LoginAsync(LoginDTO loginDTO)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Username == loginDTO.username && c.Password == loginDTO.password);

            if (customer == null)
                throw new Exception("Invalid username or password");

            return customer;
        }

        public async Task RegisterAsync(CustomerDTO customerDto)
        {
            if (customerDto == null)
            {
                throw new ArgumentNullException(nameof(customerDto), "Customer data cannot be null.");
            }

            bool usernameExists = await _context.Customers.AnyAsync(c => c.Username == customerDto.username);
            bool emailExists = await _context.Customers.AnyAsync(c => c.Email == customerDto.email);

            if (usernameExists)
            {
                throw new InvalidOperationException("Username is already taken.");
            }

            if (emailExists)
            {
                throw new InvalidOperationException("Email is already registered.");
            }
            byte[] imageBytes = Convert.FromBase64String(customerDto.image);
            var customer = new Customer
            {
                Username = customerDto.username,
                Name = customerDto.name,
                Email = customerDto.email,
                Password = customerDto.password,
                Address = customerDto.address,
                PhoneNumber = customerDto.phoneNumber,
                Image = imageBytes
            };

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task CheckoutAsync(int customerId)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Id == customerId);

            if (customer == null)
                throw new Exception("Customer not found");

            var cart = await _context.Carts
                .FirstOrDefaultAsync(p => p.CustomerId == customerId);

            if (cart == null)
                throw new Exception("Cart not found");

            if (cart.Products.Count == 0)
                throw new Exception("The cart is empty");
            decimal total = 0;
            for (int i = 0; i < cart.Products.Count; i++)
            {
                var product = _context.Products.FirstOrDefault(x => x.Id == cart.Products[i]);
                product.StockQuantity--;
                total += product.Price;
                product.Carts.Remove(cart.Id);
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }

            var order = new Order
            {
                TotalAmount = total,
                CustomerId = customer.Id,
                Products = cart.Products,
            };

            cart.Products.Clear();
            _context.Carts.Update(cart);
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

        public async Task ForgetPasswordAsync(ForgetPasswordDTO forgetPasswordDTO)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Email == forgetPasswordDTO.email);

            if (customer == null)
                throw new Exception("Customer not found");

            customer.Password = forgetPasswordDTO.newPassword;
            await _context.SaveChangesAsync();
        }

        public async Task<Cart> GetCustomerCartAsync(int customerId)
        {
            var customer = await _context.Customers
                .Include(c => c.Cart)
                .ThenInclude(cart => cart.Products)
                .FirstOrDefaultAsync(c => c.Id == customerId);

            if (customer == null)
                throw new Exception("Customer not found");

            return customer.Cart;
        }

        public async Task AddProductToCartAsync(int productId, int customerId)
        {
            var customer = await _context.Customers
                .Include(c => c.Cart)
                .FirstOrDefaultAsync(c => c.Id == customerId);

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

            customer.Cart.Products.Add(productId);
            await _context.SaveChangesAsync();
        }
    }
}
