using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibraryManagementSystem.ViewModels.Commands
{
    public class FetchCustomerDetailsCommand : ICommand
    {
        public CustomerUpdationVM CustomerUpdationVM { get; set; }

        public FetchCustomerDetailsCommand(CustomerUpdationVM customerUpdationVM)
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
            return true;
        }

        public void Execute(object parameter)
        {
            CustomerUpdationVM.FetchCustomerDetails();
        }
    }
}
