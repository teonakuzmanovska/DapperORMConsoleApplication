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
    public class OrderDetailsRepository
    {
        private readonly string connectionString;

        public OrderDetailsRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void DropOrderDetailsTableIfExists()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Drop table if it exists
                connection.Execute("DROP TABLE IF EXISTS OrderDetails");
            }
        }

        public void CreateOrderDetailsTable()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create table
                connection.Execute(@"
                CREATE TABLE OrderDetails (
                    OrderId INT,
                    ProductId INT,
                    UnitPrice INT,
                    Quantity INT,
                    Discount INT,
                    PRIMARY KEY (OrderId, ProductId),
                    FOREIGN KEY (OrderId) REFERENCES Orders(Id),
                    FOREIGN KEY (ProductId) REFERENCES Products(Id)
                )");
            }
        }

        public void InsertOrderDetails(params object[] newOrderDetails)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(@"INSERT INTO OrderDetails (OrderId, ProductId, UnitPrice, Quantity, Discount)
                                    VALUES (@OrderId, @ProductId, @UnitPrice, @Quantity, @Discount)", newOrderDetails);
            }
        }

        public IEnumerable<dynamic> GetAllOrderDetails()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query("SELECT * FROM OrderDetails");
            }
        }
    }
}
