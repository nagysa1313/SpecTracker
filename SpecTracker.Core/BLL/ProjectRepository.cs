using SharpRepository.InMemoryRepository;
using SharpRepository.XmlRepository;
using SpecTracker.Core.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecTracker.Core.BLL
{
    /// <summary>
    /// Repository per aggregate
    /// </summary>
    public class ProjectRepository : XmlRepository<Project>
    {
        public ProjectRepository()
            : base("data")
        {

        }
    }
}