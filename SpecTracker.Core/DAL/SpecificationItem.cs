using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SpecTracker.Core.DAL
{
    [DataContract]
    public class SpecificationItem : AuditableObject
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public int Priority { get; set; }
    }
}