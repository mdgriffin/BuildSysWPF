﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Globalization;

namespace BuildSys.Models
{
    class QuoteModel : BaseModel, INotifyDataErrorInfo
    {
        public int quoteId { get; } // Foreign key to quote table
        private DateTime dateIssued;
        private DateTime dateAmmended;
        //private int customerId; // Foreign key to customers table

        // The Customer that the quote is created for
        public CustomerModel customer { get; set; }

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

        private double _subtotal;
        public double subtotal
        {
            get
            {
                return _subtotal;
            }
            set
            {
                if (_subtotal != value)
                {
                    _subtotal = value;
                    NotifyPropertyChanged("subtotal");
                }
            }
        }

        private double _vat;
        public double vat
        {
            get
            {
                return _vat;
            }
            set
            {
                if (_vat != value)
                {
                    _vat = value;
                    NotifyPropertyChanged("vat");
                }
            }
        }

        private double _total;
        public double total
        {
            get
            {
                return _total;
            }
            set
            {
                if (_total != value)
                {
                    _total = value;
                    NotifyPropertyChanged("total");
                }
            }
        }

        public override void validateAllProps() {
            validateProp("description");
            validateProp("total");
        }

        public override void validateProp(String propertyName) {
            String errorMessage = "";

            switch (propertyName)
            {
                case "description":
                    if (Validator.isEmpty(description))
                    {
                        errorMessage = Validator.ERROR_IS_EMPTY;
                    }
                    break;
                case "total":
                    if (total == 0)
                    {
                        errorMessage = "Total must be greater than zero";
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

        public QuoteModel (int quoteId, DateTime dateIssued, int customerId, String description, DateTime dateAmmended, double vat, double subtotal)
        {
            this.quoteId = quoteId;
            this.dateIssued = dateIssued;
            //this.customerId = customerId;
            this._description = description;
            this.dateAmmended = dateAmmended;
            this._vat = vat;
            this._subtotal = subtotal;
            // Add the associated customer to the quot
            this.customer = CustomerModel.getCustomer(customerId);
        }

        public QuoteModel (int customerId) : this(getNextRowId("quote_id", "Quotes"), new DateTime(), customerId, "", new DateTime(), 0, 0) { }

        public static ObservableCollection<QuoteModel> getQuoteList()
        {
            DataTable quotesTable = select("SELECT * FROM Quotes WHERE status = 'A'");

            ObservableCollection<QuoteModel> quoteList = new ObservableCollection<QuoteModel>();

            foreach (DataRow row in quotesTable.Rows)
            {
                QuoteModel nextQuote = new QuoteModel(
                    Int32.Parse(row["quote_id"].ToString()),
                    DateTime.Parse(row["date_issued"].ToString()),
                    Int32.Parse(row["customer_id"].ToString()),
                    row["description"].ToString(),
                    DateTime.Parse(row["date_ammended"].ToString()),
                    Double.Parse(row["vat"].ToString()),
                    Double.Parse(row["subtotal"].ToString())
                );

                quoteList.Add(nextQuote);
            }

            return quoteList;
        }

        public static QuoteModel getQuote(int quoteId)
        {
            DataTable quotesTable = select("SELECT * FROM Quotes WHERE quote_id = " + quoteId);
            DataRow quote = quotesTable.Rows[0];

            // TODO: Test that this parses date correctly
            String format = "yy-MMM-dd hh.mm.ss.fffffff{0} tt";

            return new QuoteModel(
                Int32.Parse(quote["quote_id"].ToString()),
                DateTime.ParseExact(quote["date_issued"].ToString(), format, new CultureInfo("en-US")),
                Int32.Parse(quote["customer_id"].ToString()),
                quote["description"].ToString(),
                DateTime.ParseExact(quote["date_ammended"].ToString(), format, new CultureInfo("en-US")),
                Double.Parse(quote["vat"].ToString()),
                Double.Parse(quote["subtotal"].ToString())
            );
        }

        public static void delete(int quoteId)
        {
            String sqlDelete = "Update Quotes SET " +
                "status = 'I' " +
                "date_amended = CURRENT_TIMESTAMP" +
                " WHERE quote_id = " + quoteId;

            update(sqlDelete);
        }

        public void insertQuote()
        {
            insert("INSERT INTO Quotes VALUES(" +
                quoteId + ", " +
                "CURRENT_TIMESTAMP, " +
                customer.customerId + ", '" +
                description + "', " +
                "CURRENT_TIMESTAMP, " +
                "'A'" + ", " +
                subtotal + ", " +
                vat +
            ")");
        }

        public void update()
        {
            String sqlUpdate = "Update Quotes SET " +
                "description = " + description + ", " +
                "date_amended = CURRENT_TIMESTAMP" +
                "subtotal = " + subtotal + ", " +
                "vat = " + vat + ", " +
                " WHERE quote_id = " + quoteId;

            update(sqlUpdate);
        }

    }
}
