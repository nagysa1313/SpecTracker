using SharpRepository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SpecTracker.Core.DAL
{
    [DataContract]
    public class Project : AuditableObject
    {
        [RepositoryPrimaryKey]
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
