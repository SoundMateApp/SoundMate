using MvvmDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundMateClient.ViewModels
{
    class AboutViewModel : ViewModelBase, IModalDialogViewModel
    {
        public bool? DialogResult { get { return false; } }

        public string Content {
            get {
                return "SoundMateClient" + Environment.NewLine +
                        "Created by user" + Environment.NewLine +
                        "Address" + Environment.NewLine +
                        "2017";
            }
        }

        public string VersionText {
            get {
                var version1 = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

                // For external assemblies
                // var ver2 = typeof(Assembly1.ClassOfAssembly1).Assembly.GetName().Version;
                // var ver3 = typeof(Assembly2.ClassOfAssembly2).Assembly.GetName().Version;

                return "SoundMateClient v" + version1.ToString();
            }
        }
    }
}
