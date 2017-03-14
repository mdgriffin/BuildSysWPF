using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace BuildSys.Views
{
    /// <summary>
    /// Interaction logic for CustomerForm.xaml
    /// </summary>
    public partial class CustomerForm : UserControl
    {
        public CustomerForm()
        {
            InitializeComponent();

            //customer = new CustomerModel();

            //DataContext = this;
            //DataContext = new CustomerFormViewModel();
        }

        //public CustomerModel customer { get; set; }

        /*
        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            char accountType = radAccType_business.IsChecked.Value ? 'B' : 'P';

            if (!customer.HasErrors)
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
                // TODO: Reset the view to a new CustomerForm
            } else
            {
                // TODO: Replace with with better error handling
                MessageBox.Show("Form has errors");
            }
        }
        */

        /*
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
        */
    }
}
