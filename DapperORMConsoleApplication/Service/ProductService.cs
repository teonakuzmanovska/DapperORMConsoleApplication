using DapperORMConsoleApplication.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperORMConsoleApplication.Service
{
    public class ProductService
    {
        CategoryRepository categoryRepository;
        ProductRepository productRepository;

        public ProductService(string connectionString)
        {
            categoryRepository = new CategoryRepository(connectionString);
            productRepository = new ProductRepository(connectionString);
        }

        public void DropProductsTableIfExists()
        {
            productRepository.DropProductsTableIfExists();
        }

        public void CreateProductsTable()
        {
            productRepository.CreateProductsTable();
            Console.WriteLine("Table Products created successfully!");
        }

        public void InsertProducts()
        {
            Random random = new Random();
            var categories = categoryRepository.GetAllCategories();

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
        }

        public void GetAllProducts()
        {
            Console.WriteLine("List of all products: ");
            var products = productRepository.GetAllProducts();
            foreach (var product in products)
            {
                Console.WriteLine($"Product ID: {product.Id}, Name: {product.ProductName}, CatgeoryId: {product.CategoryId}");
            }
        }

        public void SortProductsByMostSold()
        {
            Console.WriteLine("Sorted products by most sold: ");
            var sortedProducts = productRepository.SortProductsByMostSold();
            foreach (var sortedProduct in sortedProducts)
            {
                Console.WriteLine($"Product ID: {sortedProduct.Id}, Product name: {sortedProduct.ProductName}, Sold units: {sortedProduct.Quantity}");
            }
        }
    }
}
