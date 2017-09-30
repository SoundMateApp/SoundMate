using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows;
using System.ComponentModel;
using SoundMateClient.ViewModels;
using SoundMateClient.Views;
using System.Windows.Forms;

namespace SoundMateClient
{
    public partial class App : System.Windows.Application
    {
        private static readonly ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static MainWindow app;
        public static NotifyIcon icon;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Log.Info("Application Startup");

            // For catching Global uncaught exception
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptionOccured);

            Log.Info("Starting App");
            LogMachineDetails();
            app = new MainWindow();
            var context = new MainViewModel();
            app.DataContext = context;
            app.Show();

            App.icon = new NotifyIcon();
            //icon.Click += new EventHandler(icon_Click);
            icon.Icon = SoundMateClient.Properties.Resources.icon;
            icon.Visible = true;

            if (e.Args.Length == 1) //make sure an argument is passed
            {
                Log.Info("File type association: " + e.Args[0]);
                FileInfo file = new FileInfo(e.Args[0]);
                if (file.Exists) //make sure it's actually a file
                {
                    // Here, add you own code
                    // ((MainViewModel)app.DataContext).OpenFile(file.FullName);
                }
            }
        }

        static void UnhandledExceptionOccured(object sender, UnhandledExceptionEventArgs args)
        {
            // Here change path to the log.txt file
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                                                    + "\\user\\SoundMateClient\\log.txt";

            // Show a message before closing application
            var dialogService = new MvvmDialogs.DialogService();
            dialogService.ShowMessageBox((INotifyPropertyChanged)(app.DataContext),
                "Oops, something went wrong and the application must close. Please find a " +
                "report on the issue at: " + path + Environment.NewLine +
                "If the problem persist, please contact user.",
                "Unhandled Error",
                MessageBoxButton.OK);

            Exception e = (Exception)args.ExceptionObject;
            Log.Fatal("Application has crashed", e);
        }

        private void LogMachineDetails()
        {
            var computer = new Microsoft.VisualBasic.Devices.ComputerInfo();

            string text = "OS: " + computer.OSPlatform + " v" + computer.OSVersion + Environment.NewLine +
                          computer.OSFullName + Environment.NewLine +
                          "RAM: " + computer.TotalPhysicalMemory.ToString() + Environment.NewLine +
                          "Language: " + computer.InstalledUICulture.EnglishName;
            Log.Info(text);
        }
    }
}
