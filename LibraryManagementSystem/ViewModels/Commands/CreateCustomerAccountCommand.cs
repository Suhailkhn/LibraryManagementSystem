using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibraryManagementSystem.ViewModels.Commands
{
    class CreateCustomerAccountCommand : ICommand
    {
        public CustomerCreationVM CustomerCreationVM { get; set; }

        public CreateCustomerAccountCommand(CustomerCreationVM customerCreationVM)
        {
            CustomerCreationVM = customerCreationVM;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            Customer newCustomer = parameter as Customer;
            
            if(newCustomer == null || 
               String.IsNullOrWhiteSpace(newCustomer.FirstName) ||
               String.IsNullOrWhiteSpace(newCustomer.Email) ||
               newCustomer.DateOfBirth == DateTime.MinValue)
            {
                return false;
            }

            return true;
        }

        public void Execute(object parameter)
        {
            CustomerCreationVM.CreateCustomerAccount();
        }
    }
}
