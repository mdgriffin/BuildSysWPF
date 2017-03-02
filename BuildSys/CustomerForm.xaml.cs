using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

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

            Counties = new List<string>() {
                "Antrim", "Armagh", "Carlow", "Cavan", "Clare", "Cork", "Derry", "Donegal",
                "Down", "Dublin", "Fermanagh", "Galway", "Kerry", "Kildare", "Kilkenny", "Laois",
                "Leitrim", "Limerick", "Longford", "Louth", "Mayo", "Meath", "Monaghan", "Offaly",
                "Roscommon", "Sligo", "Tipperary", "Tyrone", "Waterford", "Westmeath", "Wexford", "Wicklow"
            };

            Titles = new List<string>() { "Mr", "Mrs", "Ms", "Miss", "Mx" };

            customer = new CustomerModel();

            DataContext = this;
        }

        public List<String> Counties { get; set; }
        public List<String> Titles { get; set; }
        public CustomerModel customer { get; set; }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            char accountType = radAccType_business.IsChecked.Value ? 'B' : 'P';

            if (customer.validates())
            {
                if (accountType == 'P')
                {
                    customer.insertPrivateCustomer();
                }
                else
                {
                    customer.insertBusinessCustomer();
                }

                MessageBox.Show("Successfully Registered A New Customer");
                clearForm();
            } else
            {
                // TODO: Replace with with better error handling
                MessageBox.Show("Form has errors");
            }
                
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
