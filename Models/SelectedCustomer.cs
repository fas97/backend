using System.Runtime.Serialization;

namespace Models
{
    [DataContract]
    public class SelectedCustomer
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string customerNumber { get; set; }
    }
}
