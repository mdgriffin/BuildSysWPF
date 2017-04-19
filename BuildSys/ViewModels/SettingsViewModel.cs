using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildSys.Models;
using System.Windows;
using System.Windows.Input;

namespace BuildSys.ViewModels
{
    class SettingsViewModel : BaseViewModel
    {

        //MainViewModel parent;

        public SettingsViewModel (BaseViewModel parent)
        {
            this.parent = parent;

            setting = SettingModel.getSetting();

            if (setting == null)
            {
                setting = new SettingModel();
            }
        }

        public SettingModel setting { get; set; }
        public ICommand saveSettingCommand
        {
            get
            {
                return new RelayCommand(param => saveSetting(), param => canSaveSetting());
            }
        }

        public void saveSetting()
        {
            setting.validateAllProps();

            if (!setting.HasErrors)
            {
                setting.insertSetting();
                MessageBox.Show("Sucessfully Saved Settings");
            }
        }

        public Boolean canSaveSetting()
        {
            return !setting.HasErrors;
        }
    }
}
