﻿//-----------------------------------------------------------------------
// <copyright file="CommandLocator.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace NetsyGui
{
    using System.Windows.Input;

    using NetsyGui.Main;

    /// <summary>
    /// Class used to put commands into XAML
    /// </summary>
    public static class CommandLocator
    {
        /// <summary>
        /// The main window "next page" command
        /// </summary>
        private static readonly MainWindowNextPageCommand mainWindowNextPageCommand = new MainWindowNextPageCommand();

        /// <summary>
        /// The main window "previous page" command
        /// </summary>
        private static readonly MainWindowPreviousPageCommand mainWindowPreviousPageCommand = new MainWindowPreviousPageCommand();

        /// <summary>
        /// The main window "first page" command
        /// </summary>
        private static readonly MainWindowFirstPageCommand mainWindowFirstPageCommand = new MainWindowFirstPageCommand();

        /// <summary>
        /// The main window "load listings" command
        /// </summary>
        private static MainWindowLoadFrontFeaturedListingsCommand mainWindowLoadFrontFeaturedListingsCommand;

        /// <summary>
        /// Gets the next page command
        /// </summary>
        public static ICommand MainWindowNextPageCommand
        {
            get { return mainWindowNextPageCommand; }
        }

        /// <summary>
        /// Gets the previous page command
        /// </summary>
        public static ICommand MainWindowPreviousPageCommand
        {
            get { return mainWindowPreviousPageCommand;  }
        }

        /// <summary>
        /// Gets the first page command
        /// </summary>
        public static ICommand MainWindowFirstPageCommand
        {
            get { return mainWindowFirstPageCommand; }
        }

        /// <summary>
        /// Gets the load listings command
        /// </summary>
        public static ICommand MainWindowLoadFrontFeaturedListingsCommand
        {
            get
            {
                if (mainWindowLoadFrontFeaturedListingsCommand == null)
                {
                   mainWindowLoadFrontFeaturedListingsCommand = Locator.Resolve<MainWindowLoadFrontFeaturedListingsCommand>();
                }

                return mainWindowLoadFrontFeaturedListingsCommand;
            }
        }

        /// <summary>
        /// Trigger re-evaluating the CanExecute state of main window commands
        /// </summary>
        public static void MainWindowCanExecuteChanged()
        {
           mainWindowNextPageCommand.OnCanExecuteChanged();
           mainWindowFirstPageCommand.OnCanExecuteChanged();
           mainWindowPreviousPageCommand.OnCanExecuteChanged();

           if (mainWindowLoadFrontFeaturedListingsCommand != null)
           {
               mainWindowLoadFrontFeaturedListingsCommand.OnCanExecuteChanged();
           }
        }
    }
}
