using SpecTracker.Core.DAL;
using SpecTracker.WPF.ProjectService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SpecTracker.WPF.Utils
{
    public static class ServiceClientFactory
    {
        private static ServiceHost serviceHost;
        private static ProjectServiceClient serviceClient;

        public static bool Standalone { get; set; }
        public static bool IsConnected { get { return serviceClient != null && serviceClient.State == CommunicationState.Opened; } }

        public static ProjectServiceClient Client
        {
            get
            {
                if (serviceClient == null)
                {
                    CreateConnection();
                }

                return serviceClient;
            }
        }

        public static void CreateConnection()
        {
            if (Standalone)
            {
                var localUri = new Uri("http://localhost:8733/Design_Time_Addresses/SpecTracker.Core.Service/ProjectService/");

                serviceHost = new ServiceHost(typeof(SpecTracker.Core.Service.ProjectService), localUri);

                serviceHost.Open();

                serviceClient = new ProjectServiceClient(new BasicHttpBinding(), new EndpointAddress(localUri));
            }
            else serviceClient = new ProjectServiceClient();

            serviceClient.Open();
        }

        public static void CloseAllConnection()
        {
            if (serviceClient != null)
            {
                serviceClient.Close();

                serviceClient = null;
            }

            if (serviceHost != null)
            {
                serviceHost.Close();

                serviceHost = null;
            }
        }
    }
}