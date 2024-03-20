using Lista10.Models;
using Microsoft.EntityFrameworkCore;

namespace Lista10.Data
{
    
        public static class ModelBuilderExtension
        {
            public static void Seed(this ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Category>().HasData(
                    new Category(1, "Sweets"),
                    new Category(2, "Fruits"),
                    new Category(3, "Vegetables"),
                    new Category(4, "Meat"),
                    new Category(5, "Fish"),
                    new Category(6, "Pasta"),
                    new Category(7, "Bread"),
                    new Category(8, "Cereals"),
                    new Category(9, "Drinks")
                    );

                modelBuilder.Entity<Article>().HasData(
                    new Article(1, "Apple", 2.99m, "upload/apples.jpg", 2),
                    new Article(2, "Banana", 3.99m, "upload/banana.jpg", 2),
                    new Article(3, "Orange", 4.99m, 2),
                    new Article(4, "Sugar", 5.99m, 1),
                    new Article(5, "Potato", 6.99m, 3),
                    new Article(6, "Cucumber", 7.99m, "upload/cucumber.jpg", 3),
                    new Article(7, "Beef", 8.99m, 4),
                    new Article(8, "Pork", 9.99m, 4),
                    new Article(9, "Chicken", 10.99m, 4),
                    new Article(10, "Salmon", 11.99m, 5)
                    );
            }
        }
    }
