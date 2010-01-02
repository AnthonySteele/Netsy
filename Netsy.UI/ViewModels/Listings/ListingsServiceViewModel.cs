//-----------------------------------------------------------------------
// <copyright file="ListingsServiceViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.UI.ViewModels.Listings
{
    using System.Windows.Input;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;

    /// <summary>
    /// Base class View model for a collection of listings from the listings service
    /// </summary>
    public abstract class ListingsServiceViewModel : PagedCollectionViewModel<ListingViewModel>
    {
        /// <summary>
        /// The service to return listings
        /// </summary>
        private readonly IListingsService listingsService;

        /// <summary>
        /// Initializes a new instance of the ListingsServiceViewModel class.
        /// </summary>
        /// <param name="listingsService">the listings service</param>
        protected ListingsServiceViewModel(IListingsService listingsService)
        {
            this.listingsService = listingsService;
        }

        /// <summary>
        /// Gets or sets the command to show the listing's shop
        /// </summary>
        public ICommand ShowShopCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to show the listing in detail
        /// </summary>
        public ICommand ShowListingCommand { get; set; }

        /// <summary>
        /// Gets the service to return listings
        /// </summary>
        protected IListingsService ListingsService
        {
            get
            {
                return this.listingsService;
            }
        }

        /// <summary>
        /// Show a message after load success
        /// </summary>
        protected abstract void ShowLoadedSuccessMessage();

        /// <summary>
        /// Callback for when Listings data has been received
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event params</param>
        protected void ListingsReceived(object sender, ResultEventArgs<Listings> e)
        {
            if (!e.ResultStatus.Success)
            {
                this.StatusText = "Failed to load listings " + e.ResultStatus.ErrorMessage;
                return;
            }

            this.Items.Clear();
            foreach (Listing item in e.ResultValue.Results)
            {
                ListingViewModel viewModel = new ListingViewModel(item);
                viewModel.ShowShopCommand = this.ShowShopCommand;
                viewModel.ShowListingCommand = this.ShowListingCommand;
                this.Items.Add(viewModel);
            }

            this.ShowLoadedSuccessMessage();
        }
    }
}
