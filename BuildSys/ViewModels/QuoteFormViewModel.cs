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
        Boolean editing;

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

        public QuoteFormViewModel(MainViewModel parent, int quoteId, Boolean editing)
        {
            this.parent = parent;
            this.editing = true;

            this.quote = QuoteModel.getQuote(quoteId);
            this.customerId = quote.customer.customerId;

            materialList = MaterialModel.getMaterialList();
            quoteMaterialList = QuoteMaterialModel.getQuoteMaterialList(quoteId);

            updateTotalQuoteCosts();
        }

        public ObservableCollection<QuoteMaterialModel> quoteMaterialList { get; set; }

        public QuoteModel quote { get; set; }

        private QuoteMaterialModel _quoteMaterial;
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

        public ICommand addMaterialoToQuoteListCmd
        {
            get
            {
                // TODO: Button should disabled if the selected material has errors
                return new RelayCommand(param => addMaterialToQuoteList(), param => selectedMaterial != null && !selectedMaterial.HasErrors);
            }
        }


        public void addMaterialToQuoteList()
        {
            quoteMaterial.validateAllProps();

            if (selectedMaterial != null && quoteMaterial != null && !quoteMaterial.HasErrors)
            {
                quoteMaterialList.Add(quoteMaterial);

                quoteMaterial.listIndex = quoteMaterialList.IndexOf(quoteMaterial);

                updateTotalQuoteCosts();

                quoteMaterial = new QuoteMaterialModel(quote.quoteId, selectedMaterial.materialId, selectedMaterial.pricePerUnit, selectedMaterial.isService);
            }
            else
            {
                MessageBox.Show("Material Form has Errors");
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
            quoteMaterialList.RemoveAt(quoteMaterialListIndex);
            updateTotalQuoteCosts();
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

        public ICommand saveQuoteCmd
        {
            get
            {
                return new RelayCommand(param => saveQuote(), param => true);
            }
        }

        public void saveQuote()
        {
            //MessageBox.Show("Saving Quote!!");

            if (materialList.Count > 0)
            {
                quote.validateAllProps();

                if (!quote.HasErrors)
                {
                    // TODO: How do  know when to update?
                    quote.insertQuote();

                    foreach (QuoteMaterialModel quoteMat in quoteMaterialList)
                    {
                        if (quoteMat.quoteMaterialId != null)
                        {
                            quoteMat.insertMaterial();
                        }
                        else
                        {
                            // TODO: Update the Quote Material
                        }
                    }

                    MessageBox.Show("Quote Saved");
                } else
                {
                    MessageBox.Show("Quote has errors");
                }
            }
            else
            {
                MessageBox.Show("Add at least one material to the quote");
            }
        }

    }
}
