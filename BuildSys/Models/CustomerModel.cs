using System;
using System.ComponentModel;
using System.Data;

namespace BuildSys
{
    public class CustomerModel : BaseModel, INotifyDataErrorInfo
    {
        public int customerId { get; set; }

        public String _companyName;
        public String companyName
        {
            get
            {
                return this._companyName;
            }
            set
            {
                if (value != _companyName)
                {
                    this._companyName = value;
                    NotifyPropertyChanged("companyName");
                }
            }
        }

        public String _title;
        public String title
        {
            get
            {
                return this._title;
            }
            set
            {
                if (value != _title)
                {
                    this._title = value;
                    NotifyPropertyChanged("title");
                }
            }
        }

        private String _firstname;
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

        private String _surname;
        public String surname
        {
            get
            {
                return this._surname;
            }
            set
            {
                if (value != _surname)
                {
                    this._surname = value;
                    NotifyPropertyChanged("surname");
                }
            }
        }

        public String _street;
        public String street
        {
            get
            {
                return this._street;
            }
            set
            {
                if (value != _street)
                {
                    this._street = value;
                    NotifyPropertyChanged("street");
                }
            }
        }

        public String _town;
        public String town
        {
            get
            {
                return this._town;
            }
            set
            {
                if (value != _town)
                {
                    this._town = value;
                    NotifyPropertyChanged("town");
                }
            }
        }

        public String _county;
        public String county
        {
            get
            {
                return this._county;
            }
            set
            {
                if (value != _county)
                {
                    this._county = value;
                    NotifyPropertyChanged("county");
                }
            }
        }

        public String _telno;
        public String telno
        {
            get
            {
                return this._telno;
            }
            set
            {
                if (value != _telno)
                {
                    this._telno = value;
                    NotifyPropertyChanged("telno");
                }
            }
        }

        public String _email;

        public String email
        {
            get
            {
                return this._email;
            }
            set
            {
                if (value != _email)
                {
                    this._email = value;
                    NotifyPropertyChanged("email");
                }
            }
        }

        public String _vatNo;
        public String vatNo
        {
            get
            {
                return this._vatNo;
            }
            set
            {
                if (value != _vatNo)
                {
                    this._vatNo = value;
                    NotifyPropertyChanged("vatno");
                }
            }
        }

        public char _accountType;
        public char accountType
        {
            get
            {
                return this._accountType;
            }
            set
            {
                if (value != _accountType)
                {
                    this._accountType = value;
                    NotifyPropertyChanged("accountType");
                }
            }
        }

        public CustomerModel(String title, String firstname, String surname, String street, String town, String county, String telno, String email, char accountType, String companyName, String vatNo)
        {
            this.customerId = getNextCustomerId();
            this._companyName = companyName;
            this._title = title;
            this._firstname = firstname;
            this._surname = surname;
            this._street = street;
            this._town = town;
            this._county = county;
            this._telno = telno;
            this._email = email;
            this._vatNo = vatNo;
            this._accountType = accountType;
        }

        public CustomerModel(String title, String firstname, String surname, String street, String town, String county, String telno, String email, char accountType) : this(title, firstname, surname, street, town, county, telno, email, accountType, null, null) { }

        public CustomerModel() : this("", "", "", "", "", "", "", "", '\n', "", "") { }

        protected override bool validateProp (String propertyName)
        {
            bool valid = true;

            switch (propertyName)
            {
                case "firstname":
                    if (Validator.isEmpty(firstname))
                    {
                        AddError("firstname", Validator.ERROR_IS_EMPTY);
                        valid = false;
                    }
                    break;
                case "surname":
                    if (Validator.isEmpty(surname))
                    {
                        AddError("surname", Validator.ERROR_IS_EMPTY);
                        valid = false;
                    }
                    break;
                case "street":
                    if (Validator.isEmpty(street))
                    {
                        AddError("street", Validator.ERROR_IS_EMPTY);
                        valid = false;
                    }
                    break;
                case "town":
                    if (Validator.isEmpty(town))
                    {
                        AddError("town", Validator.ERROR_IS_EMPTY);
                        valid = false;
                    }
                    break;
                case "telno":
                    if (!Validator.isNumeric(telno))
                    {
                        AddError("telno", Validator.ERROR_IS_NUMERIC);
                        valid = false;
                    }
                    break;
                case "email":
                    if (!Validator.isEmail(email))
                    {
                        AddError("email", Validator.ERROR_IS_EMAIL);
                        valid = false;
                    }
                    break;
                case "companyName":
                    if (Validator.isEmpty(companyName))
                    {
                        AddError("companyName", Validator.ERROR_IS_EMPTY);
                        valid = false;
                    }
                    break;
                case "vatNo":
                    if (Validator.isVatNum(vatNo))
                    {
                        AddError("vatNo", Validator.ERROR_IS_VAT_NUM);
                        valid = false;
                    }
                    break;
            }
            return valid;
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
