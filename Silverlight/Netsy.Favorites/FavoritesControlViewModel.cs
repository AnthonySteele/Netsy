//-----------------------------------------------------------------------
// <copyright file="FavoritesControlViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Favorites
{
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
    public class FavoritesControlViewModel : PagedCollectionViewModel<ListingViewModel>
    {
        /// <summary>
        /// The shop service
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
        /// Initializes a new instance of the FavoritesControlViewModel class
        /// </summary>
        /// <param name="favoritesService">the favorite service</param>
        /// <param name="shopDetailsCommand">the shop details retrieval command</param>
        public FavoritesControlViewModel(IFavoritesService favoritesService, ShopDetailsCommand shopDetailsCommand) 
        {
            this.shopDetailsCommand = shopDetailsCommand;
 
            this.favoritesService = favoritesService;
            this.favoritesService.GetFavoriteListingsOfUserCompleted += this.ListingsReceived;
            
            this.MakeListingCommands(); 
        }

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
            this.LoadPageCommand = new DelegateCommand<ListingViewModel>(
                item =>
                {
                    int offset = (this.PageNumber - 1) * this.ItemsPerPage;

                    this.favoritesService.GetFavoriteListingsOfUser(this.UserId, offset, this.ItemsPerPage, DetailLevel.Medium);
                    
                    string status = string.Format(
                        CultureInfo.InvariantCulture,
                        "Getting page {0} of favorites for {1}",
                        this.PageNumber,
                        this.UserName());
                    this.StatusText = status;
                });
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
                this.StatusText = string.Format(
                    CultureInfo.InvariantCulture,
                    "Error getting favorites for {0}:{1}",
                    this.UserName(),
                    e.ResultStatus.ErrorMessage);
                return;
            }

            this.Items.Clear();
            foreach (Listing item in e.ResultValue.Results)
            {
                ListingViewModel viewModel = new ListingViewModel(item);
                viewModel.ShopLinkVisibility = Visibility.Visible;
                this.Items.Add(viewModel);
            }

            string status = string.Format(
                     CultureInfo.InvariantCulture,
                     "Got page {0} of favorites for {1}",
                     this.PageNumber,
                     this.UserName());
            this.StatusText = status;

            int nextPageOffset = this.PageNumber * this.ItemsPerPage;
            this.HasNextPage = nextPageOffset < e.ResultValue.Count;
        }
    }
}
