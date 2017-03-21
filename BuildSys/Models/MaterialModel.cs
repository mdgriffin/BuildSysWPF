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
        private int materialId;

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

        public String _unit;
        public String unit
        {
            get
            {
                return _unit;
            }
            set
            {
                if (value != _unit)
                {
                    _unit = value;
                    validateProp("unit");
                    NotifyPropertyChanged("unit");
                }
            }
        }

        public double _pricePerUnit;
        public double pricePerUnit
        {
            get
            {
                return _pricePerUnit;
            }
            set
            {
                if (value != _pricePerUnit)
                {
                    _pricePerUnit = value;
                    validateProp("pricePerUnit");
                    NotifyPropertyChanged("pricePerUnit");
                }
            }
        }

        public MaterialModel(int materialId, String name, String unit, double pricePerUnit)
        {
            this.materialId = materialId;
            this._name = name;
            this._unit = unit;
            this._pricePerUnit = pricePerUnit;
        }

        public MaterialModel() : this(getNextRowId("material_id", "materials"), "", "", 0) { }

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
