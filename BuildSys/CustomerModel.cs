using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSys
{

    class CustomerModel : BaseModel
    {
        public int customerId;
        public String companyName;
        public String title;
        public String firstname;
        public String surname;
        public String street;
        public String town;
        public String county;
        public String telno;
        public String email;
        public String vatNo;
        public char accountType;

        public Dictionary<String, String> errors;


        public CustomerModel(String title, String firstname, String surname, String street, String town, String county, String telno, String email, char accountType, String companyName, String vatNo) 
        {
            this.customerId = getNextCustomerId();
            this.companyName = companyName;
            this.title = title;
            this.firstname = firstname;
            this.surname = surname;
            this.street = street;
            this.town = town;
            this.county = county;
            this.telno = telno;
            this.email = email;
            this.vatNo = vatNo;
            this.accountType = accountType;
        }

        public CustomerModel(String title, String firstname, String surname, String street, String town, String county, String telno, String email, char accountType): this(title, firstname, surname, street, town, county, telno, email, accountType, null, null) {}

        public Boolean validates ()
        {
            Boolean valid = true;

            if (!Validator.isEmpty(firstname))
            {
                errors.Add("firstname", Validator.ERROR_IS_EMPTY);
            }

            if (Validator.isEmpty(surname))
            {
                errors.Add("surname", Validator.ERROR_IS_EMPTY);
            }

            if (Validator.isEmpty(street))
            {
                errors.Add("street", Validator.ERROR_IS_EMPTY);
            }

            if (Validator.isEmpty(town))
            {
                errors.Add("town", Validator.ERROR_IS_EMPTY);
            }

            if (Validator.isNumeric(telno))
            {
                errors.Add("telno", Validator.ERROR_IS_EMAIL);
            }

            if (Validator.isEmail(email))
            {
                errors.Add("telno", Validator.ERROR_IS_EMPTY);
            }

            if (accountType != 'P')
            {
                if (Validator.isEmpty(companyName))
                {
                    errors.Add("companyName", Validator.ERROR_IS_EMPTY);
                }

                if (Validator.isVatNum(vatNo))
                {
                    errors.Add("vatNo", Validator.ERROR_IS_VAT_NUM);
                }
            }

            if (errors.Count() > 0)
            {
                valid = false;
            }

            return valid;
        }

        public static DataTable getCustomers()
        {
            // Open the DB connection
            OracleConnection conn = new OracleConnection(CONNECTION_STRING);
            conn.Open();

            OracleCommand cmd = new OracleCommand("SELECT * FROM Customers", conn);

            OracleDataAdapter da = new OracleDataAdapter(cmd);

            // Create an empty data set
            DataSet ds = new DataSet();

            da.Fill(ds, "customers");

            // Close Database
            conn.Close();

            return ds.Tables["customers"];
        }

        public static int getNextCustomerId()
        {
            int nextStockNo;

            // Open the DB connection
            OracleConnection conn = new OracleConnection(CONNECTION_STRING);
            conn.Open();

            String selectMaxStmt = "SELECT MAX(customer_id) FROM Customers";

            OracleCommand cmd = new OracleCommand(selectMaxStmt, conn);

            OracleDataReader dr = cmd.ExecuteReader();

            // Read the first (only) value returned from the query
            // If first stockNo, assign value of 1, otherwise add 1
            dr.Read();

            if (dr.IsDBNull(0))
            {
                nextStockNo = 1;
            }
            else
            {
                nextStockNo = Convert.ToInt32(dr.GetValue(0)) + 1;
            }

            // Close Database
            conn.Close();

            return nextStockNo;
        }

        public void insertBusinessCustomer()
        {
            // Open the DB connection
            OracleConnection conn = new OracleConnection(CONNECTION_STRING);
            conn.Open();

            // Define the SQL query and INSERT stock record
            String insertStmt = "INSERT INTO Customers VALUES('" +
                customerId.ToString() + "','" +
                companyName + "','" +
                title + "','" +
                firstname + "','" +
                surname + "','" +
                street + "','" +
                town + "','" +
                county + "','" +
                telno + "','" +
                email + "','" +
                vatNo + "','" +
                accountType + "'" +
            ")";

            // Execute the command
            try
            {
                OracleCommand cmd = new OracleCommand(insertStmt, conn);
                cmd.ExecuteNonQuery();
            } catch (OracleException exc)
            {
                throw;
            }

            // Close the DB Connection
            conn.Close();
        }

        public void insertPrivateCustomer  ()
        {
            // Open the DB connection
            OracleConnection conn = new OracleConnection(CONNECTION_STRING);
            conn.Open();

            // Define the SQL query and INSERT stock record
            String insertStmt = "INSERT INTO Customers (customer_id, title, firstname, surname, street, town, county, telephone, email, account_type) VALUES('" +
                customerId.ToString() + "','" +
                title + "','" +
                firstname + "','" +
                surname + "','" +
                street + "','" +
                town + "','" +
                county + "','" +
                telno + "','" +
                email + "','" +
                accountType + "'" +
            ")";

            // Execute the command
            OracleCommand cmd = new OracleCommand(insertStmt, conn);
            cmd.ExecuteNonQuery();

            // Close the DB Connection
            conn.Close();
        }
    }
}
