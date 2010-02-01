//-----------------------------------------------------------------------
// <copyright file="ListingsControlViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Favorites
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Input;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.UI.Commands;
    using Netsy.UI.ViewModels;

    /// <summary>
    /// View model for the main page
    /// </summary>
    public class ListingsControlViewModel : PagedCollectionViewModel<ListingViewModel>
    {
        /// <summary>
        /// the service to get front listings
        /// </summary>
        private readonly IListingsService listingsService;

        /// <summary>
        /// the service to get listings
        /// </summary>
        private readonly IShopService shopService;

        /// <summary>
        /// the service to get favorites
        /// </summary>
        private readonly IFavoritesService favoritesService;

        /// <summary>
        /// The command to get shop details
        /// </summary>
        private readonly ShopDetailsCommand shopDetailsCommand;

        /// <summary>
        /// the number of columns in the view 
        /// </summary>
        private int columnCount;
        
        /// <summary>
        /// Initializes a new instance of the ListingsControlViewModel class
        /// </summary>
        /// <param name="listingsService">the service to get front listings</param>
        /// <param name="shopService">the service to get listings for a shop</param>
        /// <param name="favoritesService">the service to get favorites</param>
        /// <param name="shopDetailsCommand">the shop details retrieval command</param>
        public ListingsControlViewModel(
            IListingsService listingsService,
            IShopService shopService, 
            IFavoritesService favoritesService, 
            ShopDetailsCommand shopDetailsCommand) 
        {
            this.listingsService = listingsService;
            this.listingsService.GetFrontFeaturedListingsCompleted += this.ListingsReceived; 

            this.shopService = shopService;
            this.shopService.GetShopListingsCompleted += this.ListingsReceived;
 
            this.favoritesService = favoritesService;
            this.favoritesService.GetFavoriteListingsOfUserCompleted += this.ListingsReceived;

            this.shopDetailsCommand = shopDetailsCommand;

            this.MakeListingCommands(); 
        }

        /// <summary>
        /// Event handler - for when listings have been received
        /// </summary>
        public event EventHandler ListingsReceivedCompleted;

        /// <summary>
        /// Gets or sets the number of columns in the view 
        /// </summary>
        public int ColumnCount
        {
            get
            {
                return this.columnCount;
            } 

            set
            {
                if (this.columnCount != value)
                {
                    this.columnCount = value;
                    this.OnPropertyChanged("ColumnCount");                    
                }
            }
        }

        /// <summary>
        /// Gets or sets the Id of the shop
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to retrieve favorites (true) or listings (false)
        /// </summary>
        public ListingsRetrievalMode ListingsRetrievalMode { get; set; }

        /// <summary>
        /// Gets or sets the shop shown
        /// </summary>
        public ShopViewModel Shop { get;  set; }

        /// <summary>
        /// Gets the command to get shop details
        /// </summary>
        public ICommand ShopDetailsCommand
        {
            get { return this.shopDetailsCommand; }
        }

        /// <summary>
        /// Load inital data into this viewmodel
        /// </summary>
        public void Load()
        {
            this.ShopDetailsCommand.Execute(this);
            this.LoadPageCommand.Execute(this);
        }

        /// <summary>
        /// Gets the user name (if loaded)
        /// </summary>
        /// <returns>the user name</returns>
        private string UserName()
        {
            if (this.Shop != null && this.Shop.Shop != null && this.Shop.Shop.UserName.HasContent())
            {
                return this.Shop.Shop.UserName;
            }

            return this.UserId;
        }

        /// <summary>
        /// make the commands
        /// </summary>
        private void MakeListingCommands()
        {
            this.LoadPageCommand = new DelegateCommand<ListingViewModel>(item => this.LoadPage());
        }

        /// <summary>
        /// Load a page of data
        /// </summary>
        private void LoadPage()
        {
            switch (this.ListingsRetrievalMode)
            {
                case ListingsRetrievalMode.Favorites:
                    this.LoadPageFavorites();
                    break;
            
                case ListingsRetrievalMode.ShopListings:
                    this.LoadPageShopListings();
                    break;

                case ListingsRetrievalMode.FrontListings:
                    this.LoadPageFrontListings();
                    break;                    

                default:
                    throw new ArgumentException("Unknown ListingsRetrievalMode " + this.ListingsRetrievalMode);
            }
        }

        /// <summary>
        /// Load a page of favorites for the user
        /// </summary>
        private void LoadPageFavorites()
        {
            int offset = (this.PageNumber - 1) * this.ItemsPerPage;

            this.favoritesService.GetFavoriteListingsOfUser(this.UserId, offset, this.ItemsPerPage, DetailLevel.Medium);

            string status = string.Format(
                CultureInfo.InvariantCulture,
                "Getting page {0} of listings for {1}",
                this.PageNumber,
                this.UserName());
            this.StatusText = status;
        }

        /// <summary>
        /// load a page of listings for a shop
        /// </summary>
        private void LoadPageShopListings()
        {
            int offset = (this.PageNumber - 1) * this.ItemsPerPage;

            this.shopService.GetShopListings(this.UserId, SortField.Price, SortOrder.Down, null, offset, this.ItemsPerPage, DetailLevel.Medium);

            string status = string.Format(
                CultureInfo.InvariantCulture,
                "Getting page {0} of listings for {1}",
                this.PageNumber,
                this.UserName());
            this.StatusText = status;
        }

        /// <summary>
        /// load a page of front listings for the site
        /// </summary>
        private void LoadPageFrontListings()
        {
            int offset = (this.PageNumber - 1) * this.ItemsPerPage;

            this.listingsService.GetFrontFeaturedListings(offset, this.ItemsPerPage, DetailLevel.Medium);

            string status = string.Format(
                CultureInfo.InvariantCulture,
                "Getting page {0} of front listings",
                this.PageNumber);
            this.StatusText = status;
        }

        /// <summary>
        /// Handler for listings received
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event params</param>
        private void ListingsReceived(object sender, ResultEventArgs<Listings> e)
        {
            if (!e.ResultStatus.Success)
            {
                this.StatusText = this.ErrorStatus(e.ResultStatus.ErrorMessage);
                return;
            }

            this.Items.Clear();
            foreach (Listing item in e.ResultValue.Results)
            {
                ListingViewModel viewModel = new ListingViewModel(item);
                viewModel.ShopLinkVisibility = Visibility.Visible;
                this.Items.Add(viewModel);
            }

            this.StatusText = this.SuccessStatus();

            int nextPageOffset = this.PageNumber * this.ItemsPerPage;
            this.HasNextPage = nextPageOffset < e.ResultValue.Count;

            if (this.ListingsReceivedCompleted != null)
            {
                this.ListingsReceivedCompleted(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// A success message for the status
        /// </summary>
        /// <returns>the message</returns>
        private string SuccessStatus()
        {
            if (ListingsRetrievalMode == ListingsRetrievalMode.FrontListings)
            {
                // no user name involved
                const string NoUserFormatTemplate = "Got page {0} of {1}";
                return string.Format(
                 CultureInfo.InvariantCulture,
                 NoUserFormatTemplate,
                 this.PageNumber,
                 this.ReturnDataName());
            }

            const string FormatTemplate = "Got page {0} of {1} for {2}";
            return string.Format(
             CultureInfo.InvariantCulture,
             FormatTemplate,
             this.PageNumber,
             this.ReturnDataName(),
             this.UserName());
        }

        /// <summary>
        /// An error message for the status
        /// </summary>
        /// <param name="errorMessage">the error message from the library</param>
        /// <returns>the formatted error message for display</returns>
        private string ErrorStatus(string errorMessage)
        {
            const string FormatTemplate = "Error getting {0} for {1}:{2}";

            return string.Format(
                CultureInfo.InvariantCulture,
                FormatTemplate, 
                this.ReturnDataName(),
                this.UserName(), 
                errorMessage);
        }

        /// <summary>
        /// The name of the data returned
        /// </summary>
        /// <returns>favorites or listings</returns>
        private string ReturnDataName()
        {
            switch (this.ListingsRetrievalMode)
            {
                case ListingsRetrievalMode.Favorites:
                    return "favorites";

                case ListingsRetrievalMode.ShopListings:
                    return "listings";

                case ListingsRetrievalMode.FrontListings:
                    return "front listings";

                default:
                    throw new ArgumentException("Unknown ListingsRetrievalMode " + this.ListingsRetrievalMode);
            }
        }
    }
}
