using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSys.ViewModels
{
    class SettingsViewModel : BaseViewModel
    {

        MainViewModel parent;

        public SettingsViewModel (MainViewModel parent)
        {
            this.parent = parent;
        }
    }
}
