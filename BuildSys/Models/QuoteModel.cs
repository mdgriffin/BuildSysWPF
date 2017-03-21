using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSys.Models
{
    class QuoteModel : BaseModel, INotifyDataErrorInfo
    {
        private int quoteId; // Foreign key to quote table
        private int customerId; // Foreign key to customers table

        // TODO: Quote Should have a description as String

        public override void validateAllProps() { }

        public override void validateProp(String propertyName) { }

        public QuoteModel (int quoteId, int customerId)
        {
            this.quoteId = quoteId;
            this.customerId = customerId;
        }

        public QuoteModel (int customerId) : this(getNextRowId("quote_id", "Quotes"), customerId) { }
        /*
        public static ObservableCollection<QuoteMaterialModel> getQuoteMaterialList()
        {
            DataTable quoteMaterialsTable = select("SELECT * FROM Quote_Materials");

            ObservableCollection<QuoteMaterialModel> quoteMaterialsList = new ObservableCollection<QuoteMaterialModel>();

            foreach (DataRow row in quoteMaterialsTable.Rows)
            {
                quoteMaterialsList.Add(new QuoteMaterialModel(
                    Int32.Parse(row["quote_material_id"].ToString()),
                    Int32.Parse(row["quote_id"].ToString()),
                    Int32.Parse(row["material_id"].ToString()),
                    row["description"].ToString(),
                    row["price_per_unit"].ToString()
                ));
            }

            return quoteMaterialsList;
        }

        public static QuoteModel getQuote(int quoteId)
        {
            DataTable quotesTable = select("SELECT * FROM Quotes WHERE quote_id = " + quotId);
            DataRow quote = quotesTable.Rows[0];

            //String format = "yy-MMM-dd hh:mm:ss:fffffff";

            return new QuoteModel(Int32.Parse(quote["quote_id"], DateTime.ParseExact(quote["date_issued"].ToString(), format), Int32.Parse(quote["customer_id"]));
        }
        */

        public static void delete(int quoteId)
        {
            delete("DELETE FROM Quotes WHERE quote_id = " + quoteId);
        }

        public void insertMaterial()
        {
            insert("INSERT INTO Quotes VALUES('" +
               quoteId + 
               "CURRENT_TIMESTAMP, " +
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
