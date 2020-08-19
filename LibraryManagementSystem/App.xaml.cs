using LibraryManagementSystem.Classes;
using LibraryManagementSystem.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            DependencyInjector.Configure();
            MainWindow = DependencyInjector.Retrieve<Home>();
            MainWindow.Show();
        }
    }
}
