//-----------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.WpfUI.Windows.Main
{
    using Netsy.UI.ViewModels;
    using Netsy.UI.ViewModels.Listings;

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
        /// The view model for listings by keyword
        /// </summary>
        private readonly KeywordsListingsViewModel keywordsViewModel;

        /// <summary>
        /// The view model for listings by color 
        /// </summary>
        private readonly ColorListingsViewModel colorViewModel;
        
        /// <summary>
        /// The view model for listings by color and keyword
        /// </summary>
        private readonly ColorKeywordsListingsViewModel colorKeywordsViewModel;

        /// <summary>
        /// The text to display on the status bar
        /// </summary>
        private string statusText;

        /// <summary>
        /// Initializes a new instance of the MainWindowViewModel class
        /// </summary>
        /// <param name="frontFeaturedListingsViewModel">the view model for front featured listings</param>
        /// <param name="keywordsViewModel">the view model for listings by keywords</param>
        /// <param name="colorViewModel">the view model for listings by color</param>
        /// <param name="colorKeywordsViewModel">the view model for listings by color and keywords</param>
        public MainWindowViewModel(
            FrontFeaturedListingsViewModel frontFeaturedListingsViewModel,
            KeywordsListingsViewModel keywordsViewModel,
            ColorListingsViewModel colorViewModel,
            ColorKeywordsListingsViewModel colorKeywordsViewModel)
        {
            this.StatusText = "Netsy WPF UI";

            this.frontListingsViewModel = frontFeaturedListingsViewModel;
            this.keywordsViewModel = keywordsViewModel;
            this.colorViewModel = colorViewModel;
            this.colorKeywordsViewModel = colorKeywordsViewModel;
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

        /// <summary>
        /// Gets the view model for listings by keyword
        /// </summary>
        public KeywordsListingsViewModel KeywordsViewModel
        {
            get
            {
                return this.keywordsViewModel;
            }
        }

        /// <summary>
        /// Gets the view model for listings by color 
        /// </summary>
        public ColorListingsViewModel ColorViewModel
        {
            get
            {
                return this.colorViewModel;
            }
        }

        /// <summary>
        /// Gets the view model for front listings
        /// </summary>
        public ColorKeywordsListingsViewModel ColorKeywordsViewModel
        {
            get
            {
                return this.colorKeywordsViewModel;
            }
        }
    }
}
