using System;
using System.Collections.Generic;

namespace Models
{
    public class SaleVM
    {
        public enum Status
        {
            Started,
            Ongoing,
            Done,
            Removed
        }

        public int Id { get; set; }
        public string Reference { get; set; }
        public DateTime DateSold { get; set; }
        public DateTime DateCreated { get; set; }
        public Status StatusId { get; set; }
       // public int UserId { get; set; }

        public virtual ICollection<ArticleRow> ArticleRows { get; set; }
        public virtual SelectedCustomer SelectedCustomer { get; set; }

    }
}
