using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace BuildSys.Models
{
    /*
     * This is the base model from which all models are extended
     * Provides the methods for selecting, inserting, updating and deleting
     * to the sub-classes. Also has abstract methods to force subclasses to
     * implement methods relating to error notifying
     */

    public abstract class BaseModel : ObservableObject
    {
        // The connection string for the Oracle Database. Changed for local and college networks
        //public const string CONNECTION_STRING = "Data Source = oracle/orcl; User Id = INSERT_USER_HERE; Password = INSERT_PASSWORD_HERE;";

        public const string CONNECTION_STRING = "Data Source = Localhost; User Id = INSERT_USER_HERE; Password = INSERT_PASSWORD_HERE;";

        // Stores the errors raised on properties
        public Dictionary<string, List<string>> propErrors = new Dictionary<string, List<string>>();

        // Raised when an error status is changed
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        // Returns the error for a single property or null if that property has no error
        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            if (String.IsNullOrEmpty(propertyName) || !propErrors.ContainsKey(propertyName))
            {
                return null;
            }

            return propErrors[propertyName];
        }

        // Boolean to signal that a property of properties of a model have errors 
        public bool HasErrors
        {
            get
            {
                
                return propErrors.Count > 0;
            }
        }

        // Adds an error to to prop errors and notifies of the change in status
        public void AddError(string propertyName, string error)
        {
            this.propErrors[propertyName] = new List<string>() { error };
            this.NotifyErrorsChanged(propertyName);
        }

        // Removes an error to to prop errors and notifies of the change in status
        public void RemoveError(string propertyName)
        {
            if (this.propErrors.ContainsKey(propertyName))
                this.propErrors.Remove(propertyName);
            this.NotifyErrorsChanged(propertyName);
        }

        // Event to notifiy that the status of a property has changed
        public void NotifyErrorsChanged(string propertyName)
        {
            this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        // Helper select method
        public static DataTable select (String sqlSelect)
        {
            // Open the DB connection
            OracleConnection conn = new OracleConnection(CONNECTION_STRING);
            conn.Open();

            OracleCommand cmd = new OracleCommand(sqlSelect, conn);

            OracleDataAdapter da = new OracleDataAdapter(cmd);

            // Create an empty data set
            DataSet ds = new DataSet();

            da.Fill(ds, "result");

            // Close Database
            conn.Close();

            return ds.Tables["result"];
        }

        // Helper method to get the next row id of a specified table
        public static int getNextRowId (String primaryKeyName, String tableName)
        {
            int nextRowId;

            // Open the DB connection
            OracleConnection conn = new OracleConnection(CONNECTION_STRING);
            conn.Open();

            String selectMaxStmt = "SELECT MAX(" + primaryKeyName + ") FROM " + tableName;

            OracleCommand cmd = new OracleCommand(selectMaxStmt, conn);

            OracleDataReader dr = cmd.ExecuteReader();

            // Read the first (only) value returned from the query
            // If first stockNo, assign value of 1, otherwise add 1
            dr.Read();

            if (dr.IsDBNull(0))
            {
                nextRowId = 1;
            }
            else
            {
                nextRowId = Convert.ToInt32(dr.GetValue(0)) + 1;
            }

            // Close Database
            conn.Close();

            return nextRowId;
        }

        // Helper insert method
        public static void insert (String sqlInsert)
        {
            execNonQuery(sqlInsert);
        }

        // Helper update method
        public static void update(String sqlUpdate)
        {
            execNonQuery(sqlUpdate);
        }

        // Helper delete method
        public static void delete (String deleteStmt)
        {
            execNonQuery(deleteStmt);
        }

        // Helper Method used in this class execute non-query statements
        private static void execNonQuery (String sqlStmt)
        {
            // Open the DB connection
            OracleConnection conn = new OracleConnection(CONNECTION_STRING);
            conn.Open();

            // Execute the command
            try
            {
                OracleCommand cmd = new OracleCommand(sqlStmt, conn);
                cmd.ExecuteNonQuery();
            }
            catch (OracleException exc)
            {
                throw exc;
            }

            // Close the DB Connection
            conn.Close();
        }

        // Abstract method to force subclasses to implement a method to validate a single property
        public abstract void validateProp(String propName);

        // Abstract method to force subclasses to implement a method that check all properties for errors
        public abstract void validateAllProps();
    }
}
