using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace BuildSys
{
    public class BaseModel : INotifyPropertyChanged
    {
        public const string CONNECTION_STRING = "Data Source = oracle/orcl; User Id = T00119683; Password = w8qqptj9;";

        //public const string CONNECTION_STRING = "Data Source = Localhost; User Id = T00119683; Password = w8qqptj9;";

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

            this.validate();
        }

        /*
         * Each class will implement their own validate method
         */
        protected virtual void validate ()  {}

        Dictionary<string, List<string>> propErrors = new Dictionary<string, List<string>>();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            List<string> errors = new List<string>();

            if (propertyName != null)
            {
                propErrors.TryGetValue(propertyName, out errors);
                return errors;
            }
            else
            {
                return null;
            }
        }

        public bool HasErrors
        {
            get
            {
                try
                {
                    var propErrorsCount = propErrors.Values.FirstOrDefault(r => r.Count > 0);
                    if (propErrorsCount != null)
                        return true;
                    else
                        return false;
                }
                catch { }
                return true;
            }
        }

        public void AddError(string propertyName, string error)
        {
            this.propErrors[propertyName] = new List<string>() { error };
            this.NotifyErrorsChanged(propertyName);
        }

        public void RemoveError(string propertyName)
        {
            if (this.propErrors.ContainsKey(propertyName))
                this.propErrors.Remove(propertyName);
            this.NotifyErrorsChanged(propertyName);
        }

        public void NotifyErrorsChanged(string propertyName)
        {
            if (this.ErrorsChanged != null)
            {
                this.ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }
    }
}
