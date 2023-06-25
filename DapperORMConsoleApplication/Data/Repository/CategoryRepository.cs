using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DapperORMConsoleApplication.Data.Repository
{
    public class CategoryRepository
    {
        private readonly string connectionString;

        public CategoryRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void DropCategoriesTableIfExists()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Drop table if it exists
                connection.Execute("DROP TABLE IF EXISTS Categories");
            }
        }

        public void CreateCategoriesTable()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create table
                connection.Execute(@"
                CREATE TABLE Categories (
                    Id INT PRIMARY KEY IDENTITY(1,1),
                    CategoryName NVARCHAR(100) NOT NULL,
                    Description NVARCHAR(MAX),
                    Picture NVARCHAR(MAX)
                )");
            }
        }

        public IEnumerable<dynamic> GetAllCategories()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query("SELECT * FROM Categories");
            }
        }

        public void InsertCategory(params object[] newCategory)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute("INSERT INTO Categories (CategoryName, Description, Picture) VALUES (@CategoryName, @Description, @Picture)", newCategory);
            }
        }

        public IEnumerable<dynamic> SortCategoriesByMostSold()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query(@"select CategoryId, CategoryName, SUM(Quantity) as TotalSold from OrderDetails 
                                        join Products on OrderDetails.ProductId=Products.Id
                                        left join Categories on Products.CategoryId=Categories.Id
                                        group by CategoryId, CategoryName
                                        order by TotalSold desc");
            }
        }
    }
}
