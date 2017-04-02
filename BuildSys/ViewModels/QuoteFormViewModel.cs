﻿using System;
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

            updateTotalQuoteCosts();
        }

        /*
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
        */

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
            quoteMaterial = new QuoteMaterialModel(quote.quoteId, materialId, selectedMaterial.pricePerUnit, selectedMaterial.isService);
        }

        public ICommand saveQuoteMaterialCmd
        {
            get
            {
                // TODO: Button should e disabled if th selected material has erros
                return new RelayCommand(param => saveQuoteMaterial(), param => selectedMaterial != null && !selectedMaterial.HasErrors);
            }
        }


        public void saveQuoteMaterial ()
        {
            if (selectedMaterial != null && quoteMaterial != null && !quoteMaterial.HasErrors)
            {
                quoteMaterialList.Add(quoteMaterial);

                quoteMaterial.listIndex = quoteMaterialList.IndexOf(quoteMaterial);

                updateTotalQuoteCosts();

                quoteMaterial = new QuoteMaterialModel(quote.quoteId, selectedMaterial.materialId, selectedMaterial.pricePerUnit, selectedMaterial.isService);
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
        }


        public void updateTotalQuoteCosts ()
        {
            double subtotal = 0;
            double vat = 0;

            foreach (QuoteMaterialModel quoteMaterial in quoteMaterialList)
            {
                subtotal += quoteMaterial.totalCost;
                if (quoteMaterial.isService)
                {
                    vat += quoteMaterial.totalCost * 0.135;
                } else
                {
                    vat += quoteMaterial.totalCost * 0.21;
                }
            }

            quote.subtotal = subtotal;
            quote.vat = vat;
            quote.total = subtotal + vat;
        }

    }
}
