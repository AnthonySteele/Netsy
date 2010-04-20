//-----------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Shop
{
    using System;
    using System.Windows;

    using Microsoft.Practices.Unity;

    using Netsy.DataModel;
    using Netsy.UI;

    /// <summary>
    /// Application object For the Netsy Shop Control
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the App class
        /// </summary>
        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += Application_Exit;
            this.UnhandledException += Application_UnhandledException;

            InitializeComponent();
        }

        /// <summary>
        /// shutdown event handler
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event params</param>
        private static void Application_Exit(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Send the error into the browser
        /// </summary>
        /// <param name="e">the event parameters</param>
        private static void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Exception handler
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event params</param>
        private static void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // If the app is running outside of the debugger then report the exception using
            // the browser's exception mechanism. On IE this will display it a yellow alert 
            // icon in the status bar and Firefox will display a script error.
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                // NOTE: This will allow the application to continue running after an exception has been thrown
                // but not handled. 
                // For production applications this error handling should be replaced with something that will 
                // report the error to the website and stop the application.
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(() => ReportErrorToDOM(e));
            }
        }

        /// <summary>
        /// Use the unity container to resolve the viewmodel
        /// </summary>
        /// <returns>the built-up viewmodel</returns>
        private ShopControlViewModel ResolveViewModel()
        {
            IUnityContainer container = new UnityContainer();

            // the etsy context can be a singleton
            const string EtsyApiKey = "fxx4ppr9da9yvxzvug5hhv5a";
            container.RegisterInstance(new EtsyContext(EtsyApiKey));
            container.RegisterInstance(this.RootVisual.Dispatcher);

            UnityConfiguration.RegisterServices(container);
            return container.Resolve<ShopControlViewModel>();
        }

        /// <summary>
        /// Startup event handler
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event params</param>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            AppSettings settingsRead = new AppSettings();
            settingsRead.ReadParams(e.InitParams);

            ShopControl shopControl = new ShopControl();
            this.RootVisual = shopControl;

            ShopControlViewModel viewModel = this.ResolveViewModel();
            viewModel.UserId = settingsRead.UserId;

            shopControl.DataContext = viewModel;
            viewModel.LoadShop();
        }
    }
}
