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

        protected override void AddItem(Project entity)
        {
            entity.Created = DateTime.Now;

            base.AddItem(entity);
        }

        protected override void UpdateItem(Project entity)
        {
            var oldEntity = this.Get(entity.ID);
            entity.Created = oldEntity.Created;
            entity.Updated = DateTime.Now;

            base.UpdateItem(entity);
        }
    }
}