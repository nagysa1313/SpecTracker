using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SpecTracker.Core.DAL;
using SpecTracker.WPF.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecTracker.WPF.ViewModel
{
    public class ProjectListViewModel : ViewModelBase
    {
        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                Set(() => IsBusy, ref _isBusy, value);
                Refresh.RaiseCanExecuteChanged();
            }
        }

        private ObservableCollection<Project> _projects;

        public ObservableCollection<Project> Projects
        {
            get { return _projects; }
            set { Set(() => Projects, ref _projects, value); }
        }

        private Project _selectedProject;

        public Project SelectedProject
        {
            get { return _selectedProject; }
            set { Set(() => SelectedProject, ref _selectedProject, value); }
        }

        public RelayCommand Refresh { get; private set; }

        public ProjectListViewModel()
        {
            Projects = new ObservableCollection<Project>();

            Refresh = new RelayCommand(() =>
                {
                    if (IsBusy) return;

                    IsBusy = true;

                    Projects.Clear();
                    ServiceClientFactory.Client.ListProjectsAsync().ContinueWith(task =>
                        {
                            App.Current.Dispatcher.Invoke(() =>
                                {
                                    foreach (var proj in task.Result)
                                    {
                                        Projects.Add(proj);
                                    }
                                });
                            IsBusy = false;
                        });
                }, () => !IsBusy);
        }
    }
}