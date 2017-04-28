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

        public SettingsViewModel (BaseViewModel parent)
        {
            this.parent = parent;

            setting = SettingModel.getSetting();

            // Check is setting exists
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

        // Saves or updates the setting
        public void saveSetting()
        {
            // Force all props to be validated
            setting.validateAllProps();

            // If no errors, insert
            if (!setting.HasErrors)
            {
                setting.insertSetting();
                MessageBox.Show("Sucessfully Saved Settings");
            }
        }

        // Used to enable and disable the save button
        public Boolean canSaveSetting()
        {
            return !setting.HasErrors;
        }
    }
}
