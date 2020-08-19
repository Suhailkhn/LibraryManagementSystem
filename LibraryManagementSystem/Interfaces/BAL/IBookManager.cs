using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Interfaces.BAL
{
    public interface IBookManager
    {
        Task<bool> CreateBook(Book book);

        Task<bool> UpdateBook(Book book);

        Task<bool> DeleteBook(uint bookId);

        Task<Book> GetBook(uint bookId);

        Task<IEnumerable<Book>> SearchBooks(string query);

        Task<bool> CheckOutBook(BookTransaction transaction);

        Task<bool> CheckInBook(BookTransaction transaction);

        Task<IEnumerable<BookTransaction>> GetBookTransactionHistory(uint bookId);
    }
}
