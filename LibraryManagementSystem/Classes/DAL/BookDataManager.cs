using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Models;
using MySql.Data.MySqlClient;
using Dapper;
using LibraryManagementSystem.Interfaces.DAL;

namespace LibraryManagementSystem.Classes.DAL
{
    public class BookDataManager : IBookDataManager
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["lms-localDB"].ConnectionString;

        public async Task<bool> CreateBook(Book book)
        {
            bool success = true;
            string sql = @"INSERT INTO books (
                                ISBN,
                                PlainISBN,
                                Title,
                                Authors,
                                PublishingYear,
                                DateAdded,
                                TotalCopies,
                                AvailableCopies
                           ) VALUES (
                                @ISBN,
                                @PlainISBN,
                                @Title,
                                @Authors,
                                @PublishingYear,
                                @DateAdded,
                                @TotalCopies,
                                @AvailableCopies
                           );";

            var parameters = new
            {
                ISBN = book.ISBN,
                PlainISBN = book.PlainISBN,
                Title = book.Title,
                Authors = book.Authors,
                PublishingYear = book.PublishingYear,
                DateAdded = book.DateAdded,
                TotalCopies  = book.TotalCopies,
                AvailableCopies = book.AvailableCopies
            };

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                   int rowsAffected = await conn.ExecuteAsync(sql, parameters).ConfigureAwait(false);
                   if (rowsAffected == 0)
                       success = false;
                }
            }
            catch (Exception e)
            {
                success = false;
                Console.WriteLine(e.Message);
            }

            return success;
        }

        public async Task<bool> UpdateBook(Book book)
        {
            bool success = true;
            string sql = @"UPDATE books
                           SET ISBN = @ISBN,
                               PlainISBN = @PlainISBN,
                               Title = @Title, 
                               Authors = @Authors,
                               PublishingYear = @PublishingYear,
                               TotalCopies = @TotalCopies,
                               AvailableCopies = @AvailableCopies
                           WHERE BookId = @BookId;";

            var parameters = new
            {
                BookId = book.BookId,
                ISBN = book.ISBN,
                PlainISBN = book.PlainISBN,
                Title = book.Title,
                Authors = book.Authors,
                PublishingYear = book.PublishingYear,
                TotalCopies = book.TotalCopies,
                AvailableCopies = book.AvailableCopies
            };

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    int rowsAffected = await conn.ExecuteAsync(sql, parameters).ConfigureAwait(false);
                    if (rowsAffected == 0)
                        success = false;
                }
            }
            catch (Exception e)
            {
                success = false;
                Console.WriteLine(e.Message);
            }

            return success;
        }

        public async Task<bool> DeleteBook(uint bookId)
        {
            bool success = true;
            string sql = @"Update books 
                           SET IsActive=false 
                           WHERE BookId=@BookId
                           AND IsActive = true;";

            var parameters = new
            {
                BookId = bookId
            };

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    int rowsAffected = await conn.ExecuteAsync(sql, parameters).ConfigureAwait(false);
                    if (rowsAffected == 0)
                        success = false;
                }
            }
            catch (Exception e)
            {
                success = false;
                Console.WriteLine(e.Message);
            }

            return success;
        }

        public async Task<Book> GetBook(uint bookId)
        {
            Book result = null;

            string sql = @"SELECT 
	                            BookId,
	                            ISBN,
                                PlainISBN,
                                Title,
                                Authors,
                                PublishingYear,
                                DateAdded,
                                TotalCopies,
                                AvailableCopies
                            FROM books
                            WHERE BookId = @BookId
                            IsActive = true;";

            var parameters = new
            {
                BookId = bookId
            };

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    result = await conn.QueryFirstOrDefaultAsync<Book>(sql, parameters).ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return result;
        }

        public async Task<IEnumerable<Book>> SearchBooks(string query)
        {
            IEnumerable<Book> result = null;
            
            string sql = @"SELECT 
	                            BookId,
	                            ISBN,
                                PlainISBN,
                                Title,
                                Authors,
                                PublishingYear,
                                DateAdded,
                                TotalCopies,
                                AvailableCopies
                            FROM books
                            WHERE Title Like @Query
                            AND IsActive = true;";

            var parameters = new
            {
                Query = "%" + query + "%"
            };

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    result = await conn.QueryAsync<Book>(sql, parameters).ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return result;
        }

        public async Task<bool> CheckOutBook(BookTransaction transaction)
        {
            bool success = true;
            string sql = @"INSERT INTO book_transaction (
	                            CustomerId,
                                BookId,
                                CheckOut
                            ) Select
	                            @CustomerId,
                                @BookId,
                                @CheckOut
                            FROM DUAL
                            WHERE NOT EXISTS (
	                            SELECT TransactionId
                                FROM book_transaction
                                WHERE CustomerId = @CustomerId AND BookId = @BookId AND CheckIn IS NULL
                            );";

            string updateCopiesSql = @"UPDATE books 
                                       SET AvailableCopies = AvailableCopies - 1 
                                       WHERE BookId = {0}";

            updateCopiesSql = String.Format(updateCopiesSql, transaction.Book.BookId);

            var parameters = new
            {
                CustomerId = transaction.Customer.CustomerId,
                BookId = transaction.Book.BookId,
                CheckOut = transaction.CheckOut
            };

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    int rowsAffected = await conn.ExecuteAsync(sql, parameters).ConfigureAwait(false);
                    if (rowsAffected == 0)
                        success = false;
                    else
                        rowsAffected = await conn.ExecuteAsync(updateCopiesSql).ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                success = false;
                Console.WriteLine(e.Message);
            }

            return success;
        }

        public async Task<bool> CheckInBook(BookTransaction transaction)
        {
            bool success = true;
            string sql = @"UPDATE book_transaction
                           SET CheckIn = @CheckIn
                           WHERE CustomerId = @CustomerId 
	                             AND BookId = @BookId
	                             AND CheckIn IS NULL;";

            string updateCopiesSql = @"UPDATE books 
                                       SET AvailableCopies = AvailableCopies + 1 
                                       WHERE BookId = {0}";

            updateCopiesSql = String.Format(updateCopiesSql, transaction.Book.BookId);

            var parameters = new
            {
                CustomerId = transaction.Customer.CustomerId,
                BookId = transaction.Book.BookId,
                CheckIn = transaction.CheckIn.Value
            };

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    int rowsAffected = await conn.ExecuteAsync(sql, parameters).ConfigureAwait(false);
                    if (rowsAffected == 0)
                        success = false;
                    else
                        rowsAffected = await conn.ExecuteAsync(updateCopiesSql).ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                success = false;
                Console.WriteLine(e.Message);
            }

            return success;
        }

        public async Task<IEnumerable<BookTransaction>> GetBookTransactionHistory(uint bookId)
        {
            IEnumerable<BookTransaction> result = null;

            string sql = @"SELECT 
	                            bt.TransactionId,
                                bt.CheckOut,
                                bt.CheckIn,
	                            b.BookId,
	                            b.ISBN,
                                b.PlainISBN,
                                b.Title,
                                b.Authors,
                                b.PublishingYear,
                                b.DateAdded,
                                b.TotalCopies,
                                b.AvailableCopies,
                                c.CustomerId,
                                c.FirstName,
                                c.LastName,
                                c.Email,
                                c.DateOfBirth,
                                c.AccountCreatedOn
                            FROM book_transaction bt
                            INNER JOIN books b
	                            ON bt.BookId = b.BookId AND bt.BookId = @BookId AND b.IsActive = true
                            INNER JOIN customers c
	                            ON bt.CustomerId = c.CustomerId AND c.IsActive = true
                            ORDER BY bt.TransactionId DESC;";

            var parameters = new
            {
                BookId = bookId
            };

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    result = await conn.QueryAsync<BookTransaction, Book, Customer, BookTransaction>(sql,
                                         param: parameters,
                                         map: (bTransaction, book, customer) => {
                                             bTransaction.Book = book;
                                             bTransaction.Customer = customer;
                                             return bTransaction;
                                         },
                                         splitOn: "BookId,CustomerId")
                                         .ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return result;
        }
    }
}