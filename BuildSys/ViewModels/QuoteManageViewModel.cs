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

        //MainViewModel parent;

        public QuoteManageViewModel(BaseViewModel parent)
        {
            this.parent = parent;

            quoteList = QuoteModel.getQuoteList();

            // Keep a copy of the quoteList so that we can restore the list after filtering
            originalQuoteList = new ObservableCollection<QuoteModel>(quoteList);


        }

        public override BaseViewModel getInstance(BaseViewModel parent)
        {
            return new QuoteManageViewModel(parent);
        }

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
                    filterQuotes();
                }
            }
        }

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
                NotifyPropertyChanged("quoteList");
            }
        }

        public ICommand editQuoteCmd
        {
            get
            {
                return new RelayCommand((quoteId) => editQuote((int)quoteId), param => true);
            }
        }

        public ICommand deleteQuoteCmd
        {
            get
            {
                return new RelayCommand((quoteId) => deleteQuote((int)quoteId), param => true);
            }
        }

        public void editQuote(int quoteId)
        {
            navigateTo(new QuoteFormViewModel(parent, quoteId, true));
        }

        public void deleteQuote(int quoteId)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                QuoteModel.deleteQuote(quoteId);
                MessageBox.Show("Quote Deleted");
                quoteList.Where(quote => quote.quoteId == quoteId).ToList().All(i => quoteList.Remove(i));
                originalQuoteList.Where(quote => quote.quoteId == quoteId).ToList().All(i => quoteList.Remove(i));
            }
        }

        public void filterQuotes()
        {
            quoteList = new ObservableCollection<QuoteModel>(originalQuoteList);
            Regex matchDescription = new Regex(@"^" + quoteFilter + @".+", RegexOptions.IgnoreCase);

            if (quoteFilter.Length > 0)
            {
                quoteList.Where(cust => !matchDescription.IsMatch(cust.description))
                    .ToList()
                    .All(i => quoteList.Remove(i));
            }
        }
    }
}
