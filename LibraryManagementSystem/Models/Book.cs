using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public uint BookId { get; set; }
        public string ISBN { get; set; }
        public string PlainISBN { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public ushort PublishingYear { get; set; }
        public DateTime DateAdded { get; set; }
        public uint TotalCopies { get; set; }
        public uint AvailableCopies { get; set; }
        public uint TotalCheckOuts { get; set; }
    }
}