using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Models
{
    [DataContract]
    public class ArticleVM
    {
        [DataMember]
        public string articleNumber { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int salesPrice { get; set; }
        [DataMember]
        public string unit { get; set; }
        [DataMember]
        public string description { get; set; }
    }
}
