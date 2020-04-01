using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Models
{
    [DataContract]
    public class SaleVM
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string reference { get; set; }
        [DataMember]
        public string dateSold { get; set; }
        [DataMember]
        public string dateCreated { get; set; }
        [DataMember]
        public int statusId { get; set; }
        [DataMember]
        public SelectedCustomer customer { get; set; }
        [DataMember]
        public List<ArticleRow> articleRows { get; set; }
    }
}
