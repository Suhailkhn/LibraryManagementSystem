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

namespace LibraryManagementSystem.Controls
{
    /// <summary>
    /// Interaction logic for BookCard.xaml
    /// </summary>
    public partial class BookCard : UserControl
    {
        public Book Book
        {
            get { return (Book)GetValue(BookProperty); }
            set { SetValue(BookProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Book.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BookProperty =
            DependencyProperty.Register("Book", typeof(Book), typeof(BookCard), new PropertyMetadata(null, SetText));

        private static void SetText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BookCard bookCard = d as BookCard;

            if(bookCard != null)
            {
                var newValue = e.NewValue as Book;

                bookCard.bookTitle.Text = newValue.Title;
                bookCard.bookAuthors.Text = String.Format("Author(s) : {0}", newValue.Authors);
                bookCard.bookPublishingYear.Text = String.Format("Publishing Year : {0}", newValue.PublishingYear);
                bookCard.bookAddedDate.Text = String.Format("Date Added : {0}", newValue.DateAdded.ToString("dd/MM/yyyy"));
                bookCard.bookTotalCopies.Text = String.Format("Total Copies : {0}", newValue.TotalCopies.ToString());
                bookCard.bookAvailableCopies.Text = String.Format("Available Copies : {0}", newValue.AvailableCopies.ToString());

                var imageUrl = String.Format(@"http://covers.openlibrary.org/b/isbn/{0}-L.jpg", newValue.PlainISBN);
                bookCard.bookCover.Source = new BitmapImage(new Uri(imageUrl, UriKind.Absolute));
            }
        }

        public BookCard()
        {
            InitializeComponent();
        }
    }
}