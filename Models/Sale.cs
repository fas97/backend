using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Sale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } //++
        [Required]
        public string CustomerNumber { get; set; }
        public int UserId { get; set; } //fk
        [Required]
        public string YourReference { get; set; }
        public DateTime DateSold { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateDone { get; set; }
        public DateTime? DateEdited { get; set; }
        public Status StatusId { get; set; }
        public DateTime? DateDelivered { get; set; }

        //public virtual ICollection<SaleArticle> SaleArticles { get; set; }
        //public virtual User User { get; set; }
    }
}
