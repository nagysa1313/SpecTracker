using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SpecTracker.Services
{
    public partial class ProjectService : ServiceBase
    {
        ServiceHost host = null;

        public ProjectService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Start(args);
        }

        protected override void OnStop()
        {
            if (host != null)
            {
                host.Close();
                host = null;
            }
        }

        internal void Start(string[] args)
        {
            if (host != null)
            {
                host.Close();
            }

            host = new ServiceHost(typeof(SpecTracker.Core.Service.ProjectService));

            host.Open();
        }
    }
}