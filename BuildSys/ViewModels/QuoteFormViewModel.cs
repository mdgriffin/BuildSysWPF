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

        // Default constructor for creating a new quote
        public QuoteFormViewModel (BaseViewModel parent, int customerId)
        {
            this.parent = parent;
            // this is a new quote
            this.updating = false;
            btnText = "Create Qoute";

            this.customerId = customerId;

            // Create a new blank quote
            quote = new QuoteModel(customerId);

            // Get the customer's model
            customer = CustomerModel.getCustomer(customerId);

            // Set the visibility of the customer vat and company name details
            if (customer.companyName == null || customer.vatNo == null)
            {
                isBusinessCustomer = "Collapsed";
            } else
            {
                isBusinessCustomer = "Visible";
            }

            // The the list of all active materials
            materialList = MaterialModel.getMaterialList();
            originalMaterialList = new ObservableCollection<MaterialModel>(materialList);

            // Create an empty list ot store quote materials
            quoteMaterialList = new ObservableCollection<QuoteMaterialModel>();

            // Update the total quote cost
            updateTotalQuoteCosts();
        }

        // Form for updating an exisiting quote
        public QuoteFormViewModel(BaseViewModel parent, int quoteId, Boolean updating)
        {
            this.parent = parent;
            // will be set to true
            this.updating = updating;
            btnText = "Update Quote";

            // Get the quote
            this.quote = QuoteModel.getQuote(quoteId);
            this.customerId = quote.customer.customerId;

            // Get the customer the quote is being issued for
            customer = CustomerModel.getCustomer(customerId);

            // Set visibility of business textblocks
            if (customer.companyName == null || customer.vatNo == null)
            {
                isBusinessCustomer = "Collapsed";
            }
            else
            {
                isBusinessCustomer = "Visible";
            }

            // Get all materials
            materialList = MaterialModel.getMaterialList();
            // Keep a copy of the material list for filtering
            originalMaterialList = new ObservableCollection<MaterialModel>(materialList);

            // Get the quote materials that have been generated for this quote
            quoteMaterialList = QuoteMaterialModel.getQuoteMaterialList(quoteId);

            // Update the total quote cost
            updateTotalQuoteCosts();
        }

        // Publicly accessible properties for the view
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
                    // Notify changed so view is updated
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

        // The text of the material filter input
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

        // Filters down the list of materials based on user's input
        public void filterMaterials()
        {
            materialList = new ObservableCollection<MaterialModel>(originalMaterialList);
            Regex matchName = new Regex(@"^" + materialFilter + @".+", RegexOptions.IgnoreCase);

            if (materialFilter.Length > 0)
            {
                // Remove any non-matching materials
                materialList.Where(mat => !matchName.IsMatch(mat.name))
                    .ToList()
                    .All(i => materialList.Remove(i));
            }
        }

        // Called when add material button is pressed
        public ICommand addMaterialToQuoteCmd
        {
            get
            {
                return new RelayCommand(materialId => addMaterial((int) materialId), param => true);
            }
        }

        // Adds a material to form
        public void addMaterial (int materialId)
        {
            // Set as selected material
            selectedMaterial = MaterialModel.getMaterial(materialId);
            // Create a new blank quote material
            quoteMaterial = new QuoteMaterialModel(quote.quoteId, materialId, selectedMaterial.pricePerUnit, selectedMaterial.isService);
        }

        // Called when the add/update button is pressed
        public ICommand addOrUpdateQuoteMaterialCmd
        {
            get
            {
                return new RelayCommand(param => addOrUpdateQuoteMaterial(), param => selectedMaterial != null && !selectedMaterial.HasErrors);
            }
        }

        // Either adds a new material to the quote
        // or updates and existing material
        public void addOrUpdateQuoteMaterial()
        {
            // Force all properties to be validated
            quoteMaterial.validateAllProps();

            if (selectedMaterial != null && quoteMaterial != null && !quoteMaterial.HasErrors)
            {
                // this is a newly added material
                if (quoteMaterial.listIndex == null)
                {
                    // Add to list
                    quoteMaterialList.Add(quoteMaterial);
                    // Set's its list index
                    quoteMaterial.listIndex = quoteMaterialList.IndexOf(quoteMaterial);
                }
                // This is an existing material
                else
                {
                    quoteMaterialList[quoteMaterial.listIndex.Value] = quoteMaterial;
                }

                // Set the button back  to it's original text
                addOrUpdateQuoteMaterialBtn = "Add to Quote";
                NotifyPropertyChanged("addOrUpdateQuoteMaterialBtn");

                // Update the total again
                updateTotalQuoteCosts();
                // Initialize a new material
                quoteMaterial = new QuoteMaterialModel(quote.quoteId, selectedMaterial.materialId, selectedMaterial.pricePerUnit, selectedMaterial.isService);
            }
            else
            {
                MessageBox.Show("Material Form has Errors");
            }
            
        }

        // Called when the remove quote material button is clicked
        public ICommand removeQuoteMaterialCmd
        {
            get
            {
                return new RelayCommand(quoteMaterialIndex => removeQuoteMaterial((int)quoteMaterialIndex), param => true);
            }
        }

        // Remove a quote material from the list
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

        // Called when edit quote button is clicked
        public ICommand editQuoteMaterialCmd
        {
            get
            {
                return new RelayCommand(quoteMaterialIndex => editQuoteMaterial((int)quoteMaterialIndex), param => true);
            }
        }

        // Edit the quote material
        public void editQuoteMaterial(int quoteMaterialIndex)
        {
            addOrUpdateQuoteMaterialBtn = "Update In Quote";
            NotifyPropertyChanged("addOrUpdateQuoteMaterialBtn");

            // Create a copy of this quote material
            quoteMaterial = quoteMaterialList.ElementAt(quoteMaterialIndex).clone();
            quoteMaterial.listIndex = quoteMaterialList.ElementAt(quoteMaterialIndex).listIndex;

            // Get the original material from the list if available
            MaterialModel materialFromList = materialList.Where(mat => mat.materialId == quoteMaterial.materialId).First();

            // Check that this material is in the list
            if (materialFromList != null)
            {
                selectedMaterial = materialFromList;
            }
        }

        // Updates the total costs of the quote
        public void updateTotalQuoteCosts ()
        {
            // Initialize subtotal and vat to 0
            double subtotal = 0;
            double vat = 0;

            // Loop over all materials added to the quote
            foreach (QuoteMaterialModel quoteMaterial in quoteMaterialList)
            {
                // Add to subtotal
                subtotal += quoteMaterial.totalCost;
                // If is a service, tax at 13.5%, otherwise 21% (Todo: Should be 23%)
                if (quoteMaterial.isService)
                {
                    vat += quoteMaterial.totalCost * 0.135;
                } else
                {
                    vat += quoteMaterial.totalCost * 0.21;
                }
            }

            // Set the values of the quote model
            quote.subtotal = subtotal;
            quote.vat = vat;
            quote.total = subtotal + vat;
        }

        // Called when save quote command button is clicked
        public ICommand saveQuoteCmd
        {
            get
            {
                return new RelayCommand(param => saveQuote(), param => true);
            }
        }

        // Saves a quote
        public void saveQuote()
        {
            if (materialList.Count > 0)
            {
                quote.validateAllProps();

                // If thw quote has errors
                if (!quote.HasErrors)
                {
                    // update or insert
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

        // Called when the print quote button is clicked
        public ICommand printQuoteCmd
        {
            get
            {
                return new RelayCommand(param => printQuote(), param => true);
            }
        }

        // Prins the quote
        private void printQuote ()
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                // Creates a new instance of QuotePrint, passing in the quote model
                printDialog.PrintVisual(new QuotePrint(quote), "Quote");
            }
        }
    }
}
