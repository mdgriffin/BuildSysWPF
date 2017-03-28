using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;

namespace BuildSys.Models
{
    public class MaterialModel: BaseModel, INotifyDataErrorInfo
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
                    NotifyPropertyChanged("pricePerUnit");
                }
            }
        }

        public MaterialModel(int materialId, String name, String unit, String pricePerUnit)
        {
            this.materialId = materialId;
            this._name = name;
            this._unit = unit;
            this._pricePerUnit = pricePerUnit;
        }

        public MaterialModel() : this(getNextRowId("material_id", "materials"), "", "", "") { }

        public override void validateAllProps()
        {
            validateProp("name");
            validateProp("unit");
            validateProp("pricePerUnit");
        }

        public override void validateProp(String propertyName)
        {
            String errorMessage = "";

            switch (propertyName)
            {
                case "name":
                    if (Validator.isEmpty(name))
                    {
                        errorMessage = Validator.ERROR_IS_EMPTY;
                    }
                    break;
                case "unit":
                    if (Validator.isEmpty(unit))
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

        public static ObservableCollection<MaterialModel> getMaterialList()
        {
            DataTable materialsTable = select("SELECT * FROM Materials WHERE status = 'A'");

            ObservableCollection<MaterialModel> materialList = new ObservableCollection<MaterialModel>();

            foreach (DataRow row in materialsTable.Rows)
            {
                materialList.Add(new MaterialModel(Int32.Parse(row["material_id"].ToString()), row["name"].ToString(), row["unit"].ToString(), row["price_per_unit"].ToString()));
            }

            return materialList;
        }

        public static MaterialModel getMaterial(int materialId)
        {
            DataTable materialsTable = select("SELECT * FROM Materials WHERE material_id = " + materialId);
            DataRow material = materialsTable.Rows[0];

            return new MaterialModel(Int32.Parse(material["material_id"].ToString()), material["name"].ToString(), material["unit"].ToString(), material["price_per_unit"].ToString());
        }

        public static void deleteMaterial(int materialId)
        {
            String sqlDelete = "Update Materials SET " +
                "status = 'I' " +
                " WHERE material_id = " + materialId;

            update(sqlDelete);
        }

        public void insertMaterial()
        {
            String insertStmt = "INSERT INTO Materials (material_id, name, unit, price_per_unit) VALUES(" +
                materialId + ", '" +
               name + "', '" +
               unit + "', " +
               Double.Parse(pricePerUnit) +
            ")";

            insert(insertStmt);
        }

        public void update()
        {
            String sqlUpdate = "Update Materials SET " +
                "name = '" + name + "', " +
                "unit = '" + unit + "', " +
                "price_per_unit = " + Double.Parse(pricePerUnit) +
                " WHERE material_id = " + materialId;

            update(sqlUpdate);
        }
    }
}
