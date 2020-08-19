using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Classes.BAL
{
    partial class Validation
    {
        public bool ValidateCustomer(Customer customer)
        {
            if (String.IsNullOrWhiteSpace(customer.FirstName) ||
               !IsValidEmail(customer.Email) ||
               customer.DateOfBirth.Date == DateTime.Now.Date)
            {
                return false;
            }

            return true;
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                var mail = new MailAddress(email);
                return true;
            }
            catch (FormatException e)
            {
                return false;
            }
        }
    }
}
