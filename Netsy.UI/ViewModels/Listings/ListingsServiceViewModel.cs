//-----------------------------------------------------------------------
// <copyright file="ListingsServiceViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.UI.ViewModels.Listings
{
    using System.Windows.Threading;

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
        /// The theading dispatcher
        /// </summary>
        private readonly Dispatcher dispatcher;

        /// <summary>
        /// Initializes a new instance of the ListingsServiceViewModel class.
        /// </summary>
        /// <param name="listingsService">the listings service</param>
        /// <param name="dispatcher">the thread dispatcher</param>
        protected ListingsServiceViewModel(IListingsService listingsService, Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
            this.listingsService = listingsService;
        }

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
        /// Callback for when Listings data has been received
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event params</param>
        protected void ListingsReceived(object sender, ResultEventArgs<Listings> e)
        {
            // put it onto the Ui thread
            this.dispatcher.Invoke(
                DispatcherPriority.Normal,
                new ResultsReceivedHandler<Listings>(this.ListingsReceivedSync),
                e);
        }

        /// <summary>
        /// Show a message after load success
        /// </summary>
        protected abstract void ShowLoadedSuccessMessage();

        /// <summary>
        /// Listings data has been received
        /// </summary>
        /// <param name="listingsReceived">the listings</param>
        private void ListingsReceivedSync(ResultEventArgs<Listings> listingsReceived)
        {
            if (!listingsReceived.ResultStatus.Success)
            {
                this.StatusText = "Failed to load listings " + listingsReceived.ResultStatus.ErrorMessage;
                return;
            }

            this.Items.Clear();
            foreach (Listing item in listingsReceived.ResultValue.Results)
            {
                ListingViewModel viewModel = new ListingViewModel(item);
                this.Items.Add(viewModel);
            }

            this.ShowLoadedSuccessMessage();
        }
    }
}
