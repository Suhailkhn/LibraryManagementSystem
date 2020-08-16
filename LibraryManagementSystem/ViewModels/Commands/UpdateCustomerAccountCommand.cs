using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibraryManagementSystem.ViewModels.Commands
{
    public class UpdateCustomerAccountCommand : ICommand
    {
        public CustomerUpdationVM CustomerUpdationVM { get; set; }

        public UpdateCustomerAccountCommand(CustomerUpdationVM customerUpdationVM)
        {
            CustomerUpdationVM = customerUpdationVM;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            Customer updatedCustomer = parameter as Customer;

            if (updatedCustomer == null ||
               String.IsNullOrWhiteSpace(updatedCustomer.FirstName) ||
               String.IsNullOrWhiteSpace(updatedCustomer.Email) ||
               updatedCustomer.DateOfBirth == DateTime.MinValue)
            {
                return false;
            }

            return true;
        }

        public void Execute(object parameter)
        {
            CustomerUpdationVM.UpdateCustomerAccount();
        }
    }
}
