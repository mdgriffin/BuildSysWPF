using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSys.Models
{
    class QuoteModel : BaseModel, INotifyDataErrorInfo
    {
        private int quoteId; // Foreign key to quote table
        private DateTime dateIssued;
        private int customerId; // Foreign key to customers table

        // TODO: Quote Should have a description as String
        // TODO: Add date amended

        public override void validateAllProps() { }

        public override void validateProp(String propertyName) { }

        public QuoteModel (int quoteId, DateTime dateIssued, int customerId)
        {
            this.quoteId = quoteId;
            this.customerId = customerId;
        }

        public QuoteModel (int customerId) : this(getNextRowId("quote_id", "Quotes"), new DateTime(), customerId) { }

        public static ObservableCollection<QuoteModel> getQuoteList()
        {
            String format = "yy-MMM-dd hh.mm.ss.fffffff{0} tt";
            DataTable quotesTable = select("SELECT * FROM Quotes");

            ObservableCollection<QuoteModel> quoteList = new ObservableCollection<QuoteModel>();

            foreach (DataRow row in quotesTable.Rows)
            {
                quoteList.Add(new QuoteModel(
                    Int32.Parse(row["quote_id"].ToString()),
                    DateTime.ParseExact(row["date_issued"].ToString(), format, new CultureInfo("en-US")),
                    Int32.Parse(row["customer_id"].ToString())
                ));
            }

            return quoteList;
        }

        public static QuoteModel getQuote(int quoteId)
        {
            DataTable quotesTable = select("SELECT * FROM Quotes WHERE quote_id = " + quoteId);
            DataRow quote = quotesTable.Rows[0];

            // TODO: Test that this parses date correctly
            String format = "yy-MMM-dd hh.mm.ss.fffffff{0} tt";

            return new QuoteModel(Int32.Parse(quote["quote_id"].ToString()), DateTime.ParseExact(quote["date_issued"].ToString(), format, new CultureInfo("en-US")), Int32.Parse(quote["customer_id"].ToString()));
        }

        public static void delete(int quoteId)
        {
            delete("DELETE FROM Quotes WHERE quote_id = " + quoteId);
        }

        public void insertMaterial()
        {
            // TODO: Test date formatting
            insert("INSERT INTO Quotes VALUES('" +
               quoteId +
               dateIssued.ToString("MM/dd/yyyy hh:mm:ss tt") + 
               customerId +
            ")");
        }

        public void update()
        {
            String sqlUpdate = "Update Quotes SET " +
                "customer_id = " + customerId + ", " +
                "date_issued = CURRENT_TIMESTAMP" +
                " WHERE quote_id = " + quoteId;

            update(sqlUpdate);
        }

    }
}
