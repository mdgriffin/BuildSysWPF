using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildSys.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

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

        public MaterialModel _selectedMaterial;
        public MaterialModel selectedMaterial {
            get
            {
                return _selectedMaterial;
            }
            set
            {
                if (value != _selectedMaterial)
                {
                    _selectedMaterial = value;
                    NotifyPropertyChanged("selectedMaterial");
                }
            }
        }

        public ObservableCollection<MaterialModel> materialList { get; set; }

        public ICommand addMaterialToQuoteCmd
        {
            get
            {
                return new RelayCommand(materialId => addMaterial((int) materialId), param => canAddMaterial());
            }
        }


        public void addMaterial (int materialId)
        {
            selectedMaterial = MaterialModel.getMaterial(materialId);
        }


        public Boolean canAddMaterial ()
        {
            return true;
        }
    }
}
