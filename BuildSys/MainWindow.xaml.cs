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

namespace BuildSys
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Register a new customer");
            // TODO: Validate Data
            CustomerModel newCust = new CustomerModel(
               cmbTitle.SelectedItem.ToString(), txtFirstName.Text, txtSurname.Text, txtStreet.Text, txtTown.Text, cmbCounty.SelectedItem.ToString(), txtTel.Text, txtEmail.Text
            );

            newCust.insertPrivateCustomer();
        }

        private void cmbCounty_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>() {
                "Antrim", "Armagh", "Carlow", "Cavan", "Clare", "Cork", "Derry", "Donegal",
                "Down", "Dublin", "Fermanagh", "Galway", "Kerry", "Kildare", "Kilkenny", "Laois",
                "Leitrim", "Limerick", "Longford", "Louth", "Mayo", "Meath", "Monaghan", "Offaly",
                "Roscommon", "Sligo", "Tipperary", "Tyrone", "Waterford", "Westmeath", "Wexford", "Wicklow"
            };

            cmbCounty.ItemsSource = data;
        }

        private void cmbTitle_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>() { "Mr", "Mrs", "Ms", "Miss", "Mx"};

            cmbTitle.ItemsSource = data;
        }
    }
}
