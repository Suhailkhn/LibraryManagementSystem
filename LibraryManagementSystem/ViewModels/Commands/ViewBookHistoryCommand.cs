using LibraryManagementSystem.Classes;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LibraryManagementSystem.ViewModels.Commands
{
    public class ViewBookHistoryCommand : ICommand
    {
        public BookSearchVM BookSearchVM { get; set; }

        public ViewBookHistoryCommand(BookSearchVM bookSearchVM)
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
            if (selectedBook == null)
                return;

            BookSearchVM.SelectedBook = selectedBook;
            BookSearchVM.GetBookHistory();

            if(BookSearchVM.BookHistory != null && BookSearchVM.BookHistory.Any())
            {
                var newWindow = DependencyInjector.Retrieve<BookHistoryWindow>();
                newWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("This book has not been checked out yet.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
