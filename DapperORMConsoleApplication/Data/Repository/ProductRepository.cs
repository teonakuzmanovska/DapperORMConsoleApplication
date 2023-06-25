using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperORMConsoleApplication.Data.Repository
{
    public class ProductRepository
    {
        private readonly string connectionString;

        public ProductRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void DropProductsTableIfExists()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Drop table if it exists
                connection.Execute("DROP TABLE IF EXISTS Products");
            }
        }

        public void CreateProductsTable()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create table
                connection.Execute(@"
                CREATE TABLE Products (
                    Id INT PRIMARY KEY IDENTITY(1,1),
                    ProductName NVARCHAR(100) NOT NULL,
                    SupplierId INT,
                    CategoryId INT,
                    QuantityPerUnit INT,
                    UnitPrice INT,
                    UnitsInStock INT,
                    UnitsOnOrder INT,
                    ReorderLevel INT,
                    Discontinued BIT,
                    LastUserId INT,
                    LastDateUpdated DATETIME,
                    FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
                )");
            }
        }

        public IEnumerable<dynamic> GetAllProducts()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query("SELECT * FROM Products");
            }
        }

        public void InsertProduct(params object[] newProduct)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(@"INSERT INTO Products 
                                    (ProductName, SupplierId, CategoryId, QuantityPerUnit, 
                                    UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, 
                                    Discontinued, LastUserId, LastDateUpdated) 
                                    VALUES (@ProductName, @SupplierId, @CategoryId, @QuantityPerUnit, 
                                    @UnitPrice, @UnitsInStock, @UnitsOnOrder, @ReorderLevel, 
                                    @Discontinued, @LastUserId, @LastDateUpdated)", newProduct);
            }
        }

        public IEnumerable<dynamic> SortProductsByMostSold()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query(@"select * from OrderDetails 
                                        join Products on OrderDetails.ProductId = Products.Id
                                        order by OrderDetails.Quantity desc");
            }
        }

    }
}
