﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    class Customer
    {
        public uint Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}"; }  }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime AccountCreatedOn { get; set; }
    }
}