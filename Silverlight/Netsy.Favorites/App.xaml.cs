//-----------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Favorites
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    /// <summary>
    /// Application object For the Netsy Favorites Control
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the App class
        /// </summary>
        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;

            InitializeComponent();
        }

        /// <summary>
        /// Gets the user is read from the control params
        /// </summary>
        public string UserId { get; private set; }

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
                // ignore the error
            }
        }

        /// <summary>
        /// Startup event handler
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event params</param>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            this.RootVisual = new FavoritesControl();
            this.ReadParams(e.InitParams);
        }

        /// <summary>
        /// shutdown event handler
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event params</param>
        private void Application_Exit(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Read the commandline parameters
        /// </summary>
        /// <param name="initParams">the control params</param>
        private void ReadParams(IDictionary<string, string> initParams)
        {
            const string UserIdKey = "UserId";

            if (!initParams.ContainsKey(UserIdKey))
            {
                throw new ArgumentException("No User id found in Control params");
            }

            this.UserId = initParams[UserIdKey];
        }

        /// <summary>
        /// Exception handler
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event params</param>
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
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
                Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e); });
            }
        }
    }
}
