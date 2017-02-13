using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
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

        public CustomerModel(int customerId, String companyName, String title, String firstname, String surname, String street, String town, String county, String telno, String email, String vatNo, char accountType)
        {
            this.customerId = customerId;
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

        public static int getNextStockNo()
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

        public void regStock()
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
                county + "','" +
                telno + "','" +
                email + "','" +
                vatNo + "','" +
                accountType + "','" +
            "')";

            // Execute the command
            OracleCommand cmd = new OracleCommand(insertStmt, conn);
            cmd.ExecuteNonQuery();

            // Close the DB Connection
            conn.Close();
        }
    }
}
