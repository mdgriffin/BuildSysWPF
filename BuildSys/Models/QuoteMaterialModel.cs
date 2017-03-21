using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSys.Models
{
    class QuoteMaterialModel
    {
        private int quoteMaterialId; // Primary Key
        private int quoteId; // Foreign key to quotes
        private int materialId; // Foreign key to materials
        
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

        public double _pricePerUnit;
        public double pricePerUnit
        {
            get
            {
                return _pricePerUnit;
            }
            set
            {
                if (value != _pricePerUnit)
                {
                    _pricePerUnit = value;
                }
            }
        }
    }
}
