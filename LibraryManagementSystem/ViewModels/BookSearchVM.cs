using LibraryManagementSystem.Classes.BAL;
using LibraryManagementSystem.Interfaces.BAL;
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

        private static readonly string BookCheckOutSuccess = "Successfully checked out book.";
        private static readonly string BookCheckOutConditionError = "Customer ID should be a number greater than 0 OR all copies of this book have been checked out.";
        private static readonly string BookCheckOutError = "Could not check out book. Please enter a valid Customer ID OR the customer currently has this book checked out.";

        private static readonly string BookCheckInSuccess = "Successfully checked in book.";
        private static readonly string BookCheckInConditionError = "Customer ID should be a number greater than 0 OR all copies of this book have already been checked in.";
        private static readonly string BookCheckInError = "Could not check in book. Please enter a valid Customer ID OR the customer has not currently checked out this book.";

        #endregion

        #region Dependencies

        private readonly IBookManager bookManager;
        private readonly IValidation validation;

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

        public ObservableCollection<Book> Books { get; set; }

        public Book SelectedBook { get; set; }

        public uint SuppliedCustomerId { get; set; }

        public List<BookTransaction> BookHistory { get; set; }

        #region Commands

        public SearchBookCommand SearchBooksCommand { get; set; }

        public DeleteBookCommand DeleteBookCommand { get; set; }

        public OpenBookUpdateWindowCommand OpenUpdateCommand { get; set; }

        public UpdateBookCommand UpdateBookCommand { get; set; }

        public EnableBookCheckOutCommand EnableCheckOutCommand { get; set; }

        public EnableBookCheckInCommand EnableCheckInCommand { get; set; }

        public BookCheckOutCommand CheckOutCommand { get; set; }

        public BookCheckInCommand CheckInCommand { get; set; }

        public ViewBookHistoryCommand ViewBookHistoryCommand { get; set; }

        #endregion

        #endregion

        #region Event Handlers

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public BookSearchVM(IBookManager _bookManager, IValidation _validation)
        {
            bookManager = _bookManager;
            validation = _validation;

            SearchBooksCommand = new SearchBookCommand(this);
            DeleteBookCommand = new DeleteBookCommand(this);
            OpenUpdateCommand = new OpenBookUpdateWindowCommand(this);
            UpdateBookCommand = new UpdateBookCommand(this);
            EnableCheckOutCommand = new EnableBookCheckOutCommand(this);
            EnableCheckInCommand = new EnableBookCheckInCommand(this);
            CheckOutCommand = new BookCheckOutCommand(this);
            CheckInCommand = new BookCheckInCommand(this);
            ViewBookHistoryCommand = new ViewBookHistoryCommand(this);

            Books = new ObservableCollection<Book>();
            BookHistory = new List<BookTransaction>();
        }

        #region Functions

        public async void SearchBooks()
        {
            var books = await bookManager.SearchBooks(Query).ConfigureAwait(false);

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

            bool success = await bookManager.DeleteBook(SelectedBook.BookId).ConfigureAwait(false);

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

            bool success = await bookManager.UpdateBook(SelectedBook).ConfigureAwait(false);

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

        public async void CheckOutBook()
        {
            if(!PerformCheckOutValidation())
            {
                DisplayErrorMessage(BookCheckOutConditionError);
                return;
            }

            var transaction = new BookTransaction
            {
                Customer = new Customer
                {
                    CustomerId = SuppliedCustomerId
                },
                Book = new Book
                {
                    BookId = SelectedBook.BookId
                },
                CheckOut = DateTime.Now
            };

            bool success = await bookManager.CheckOutBook(transaction).ConfigureAwait(false);

            SuppliedCustomerId = 0;
            OnPropertyChanged("SuppliedCustomerId");

            if (success)
            {
                --SelectedBook.AvailableCopies;
                CollectionViewSource.GetDefaultView(this.Books).Refresh();

                DisplayInfoMessage(BookCheckOutSuccess, "Success!");
            }
            else
            {
                DisplayErrorMessage(BookCheckOutError);
            }
        }

        public async void CheckInBook()
        {
            if (!PerformCheckInValidation())
            {
                DisplayErrorMessage(BookCheckInConditionError);
                return;
            }

            var transaction = new BookTransaction
            {
                Customer = new Customer
                {
                    CustomerId = SuppliedCustomerId
                },
                Book = new Book
                {
                    BookId = SelectedBook.BookId
                },
                CheckIn = DateTime.Now
            };

            bool success = await bookManager.CheckInBook(transaction).ConfigureAwait(false);

            SuppliedCustomerId = 0;
            OnPropertyChanged("SuppliedCustomerId");

            if (success)
            {
                ++SelectedBook.AvailableCopies;
                CollectionViewSource.GetDefaultView(this.Books).Refresh();

                DisplayInfoMessage(BookCheckInSuccess, "Success!");
            }
            else
            {
                DisplayErrorMessage(BookCheckInError);
            }
        }

        public async void GetBookHistory()
        {
            BookHistory.Clear();
            var result = await bookManager.GetBookTransactionHistory(SelectedBook.BookId).ConfigureAwait(false);
            foreach (var record in result)
            {
                BookHistory.Add(record);
            }
        }

        private bool PerformBookDeletionValidation()
        {
            return SelectedBook != null && SelectedBook.BookId > 0;
        }

        private bool PerformBookUpdationValidation()
        {
            return validation.ValidateBook(SelectedBook);
        }

        private bool PerformCheckOutValidation()
        {
            return validation.BookValidForCheckOut(SelectedBook) && SuppliedCustomerId > 0;
        }

        private bool PerformCheckInValidation()
        {
            return validation.BookValidForCheckIn(SelectedBook) && SuppliedCustomerId > 0;
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