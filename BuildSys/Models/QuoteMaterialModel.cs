using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;

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

        public static ObservableCollection<QuoteMaterialModel> getQuoteMaterialList()
        {
            DataTable quoteMaterialsTable = select("SELECT * FROM Quote_Materials");

            ObservableCollection<QuoteMaterialModel> quoteMaterialsList = new ObservableCollection<QuoteMaterialModel>();

            foreach (DataRow row in quoteMaterialsTable.Rows)
            {
                quoteMaterialsList.Add(new QuoteMaterialModel(
                    Int32.Parse(row["quote_material_id"].ToString()),
                    Int32.Parse(row["quote_id"].ToString()),
                    Int32.Parse(row["material_id"].ToString()),
                    row["description"].ToString(),
                    row["price_per_unit"].ToString()
                ));
            }

            return quoteMaterialsList;
        }

        public static QuoteMaterialModel getMaterial(int quoteMaterialId)
        {
            DataTable quoteMaterialsTable = select("SELECT * FROM Quote_Materials WHERE quote_material_id = " + quoteMaterialId);
            DataRow quoteMat = quoteMaterialsTable.Rows[0];

            return new QuoteMaterialModel(
                Int32.Parse(quoteMat["quote_material_id"].ToString()),
                Int32.Parse(quoteMat["quote_id"].ToString()),
                Int32.Parse(quoteMat["material_id"].ToString()),
                quoteMat["description"].ToString(),
                quoteMat["price_per_unit"].ToString()
            );
        }

        public static void delete(int quoteMaterialId)
        {
            delete("DELETE FROM Quote_Materials WHERE quote_material_id = " + quoteMaterialId);
        }

        public void insertMaterial()
        {
            insert("INSERT INTO Materials VALUES('" +
               quoteMaterialId + "', '" +
               quoteId + "', '" +
               materialId + "', '" +
               description + "', " +
               Double.Parse(pricePerUnit) +
            ")");
        }

        public void update()
        {
            String sqlUpdate = "Update Quote_materials SET " +
                "description = '" + description + "', " +
                "price_per_unit = " + Double.Parse(pricePerUnit) +
                " WHERE quote_material_id = " + quoteMaterialId;

            update(sqlUpdate);
        }
    }
}
