using LibraryManagementSystem.Classes.DAL;
using LibraryManagementSystem.Interfaces.BAL;
using LibraryManagementSystem.Interfaces.DAL;
using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Classes.BAL
{
    class BookManager : IBookManager
    {
        private readonly IBookDataManager bookDataManager;

        public BookManager(IBookDataManager _bookDataManager)
        {
            bookDataManager = _bookDataManager;
        }

        public Task<bool> CreateBook(Book book)
        {
            return bookDataManager.CreateBook(book);
        }

        public Task<bool> UpdateBook(Book book)
        {
            return bookDataManager.UpdateBook(book);
        }

        public Task<bool> DeleteBook(uint bookId)
        {
            return bookDataManager.DeleteBook(bookId);
        }

        public Task<Book> GetBook(uint bookId)
        {
            return bookDataManager.GetBook(bookId);
        }

        public Task<IEnumerable<Book>> SearchBooks(string query)
        {
            return bookDataManager.SearchBooks(query);
        }

        public Task<bool> CheckOutBook(BookTransaction transaction)
        {
            return bookDataManager.CheckOutBook(transaction);
        }

        public Task<bool> CheckInBook(BookTransaction transaction)
        {
            return bookDataManager.CheckInBook(transaction);
        }

        public Task<IEnumerable<BookTransaction>> GetBookTransactionHistory(uint bookId)
        {
            return bookDataManager.GetBookTransactionHistory(bookId);
        }
    }
}