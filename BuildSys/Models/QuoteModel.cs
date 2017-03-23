using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Globalization;

namespace BuildSys.Models
{
    class QuoteModel : BaseModel, INotifyDataErrorInfo
    {
        private int quoteId; // Foreign key to quote table
        private DateTime dateIssued;
        private DateTime dateAmmended;
        private int customerId; // Foreign key to customers table

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
                }
            }
        }

        public override void validateAllProps() {

        }

        public override void validateProp(String propertyName) {
            String errorMessage = "";

            switch (propertyName)
            {
                case "description":
                    if (Validator.isEmpty(description))
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

        public QuoteModel (int quoteId, DateTime dateIssued, int customerId, String description, DateTime dateAmmended)
        {
            this.quoteId = quoteId;
            this.dateIssued = dateIssued;
            this.customerId = customerId;
            this._description = description;
            this.dateAmmended = dateAmmended;
        }

        public QuoteModel (int customerId) : this(getNextRowId("quote_id", "Quotes"), new DateTime(), customerId, "", new DateTime()) { }

        public static ObservableCollection<QuoteModel> getQuoteList()
        {
            String format = "yy-MMM-dd hh.mm.ss.fffffff{0} tt";
            DataTable quotesTable = select("SELECT * FROM Quotes WHERE status = 'A'");

            ObservableCollection<QuoteModel> quoteList = new ObservableCollection<QuoteModel>();

            foreach (DataRow row in quotesTable.Rows)
            {
                quoteList.Add(new QuoteModel(
                    Int32.Parse(row["quote_id"].ToString()),
                    DateTime.ParseExact(row["date_issued"].ToString(), format, new CultureInfo("en-US")),
                    Int32.Parse(row["customer_id"].ToString()),
                    row["description"].ToString(),
                    DateTime.ParseExact(row["date_ammended"].ToString(), format, new CultureInfo("en-US"))
                ));
            }

            return quoteList;
        }

        public static QuoteModel getQuote(int quoteId)
        {
            DataTable quotesTable = select("SELECT * FROM Quotes WHERE status = 'A' && quote_id = " + quoteId);
            DataRow quote = quotesTable.Rows[0];

            // TODO: Test that this parses date correctly
            String format = "yy-MMM-dd hh.mm.ss.fffffff{0} tt";

            return new QuoteModel(
                Int32.Parse(quote["quote_id"].ToString()),
                DateTime.ParseExact(quote["date_issued"].ToString(), format, new CultureInfo("en-US")),
                Int32.Parse(quote["customer_id"].ToString()),
                quote["description"].ToString(),
                DateTime.ParseExact(quote["date_ammended"].ToString(), format, new CultureInfo("en-US"))
            );
        }

        public static void delete(int quoteId)
        {
            String sqlDelete = "Update Quotes SET " +
                "status = 'I', " +
                "date_amended = CURRENT_TIMESTAMP" +
                " WHERE quote_id = " + quoteId;

            update(sqlDelete);
        }

        public void insertQuote()
        {
            // TODO: Test date formatting
            insert("INSERT INTO Quotes VALUES('" +
               quoteId +
               dateIssued.ToString("MM/dd/yyyy hh:mm:ss tt") + 
               customerId +
               description +
               dateAmmended.ToString("MM/dd/yyyy hh:mm:ss tt") +
            ")");
        }

        public void update()
        {
            String sqlUpdate = "Update Quotes SET " +
                "description = " + description + ", " +
                "date_amended = CURRENT_TIMESTAMP" +
                " WHERE quote_id = " + quoteId;

            update(sqlUpdate);
        }

    }
}
