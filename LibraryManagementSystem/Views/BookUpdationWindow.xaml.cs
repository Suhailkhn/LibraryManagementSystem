using LibraryManagementSystem.ViewModels;
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
using System.Windows.Shapes;
using Unity;

namespace LibraryManagementSystem.Views
{
    /// <summary>
    /// Interaction logic for BookUpdationWindow.xaml
    /// </summary>
    public partial class BookUpdationWindow : Window
    {
        [Dependency]
        public BookSearchVM BookSearchVM
        {
            set
            {
                DataContext = value;
            }
        }

        public BookUpdationWindow()
        {
            InitializeComponent();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
