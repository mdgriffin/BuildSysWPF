using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Controls;

namespace BuildSys
{
    public class CustomerModel : BaseModel, INotifyDataErrorInfo
    {
        public int customerId { get; set; }
        public String companyName { get; set; }
        public String title { get; set; }

        private string _firstname;
        public String firstname
        {
            get
            {
                return this._firstname;
            }
            set
            {
                if (value != _firstname)
                {
                    this._firstname = value;
                    NotifyPropertyChanged("firstname");
                }
            }
        }

        public String surname { get; set; }
        public String street { get; set; }
        public String town { get; set; }
        public String county { get; set; }
        public String telno { get; set; }
        public String email { get; set; }
        public String vatNo { get; set; }
        public char accountType { get; set; }



        //public Dictionary<String, String> errors;

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

            // Disable the Errors from bwing shown on first load
            propErrors.Clear();
        }

        public CustomerModel(String title, String firstname, String surname, String street, String town, String county, String telno, String email, char accountType) : this(title, firstname, surname, street, town, county, telno, email, accountType, null, null) { }

        public CustomerModel() : this("", "", "", "", "", "", "", "", '\n', "", "") { }

        protected override void validate()
        {
            //Boolean valid = true;

            //propErrors = new Dictionary<string, string>();

            if (Validator.isEmpty(firstname))
            {
                AddError("firstname", Validator.ERROR_IS_EMPTY);
            } else
            {
                RemoveError("firstname");
            }

            /*
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

            if (!Validator.isNumeric(telno))
            {
                errors.Add("telno", Validator.ERROR_IS_NUMERIC);
            }

            if (!Validator.isEmail(email))
            {
                errors.Add("telno", Validator.ERROR_IS_EMAIL);
            }

            // TODO: Handle Saving of business customers
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
            */

            //return valid;
        }

        public static DataTable getCustomers()
        {
            return select("SELECT * FROM Customers");
        }

        public static int getNextCustomerId()
        {
            return getNextRowId("customer_id", "Customers");
        }

        public void insertBusinessCustomer()
        {
            insert("INSERT INTO Customers VALUES('" +
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
            ")");
        }

        public void insertPrivateCustomer()
        {
            insert("INSERT INTO Customers (customer_id, title, firstname, surname, street, town, county, telephone, email, account_type) VALUES('" +
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
            ")");
        }
    }
}
