using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Models
{
    [DataContract]
    public enum Status
    {
        Started,
        Ongoing,
        Done,
        Removed
    }
}
