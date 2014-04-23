using SharpRepository.Repository;
using SpecTracker.Core.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SpecTracker.Core.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProjectService" in both code and config file together.
    public class ProjectService : IProjectService
    {
        IRepository<Project, int> projectRepository;

        public ProjectService()
        {
            projectRepository = RepositoryFactory.GetInstance<Project>();
        }

        public IEnumerable<Project> ListProjects()
        {
            return projectRepository.GetAll()
                .Select(s => new Project() 
                        { 
                            ID = s.ID,
                            Name = s.Name,
                            Created = s.Created,
                            Updated = s.Updated
                        }); // to block later complex objects from downloading to clients
        }

        public Project LoadProject(int id)
        {
            return projectRepository.Get(id);
        }

        public Project SaveProject(Project project)
        {
            if (projectRepository.Exists(project.ID))
            {
                // concurrency check
                var storedProj = projectRepository.Get(project.ID);
                if (storedProj.Updated <= project.Updated)
                    projectRepository.Update(project);
                else throw new InvalidOperationException("Someone changed your object while you edited you copy! Refresh, solve the conflicts at your side and try again!");
            }
            else projectRepository.Add(project);

            return projectRepository.Get(project.ID);
            // reloading the saved object, there is possibility to exist changed properties after applying business logic
        }

        public bool RemoveProject(Project project)
        {
            if (projectRepository.Exists(project.ID))
            {
                // concurrency check
                var storedProj = projectRepository.Get(project.ID);
                if (storedProj.Updated <= project.Updated)
                    projectRepository.Delete(project);
                else throw new InvalidOperationException("Someone changed your object while you edited you copy! Refresh, solve the conflicts at your side and try again!");

                return true;
            }
            else return false;
        }
    }
}