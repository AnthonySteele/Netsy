//-----------------------------------------------------------------------
// <copyright file="ShopWindowLoadListingsCommand.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.WpfUI.Windows.Shop
{
    using System.Globalization;
    using System.Windows.Threading;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.UI;
    using Netsy.UI.Commands;
    using Netsy.UI.ViewModels;

    /// <summary>
    /// Command to load listings for a shop
    /// </summary>
    public class ShopWindowLoadListingsCommand : GenericCommandBase<ShopWindowViewModel>
    {
          /// <summary>
        /// The service to return shop details
        /// </summary>
        private readonly IShopService shopService;

        /// <summary>
        /// the view model currently in use
        /// </summary>
        private ShopWindowViewModel currentViewModel;

        /// <summary>
        /// Initializes a new instance of the ShopWindowLoadListingsCommand class.
        /// </summary>
        /// <param name="shopService">the shop service</param>
        /// <param name="dispatcher">the thread dispatcher</param>
        public ShopWindowLoadListingsCommand(IShopService shopService, Dispatcher dispatcher)
        {
            this.shopService = shopService;
            this.shopService.GetShopListingsCompleted += this.ShopListingsReceived;
        }

        /// <summary>
        /// Execute the command and move to the next page
        /// </summary>
        /// <param name="value">the view model</param>
        public override void ExecuteOnValue(ShopWindowViewModel value)
        {
            this.currentViewModel = value;

            this.shopService.GetShopListings(this.currentViewModel.UserId, SortField.Created, SortOrder.Up, null, 0, value.ListingsPerPage, DetailLevel.Medium);
            string status = string.Format(CultureInfo.InvariantCulture, "Getting {0} listings for shop", value.ListingsPerPage);
            value.StatusText = status;
        }

        /// <summary>
        /// Callback for when Listings data has been received
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event params</param>
        private void ShopListingsReceived(object sender, ResultEventArgs<Listings> e)
        {
            // put it onto the Ui thread
            DispatcherHelper.Invoke(
                new ResultsReceivedHandler<Listings>(this.ShopListingsReceivedSync),
                e);
        }

        /// <summary>
        /// Listings data has been received
        /// </summary>
        /// <param name="listingsReceived">the listings</param>
        private void ShopListingsReceivedSync(ResultEventArgs<Listings> listingsReceived)
        {
            if (!listingsReceived.ResultStatus.Success)
            {
                this.currentViewModel.StatusText = "Failed to load listings " + listingsReceived.ResultStatus.ErrorMessage;
                return;
            }

            this.currentViewModel.Listings.Clear();
            foreach (Listing item in listingsReceived.ResultValue.Results)
            {
                ListingViewModel viewModel = new ListingViewModel(item);
                this.currentViewModel.Listings.Add(viewModel);
            }

            string status = string.Format(CultureInfo.InvariantCulture, "Loaded {0} listings for shop", this.currentViewModel.Listings.Count);
            this.currentViewModel.StatusText = status;
        }
    }
}
