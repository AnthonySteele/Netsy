//-----------------------------------------------------------------------
// <copyright file="ListingWindowLoadListingCommand.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.WpfUI.Windows.Listing
{
    using System.Globalization;
    using System.Windows;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.UI;
    using Netsy.UI.Commands;

    using UI.ViewModels;

    /// <summary>
    /// Command to load a listing
    /// </summary>
    public class ListingWindowLoadListingCommand : GenericCommandBase<ListingWindowViewModel>
    {
        /// <summary>
        /// The service to return listing details
        /// </summary>
        private readonly IListingsService listingService;

        /// <summary>
        /// the view model currently in use
        /// </summary>
        private ListingWindowViewModel currentViewModel;

        /// <summary>
        /// Initializes a new instance of the ListingWindowLoadListingCommand class.
        /// </summary>
        /// <param name="listingService">the listing service</param>
        public ListingWindowLoadListingCommand(IListingsService listingService)
        {
            this.listingService = listingService;
            this.listingService.GetListingDetailsCompleted += this.ListingReceived;
        }

        /// <summary>
        /// Execute the command and move to the next page
        /// </summary>
        /// <param name="value">the view model</param>
        public override void ExecuteOnValue(ListingWindowViewModel value)
        {
            this.currentViewModel = value;

            this.listingService.GetListingDetails(this.currentViewModel.ListingId, DetailLevel.High);
            string status = string.Format(CultureInfo.InvariantCulture, "Getting listing {0}", this.currentViewModel.ListingId);
            value.StatusText = status;
        }

        /// <summary>
        /// Callback for when Listings data has been received
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event params</param>
        private void ListingReceived(object sender, ResultEventArgs<Listings> e)
        {
            // put it onto the Ui thread
            DispatcherHelper.Invoke(
                new ResultsReceivedHandler<Listings>(this.ListingReceivedSync),
                e);
        }

        /// <summary>
        /// Listings data has been received
        /// </summary>
        /// <param name="listingsReceived">the listings</param>
        private void ListingReceivedSync(ResultEventArgs<Listings> listingsReceived)
        {
            if (!listingsReceived.ResultStatus.Success)
            {
                this.currentViewModel.StatusText = "Failed to load listing " + listingsReceived.ResultStatus.ErrorMessage;
                return;
            }

            Listing listing = listingsReceived.ResultValue.Results[0];
            
            this.currentViewModel.Listing = new ListingViewModel(listing);
            this.currentViewModel.Listing.ShopLinkVisibility = Visibility.Hidden;

            string status = string.Format(CultureInfo.InvariantCulture, "Loaded listing {0}", this.currentViewModel.ListingId);
            this.currentViewModel.StatusText = status;
        }
    }
}
