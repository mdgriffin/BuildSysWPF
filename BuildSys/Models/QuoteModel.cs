using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSys.Models
{
    class QuoteModel : BaseModel, INotifyDataErrorInfo
    {
        private int quoteId; // Foreign ket to quote table
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


    }
}
