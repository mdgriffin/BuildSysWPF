using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;

namespace BuildSys.Models
{
    /*
     *  Models a Material as part of a quote and Provides access to Database functions
     */

    public class QuoteMaterialModel: BaseModel, INotifyDataErrorInfo
    {
        // If a Quote Material has an id, it has already been saved
        public int? quoteMaterialId; // Primary Key
        public int quoteId; // Foreign key to quotes
        public int materialId; // Foreign key to materials

        // used in conjunction with the quote material list to get the specific index of a list item
        public int? _listIndex;
        public int? listIndex {
            get
            {
                return _listIndex;
            }
            set
            {
                if (value != _listIndex)
                {
                    _listIndex = value.Value;
                    NotifyPropertyChanged("listIndex");
                }
            }
        }

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
                    validateProp("description");
                }
            }
        }

        public String _numUnits;
        public String numUnits
        {
            get
            {
                return _numUnits;
            }
            set
            {
                if (value != _numUnits)
                {
                    _numUnits = value;
                    validateProp("numUnits");
                    if (Validator.isNumeric(numUnits))
                    {
                        this.totalCost = Int32.Parse(numUnits) * Double.Parse(pricePerUnit);
                    }
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
                    validateProp("pricePerUnit");
                    if (Validator.isPrice(pricePerUnit))
                    {
                        this.totalCost = Int32.Parse(numUnits) * Double.Parse(pricePerUnit);
                    }
                    
                }
            }
        }

        private Boolean _isService;
        public Boolean isService
        {
            get
            {
                return _isService;
            }
            set
            {
                if (value != _isService)
                {
                    _isService = value;
                    //NotifyPropertyChanged("isService");
                }
            }
        }

        private double _totalCost;
        public double totalCost
        {
            get
            {
                return _totalCost;
            }
            set
            {
                if (value != _totalCost)
                {
                    _totalCost = value;
                    NotifyPropertyChanged("totalCost");
                }
            }
        }

        public override void validateAllProps()
        {
            validateProp("description");
            validateProp("numUnits");
        }

        public override void validateProp(String propertyName)
        {
            String errorMessage = "";

            switch (propertyName)
            {
                case "description":
                    if (Validator.isEmpty(description))
                    {
                        errorMessage = Validator.ERROR_IS_EMPTY;
                    }
                    break;
                case "pricePerUnit":
                    if (Validator.isEmpty(pricePerUnit))
                    {
                        errorMessage = Validator.ERROR_IS_EMPTY;
                    } else if (!Validator.isPrice(pricePerUnit))
                    {
                        errorMessage = Validator.ERROR_IS_PRICE;
                    } else if (Double.Parse(pricePerUnit) == 0)
                    {
                        errorMessage = "Price Per Unit must be greater than 0";
                    }
                    break;
                case "numUnits":
                    if (Validator.isEmpty(numUnits.ToString()))
                    {
                        errorMessage = Validator.ERROR_IS_EMPTY;
                    }
                    else if (!Validator.isNumeric(numUnits.ToString()))
                    {
                        errorMessage = Validator.ERROR_IS_NUMERIC;

                    }
                    else if (!Validator.isNumeric(numUnits))
                    {
                        errorMessage = Validator.ERROR_IS_NUMERIC;
                    }
                    else if (Int32.Parse(numUnits) == 0)
                    {
                        errorMessage = "Unit must be greater than 0";
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

        /*
        public void notifyQuoteMaterialUpdated ()
        {
           
            NotifyPropertyChanged("description");
            NotifyPropertyChanged("pricePerUnit");
            NotifyPropertyChanged("numUnits");
            NotifyPropertyChanged("totalCost");
        }
        */

        public QuoteMaterialModel (int quoteMaterialId, int quoteId, int materialId, String description, String pricePerUnit, String numUnits, Boolean isService)
        {
            this.quoteMaterialId = quoteMaterialId;
            this.quoteId = quoteId;
            this.materialId = materialId;
            this._description = description;
            this._numUnits = numUnits;
            this._pricePerUnit = pricePerUnit;
            this._totalCost = Int32.Parse(numUnits) * Double.Parse(pricePerUnit);
            this._isService = isService;
        }

        public QuoteMaterialModel(int quoteId, int materialId, String pricePerUnit, Boolean isService) : this(0, quoteId, materialId, "", pricePerUnit, "0", isService) { }

        public QuoteMaterialModel clone ()
        {
            if (quoteMaterialId == null)
            {
                return new QuoteMaterialModel(quoteId, materialId, materialId, description, pricePerUnit, numUnits, isService);
            }
            else
            {
                return new QuoteMaterialModel(quoteMaterialId.Value, materialId, materialId, description, pricePerUnit, numUnits, isService);
            }
            
        }

        public static ObservableCollection<QuoteMaterialModel> getQuoteMaterialList(int quoteId)
        {
            DataTable quoteMaterialsTable = select("SELECT * FROM Quote_Materials WHERE quote_id = " + quoteId);
            ObservableCollection<QuoteMaterialModel> quoteMaterialsList = new ObservableCollection<QuoteMaterialModel>();

            int rowIndex = 0;

            foreach (DataRow row in quoteMaterialsTable.Rows)
            {
                QuoteMaterialModel qm = new QuoteMaterialModel(
                    Int32.Parse(row["quote_material_id"].ToString()),
                    Int32.Parse(row["quote_id"].ToString()),
                    Int32.Parse(row["material_id"].ToString()),
                    row["description"].ToString(),
                    row["price_per_unit"].ToString(),
                    row["num_units"].ToString(),
                    row["is_service"].ToString() == "1"
                );

                qm.listIndex = rowIndex++;

                quoteMaterialsList.Add(qm);
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
                quoteMat["price_per_unit"].ToString(),
                quoteMat["num_units"].ToString(),
                quoteMat["is_service"].ToString() == "1"
            );
        }

        public static void delete(int quoteMaterialId)
        {
            delete("DELETE FROM Quote_Materials WHERE quote_material_id = " + quoteMaterialId);
        }

        public void insertMaterial()
        {
            insert("INSERT INTO Quote_Materials VALUES(" +
               getNextRowId("quote_material_id", "Quote_Materials") + ", " +
               quoteId + ", " +
               materialId + ", '" +
               description + "', " +
               pricePerUnit + ", " +
               numUnits + ", " +
               (isService? "1" : "0") +
            " )");
        }

        public void updateMaterial()
        {
            String sqlUpdate = "Update Quote_materials SET " +
                "description = '" + description + "', " +
                "price_per_unit = " + numUnits +
                " WHERE quote_material_id = " + quoteMaterialId;

            update(sqlUpdate);
        }
    }
}
