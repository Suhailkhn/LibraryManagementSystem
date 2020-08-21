using LibraryManagementSystem.Classes.BAL;
using LibraryManagementSystem.Interfaces.BAL;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryManagementSystem.ViewModels
{
    public class BookCreationVM : INotifyPropertyChanged
    {
        #region StaticMembers

        private static readonly string FormatError = "Please enter the book details in a valid format.";

        private static readonly string InsertionError = "Cannot insert book. The book already exists in the system or has been deleted permanently from the system preventing reinsertion.";

        #endregion

        #region Dependencies

        private readonly IBookManager bookManager;
        private readonly IValidation validation;

        #endregion

        #region Properties

        public Book NewBook { get; set; }

        public AddBookCommand CreateBookCommand { get; set; }

        #endregion

        #region EventHandlers

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public BookCreationVM(IBookManager _bookManager, IValidation _validation)
        {
            bookManager = _bookManager;
            validation = _validation;

            NewBook = new Book();
            CreateBookCommand = new AddBookCommand(this);
        }

        #region Functions

        private bool PerformValidation()
        {
            return validation.ValidateBook(NewBook);
        }

        public async void CreateBook()
        {
            NewBook.DateAdded = DateTime.Now;
            NewBook.AvailableCopies = NewBook.TotalCopies;

            if (!PerformValidation())
            {
                DisplayErrorMessage(FormatError);
                return;
            }

            bool success = await bookManager.CreateBook(NewBook).ConfigureAwait(false);

            if (success)
            {
                ClearBookDetails(NewBook);
                OnPropertyChanged("NewBook");
                MessageBox.Show("Book Successfully Added !", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                DisplayErrorMessage(InsertionError);
            }   
        }

        private void ClearBookDetails(Book newBook)
        {
            newBook.Title = String.Empty;
            newBook.Authors = String.Empty;
            newBook.ISBN = String.Empty;
            newBook.PlainISBN = String.Empty;
            newBook.TotalCopies = 0;
            newBook.PublishingYear = 0;
        }

        private void DisplayErrorMessage(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion
    }
}