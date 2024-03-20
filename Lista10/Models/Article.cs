using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lista10.Models
{
    
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "To short name")]
        [Display(Name = "Name")]
        [MaxLength(20, ErrorMessage = " To long name, do not exceed {1}")]
        public string ArticleName { get; set; }
        
        [Required]
        [Range(0.00, 5000.00, ErrorMessage = "Price must be between {1} and {2}")]
        public decimal Price { get; set; }
        
        [Required]
        public int CategoryId { get; set; }
        public string? Image { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public Article(int id, string name, decimal price, string image, int categoryId)
        {
            ArticleId = id;
            ArticleName = name;
            Price = price;
            Image = image;
            CategoryId = categoryId;
        }

        public Article(int id, string name, decimal price, int categoryId)
        {
            ArticleId = id;
            ArticleName = name;
            Price = price;
            Image = null;
            CategoryId = categoryId;
        }

        public Article(int id, string name, decimal price, string image, int categoryId, IFormFile imageFile)
        {
            ArticleId = id;
            ArticleName = name;
            Price = price;
            Image = image;
            CategoryId = categoryId;
            ImageFile = imageFile;
        }

        public Article(int id, string name, decimal price, int categoryId, IFormFile imageFile)
        {
            ArticleId = id;
            ArticleName = name;
            Price = price;
            Image = null;
            CategoryId = categoryId;
            ImageFile = imageFile;
        }
        public Article() { }
        public override string ToString()
        {
            return $"{CategoryId}";
        }
    }



}
