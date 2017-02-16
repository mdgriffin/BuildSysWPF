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
            // TODO: Validate Data
            // TODO: Cannot save customer with a single quote in their surname
            char accountType = radAccType_business.IsChecked.Value ? 'B' : 'P';

            if (accountType == 'P')
            {
                try
                {
                    new CustomerModel(
                        cmbTitle.SelectedItem.ToString(),
                        txtFirstname.Text,
                        txtSurname.Text,
                        txtStreet.Text,
                        txtTown.Text,
                        cmbCounty.SelectedItem.ToString(),
                        txtTel.Text, txtEmail.Text,
                        accountType
                    ).insertPrivateCustomer();

                    clearForm();

                    MessageBox.Show("New Private Customer Created");
                } catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                    MessageBox.Show("Error Saving Customer, please try again");
                }
            } else
            {
                try
                {
                    new CustomerModel(
                        cmbTitle.SelectedItem.ToString(),
                        txtFirstname.Text,
                        txtSurname.Text,
                        txtStreet.Text,
                        txtTown.Text,
                        cmbCounty.SelectedItem.ToString(),
                        txtTel.Text,
                        txtEmail.Text,
                        accountType,
                        txtCompanyName.Text,
                        txtVatNumber.Text
                    ).insertBusinessCustomer();

                    clearForm();

                    MessageBox.Show("New Business Customer Created");
                } catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                    MessageBox.Show("Error Saving Customer, please try again");
                }
            }
            
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

        private void radAccType_Checked(object sender, RoutedEventArgs e)
        {
            if (radAccType_business != null)
            {
                char accountType = radAccType_business.IsChecked.Value ? 'B' : 'P';

                if (accountType == 'P')
                {
                    wpBusinessName.Visibility = Visibility.Collapsed;
                    wpVatNumber.Visibility = Visibility.Collapsed;
                }
                else
                {
                    wpBusinessName.Visibility = Visibility.Visible;
                    wpVatNumber.Visibility = Visibility.Visible;
                }
            }

        }

        private void clearForm ()
        {
            cmbTitle.SelectedIndex = 0;
            txtFirstname.Text = "";
            txtSurname.Text = "";
            txtStreet.Text = "";
            txtTown.Text = "";
            cmbCounty.SelectedIndex = 0;
            txtTel.Text = "";
            txtEmail.Text = "";
            txtCompanyName.Text = "";
            txtVatNumber.Text = "";
        }
    }
}
