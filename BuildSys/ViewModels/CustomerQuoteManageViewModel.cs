using BuildSys.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace BuildSys.ViewModels
{
    class CustomerQuoteManageViewModel : BaseViewModel
    {

        public CustomerQuoteManageViewModel(BaseViewModel parent, int customerId)
        {
            this.parent = parent;

            // Get the list of the customer's quotes
            quoteList = QuoteModel.getCustomersQuotes(customerId);
            customer = CustomerModel.getCustomer(customerId);

            // Set the heading text using the customer's name
            headingText = "Quotes issued to " + customer.firstname + " " + customer.surname;

            // Keep a copy of the quoteList so that we can restore the list after filtering
            originalQuoteList = new ObservableCollection<QuoteModel>(quoteList);
        }

        // public properties accessible from view
        public CustomerModel customer { get; set; }
        public String headingText { get; set; }

        private String _quoteFilter;
        public String quoteFilter
        {
            get
            {
                return _quoteFilter;
            }
            set
            {
                if (value != _quoteFilter)
                {
                    _quoteFilter = value;
                    // When the filter changes filter the quotes
                    filterQuotes();
                }
            }
        }

        // Copy of the original quote list
        private ObservableCollection<QuoteModel> originalQuoteList;

        private ObservableCollection<QuoteModel> _quoteList = new ObservableCollection<QuoteModel>();
        public ObservableCollection<QuoteModel> quoteList
        {
            get
            {
                return _quoteList ?? (_quoteList = new ObservableCollection<QuoteModel>());
            }
            set
            {
                if (value == null) return;
                _quoteList = value;
                // Notify that the list has been changed so that the view can be updated
                NotifyPropertyChanged("quoteList");
            }
        }

        // Called when edit quote button clicked
        public ICommand editQuoteCmd
        {
            get
            {
                return new RelayCommand((quoteId) => editQuote((int)quoteId), param => true);
            }
        }

        // Called when delete quote button pressed
        public ICommand deleteQuoteCmd
        {
            get
            {
                return new RelayCommand((quoteId) => deleteQuote((int)quoteId), param => true);
            }
        }

        // Called when the edit quote is clicked
        public void editQuote(int quoteId)
        {
            navigateTo(new QuoteFormViewModel(parent, quoteId, true));
        }

        // Called when the delete quote button is clicked
        public void deleteQuote(int quoteId)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);

            // Confirm before deleting
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                // Delete the specified quotes
                QuoteModel.deleteQuote(quoteId);
                MessageBox.Show("Quote Deleted");
                // Updated list to reflect remoded quote
                quoteList.Where(quote => quote.quoteId == quoteId).ToList().All(i => quoteList.Remove(i));
                originalQuoteList.Where(quote => quote.quoteId == quoteId).ToList().All(i => quoteList.Remove(i));
            }
        }

        // Filter the quotes based on the user's input
        public void filterQuotes()
        {
            // Get the original list so that the filtering is on the whole list
            quoteList = new ObservableCollection<QuoteModel>(originalQuoteList);
            Regex matchDescription = new Regex(@"^" + quoteFilter + @".+", RegexOptions.IgnoreCase);

            if (quoteFilter.Length > 0)
            {
                // Remove from where where there is no match
                quoteList.Where(cust => !matchDescription.IsMatch(cust.description))
                    .ToList()
                    .All(i => quoteList.Remove(i));
            }
        }
    }
}
