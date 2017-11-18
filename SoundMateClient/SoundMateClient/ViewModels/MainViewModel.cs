using MvvmDialogs;
using log4net;
using MvvmDialogs.FrameworkDialogs.OpenFile;
using MvvmDialogs.FrameworkDialogs.SaveFile;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Input;
using System.Xml.Linq;
using SoundMateClient.Views;
using SoundMateClient.Utils;
using System.Diagnostics;
using SoundMateClient.Controls;
using System.Windows;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace SoundMateClient.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        #region Parameters
        private readonly IDialogService DialogService;

        /// <summary>
        /// Title of the application, as displayed in the top bar of the window
        /// </summary>
        public string Title {
            get { return "SoundMate Client"; }
        }

        public ObservableCollection<ProcessList> Processes {
            get;
            set;
        }

        private string dataInput = "";
        public string DataInput {
            get { return dataInput; }
            set {
                if (dataInput != value) {
                    dataInput = value;
                    NotifyPropertyChanged("DataInput");
                }
            }
        }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            // DialogService is used to handle dialogs
            this.DialogService = new MvvmDialogs.DialogService();
            OnLoad();
            Network x = new Network();
            Task.Factory.StartNew(() => x.Start(ref dataInput));
        }

        #endregion

        #region Methods
        public void OnLoad()
        {
            Process[] localAll = Process.GetProcesses();
            Process[] processlist = Process.GetProcesses();

            ObservableCollection<ProcessList> processes = new ObservableCollection<ProcessList>();
            foreach (Process theprocess in processlist) {            
                if (VolumeMixer.GetApplicationVolume(theprocess.Id) >= 0) {
                    processes.Add(new ProcessList(theprocess.ProcessName, theprocess.Id, VolumeMixer.GetApplicationVolume(theprocess.Id)));
                }
            }
            Processes = processes;
        }
        #endregion

        #region Commands
        public RelayCommand<object> test { get { return new RelayCommand<object>(OnTest); } }

        
        private void OnTest(object obj)
        {
            

        }

        #endregion

        #region Events

        #endregion
    }

    public class ProcessList : ViewModelBase
    {
        public ProcessList(string name, int id, float volume)
        {
            this.name = name;
            this.id = id;
            this.volume = volume;
        }

        private string name = "";
        public string Name {
            get {
                return name;
            }
            set {
                if (name != value) {
                    name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        private int id = 0;
        public int ID {
            get {
                return id;
            }
            set {
                if (id != value) {
                    id = value;
                    NotifyPropertyChanged("ID");
                }
            }
        }

        private float volume;
        public float Volume {
            get { return volume; }
            set {
                if (volume != value) {
                    volume = value;
                    VolumeMixer.SetApplicationVolume(this.id, volume);
                    NotifyPropertyChanged("Volume");
                }
            }
        }
        
    }
}
