using BuildSys.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BuildSys.ViewModels
{
    class QuoteManageViewModel: BaseViewModel
    {

        public QuoteManageViewModel(BaseViewModel parent)
        {
            this.parent = parent;

            // Get the list of all quotes
            quoteList = QuoteModel.getQuoteList();
            // gets the list of all deleted questions
            deletedQuoteList = QuoteModel.getDeletedQuoteList();

            // Keep a copy of the quoteList so that we can restore the list after filtering
            originalQuoteList = new ObservableCollection<QuoteModel>(quoteList);
        }

        // The filter input text
        private String _quoteFilter = "";
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
                    // filter quotes when filter has changed
                    filterQuotes();
                }
            }
        }

        // publicly accessible properties from view
        public ObservableCollection<QuoteModel> deletedQuoteList { get; set; }

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
                // Notify that the quote list has changed
                NotifyPropertyChanged("quoteList");
            }
        }

        // Called when the edit quote button is clicked
        public ICommand editQuoteCmd
        {
            get
            {
                return new RelayCommand((quoteId) => editQuote((int)quoteId), param => true);
            }
        }

        // Called when the delete quote command button is clicked
        public ICommand deleteQuoteCmd
        {
            get
            {
                return new RelayCommand((quoteId) => deleteQuote((int)quoteId), param => true);
            }
        }

        // Called when the restore quote button is clicked
        public ICommand restoreQuoteCmd
        {
            get
            {
                return new RelayCommand((quoteId) => restoreQuote((int)quoteId), param => true);
            }
        }

        // Navigates to the edit quote view
        public void editQuote(int quoteId)
        {
            navigateTo(new QuoteFormViewModel(parent, quoteId, true));
        }

        // Delete a quote
        // Remove from the active quote list
        // And add to the deleted quote list
        public void deleteQuote(int quoteId)
        {
            QuoteModel.deleteQuote(quoteId);

            QuoteModel deletedQuote = quoteList.Where(quote => quote.quoteId == quoteId).First();

            quoteList.Remove(deletedQuote);
            originalQuoteList.Remove(deletedQuote);

            deletedQuoteList.Add(deletedQuote);
        }

        // Restores a deleted quote
        // Removing from deleted quotes
        // Adding to active quotes
        public void restoreQuote(int quoteId)
        {
            QuoteModel.restoreQuote(quoteId);
            QuoteModel restoredQuote = deletedQuoteList.Where(quote => quote.quoteId == quoteId).First();

            quoteList.Add(restoredQuote);
            originalQuoteList.Add(restoredQuote);

            deletedQuoteList.Remove(restoredQuote);

            filterQuotes();
        }

        // Filters the based on user input
        public void filterQuotes()
        {
            quoteList = new ObservableCollection<QuoteModel>(originalQuoteList);
            Regex matchDescription = new Regex(@"^" + quoteFilter + @".+", RegexOptions.IgnoreCase);

            if (quoteFilter.Length > 0)
            {
                // Remove from quote list where the quote does not match
                quoteList.Where(cust => !matchDescription.IsMatch(cust.description))
                    .ToList()
                    .All(i => quoteList.Remove(i));
            }
        }
    }
}
