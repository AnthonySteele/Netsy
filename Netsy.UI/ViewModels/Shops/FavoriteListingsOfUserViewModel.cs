//-----------------------------------------------------------------------
// <copyright file="FavoriteListingsOfUserViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.UI.ViewModels.Shops
{
    using System.Globalization;
    using System.Windows;
    using System.Windows.Input;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.UI.Commands;

    /// <summary>
    /// Class for viewing a collection of listings for a shop
    /// </summary>
    public class FavoriteListingsOfUserViewModel : PagedCollectionViewModel<ListingViewModel>
    {
        /// <summary>
        /// The shop service
        /// </summary>
        private readonly IFavoritesService favoritesService;

        /// <summary>
        /// the Id of the user
        /// </summary>
        private string userId;

        /// <summary>
        /// Initializes a new instance of the FavoriteListingsOfUserViewModel class
        /// </summary>
        /// <param name="favoritesService">the favorites service</param>
        public FavoriteListingsOfUserViewModel(IFavoritesService favoritesService)
        {
            this.favoritesService = favoritesService;
            this.favoritesService.GetFavoriteListingsOfUserCompleted += this.ListingsReceived;
            this.MakeListingCommands();

            this.ListingsPerPage = Constants.DefaultItemsPerPage;
        }

        /// <summary>
        /// Gets or sets the Id of the shop
        /// </summary>
        public string UserId
        {
            get { return this.userId; }
            set { this.userId = value; }
        }

        /// <summary>
        /// Gets or sets the command to show a listing's details
        /// </summary>
        public ICommand ShowListingCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to show a shop's details
        /// </summary>
        public ICommand ShowShopCommand { get; set; }

        /// <summary>
        /// Gets the number of listings per page
        /// </summary>
        public int ListingsPerPage { get; private set; }

        /// <summary>
        /// make the commands
        /// </summary>
        private void MakeListingCommands()
        {
            this.LoadPageCommand = new DelegateCommand<ListingViewModel>(
                item =>
                {
                    int offset = (this.PageNumber - 1) * this.ItemsPerPage;

                    this.favoritesService.GetFavoriteListingsOfUser(this.UserId, offset, this.ListingsPerPage, DetailLevel.Medium);
                    string status = string.Format(
                        CultureInfo.InvariantCulture,
                        "Getting {0} favorite listings on page {1} for shop",
                        this.ListingsPerPage,
                        this.ItemsPerPage);
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
                this.StatusText = "Failed to load favorite listings " + e.ResultStatus.ErrorMessage;
                return;
            }

            this.Items.Clear();
            foreach (Listing item in e.ResultValue.Results)
            {
                ListingViewModel viewModel = new ListingViewModel(item);
                viewModel.ShopLinkVisibility = Visibility.Visible;
                viewModel.ShowListingCommand = this.ShowListingCommand;
                viewModel.ShowShopCommand = this.ShowShopCommand;
                this.Items.Add(viewModel);
            }

            string status = string.Format(
                CultureInfo.InvariantCulture,
                "Got {0} favorite listings on page {1} of {2} for shop",
                e.ResultValue.Results.Length,
                this.PageNumber,
                e.ResultValue.Count);
            this.StatusText = status;

            int nextPageOffset = this.PageNumber * this.ItemsPerPage;
            this.HasNextPage = nextPageOffset < e.ResultValue.Count;
        }
    }
}
