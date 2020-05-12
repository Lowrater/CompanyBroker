using CompanyBroker.View.Windows;
using CompanyBroker_API_Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Configuration;
using System.IO;
using System.Windows;

namespace CompanyBroker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //-- Reference to the CompanyBroker_API_Helper class libary and setups the client
            APIHelper.InitializeApiClient();
        }
    }
}
