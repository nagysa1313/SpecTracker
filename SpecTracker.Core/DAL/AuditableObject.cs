using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SpecTracker.Core.DAL
{
    [DataContract]
    public abstract class AuditableObject
    {
        [DataMember]
        public DateTime Created { get; set; }
        [DataMember]
        public DateTime Updated { get; set; }
    }
}