using System;
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

        // TODO: Add a constructor that accepts the quoteId  
        public QuoteFormViewModel (MainViewModel parent, int customerId)
        {
            this.parent = parent;
            this.customerId = customerId;

            quote = new QuoteModel(customerId);

            materialList = MaterialModel.getMaterialList();

            quoteMaterialList = new ObservableCollection<QuoteMaterialModel>();

            updateTotalQuoteCost();
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
                quoteMaterialList.Add(quoteMaterial);

                quoteMaterial.listIndex = quoteMaterialList.IndexOf(quoteMaterial);

                updateTotalQuoteCost();

                quoteMaterial = new QuoteMaterialModel(quote.quoteId, selectedMaterial.materialId, selectedMaterial.pricePerUnit);

            }
            
        }

        public ICommand removeQuoteMaterialCmd
        {
            get
            {
                return new RelayCommand(quoteMaterialListIndex => removeQuoteMaterial((int)quoteMaterialListIndex), param => true);
            }
        }
        

        public void removeQuoteMaterial (int quoteMaterialListIndex)
        {
            MessageBox.Show("List index: " + quoteMaterialListIndex);
            //MessageBox.Show("Row ID = " + rowId);
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
