//-----------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.WpfUI.Windows.Main
{
    using System.Windows.Input;

    using Netsy.UI.ViewModels;
    using Netsy.UI.ViewModels.Listings;
    using Netsy.UI.ViewModels.Shops;

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
        /// The view model for listings by materials
        /// </summary>
        private readonly MaterialsListingsViewModel materialsViewModel;

        /// <summary>
        /// The view model for listings by tags
        /// </summary>
        private readonly TagsListingsViewModel tagsViewModel;

        /// <summary>
        /// The viewmodel for shops by name
        /// </summary>
        private readonly ShopsByNameViewModel shopsByNameViewModel;

        /// <summary>
        /// the command to show a shop window
        /// </summary>
        private readonly ICommand showShopWindowCommand;

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
        /// <param name="materialsListingsViewModel">the view model for listings by materials</param>
        /// <param name="tagsListingsViewModel">the view model for listings by tags</param>
        /// <param name="shopsByNameViewModel">the view model for shops by name</param>
        /// <param name="showShopWindowCommand">the command to show a shop window</param>
        public MainWindowViewModel(
            FrontFeaturedListingsViewModel frontFeaturedListingsViewModel,
            KeywordsListingsViewModel keywordsViewModel,
            ColorListingsViewModel colorViewModel,
            ColorKeywordsListingsViewModel colorKeywordsViewModel,
            MaterialsListingsViewModel materialsListingsViewModel,
            TagsListingsViewModel tagsListingsViewModel,
            ShopsByNameViewModel shopsByNameViewModel,
            ShowShopWindowCommand showShopWindowCommand)
        {
            this.StatusText = "Netsy WPF UI";

            this.frontListingsViewModel = frontFeaturedListingsViewModel;
            this.keywordsViewModel = keywordsViewModel;
            this.colorViewModel = colorViewModel;
            this.colorKeywordsViewModel = colorKeywordsViewModel;
            this.materialsViewModel = materialsListingsViewModel;
            this.tagsViewModel = tagsListingsViewModel;
            this.shopsByNameViewModel = shopsByNameViewModel;

            this.showShopWindowCommand = showShopWindowCommand;
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

        /// <summary>
        /// Gets the view model for listings by materials
        /// </summary>
        public MaterialsListingsViewModel MaterialsViewModel
        {
            get
            {
                return this.materialsViewModel;
            }
        }

        /// <summary>
        /// Gets the view model for listings by tags
        /// </summary>
        public TagsListingsViewModel TagsViewModel
        {
            get
            {
                return this.tagsViewModel;
            }
        }

        /// <summary>
        /// Gets the viewmodel for shops by name
        /// </summary>
        public ShopsByNameViewModel ShopsByNameViewModel
        {
            get
            {
                return this.shopsByNameViewModel;
            }
        }

        /// <summary>
        /// Gets the command to show a shop window
        /// </summary>
        public ICommand ShowShopWindowCommand
        {
            get
            {
                return this.showShopWindowCommand;
            }
        }
    }
}
