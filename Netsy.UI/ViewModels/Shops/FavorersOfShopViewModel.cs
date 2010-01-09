//-----------------------------------------------------------------------
// <copyright file="FavorersOfShopViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.UI.ViewModels.Shops
{
    using System.Globalization;
    using System.Windows.Input;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.UI.Commands;

    /// <summary>
    /// Class for viewing a collection of listings for a shop
    /// </summary>
    public class FavorersOfShopViewModel : PagedCollectionViewModel<UserViewModel>
    {
        /// <summary>
        /// The shop service
        /// </summary>
        private readonly IFavoritesService favoritesService;

        /// <summary>
        /// the Id of the user
        /// </summary>
        private int userId;

        /// <summary>
        /// Initializes a new instance of the FavorersOfShopViewModel class
        /// </summary>
        /// <param name="favoritesService">the favorites service</param>
        public FavorersOfShopViewModel(IFavoritesService favoritesService)
        {
            this.favoritesService = favoritesService;
            this.favoritesService.GetFavorersOfShopCompleted += this.UsersReceived;
            this.MakeListingCommands();

            this.ListingsPerPage = Constants.DefaultItemsPerPage;
        }

        /// <summary>
        /// Gets or sets the Id of the shop
        /// </summary>
        public int UserId
        {
            get { return this.userId; }
            set { this.userId = value; }
        }

        /// <summary>
        /// Gets or sets the command to show listings
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
            this.LoadPageCommand = new DelegateCommand<UserViewModel>(
                item =>
                {
                    int offset = (this.PageNumber - 1) * this.ItemsPerPage;

                    this.favoritesService.GetFavorersOfShop(this.UserId, offset, this.ListingsPerPage, DetailLevel.Medium);
                    string status = string.Format(
                        CultureInfo.InvariantCulture,
                        "Getting {0} favorite shops on page {1} for user",
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
        private void UsersReceived(object sender, ResultEventArgs<Users> e)
        {
            if (!e.ResultStatus.Success)
            {
                this.StatusText = "Failed to load favorers of shop " + e.ResultStatus.ErrorMessage;
                return;
            }

            this.Items.Clear();
            foreach (User item in e.ResultValue.Results)
            {
                UserViewModel viewModel = new UserViewModel(item);
                this.Items.Add(viewModel);
            }

            string status = string.Format(
                CultureInfo.InvariantCulture,
                "Got {0} favorers of shop on page {1} of {2} for shop",
                e.ResultValue.Results.Length,
                this.PageNumber,
                e.ResultValue.Count);
            this.StatusText = status;

            int nextPageOffset = this.PageNumber * this.ItemsPerPage;
            this.HasNextPage = nextPageOffset < e.ResultValue.Count;
        }
    }
}
