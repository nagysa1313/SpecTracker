using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecTracker.Core.DAL
{
    public abstract class AuditableObject
    {
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}