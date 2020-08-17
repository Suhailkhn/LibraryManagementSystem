using LibraryManagementSystem.Models;
using LibraryManagementSystem.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibraryManagementSystem.ViewModels.Commands
{
    public class OpenBookUpdateWindowCommand : ICommand
    {
        public BookSearchVM BookSearchVM { get; set; }

        public OpenBookUpdateWindowCommand(BookSearchVM bookSearchVM)
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
            return true;
        }

        public void Execute(object parameter)
        {
            var selectedBook = parameter as Book;
            if(selectedBook != null)
            {
                BookSearchVM.SelectedBook = selectedBook;
                var newWindow = new BookUpdationWindow();
                newWindow.DataContext = BookSearchVM;
                newWindow.ShowDialog();
            }
        }
    }
}
