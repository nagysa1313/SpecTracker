using FirstFloor.ModernUI.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SpecTracker.WPF.ProjectService;
using System;
using System.ServiceModel;
using System.Windows;
using SpecTracker.WPF;
using System.Threading.Tasks;
using SpecTracker.WPF.Utils;

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
        public bool IsConnected { get { return ServiceClientFactory.IsConnected; } }

        public RelayCommand<bool> InitConnection { get; private set; }

        private bool _isConnecting;

        public bool IsConnecting
        {
            get { return _isConnecting; }
            set
            {
                Set(() => IsConnecting, ref _isConnecting, value);
                InitConnection.RaiseCanExecuteChanged();
            }
        }

        private void RaiseConnectionStateChange()
        {
            RaisePropertyChanged(() => IsConnected);
            InitConnection.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            InitConnection = new RelayCommand<bool>((standalone) =>
                {
                    IsConnecting = true;

                    Task.Factory.StartNew(() =>
                        {
                            ServiceClientFactory.CloseAllConnection();

                            ServiceClientFactory.Standalone = standalone;

                            ServiceClientFactory.CreateConnection();

                            RaiseConnectionStateChange();
                        }).ContinueWith(task =>
                            {
                                IsConnecting = false;
                                App.Current.Dispatcher.Invoke(() => InitApplication());
                            });
                }, (standalone) => !IsConnecting && !IsConnected);
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