using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibraryManagementSystem.ViewModels.Commands
{
    class SearchBookCommand : ICommand
    {
        public BookSearchVM BookSearchVM { get; set; }

        public SearchBookCommand(BookSearchVM bookSearchVM)
        {
            BookSearchVM = bookSearchVM;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            string query = parameter as string;
            if (String.IsNullOrWhiteSpace(query))
                return false;
            return true;
        }

        public void Execute(object parameter)
        {
            BookSearchVM.SearchBooks();
        }
    }
}
