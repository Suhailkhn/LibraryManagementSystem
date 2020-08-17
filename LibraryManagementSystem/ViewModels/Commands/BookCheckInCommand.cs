using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibraryManagementSystem.ViewModels.Commands
{
    public class BookCheckInCommand : ICommand
    {
        public BookSearchVM BookSearchVM { get; set; }

        public BookCheckInCommand(BookSearchVM bookSearchVM)
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
            return BookSearchVM.SelectedBook.AvailableCopies < BookSearchVM.SelectedBook.TotalCopies;
        }

        public void Execute(object parameter)
        {
            BookSearchVM.CheckInBook();
        }
    }
}
