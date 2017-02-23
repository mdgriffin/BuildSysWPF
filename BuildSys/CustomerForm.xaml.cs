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
    /// Interaction logic for CustomerForm.xaml
    /// </summary>
    public partial class CustomerForm : UserControl
    {
        public CustomerForm()
        {
            InitializeComponent();
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Validate Data
            // TODO: Cannot save customer with a single quote in their surname
            String title = cmbTitle.SelectedItem.ToString();
            String firstName = txtFirstname.Text;
            String surname = txtSurname.Text;
            String street = txtStreet.Text;
            String town = txtTown.Text;
            String county = cmbCounty.SelectedItem.ToString();
            String telNum = txtTel.Text;
            String email = txtEmail.Text;
            char accountType = radAccType_business.IsChecked.Value ? 'B' : 'P';

            // Company Details
            String company = txtCompanyName.Text;
            String vatNum = txtVatNumber.Text;


            if (accountType == 'P')
            {
                try
                {
                    CustomerModel cust = new CustomerModel(title, firstName, surname, street, town, county, telNum, email, accountType);

                    if (cust.validates())
                    {
                        cust.insertPrivateCustomer();
                        clearForm();
                        MessageBox.Show("New Private Customer Created");
                    }
                    else
                    {
                        // TODO: Show errors under form fields
                        // cust.getErrors();
                        MessageBox.Show("Form has errors");
                    }
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                    MessageBox.Show("Error Saving Customer, please try again");
                }
            }
            else
            {
                try
                {
                    CustomerModel businessCust = new CustomerModel(title, firstName, surname, street, town, county, telNum, email, accountType, company, vatNum);

                    if (businessCust.validates())
                    {
                        businessCust.insertBusinessCustomer();
                        clearForm();
                        MessageBox.Show("New Business Customer Created");
                    }
                    else
                    {
                        // TODO: Show errors under form fields
                        // businessCust.getErrors();
                        MessageBox.Show("Form has errors");
                    }
                }
                catch (Exception exc)
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
            List<string> data = new List<string>() { "Mr", "Mrs", "Ms", "Miss", "Mx" };

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

        private void clearForm()
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
