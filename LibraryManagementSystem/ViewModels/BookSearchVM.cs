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

namespace LibraryManagementSystem.ViewModels
{
    class BookSearchVM : INotifyPropertyChanged
    {
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

        public ObservableCollection<Book> Books { get; set; }

        private Book selectedBook;

        public Book SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                OnPropertyChanged("SelectedBook");
            }
        }

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
                MessageBox.Show("No matching books found.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        #endregion
    }
}
