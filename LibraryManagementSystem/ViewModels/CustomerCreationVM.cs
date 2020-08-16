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
    class CustomerCreationVM : INotifyPropertyChanged
    {
        #region Static Members

        private static readonly string FormatError = "Please enter all the customer details in a valid format.";

        private static readonly string InsertionError = "Cannot create customer account.";

        private static readonly string InsertionSuccess = "Successfully created customer account!\nCustomer ID : {0}";

        #endregion

        #region Properties

        public Customer NewCustomer { get; set; }

        public CreateCustomerAccountCommand CreateCustomerCommand { get; set; }

        #endregion

        #region Event Handlers

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public CustomerCreationVM()
        {
            NewCustomer = new Customer();
            NewCustomer.DateOfBirth = DateTime.Now.Date;
            CreateCustomerCommand = new CreateCustomerAccountCommand(this);
        }

        #region Functions

        public async void CreateCustomerAccount()
        {
            if(!PerformValidation())
            {
                DisplayErrorMessage(FormatError);
                return;
            }

            NewCustomer.AccountCreatedOn = DateTime.Now;

            uint customerId = await CustomerManager.CreateCustomer(NewCustomer).ConfigureAwait(false);

            if(customerId == 0)
            {
                DisplayErrorMessage(InsertionError);
            }
            else
            {
                ClearCustomerDetails();
                OnPropertyChanged("NewCustomer");
                MessageBox.Show(String.Format(InsertionSuccess, customerId), "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ClearCustomerDetails()
        {
            NewCustomer.FirstName = String.Empty;
            NewCustomer.LastName = String.Empty;
            NewCustomer.Email = String.Empty;
            NewCustomer.DateOfBirth = DateTime.Now.Date;
        }

        private bool PerformValidation()
        {
            return Validation.ValidateCustomer(NewCustomer);
        }

        private void DisplayErrorMessage(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion
    }
}