using LibraryManagementSystem.Classes.BAL;
using LibraryManagementSystem.Interfaces.BAL;
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
    public class CustomerDeletionVM : INotifyPropertyChanged
    {
        #region Static Members

        private static readonly string FormatError = "Please enter a number greater than 0.";

        private static readonly string DeletionError = "Could not delete customer account. Please enter a valid ID OR the customer account has already been deleted.";

        #endregion

        #region Dependencies

        private readonly ICustomerManager customerManager;

        #endregion

        #region Properties

        public uint CustomerId { get; set; }

        public DeleteCustomerAccountCommand DeleteCustomer { get; set; }

        #endregion

        #region Event Handlers

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public CustomerDeletionVM(ICustomerManager _customerManager)
        {
            customerManager = _customerManager;
            DeleteCustomer = new DeleteCustomerAccountCommand(this);
        }

        #region Functions

        public async void DeleterCustomerAccount()
        {
            if(!PerformValidation())
            {
                DisplayErrorMessage(FormatError);
                return;
            }

            bool success = await customerManager.DeleteCustomer(CustomerId).ConfigureAwait(false);

            if(success)
            {
                CustomerId = 0;
                OnPropertyChanged("CustomerId");
                MessageBox.Show("Successfully deleted customer account", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                DisplayErrorMessage(DeletionError);
            }
        }

        private void DisplayErrorMessage(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private bool PerformValidation()
        {
            if (CustomerId > 0)
                return true;
            return false;
        }

        #endregion
    }
}