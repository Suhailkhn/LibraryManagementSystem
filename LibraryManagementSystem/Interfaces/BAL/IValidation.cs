using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Interfaces.BAL
{
    public interface IValidation
    {
        bool ValidateBook(Book book);

        bool IsValidISBN(string iSBN);

        bool BookValidForCheckOut(Book book);

        bool BookValidForCheckIn(Book book);

        bool ValidateCustomer(Customer customer);

        bool IsValidEmail(string email);
    }
}