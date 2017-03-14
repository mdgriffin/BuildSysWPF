﻿using BuildSys.Models;
using System.Windows;
using System.Windows.Input;

namespace BuildSys.ViewModels
{
    
    public class CustomerFormViewModel: BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the CustomerFormViewModel class.
        /// </summary>
        public CustomerFormViewModel()
        {
            customer = new CustomerModel();
        }

        public CustomerModel customer { get; set; }

        public ICommand saveCustomerCommand
        {
            get
            {
                return new RelayCommand(() => saveCustomer());
            }
        }

        public void saveCustomer ()
        {
            MessageBox.Show("Now Save the Customer", "Information");

            // Return to the home page?
        }

    }
}