using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibraryManagementSystem.ViewModels.Commands
{
    public class AddBookCommand : ICommand
    {
        public BookCreationVM BookCreationVM { get; set; }

        public AddBookCommand(BookCreationVM bookCreationVM)
        {
            BookCreationVM = bookCreationVM;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            Book book = parameter as Book;
            if (book == null || String.IsNullOrWhiteSpace(book.Title) || String.IsNullOrWhiteSpace(book.ISBN))
                return false;
            return true;
        }

        public void Execute(object parameter)
        {
            BookCreationVM.CreateBook();
        }
    }
}