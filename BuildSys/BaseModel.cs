using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BuildSys
{
    public class BaseModel : INotifyPropertyChanged
    {
        //public const string CONNECTION_STRING = "Data Source = oracle/orcl; User Id = T00119683; Password = w8qqptj9;";
        public const string CONNECTION_STRING = "Data Source = Localhost; User Id = T00119683; Password = w8qqptj9;";

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
