using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class SaleArticle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SalesId { get; set; }
        [Required]
        public string ArticleNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateEdited { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int TotalSum { get; set; }
        public string Description { get; set; }


        public virtual Sale Sale { get; set; }
    }
}
