using Creators_Corner_App_API.Models;

namespace Creators_Corner_App_API.Data
{
    public static class DataSeeder
    {
        public static void Seed(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Brands.Any())
            {
                var brands = new List<Brand>
                {
                    new Brand
                    {
                        Username = "brand1",
                        Name = "Brand One",
                        Bio = "This is Brand One",
                        Image = new byte[0],
                        Email = "brand1@example.com",
                        Password = "password1",
                        FirstLogin = false,
                        Products = new List<int> { 1, 2, 3 }
                    },
                    new Brand
                    {
                        Username = "brand2",
                        Name = "Brand Two",
                        Bio = "This is Brand Two",
                        Image = new byte[0],
                        Email = "brand2@example.com",
                        Password = "password2",
                        FirstLogin = true,
                        Products = new List<int> { 4, 5, 6 }
                    }
                };

                context.Brands.AddRange(brands);
                context.SaveChanges();
            }

            if (!context.BrandApplications.Any())
            {
                var brandApplications = new List<BrandApplication>
                {
                    new BrandApplication
                    {
                        BrandName = "New Brand 1",
                        Email = "newbrand1@example.com",
                        InstagramAccount = "@newbrand1",
                        ApplicationDate = DateTime.UtcNow,
                        Status = "Pending"
                    },
                    new BrandApplication
                    {
                        BrandName = "New Brand 2",
                        Email = "newbrand2@example.com",
                        InstagramAccount = "@newbrand2",
                        ApplicationDate = DateTime.UtcNow,
                        Status = "Approved"
                    }
                };

                context.BrandApplications.AddRange(brandApplications);
                context.SaveChanges();
            }

            if (!context.Customers.Any())
            {
                var customers = new List<Customer>
                {
                    new Customer
                    {
                        Username = "customer1",
                        Name = "Customer One",
                        Image = new byte[0],
                        Email = "customer1@example.com",
                        Password = "password1",
                        Address = "123 Main St",
                        PhoneNumber = "555-1234",
                        Cart = new Cart { CustomerId = 1, Products = new List<int> { 1, 2 } },
                        Orders = new List<int> { 1 }
                    },
                    new Customer
                    {
                        Username = "customer2",
                        Name = "Customer Two",
                        Image = new byte[0], // Replace with actual image bytes
                        Email = "customer2@example.com",
                        Password = "password2",
                        Address = "456 Elm St",
                        PhoneNumber = "555-5678",
                        Cart = new Cart { CustomerId = 2, Products = new List<int> { 3, 4 } }, // Product IDs
                        Orders = new List<int> { 2 } // Order IDs
                    }
                };

                context.Customers.AddRange(customers);
                context.SaveChanges();
            }

            // Seed Products (6 records)
            if (!context.Products.Any())
            {
                var products = new List<Product>
                {
                    new Product
                    {
                        Name = "Product One",
                        Images = [new byte[0]], // Replace with actual image bytes
                        Description = "This is Product One",
                        Price = 19.99m,
                        StockQuantity = 10,
                        BrandId = 1, // Brand ID
                        Orders = new List<int> { 1 }, // Order IDs
                        Carts = new List<int> { 1 } // Cart IDs
                    },
                    new Product
                    {
                        Name = "Product Two",
                        Images = [new byte[0]], // Replace with actual Images bytes
                        Description = "This is Product Two",
                        Price = 29.99m,
                        StockQuantity = 5,
                        BrandId = 1, // Brand ID
                        Orders = new List<int>(), // No orders
                        Carts = new List<int> { 1 } // Cart IDs
                    },
                    new Product
                    {
                        Name = "Product Three",
                        Images = [new byte[0]], // Replace with actual Images bytes
                        Description = "This is Product Three",
                        Price = 39.99m,
                        StockQuantity = 15,
                        BrandId = 1, // Brand ID
                        Orders = new List<int> { 2 }, // Order IDs
                        Carts = new List<int> { 2 } // Cart IDs
                    },
                    new Product
                    {
                        Name = "Product Four",
                        Images = [new byte[0]], // Replace with actual Images bytes
                        Description = "This is Product Four",
                        Price = 49.99m,
                        StockQuantity = 20,
                        BrandId = 2, // Brand ID
                        Orders = new List<int>(), // No orders
                        Carts = new List<int> { 2 } // Cart IDs
                    },
                    new Product
                    {
                        Name = "Product Five",
                        Images = [new byte[0]], // Replace with actual Images bytes
                        Description = "This is Product Five",
                        Price = 59.99m,
                        StockQuantity = 25,
                        BrandId = 2, // Brand ID
                        Orders = new List<int> { 2 }, // Order IDs
                        Carts = new List<int>() // No carts
                    },
                    new Product
                    {
                        Name = "Product Six",
                        Images = [new byte[0]], // Replace with actual image bytes
                        Description = "This is Product Six",
                        Price = 69.99m,
                        StockQuantity = 30,
                        BrandId = 2, // Brand ID
                        Orders = new List<int>(), // No orders
                        Carts = new List<int>() // No carts
                    }
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }

            // Seed Orders (2 records)
            if (!context.Orders.Any())
            {
                var orders = new List<Order>
                {
                    new Order
                    {
                        OrderDate = DateTime.Now,
                        TotalAmount = 19.99m,
                        CustomerId = 1, // Customer ID
                        Products = new List<int> { 1 } // Product IDs
                    },
                    new Order
                    {
                        OrderDate = DateTime.Now,
                        TotalAmount = 39.99m,
                        CustomerId = 2, // Customer ID
                        Products = new List<int> { 3 } // Product IDs
                    }
                };

                context.Orders.AddRange(orders);
                context.SaveChanges();
            }
        }
    }
}
