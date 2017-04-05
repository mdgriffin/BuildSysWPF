using System;
using System.Collections.Generic;
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
    /// Interaction logic for QuoteForm.xaml
    /// </summary>
    public partial class QuoteForm : UserControl
    {
        public QuoteForm()
        {
            InitializeComponent();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                
                spFilter.Visibility = Visibility.Collapsed;
                spGrdMtrial.Visibility = Visibility.Collapsed;
                spMaterialFom.Visibility = Visibility.Collapsed;

                printDialog.PrintVisual(quoteFrm, "My First Print Job");

                spFilter.Visibility = Visibility.Visible;
                spGrdMtrial.Visibility = Visibility.Visible;
                spMaterialFom.Visibility = Visibility.Visible;
            }
        }
    }
}
