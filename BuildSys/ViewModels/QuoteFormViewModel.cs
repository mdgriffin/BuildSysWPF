using System;
using BuildSys.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using BuildSys.Views;
using System.Text.RegularExpressions;
using System.Linq;

namespace BuildSys.ViewModels
{
    class QuoteFormViewModel: BaseViewModel
    {
        //MainViewModel parent;
        int customerId;
        // Set to true when the form has saved and we are updating not registering
        Boolean updating;

        public QuoteFormViewModel (BaseViewModel parent, int customerId)
        {
            this.parent = parent;
            // this is a new quote
            this.updating = false;
            btnText = "Create Qoute";

            this.customerId = customerId;

            quote = new QuoteModel(customerId);

            customer = CustomerModel.getCustomer(customerId);

            if (customer.companyName == null || customer.vatNo == null)
            {
                isBusinessCustomer = "Collapsed";
            } else
            {
                isBusinessCustomer = "Visible";
            }

            materialList = MaterialModel.getMaterialList();
            originalMaterialList = new ObservableCollection<MaterialModel>(materialList);

            quoteMaterialList = new ObservableCollection<QuoteMaterialModel>();

            updateTotalQuoteCosts();
        }

        public QuoteFormViewModel(BaseViewModel parent, int quoteId, Boolean updating)
        {
            this.parent = parent;
            // will be set to true
            this.updating = updating;
            btnText = "Update Quote";

            this.quote = QuoteModel.getQuote(quoteId);
            this.customerId = quote.customer.customerId;

            customer = CustomerModel.getCustomer(customerId);

            if (customer.companyName == null || customer.vatNo == null)
            {
                isBusinessCustomer = "Collapsed";
            }
            else
            {
                isBusinessCustomer = "Visible";
            }

            materialList = MaterialModel.getMaterialList();
            originalMaterialList = new ObservableCollection<MaterialModel>(materialList);

            quoteMaterialList = QuoteMaterialModel.getQuoteMaterialList(quoteId);

            updateTotalQuoteCosts();
        }

        public String addOrUpdateQuoteMaterialBtn { get; set; } = "Add to Quote";

        public ObservableCollection<QuoteMaterialModel> quoteMaterialList { get; set; }
        private ObservableCollection<QuoteMaterialModel> removedQuoteMaterialList = new ObservableCollection<QuoteMaterialModel>();

        public String isBusinessCustomer { get; set; }

        public QuoteModel quote { get; set; }
        public CustomerModel customer { get; set; }
        public String btnText { get; set; }

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

        private String _materialFilter;
        public String materialFilter
        {
            get
            {
                return _materialFilter;
            }
            set
            {
                if (value != _materialFilter)
                {
                    _materialFilter = value;
                    filterMaterials();
                }
            }
        }

        private ObservableCollection<MaterialModel> originalMaterialList;

        private ObservableCollection<MaterialModel> _materialList = new ObservableCollection<MaterialModel>();
        public ObservableCollection<MaterialModel> materialList
        {
            get { return _materialList ?? (_materialList = new ObservableCollection<MaterialModel>()); }
            set
            {
                if (value == null) return;
                _materialList = value;
                NotifyPropertyChanged("materialList");
            }
        }

        public void filterMaterials()
        {
            materialList = new ObservableCollection<MaterialModel>(originalMaterialList);
            Regex matchName = new Regex(@"^" + materialFilter + @".+", RegexOptions.IgnoreCase);

            if (materialFilter.Length > 0)
            {
                materialList.Where(mat => !matchName.IsMatch(mat.name))
                    .ToList()
                    .All(i => materialList.Remove(i));
            }
        }

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

        public ICommand addOrUpdateQuoteMaterialCmd
        {
            get
            {
                return new RelayCommand(param => addOrUpdateQuoteMaterial(), param => selectedMaterial != null && !selectedMaterial.HasErrors);
            }
        }


        public void addOrUpdateQuoteMaterial()
        {
            quoteMaterial.validateAllProps();

            if (selectedMaterial != null && quoteMaterial != null && !quoteMaterial.HasErrors)
            {
                if (quoteMaterial.quoteMaterialId == null)
                {
                    quoteMaterialList.Add(quoteMaterial);
                    quoteMaterial.listIndex = quoteMaterialList.IndexOf(quoteMaterial);
                }
                else
                {
                    quoteMaterialList[quoteMaterial.listIndex] = quoteMaterial;
                }

                addOrUpdateQuoteMaterialBtn = "Add to Quote";
                NotifyPropertyChanged("addOrUpdateQuoteMaterialBtn");

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
                return new RelayCommand(quoteMaterialIndex => removeQuoteMaterial((int)quoteMaterialIndex), param => true);
            }
        }

        public void removeQuoteMaterial (int quoteMaterialIndex)
        {
            // Check if this material has been saved
            if (quoteMaterialList.ElementAt(quoteMaterialIndex).quoteMaterialId != null)
            {
                removedQuoteMaterialList.Add(quoteMaterialList.ElementAt(quoteMaterialIndex));
            }
            
            // remove from the list
            quoteMaterialList.RemoveAt(quoteMaterialIndex);

            int currentIndex = 0;
            // Ensure each element is set ot the correct index
            quoteMaterialList.ToList().ForEach(qm => qm.listIndex = currentIndex++);

            updateTotalQuoteCosts();
        }

        public ICommand editQuoteMaterialCmd
        {
            get
            {
                return new RelayCommand(quoteMaterialIndex => editQuoteMaterial((int)quoteMaterialIndex), param => true);
            }
        }

        public void editQuoteMaterial(int quoteMaterialIndex)
        {
            addOrUpdateQuoteMaterialBtn = "Update In Quote";
            NotifyPropertyChanged("addOrUpdateQuoteMaterialBtn");

            // Create a copy of this quote material
            quoteMaterial = quoteMaterialList.ElementAt(quoteMaterialIndex).clone();
            // This material may have been deleted
            //selectedMaterial = MaterialModel.getMaterial(quoteMaterial.materialId);
            selectedMaterial = new MaterialModel();
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
            if (materialList.Count > 0)
            {
                quote.validateAllProps();

                if (!quote.HasErrors)
                {
                    if (updating)
                    {
                        quote.updateQuote();
                    }
                    else
                    {
                        quote.insertQuote();
                    }
                    
                    foreach (QuoteMaterialModel quoteMat in quoteMaterialList)
                    {
                        if (!quoteMat.HasErrors)
                        {
                            if (quoteMat.quoteMaterialId != 0)
                            {
                                quoteMat.updateMaterial();
                            }
                            else
                            {
                                quoteMat.insertMaterial();
                            }
                        }
                    }

                    foreach (QuoteMaterialModel quoteMat in removedQuoteMaterialList)
                    {
                        QuoteMaterialModel.delete(quoteMat.quoteMaterialId.Value);
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


        public ICommand printQuoteCmd
        {
            get
            {
                return new RelayCommand(param => printQuote(), param => true);
            }
        }

        private void printQuote ()
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(new QuotePrint(quote), "My First Print Job");
            }
        }

    }
}
