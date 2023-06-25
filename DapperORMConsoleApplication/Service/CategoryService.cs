using DapperORMConsoleApplication.Data.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperORMConsoleApplication.Service
{
    public class CategoryService
    {
        CategoryRepository categoryRepository;

        public CategoryService(string connectionString)
        {
            categoryRepository = new CategoryRepository(connectionString);
        }

        public void DropCategoriesTableIfExists()
        {
            categoryRepository.DropCategoriesTableIfExists();
        }

        public void CreateCategoriesTable()
        {
            categoryRepository.CreateCategoriesTable();
            Console.WriteLine("Table Categories created successfully!");
        }

        public void InsertCategories()
        {
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
        }

        public void GetAllCategories()
        {
            Console.WriteLine("List of all categories: ");
            var categories = categoryRepository.GetAllCategories();
            foreach (var category in categories)
            {
                Console.WriteLine($"Category ID: {category.Id}, Name: {category.CategoryName}");
            }
        }

        public void SortCategoriesByMostSold()
        {
            Console.WriteLine("Sorted categories by most sold: ");
            var sortedCategories = categoryRepository.SortCategoriesByMostSold();
            foreach (var sortedCategory in sortedCategories)
            {
                Console.WriteLine($"Catgeory Id: {sortedCategory.CategoryId}, Category Name: {sortedCategory.CategoryName}, Sold products: {sortedCategory.TotalSold}");
            }
        }

    }
}
