using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SpecTracker.Services
{
    [RunInstaller(true)]
    public class ProjectInstaller : Installer
    {
        private ServiceProcessInstaller process;
        private ServiceInstaller service;

        public ProjectInstaller()
        {
            process = new ServiceProcessInstaller();
            process.Account = ServiceAccount.LocalSystem;
            service = new ServiceInstaller();
            Installers.Add(process);
            Installers.Add(service);
        }

        public override void Install(System.Collections.IDictionary stateSaver)
        {
            service.ServiceName = this.Context.Parameters["ServiceName"] ?? "SpecTracker.ProjectService";
            service.DisplayName = this.Context.Parameters["DisplayName"] ?? "SpecTracker.ProjectService";

            base.Install(stateSaver);
        }

        public override void Uninstall(System.Collections.IDictionary savedState)
        {
            service.ServiceName = this.Context.Parameters["ServiceName"] ?? "SpecTracker.ProjectService";
            service.DisplayName = this.Context.Parameters["DisplayName"] ?? "SpecTracker.ProjectService";

            base.Uninstall(savedState);
        }
    }
}