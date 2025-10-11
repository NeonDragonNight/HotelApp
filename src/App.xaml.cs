using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Configuration;
using System.Data;
using System.Windows;

namespace hotelapp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DatabaseFacade facade = new DatabaseFacade(new DataContext());
            facade.EnsureCreated();
        }
    }

}
