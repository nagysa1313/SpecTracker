using SharpRepository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecTracker.Core.DAL
{
    public class Project : AuditableObject
    {
        [RepositoryPrimaryKey]
        public int ID { get; set; }

        public string Name { get; set; }
    }
}
