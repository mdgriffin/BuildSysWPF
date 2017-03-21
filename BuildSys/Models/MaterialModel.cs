using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSys.Models
{
    class MaterialModel: BaseModel, INotifyDataErrorInfo
    {
        public int materialId { get; set; }

        public String _name;
        public String name
        {
            get
            {
                return this._name;
            }
            set
            {
                if (value != _name)
                {
                    this._name = value;
                    validateProp("name");
                    NotifyPropertyChanged("name");
                }
            }
        }

        // TODO: All Props: Unit String, pricePerUnit double
        // TODO: All Two constructors, a four arg, and a no arg
        // TODO: Add methods for saving, updating, inserting and deleting

        public override void validateAllProps()
        {
            validateProp("name");
        }

        public override void validateProp(String propertyName)
        {
            String errorMessage = "";

            switch (propertyName)
            {
                case "name":
                    if (Validator.isEmpty(name))
                    {
                        errorMessage = Validator.ERROR_IS_VAT_NUM;
                    }
                    break;
            }
            if (errorMessage != "")
            {
                AddError(propertyName, errorMessage);
            }
            else
            {
                RemoveError(propertyName);
            }
        }
    }
}
