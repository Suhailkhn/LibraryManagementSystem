using LibraryManagementSystem.Classes.BAL;
using LibraryManagementSystem.Classes.DAL;
using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibraryManagementSystem.Views
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
        }

        private void AddBookBtn_Click(object sender, RoutedEventArgs e)
        {
            //var book = new Book
            //{
            //    ISBN = 1617294691,
            //    Title = "C++ Concurrency in Action, 2nd Edition",
            //    Authors = "Anthony Williams",
            //    PublishingYear = 2012,
            //    DateAdded = DateTime.Now,
            //    TotalCopies = 7,
            //    AvailableCopies = 3
            //};

            var newWindow = new BookCreationWindow();
            newWindow.ShowDialog();
        }

        private void CreateCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new CustomerCreationWindow();
            newWindow.ShowDialog();
        }
    }
}
