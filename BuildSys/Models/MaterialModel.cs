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
                    NotifyPropertyChanged("isService");
                }
            }
        }

        public MaterialModel(int materialId, String name, String unit, String pricePerUnit, Boolean isService)
        {
            this.materialId = materialId;
            this._name = name;
            this._unit = unit;
            this._pricePerUnit = pricePerUnit;
            this._isService = isService;
        }

        public MaterialModel() : this(getNextRowId("material_id", "materials"), "", "", "", false) { }

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
                materialList.Add(new MaterialModel(Int32.Parse(row["material_id"].ToString()), row["name"].ToString(), row["unit"].ToString(), row["price_per_unit"].ToString(), row["is_service"].ToString().ToCharArray()[0] == '1'));
            }

            return materialList;
        }

        public static ObservableCollection<MaterialModel> getDeletedMaterialList()
        {
            DataTable materialsTable = select("SELECT * FROM Materials WHERE status = 'I'");

            ObservableCollection<MaterialModel> materialList = new ObservableCollection<MaterialModel>();

            foreach (DataRow row in materialsTable.Rows)
            {
                materialList.Add(new MaterialModel(Int32.Parse(row["material_id"].ToString()), row["name"].ToString(), row["unit"].ToString(), row["price_per_unit"].ToString(), row["is_service"].ToString().ToCharArray()[0] == '1'));
            }

            return materialList;
        }

        public static MaterialModel getMaterial(int materialId)
        {
            DataTable materialsTable = select("SELECT * FROM Materials WHERE material_id = " + materialId);
            DataRow material = materialsTable.Rows[0];

            return new MaterialModel(Int32.Parse(material["material_id"].ToString()), material["name"].ToString(), material["unit"].ToString(), material["price_per_unit"].ToString(), material["is_service"].ToString().ToCharArray()[0] == '1');
        }

        public static void deleteMaterial(int materialId)
        {
            String sqlDelete = "Update Materials SET " +
                "status = 'I' " +
                " WHERE material_id = " + materialId;

            update(sqlDelete);
        }

        public static void restoreMaterial(int materialId)
        {
            String sqlDelete = "Update Materials SET " +
                "status = 'A' " +
                " WHERE material_id = " + materialId;

            update(sqlDelete);
        }

        public void insertMaterial()
        {
            String insertStmt = "INSERT INTO Materials (material_id, name, unit, price_per_unit, is_service) VALUES(" +
                materialId + ", '" +
                name + "', '" +
                unit + "', " +
                Double.Parse(pricePerUnit) + ", " +
                (isService? "'1'" : "'0'") +
            ")";

            insert(insertStmt);
        }

        public void update()
        {
            String sqlUpdate = "Update Materials SET " +
                "name = '" + name + "', " +
                "unit = '" + unit + "', " +
                "price_per_unit = " + Double.Parse(pricePerUnit) +
                ", is_service = " + (isService? "'1'" : "'0'" ) + 
                " WHERE material_id = " + materialId;

            update(sqlUpdate);
        }

        public static int getNumMaterials()
        {
            DataTable materialsTable = select("SELECT COUNT(material_id) FROM Materials WHERE status = 'A'");
            DataRow mat = materialsTable.Rows[0];

            return Int32.Parse(mat[0].ToString());
        }

        public static double getAvgMaterialCost()
        {
            DataTable materialsTable = select("SELECT ROUND(AVG(price_per_unit), 2) FROM Materials WHERE status = 'A'");
            DataRow mat = materialsTable.Rows[0];

            return Double.Parse(mat[0].ToString());
        }

        public static DataTable getMaterialsByCost ()
        {
            return select(
                "SELECT M.price_range as Material_Price_Range, COUNT(*) AS number_of_occurences " +
                    "FROM (" +
                      "SELECT CASE " +
                        "WHEN price_per_unit BETWEEN  0 and 10  THEN  '€0 - €10' " +
                        "WHEN price_per_unit BETWEEN 10 and 20  THEN '€10 - €20' " +
                        "WHEN price_per_unit BETWEEN 20 and 30  THEN '€20 - €30' " +
                        "WHEN price_per_unit BETWEEN 30 and 40  THEN '€30 - €40' " +
                        "WHEN price_per_unit BETWEEN 40 and 50  THEN '€40 - €50' " +
                        "WHEN price_per_unit BETWEEN 50 and 60  THEN '€50 - €60' " +
                        "WHEN price_per_unit BETWEEN 60 and 70  THEN '€60 - €70' " +
                        "WHEN price_per_unit BETWEEN 70 and 80  THEN '€70 - €80' " +
                        "WHEN price_per_unit BETWEEN 80 and 80  THEN '€80 - €90' " +
                        "WHEN price_per_unit BETWEEN 90 and 100 THEN '€90 - €100' " +
                        "ELSE 'Over €100' END AS price_range " +
                      "FROM Materials) M " +
                    "GROUP BY M.price_range " +
                    "ORDER BY Material_Price_Range"
            );
        }

        public static MaterialModel getMostUsedMaterial()
        {
            DataTable materialTable = select(
                "SELECT * FROM Materials " +
                  "WHERE material_id IN(SELECT material_id FROM( " +
                    "SELECT material_id " +
                      "FROM Quote_Materials " +
                      "GROUP BY material_id " +
                      "ORDER BY COUNT(material_id) DESC" +
                  ")) AND ROWNUM = 1"
            );

            if (materialTable.Rows.Count > 0)
            {
                DataRow material = materialTable.Rows[0];   
                return new MaterialModel(Int32.Parse(material["material_id"].ToString()), material["name"].ToString(), material["unit"].ToString(), material["price_per_unit"].ToString(), material["is_service"].ToString().ToCharArray()[0] == '1');
            }
            else
            {
                return new MaterialModel();
            }
        }
    }
}
