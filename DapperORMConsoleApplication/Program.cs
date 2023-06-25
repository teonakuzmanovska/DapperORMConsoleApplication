using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using DapperORMConsoleApplication.Data.Repository;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using DapperORMConsoleApplication.Service;

namespace DapperORMConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            // Create instances of the services
            CategoryService categoryService = new CategoryService(connectionString);
            ProductService productService = new ProductService(connectionString);
            OrderService orderService = new OrderService(connectionString);
            OrderDetailsService orderDetailsService = new OrderDetailsService(connectionString);

            // Drop tables if they exist
            orderDetailsService.DropOrderdetailsTableIfExists();
            orderService.DropOrdersTableIfExists();
            productService.DropProductsTableIfExists();
            categoryService.DropCategoriesTableIfExists();

            // Calling functions for creating tables
            categoryService.CreateCategoriesTable();
            productService.CreateProductsTable();
            orderService.CreateOrdersTable();
            orderDetailsService.CreateOrderDetailsTable();

            // Inserting new categories
            categoryService.InsertCategories();
            // Get all categories
            categoryService.GetAllCategories();

            // Inserting new products
            productService.InsertProducts();
            // Get all products
            productService.GetAllProducts();

            // Inserting new orders            
            orderService.InsertOrders();
            // Get all orders
            orderService.GetAllOrders();

            // Inserting new orderdetails
            orderDetailsService.InsertOrderDetails();
            // Get all orderdetails
            orderDetailsService.GetAllOrderDetails();




            // Sort orders by date
            orderService.SortOrdersByDate();

            // Sort products by most sold
            productService.SortProductsByMostSold();

            // Sort categories bt most sold
            categoryService.SortCategoriesByMostSold();

            // keep the console open
            Console.ReadLine();

        }
    }
}
