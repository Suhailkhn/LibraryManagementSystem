using LibraryManagementSystem.Classes.BAL;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryManagementSystem.ViewModels
{
    public class CustomerUpdationVM :INotifyPropertyChanged
    {
        #region Static Members

        private static readonly string FormatError = "Please enter a number greater than 0.";
        private static readonly string FetchError = "Could not find customer. Please enter a valid Customer ID OR the customer account has already been deleted.";
        private static readonly string UpdationFormatError = "Please enter all the customer details in a valid format.";
        private static readonly string UpdationError = "Could not update customer details.";

        #endregion

        #region Properties

        private Customer fetchedCustomer;

        public Customer FetchedCustomer
        {
            get { return fetchedCustomer; }
            set
            {
                fetchedCustomer = value;
                OnPropertyChanged("FetchedCustomer");
            }
        }

        private uint suppliedCustomerId;

        public uint SuppliedCustomerId
        {
            get { return suppliedCustomerId; }
            set
            {
                suppliedCustomerId = value;
                OnPropertyChanged("SuppliedCustomerId");
            }
        }

        public FetchCustomerDetailsCommand FetchCustomerCommand { get; set; }

        public UpdateCustomerAccountCommand UpdateCustomerCommand { get; set; }

        private Visibility updationVisibility;

        public Visibility UpdationVisibility
        {
            get { return updationVisibility; }
            set
            {
                
                updationVisibility = value;
                OnPropertyChanged("UpdationVisibility");
            }
        }

        #endregion

        #region Event Handlers

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public CustomerUpdationVM()
        {
            FetchCustomerCommand = new FetchCustomerDetailsCommand(this);
            UpdateCustomerCommand = new UpdateCustomerAccountCommand(this);
            FetchedCustomer = new Customer();
            UpdationVisibility = Visibility.Hidden;
        }

        #region Functions

        public async void FetchCustomerDetails()
        {
            if(!PerformFetchValidation())
            {
                DisplayErrorMessage(FormatError);
                return;
            }

            var customer = await CustomerManager.GetCustomerDetails(SuppliedCustomerId).ConfigureAwait(false);

            if(customer == null)
            {
                DisplayErrorMessage(FetchError);
            }
            else
            {
                FetchedCustomer = customer;
                UpdationVisibility = Visibility.Visible;
            }
        }

        public async void UpdateCustomerAccount()
        {
            if(!PerformUpdateValidation())
            {
                DisplayErrorMessage(UpdationFormatError);
                return;
            }

            bool success = await CustomerManager.UpdateCustomer(FetchedCustomer).ConfigureAwait(false);

            if(success)
            {
                SuppliedCustomerId = 0;
                UpdationVisibility = Visibility.Hidden;
                MessageBox.Show("Successfully updated customer details.", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                DisplayErrorMessage(UpdationError);
            }
        }

        private void DisplayErrorMessage(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private bool PerformFetchValidation()
        {
            return SuppliedCustomerId > 0;
        }

        private bool PerformUpdateValidation()
        {
            return Validation.ValidateCustomer(FetchedCustomer);
        }

        #endregion
    }
}