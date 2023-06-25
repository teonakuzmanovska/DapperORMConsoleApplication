using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperORMConsoleApplication.Data.Entities
{
    public class OrderRepository
    {
        private readonly string connectionString;

        public OrderRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void CreateOrdersTable()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create table
                connection.Execute(@"
                CREATE TABLE Orders (
                    Id INT PRIMARY KEY IDENTITY(1,1),
                    CustomerId INT,
                    EmployeeId INT,
                    OrderDate DATETIME,
                    RequiredDate DATETIME,
                    ShippedDate DATETIME,
                    ShipVia NVARCHAR(100),
                    Freight INT,
                    ShipName NVARCHAR(100),
                    ShipAddress NVARCHAR(MAX),
                    ShipCity NVARCHAR(100),
                    ShipRegion NVARCHAR(100),
                    ShipPostalCode NVARCHAR(20),
                    ShipCountry NVARCHAR(100)
                )");
            }
        }

        public void InsertOrder(params object[] newOrder)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(@"INSERT INTO Orders (CustomerId, EmployeeId, OrderDate, RequiredDate, ShippedDate, ShipVia, Freight, 
                                    ShipName, ShipAddress, ShipCity, ShipRegion, ShipPostalCode, ShipCountry) 
                                    VALUES(@CustomerId, @EmployeeId, @OrderDate, @RequiredDate, @ShippedDate, @ShipVia, @Freight, 
                                    @ShipName, @ShipAddress, @ShipCity, @ShipRegion, @ShipPostalCode, @ShipCountry)", newOrder);
            }
        }

        public IEnumerable<dynamic> GetAllOrders()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query("SELECT * FROM Orders");
            }
        }

        public IEnumerable<dynamic> SortOrdersByDate()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query("SELECT * FROM Orders ORDER BY OrderDate");
            }
        }
    }
}
