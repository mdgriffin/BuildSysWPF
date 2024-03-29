﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSys.Models
{
    /*
     *  Models the Settings of the System and Provides access to Database functions
     *  There can only be a single setting, so update and insert check for the 
     *  exisitence of a row in the settings table and then decide whether to update or insert
     */

    public class SettingModel : BaseModel, INotifyDataErrorInfo
    {

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
                    validateProp("companyName");
                    NotifyPropertyChanged("companyName");
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
                    validateProp("street");
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
                    validateProp("town");
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
                    validateProp("county");
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
                    validateProp("telno");
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
                    validateProp("email");
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
                    validateProp("vatno");
                    NotifyPropertyChanged("vatno");
                }
            }
        }


        public SettingModel(String companyName, String street, String town, String county, String telno, String email, String vatNo)
        {
            this._companyName = companyName;
            this._street = street;
            this._town = town;
            this._county = county;
            this._telno = telno;
            this._email = email;
            this._vatNo = vatNo;
        }

        public SettingModel() : this("", "", "", "", "", "", "") { }

        public override void validateAllProps()
        {
            validateProp("companyName");
            validateProp("street");
            validateProp("town");
            validateProp("telno");
            validateProp("email");
            validateProp("vatNo");
        }

        public override void validateProp(String propertyName)
        {
            String errorMessage = "";

            switch (propertyName)
            {
                case "companyName":
                    if (Validator.isEmpty(companyName))
                    {
                        errorMessage = Validator.ERROR_IS_EMPTY;
                    }
                    break;
                case "street":
                    if (Validator.isEmpty(street))
                    {
                        errorMessage = Validator.ERROR_IS_EMPTY;
                    }
                    break;
                case "town":
                    if (Validator.isEmpty(town))
                    {
                        errorMessage = Validator.ERROR_IS_EMPTY;
                    }
                    break;
                case "telno":
                    if (!Validator.isTelNum(telno))
                    {
                        errorMessage = Validator.ERROR_IS_TEL_NUM;
                    }
                    break;
                case "email":
                    if (!Validator.isEmail(email))
                    {
                        errorMessage = Validator.ERROR_IS_EMAIL;
                    }
                    break;

                case "vatNo":
                    if (Validator.isVatNum(vatNo))
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


        public static SettingModel getSetting()
        {
            SettingModel settingModel = null;
           
            DataTable settingsTable = select("SELECT * FROM Settings WHERE setting_id = 1");

            if (settingsTable.Rows.Count > 0)
            {
                DataRow setting = settingsTable.Rows[0];
                settingModel = new SettingModel(setting["company_name"].ToString(), setting["street"].ToString(), setting["town"].ToString(), setting["county"].ToString(), setting["telephone"].ToString(), setting["email"].ToString(), setting["vat_no"].ToString());
            }

            return settingModel;
        }



        public void insertSetting()
        {
            if (getSetting() == null)
            {
                insert("INSERT INTO Settings (setting_id, company_name,  street, town, county, telephone, email, vat_no) VALUES(1, '" +
                    companyName + "','" +
                    street + "','" +
                    town + "','" +
                    county + "','" +
                    telno + "','" +
                    email + "','" +
                    vatNo + "')");
            }
            else
            {
                updateSetting();
            }
            
        }

        public void updateSetting()
        {
            if (getSetting() != null)
            {
                update("Update Settings SET " +
                    "company_name = '" + companyName + "', " +
                    "street = '" + street + "', " +
                    "town = '" + town + "', " +
                    "county = '" + county + "', " +
                    "telephone = '" + telno + "', " +
                    "email = '" + email + "', " +
                    "vat_no = '" + vatNo + "' WHERE setting_id = 1"
                );
            }
            else
            {
                insertSetting();
            }
            

        }
    }
}
