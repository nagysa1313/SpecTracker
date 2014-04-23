using SpecTracker.Core.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SpecTracker.Core.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProjectService" in both code and config file together.
    [ServiceContract]
    public interface IProjectService
    {
        [OperationContract]
        IEnumerable<Project> ListProjects();

        [OperationContract]
        Project LoadProject(int id);

        [OperationContract]
        Project SaveProject(Project project);

        [OperationContract]
        bool RemoveProject(Project project);
    }
}