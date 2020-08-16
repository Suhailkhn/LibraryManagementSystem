﻿using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Classes.BAL
{
    partial class Validation
    {
        public static bool ValidateBook(Book book)
        {
            if (IsValidISBN(book.ISBN) && !String.IsNullOrWhiteSpace(book.Title))
            {
                book.PlainISBN = book.ISBN.Replace("-", "");
                return true;
            }
            else
                return false;
        }

        public static bool IsValidISBN(string iSBN)
        {
            if (String.IsNullOrWhiteSpace(iSBN))
                return false;

            string plainISBN = iSBN.Replace("-", "");
            int length = plainISBN.Length;

            if (length == 10 || length == 13)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}