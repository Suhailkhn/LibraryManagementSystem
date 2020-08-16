using Dapper;
using LibraryManagementSystem.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Classes.DAL
{
    class CustomerDataManager
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["lms-localDB"].ConnectionString;

        public static async Task<uint> CreateCustomer(Customer customer)
        {
            uint result = 0;
            string sql = @"INSERT INTO customers (
                                FirstName,
                                LastName,
                                Email,
                                DateOfBirth,
                                AccountCreatedOn
                           ) VALUES (
                                @FirstName,
                                @LastName,
                                @Email,
                                @DateOfBirth,
                                @AccountCreatedOn
                           );
                           SELECT LAST_INSERT_ID();";

            var parameters = new
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                DateOfBirth = customer.DateOfBirth,
                AccountCreatedOn = customer.AccountCreatedOn
            };

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    result = await conn.QueryFirstOrDefaultAsync<uint>(sql, parameters)
                                       .ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return result;
        }

        public static async Task<Customer> GetCustomerDetails(uint customerId)
        {
            Customer result = null;
            string sql = @"SELECT
                                CustomerId
                                FirstName,
                                LastName,
                                Email,
                                DateOfBirth,
                                AccountCreatedOn
                           FROM customers
                           WHERE CustomerId = @CustomerId
                           AND IsActive = true;";

            var parameters = new
            {
                CustomerId = customerId
            };

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    result = await conn.QueryFirstOrDefaultAsync<Customer>(sql, parameters)
                                       .ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return result;
        }

        public static async Task<bool> UpdateCustomer(Customer customer)
        {
            bool success = true;
            string sql = @"UPDATE customers
                           SET FirstName = @FirstName,
                               LastName = @LastName, 
                               Email = @Email, 
                               DateOfBirth = @DateOfBirth
                           WHERE CustomerId = @CustomerId;";

            var parameters = new
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                DateOfBirth = customer.DateOfBirth
            };

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    int rowsAffected = await conn.ExecuteAsync(sql, parameters)
                                                 .ConfigureAwait(false);
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

        public static async Task<bool> DeleteCustomer(uint customerId)
        {
            bool success = true;
            string sql = @"UPDATE customers
                           SET IsActive=false
                           WHERE CustomerId = @CustomerId 
                           AND IsActive = true;";

            var parameters = new
            {
                CustomerId = customerId
            };

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    int rowsAffected = await conn.ExecuteAsync(sql, parameters)
                                                 .ConfigureAwait(false);
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
    }
}