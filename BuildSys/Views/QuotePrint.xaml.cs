using BuildSys.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BuildSys.Views
{
    /// <summary>
    /// Interaction logic for QuotePrint.xaml
    /// </summary>
    public partial class QuotePrint : Page
    {
        public QuotePrint(QuoteModel quote)
        {
            InitializeComponent();

            viewQuotePrint.DataContext = this;

            quoteMaterialList = QuoteMaterialModel.getQuoteMaterialList(quote.quoteId);

            customer = CustomerModel.getCustomer(quote.customer.customerId);

            setting = SettingModel.getSetting();

            this.quote = quote;
        }

        public CustomerModel customer { get; set; }
        public SettingModel setting { get; set; }
        public QuoteModel quote { get; set; }
        public Collection<QuoteMaterialModel> quoteMaterialList { get; set; }
    }
}
