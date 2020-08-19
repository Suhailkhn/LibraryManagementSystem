using LibraryManagementSystem.Interfaces.BAL;
using LibraryManagementSystem.Interfaces.DAL;
using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Classes.BAL
{
    class CustomerManager : ICustomerManager
    {
        private readonly ICustomerDataManager customerDataManager;

        public CustomerManager(ICustomerDataManager _customerDataManager)
        {
            customerDataManager = _customerDataManager;
        }

        public Task<uint> CreateCustomer(Customer customer)
        {
            return customerDataManager.CreateCustomer(customer);
        }

        public Task<Customer> GetCustomerDetails(uint customerId)
        {
            return customerDataManager.GetCustomerDetails(customerId);
        }

        public Task<bool> UpdateCustomer(Customer customer)
        {
            return customerDataManager.UpdateCustomer(customer);
        }

        public Task<bool> DeleteCustomer(uint customerId)
        {
            return customerDataManager.DeleteCustomer(customerId);
        }
    }
}
