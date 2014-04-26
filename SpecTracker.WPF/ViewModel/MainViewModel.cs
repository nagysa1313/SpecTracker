using FirstFloor.ModernUI.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SpecTracker.WPF.ProjectService;
using System;
using System.ServiceModel;
using System.Windows;
using SpecTracker.WPF;
using System.Threading.Tasks;

namespace SpecTracker.WPF.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private ServiceHost serviceHost;
        private ProjectServiceClient serviceClient;

        public bool Standalone { get { return serviceHost == null; } }
        public ProjectServiceClient ServiceClient { get { return serviceClient; } }
        public bool IsConnected { get { return serviceClient != null && serviceClient.State == CommunicationState.Opened; } }

        public RelayCommand InitStandalone { get; private set; }
        public RelayCommand InitConnected { get; private set; }

        private bool _isConnecting;

        public bool IsConnecting
        {
            get { return _isConnecting; }
            set
            {
                Set(() => IsConnecting, ref _isConnecting, value);
                InitStandalone.RaiseCanExecuteChanged();
                InitConnected.RaiseCanExecuteChanged();
            }
        }

        private void RaiseConnectionStateChange()
        {
            RaisePropertyChanged(() => Standalone);
            RaisePropertyChanged(() => ServiceClient);
            RaisePropertyChanged(() => IsConnected);
            InitStandalone.RaiseCanExecuteChanged();
            InitConnected.RaiseCanExecuteChanged();
        }

        private void CloseAllConnection()
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

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            InitStandalone = new RelayCommand(() =>
                {
                    IsConnecting = true;

                    Task.Factory.StartNew(() =>
                        {
                            CloseAllConnection();

                            var localUri = new Uri("http://localhost:8733/Design_Time_Addresses/SpecTracker.Core.Service/ProjectService/");

                            serviceHost = new ServiceHost(typeof(SpecTracker.Core.Service.ProjectService), localUri);

                            serviceHost.Open();

                            serviceClient = new ProjectServiceClient(new BasicHttpBinding(), new EndpointAddress(localUri));

                            serviceClient.Open();

                            RaiseConnectionStateChange();
                        }).ContinueWith(task =>
                            {
                                IsConnecting = false;
                                App.Current.Dispatcher.Invoke(() => InitApplication());
                            });
                }, () => !IsConnecting && !IsConnected);

            InitConnected = new RelayCommand(() =>
                {
                    IsConnecting = true;

                    Task.Factory.StartNew(() =>
                        {
                            CloseAllConnection();

                            serviceClient = new ProjectServiceClient();

                            serviceClient.Open();

                            RaiseConnectionStateChange();
                        }).ContinueWith(task =>
                            {
                                IsConnecting = false;
                                App.Current.Dispatcher.Invoke(() => InitApplication());
                            });
                }, () => !IsConnecting && !IsConnected);
        }

        private void InitApplication()
        {
            var modernWindow = App.Current.MainWindow as ModernWindow;

            var mainMenu = App.Current.Resources["MainMenu"] as FirstFloor.ModernUI.Presentation.LinkGroupCollection;
            modernWindow.MenuLinkGroups = mainMenu;
            modernWindow.ContentSource = new Uri("../Pages/ProjectList.xaml", UriKind.Relative);
        }
    }
}