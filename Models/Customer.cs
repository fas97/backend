using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Customer
    {
        [Key]
        [Required]
        public string CustomerNumber { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address1 { get; set; }
        [Required]
        public string Address2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string OrganisationNumber { get; set; }
        [Required]
        public string ZipCode { get; set; }

        //public virtual ICollection<Sales> Sales { get; set; }

    }
}
