using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibraryManagementSystem.ViewModels.Commands
{
    public class UpdateBookCommand : ICommand
    {
        public BookSearchVM BookSearchVM { get; set; }

        public UpdateBookCommand(BookSearchVM bookSearchVM)
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
            var selectedBook = BookSearchVM.SelectedBook;

            if(selectedBook == null || String.IsNullOrWhiteSpace(selectedBook.Title) || String.IsNullOrWhiteSpace(selectedBook.ISBN))
            {
                return false;
            }

            return true;
        }

        public void Execute(object parameter)
        {
            BookSearchVM.UpdateBook();
        }
    }
}
