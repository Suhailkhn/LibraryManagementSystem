using LibraryManagementSystem.Classes.DAL;
using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Classes.BAL
{
    class BookManager
    {
        public static Task<bool> CreateBook(Book book)
        {
            return BookDataManager.CreateBook(book);
        }

        public static Task<bool> UpdateBook(Book book)
        {
            return BookDataManager.UpdateBook(book);
        }

        public static Task<bool> DeleteBook(uint bookId)
        {
            return BookDataManager.DeleteBook(bookId);
        }

        public static Task<Book> GetBook(uint bookId)
        {
            return BookDataManager.GetBook(bookId);
        }

        public static Task<IEnumerable<Book>> SearchBooks(string query)
        {
            return BookDataManager.SearchBooks(query);
        }

        public static Task<bool> CheckOutBook(BookTransaction transaction)
        {
            return BookDataManager.CheckOutBook(transaction);
        }

        public static Task<bool> CheckInBook(BookTransaction transaction)
        {
            return BookDataManager.CheckInBook(transaction);
        }

        public static Task<IEnumerable<BookTransaction>> GetBookTransactionHistory(uint bookId)
        {
            return BookDataManager.GetBookTransactionHistory(bookId);
        }
    }
}