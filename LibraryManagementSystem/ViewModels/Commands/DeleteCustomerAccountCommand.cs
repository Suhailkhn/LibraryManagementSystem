using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibraryManagementSystem.ViewModels.Commands
{
    public class DeleteCustomerAccountCommand : ICommand
    {
        public CustomerDeletionVM CustomerDeletionVM { get; set; }

        public DeleteCustomerAccountCommand(CustomerDeletionVM customerDeletionVM)
        {
            CustomerDeletionVM = customerDeletionVM;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            CustomerDeletionVM.DeleterCustomerAccount();
        }
    }
}
