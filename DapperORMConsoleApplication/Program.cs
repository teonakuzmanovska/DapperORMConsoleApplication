using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using DapperORMConsoleApplication.Data.Entities;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace DapperORMConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            // Create instances of the repositories
            CategoryRepository categoryRepository = new CategoryRepository(connectionString);
            ProductRepository productRepository = new ProductRepository(connectionString);
            OrderRepository orderRepository = new OrderRepository(connectionString);
            OrderDetailsRepository orderDetailsRepository = new OrderDetailsRepository(connectionString);

            // Drop tables if they exist

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Drop table if it exists
                connection.Execute("DROP TABLE IF EXISTS OrderDetails");
                connection.Execute("DROP TABLE IF EXISTS Orders");
                connection.Execute("DROP TABLE IF EXISTS Products");
                connection.Execute("DROP TABLE IF EXISTS Categories");
            }

            // Calling functions for creating tables
            categoryRepository.CreateCategoriesTable();
            Console.WriteLine("Table Categories successfully!");
            productRepository.CreateProductsTable();
            Console.WriteLine("Table Products successfully!");
            orderRepository.CreateOrdersTable();
            Console.WriteLine("Table Orders successfully!");
            orderDetailsRepository.CreateOrderDetailsTable();
            Console.WriteLine("Table OrderDetails successfully!");

            // Inserting into tables

            // Inserting new categories

            string picture;

            for (var i = 0; i < 2; i++)
            {
                if (i % 2 == 0)
                    picture = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQyNDBuoXEayLO2l-GxlcRsaawFMmACc2phigkFN0j4pTvYzxo4d7V9aORbvlejfU6knCk&usqp=CAU";
                else
                    picture = "https://png.pngtree.com/png-clipart/20210311/original/pngtree-brush-circle-creative-brush-effect-png-image_6020152.jpg";

                var newCategory = new
                {
                    CategoryName = "Category " + i,
                    Description = "Category " + i + " description",
                    Picture = picture
                };

                categoryRepository.InsertCategory(newCategory);
                Console.WriteLine("New category inserted successfully!");
            }

            // Get all categories
            Console.WriteLine("List of all categories: ");
            var categories = categoryRepository.GetAllCategories();
            foreach (var category in categories)
            {
                Console.WriteLine($"Category ID: {category.Id}, Name: {category.CategoryName}");
            }

            // Inserting new products

            Random random = new Random();

            for (var i = 0; i <= 6; i++)
            {
                var newProduct = new
                {
                    ProductName = "Product " + i,
                    SupplierId = 1,
                    CategoryId = categories.ElementAt(random.Next(0, 2)).Id,
                    QuantityPerUnit = 10,
                    UnitPrice = 19,
                    UnitsInStock = 100,
                    UnitsOnOrder = 20,
                    ReorderLevel = 10,
                    Discontinued = false,
                    LastUserId = 1,
                    LastDateUpdated = DateTime.Now
                };
                productRepository.InsertProduct(newProduct);
                Console.WriteLine("New product inserted successfully!");
            }

            // Get all products
            Console.WriteLine("List of all products: ");
            var products = productRepository.GetAllProducts();
            foreach (var product in products)
            {
                Console.WriteLine($"Product ID: {product.Id}, Name: {product.ProductName}, CatgeoryId: {product.CategoryId}");
            }

            // Insert new orders            

            DateTime orderDate;
            for (var i = 0; i <= 5; i++)
            {
                orderDate = DateTime.Now.AddDays(random.Next(0, 10));
                var newOrder = new
                {
                    CustomerId = 1,
                    EmployeeId = 1,
                    OrderDate = orderDate,
                    RequiredDate = orderDate.AddDays(10),
                    ShippedDate = orderDate.AddDays(3),
                    ShipVia = "shipvia",
                    Freight = 3,
                    ShipName = "ship 1",
                    ShipAddress = "address 1",
                    ShipCity = "shipcity 1",
                    ShipRegion = "shipregion 1",
                    ShipPostalCode = "1000",
                    ShipCountry = "country 1"
                };

                orderRepository.InsertOrder(newOrder);
                Console.WriteLine("New order inserted successfully!");
            }

            // Get all orders
            var orders = orderRepository.GetAllOrders();
            foreach (var order in orders)
            {
                Console.WriteLine($"Order ID: {order.Id}, Order date: {order.OrderDate}");
            }

            // Inserting new orderdetails
            foreach (var product in products)
            {
                var newOrderDetails = new
                {
                    OrderId = 1, // all of the products will be placed in the first order, it doesn't matter for sorting most sold products
                    ProductId = product.Id,
                    UnitPrice = 10,
                    Quantity = random.Next(1, 20),
                    Discount = 0
                };

                orderDetailsRepository.InsertOrderDetails(newOrderDetails);
                Console.WriteLine("New OrderDetails inserted successfully!");
            }

            // Get all orderdetails
            Console.WriteLine("List of all orderdetails: ");
            var orderDetails = orderDetailsRepository.GetAllOrderDetails();
            foreach (var orderDetail in orderDetails)
            {
                Console.WriteLine($"OrderDetails ID: {orderDetail.OrderId},{orderDetail.ProductId}, Quantity: {orderDetail.Quantity}");
            }

            // Sort orders by date
            Console.WriteLine("Sorted orders by date: ");
            var sortedOrders = orderRepository.SortOrdersByDate();
            foreach (var sortedOrder in sortedOrders)
            {
                Console.WriteLine($"Order ID: {sortedOrder.Id}, Order date: {sortedOrder.OrderDate}");
            }

            // Sort products by most sold
            Console.WriteLine("Sorted products by most sold: ");
            var sortedProducts = productRepository.SortProductsByMostSold();
            foreach (var sortedProduct in sortedProducts)
            {
                Console.WriteLine($"Product ID: {sortedProduct.Id}, Product name: {sortedProduct.ProductName}, Sold units: {sortedProduct.Quantity}");
            }

            // Sort categories bt most sold
            Console.WriteLine("Sorted categories by most sold: ");
            var sortedCategories = categoryRepository.SortCategoriesByMostSold();
            foreach (var sortedCategory in sortedCategories)
            {
                Console.WriteLine($"Catgeory Id: {sortedCategory.CategoryId}, Category Name: {sortedCategory.CategoryName}, Sold products: {sortedCategory.TotalSold}");
            }

            // keep the console open
            Console.ReadLine();

        }
    }
}
