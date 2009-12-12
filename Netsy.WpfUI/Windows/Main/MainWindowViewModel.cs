//-----------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.WpfUI
{
    using Netsy.UI.ViewModels;

    /// <summary>
    /// View model for the main window
    /// </summary>
    public class MainWindowViewModel : BaseViewModel
    {
        /// <summary>
        /// The view model for front listings
        /// </summary>
        private readonly FrontFeaturedListingsViewModel frontListingsViewModel;

        /// <summary>
        /// The text to display on the status bar
        /// </summary>
        private string statusText;

        /// <summary>
        /// Initializes a new instance of the MainWindowViewModel class
        /// </summary>
        /// <param name="frontFeaturedListingsViewModel">the view model for front featured listings</param>
        public MainWindowViewModel(FrontFeaturedListingsViewModel frontFeaturedListingsViewModel)
        {
            this.StatusText = "Netsy WPF UI";
            this.frontListingsViewModel = frontFeaturedListingsViewModel;
        }

        /// <summary>
        /// Gets or sets the status bar text
        /// </summary>
        public string StatusText
        {
            get
            {
                return this.statusText;
            }

            set
            {
                if (this.statusText != value)
                {
                    this.statusText = value;
                    this.OnPropertyChanged("StatusText");
                }
            }
        }

        /// <summary>
        /// Gets the view model for front listings
        /// </summary>
        public FrontFeaturedListingsViewModel FrontFeaturedListings
        {
            get
            {
                return this.frontListingsViewModel;
            }
        }
    }
}
