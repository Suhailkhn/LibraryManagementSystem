using LibraryManagementSystem.Classes.DAL;
using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Classes.BAL
{
    class CustomerManager
    {
        public static Task<uint> CreateCustomer(Customer customer)
        {
            return CustomerDataManager.CreateCustomer(customer);
        }

        public static Task<Customer> GetCustomerDetails(uint customerId)
        {
            return CustomerDataManager.GetCustomerDetails(customerId);
        }

        public static Task<bool> UpdateCustomer(Customer customer)
        {
            return CustomerDataManager.UpdateCustomer(customer);
        }

        public static Task<bool> DeleteCustomer(uint customerId)
        {
            return CustomerDataManager.DeleteCustomer(customerId);
        }
    }
}
