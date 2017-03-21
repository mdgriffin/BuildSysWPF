using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSys.Models
{
    class QuoteMaterialModel: BaseModel, INotifyDataErrorInfo
    {
        private int quoteMaterialId; // Primary Key
        private int quoteId; // Foreign key to quotes
        private int materialId; // Foreign key to materials
        
        public String _description;
        public String description
        {
            get
            {
                return _description;
            }
            set
            {
                if (value != _description)
                {
                    _description = value;
                }
            }
        }

        public String _pricePerUnit;
        public String pricePerUnit
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
                }
            }
        }

        public override void validateAllProps()
        {
            validateProp("description");
            validateProp("pricePerUnit");
        }

        public override void validateProp(String propertyName)
        {
            String errorMessage = "";

            switch (propertyName)
            {
                case "description":
                    if (Validator.isEmpty(description))
                    {
                        errorMessage = Validator.ERROR_IS_VAT_NUM;
                    }
                    break;
                case "pricePerUnit":
                    if (Validator.isEmpty(pricePerUnit))
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

        public QuoteMaterialModel (int quoteMaterialId, int quoteId, int materialId, String description, String pricePerUnit)
        {
            this.quoteMaterialId = quoteMaterialId;
            this.quoteId = quoteId;
            this.materialId = materialId;
            this._description = description;
            this._pricePerUnit = pricePerUnit;
        }

        public QuoteMaterialModel (int quoteId, int materialId) : this(getNextRowId("quote_material_id", "Quote_Materials"), quoteId, materialId, "", "") {  }
    }
}
