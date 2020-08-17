using LibraryManagementSystem.Classes.BAL;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace LibraryManagementSystem.ViewModels
{
    public class BookSearchVM : INotifyPropertyChanged
    {
        #region Static Members

        private static readonly string BookSearchEmpty = "No matching books found.";

        private static readonly string BookUpdateFormatError = "Please enter the book details in a valid format.";
        private static readonly string BookUpdationError = "Could not update book details.";
        private static readonly string BookUpdationSuccess = "Successfully updated book details.";

        private static readonly string BookDeletionFormatError = "The Book ID should be a number greater than 0.";
        private static readonly string BookDeletionError = "Cannot delete book. The book does not exist or has already been deleted.";
        private static readonly string BookDeletionSuccess = "Successfully deleted book";

        #endregion

        #region Properties

        private string query;

        public string Query
        {
            get { return query; }
            set
            {
                query = value;
                OnPropertyChanged("Query");
            }
        }

        public SearchBookCommand SearchBooksCommand { get; set; }

        public DeleteBookCommand DeleteBookCommand { get; set; }

        public OpenBookUpdateWindowCommand OpenUpdateCommand { get; set; }

        public UpdateBookCommand UpdateBookCommand { get; set; }

        public ObservableCollection<Book> Books { get; set; }

        public Book SelectedBook { get; set; }

        #endregion

        #region Event Handlers

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public BookSearchVM()
        {
            SearchBooksCommand = new SearchBookCommand(this);
            DeleteBookCommand = new DeleteBookCommand(this);
            OpenUpdateCommand = new OpenBookUpdateWindowCommand(this);
            UpdateBookCommand = new UpdateBookCommand(this);

            Books = new ObservableCollection<Book>();
        }

        #region Functions

        public async void SearchBooks()
        {
            var books = await BookManager.SearchBooks(Query).ConfigureAwait(false);

            Books.Clear();

            if(books != null && books.Any())
            {
                foreach (var book in books)
                {
                    Books.Add(book);
                }
            }
            else
            {
                DisplayInfoMessage(BookSearchEmpty, "Info");
            }
        }

        public async void DeleteBook()
        {
            if(!PerformBookDeletionValidation())
            {
                DisplayErrorMessage(BookDeletionFormatError);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete this book pemanently?", "Confirmation", MessageBoxButton.YesNo);
            if (result != MessageBoxResult.Yes)
                return;

            bool success = await BookManager.DeleteBook(SelectedBook.BookId).ConfigureAwait(false);

            if (success)
            {
                Books.Remove(SelectedBook);
                DisplayInfoMessage(BookDeletionSuccess, "Success!");
            }
            else
            {
                DisplayErrorMessage(BookDeletionError);
            }
        }

        public async void UpdateBook()
        {
            if(!PerformBookUpdationValidation())
            {
                DisplayErrorMessage(BookUpdateFormatError);
                return;
            }

            bool success = await BookManager.UpdateBook(SelectedBook).ConfigureAwait(false);

            if(success)
            {
                CollectionViewSource.GetDefaultView(this.Books).Refresh();
                DisplayInfoMessage(BookUpdationSuccess, "Success!");
            }
            else
            {
                DisplayErrorMessage(BookUpdationError);
            }
        }

        private bool PerformBookDeletionValidation()
        {
            return SelectedBook != null && SelectedBook.BookId > 0;
        }

        private bool PerformBookUpdationValidation()
        {
            return Validation.ValidateBook(SelectedBook);
        }

        private void DisplayErrorMessage(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void DisplayInfoMessage(string errorMessage, string caption)
        {
            MessageBox.Show(errorMessage, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion
    }
}