using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildSys.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;

namespace BuildSys.ViewModels
{
    class QuoteFormViewModel: BaseViewModel
    {
        MainViewModel parent;
        int customerId;

        public QuoteFormViewModel (MainViewModel parent, int customerId)
        {
            this.parent = parent;
            this.customerId = customerId;

            quote = new QuoteModel(customerId);

            materialList = MaterialModel.getMaterialList();

            quoteMaterialList = new ObservableCollection<QuoteMaterialModel>();

            updateTotalQuoteCost();
            // TODO: Set the Quote Material when the customer clicks selects a material to add
            //material = new QuoteMaterialModel();
        }


        private String _totalQuoteCost;
        public String totalQuoteCost
        {
            get
            {
                return _totalQuoteCost;
            }
            set
            {
                if (value != _totalQuoteCost)
                {
                    _totalQuoteCost = value;
                    NotifyPropertyChanged("totalQuoteCost");
                }
            }
        }


        public ObservableCollection<QuoteMaterialModel> quoteMaterialList { get; set; }

        // TODO: Add a constructor that accepts the quoteId    

        public QuoteModel quote { get; set; }


        private QuoteMaterialModel _quoteMaterial { get; set; }
        public QuoteMaterialModel quoteMaterial
        {
            get
            {
                return _quoteMaterial;
            }
            set
            {
                if (value != _quoteMaterial)
                {
                    _quoteMaterial = value;
                    NotifyPropertyChanged("quoteMaterial");
                }
            }
        }

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
                return new RelayCommand(materialId => addMaterial((int) materialId), param => true);
            }
        }


        public void addMaterial (int materialId)
        {
            selectedMaterial = MaterialModel.getMaterial(materialId);
            quoteMaterial = new QuoteMaterialModel(quote.quoteId, materialId, selectedMaterial.pricePerUnit);
        }

        public ICommand saveQuoteMaterialCmd
        {
            get
            {
                return new RelayCommand(param => saveQuoteMaterial(), param => true);
            }
        }


        public void saveQuoteMaterial ()
        {
            if (selectedMaterial != null && quoteMaterial != null && !quoteMaterial.HasErrors)
            {
                //quoteMaterial.insertMaterial();

                // TODO: Add to the QuotMaterialsList

                quoteMaterialList.Add(quoteMaterial);

                updateTotalQuoteCost();

                quoteMaterial = new QuoteMaterialModel(quote.quoteId, selectedMaterial.materialId, selectedMaterial.pricePerUnit);

            }
            
        }

        public void updateTotalQuoteCost ()
        {
            double totalCost = 0;

            foreach (QuoteMaterialModel quoteMaterial in quoteMaterialList)
            {
                totalCost += quoteMaterial.totalCost;
            }

            totalQuoteCost = "Total: €" + totalCost;
        }

    }
}
