using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lista10.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "To short name")]
        [Display(Name = "Category name")]
        [MaxLength(20, ErrorMessage = " To long name, do not exceed {1}")]
        public string CategoryName { get; set; }
        public ICollection<Article>? Articles { get; set; }

        public Category(int id, string name)
        {
            CategoryId = id;
            CategoryName = name;
            Articles = new List<Article>();
        }
        public Category(){}
        


    }
}
