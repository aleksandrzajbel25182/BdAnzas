using BdAnzas.Content;
using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BdAnzas
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            var window = new MainWindow();
            window.DataContext = new MainViewModel();

            window.Show();
        }

    }

}
