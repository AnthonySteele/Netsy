//-----------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 
namespace Silverlight.Netsy.TestControl
{
    using System;
    using System.Windows;

    /// <summary>
    /// Application class
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
        /// Application startup event
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event params</param>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            this.RootVisual = new MainPage();
        }

        /// <summary>
        /// Application exit event
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event params</param>
        private void Application_Exit(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Application unhandled exception event
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event params</param>
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
                Deployment.Current.Dispatcher.BeginInvoke(delegate { this.ReportErrorToDOM(e); });
            }
        }

        /// <summary>
        /// Application error event
        /// </summary>
        /// <param name="e">event params</param>
        private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
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
    }
}
