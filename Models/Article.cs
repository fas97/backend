using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Article
    {
        [Key] 
        public string ArticleNumber { get; set; }
        [Required]
        public string Name { get; set; } 
        public float? SalesPrice { get; set; }
        [Required]
        public string Unit { get; set; }

    }
}
