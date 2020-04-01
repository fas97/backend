using System.Runtime.Serialization;

namespace Models
{
    [DataContract]
    public class ArticleRow
    {
        [DataMember]
        public int quantity { get; set; }
        [DataMember]
        public ArticleVM article { get; set; }
    }

}
