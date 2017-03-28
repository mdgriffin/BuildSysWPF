using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildSys.Models;
using System.Collections.ObjectModel;

namespace BuildSys.ViewModels
{
    class QuoteFormViewModel: BaseViewModel
    {
        MainViewModel parent;

        public QuoteFormViewModel (MainViewModel parent, int customerId)
        {
            this.parent = parent;

            quote = new QuoteModel(customerId);

            materialList = MaterialModel.getMaterialList();

            // TODO: Set the Quote Material when the customer clicks selects a material to add
            //material = new QuoteMaterialModel();
        }

        public QuoteFormViewModel(MainViewModel parent)
        {
            this.parent = parent;

            //quote = new QuoteModel(customerId);

            materialList = MaterialModel.getMaterialList();

            // TODO: Set the Quote Material when the customer clicks selects a material to add
            //material = new QuoteMaterialModel();
        }

        public QuoteModel quote { get; set; }
        public QuoteMaterialModel material { get; set; }

        public ObservableCollection<MaterialModel> materialList { get; set; }
    }
}
