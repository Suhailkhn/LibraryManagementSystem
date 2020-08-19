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
    /// Interaction logic for CustomerDeletionWindow.xaml
    /// </summary>
    public partial class CustomerDeletionWindow : Window
    {
        [Dependency]
        public CustomerDeletionVM CustomerDeletionVM
        {
            set
            {
                DataContext = value;
            }
        }

        public CustomerDeletionWindow()
        {
            InitializeComponent();
        }

        private void cancelBtnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
