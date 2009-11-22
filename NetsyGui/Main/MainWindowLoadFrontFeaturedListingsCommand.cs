//-----------------------------------------------------------------------
// <copyright file="MainWindowLoadFrontFeaturedListingsCommand.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace NetsyGui.Main
{
    using System.Globalization;
    using System.Windows.Threading;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;

    using ViewModels;

    /// <summary>
    /// Command to load front featured listings on the main window
    /// </summary>
    public class MainWindowLoadFrontFeaturedListingsCommand : GenericCommandBase<MainWindowViewModel>
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
        /// the view model currently in use
        /// </summary>
        private MainWindowViewModel currentViewModel;

        /// <summary>
        /// Initializes a new instance of the MainWindowLoadFrontFeaturedListingsCommand class.
        /// </summary>
        /// <param name="listingsService">the lsistings service</param>
        /// <param name="dispatcher">the thread dispatcher</param>
        public MainWindowLoadFrontFeaturedListingsCommand(IListingsService listingsService, Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
            this.listingsService = listingsService;
            this.listingsService.GetFrontFeaturedListingsCompleted += this.FrontFeaturedListingsReceived;
        }

        /// <summary>
        /// Execute the command and move to the next page
        /// </summary>
        /// <param name="value">the view model</param>
        public override void ExecuteOnValue(MainWindowViewModel value)
        {
            this.currentViewModel = value;
            int offset = (value.PageNumber - 1) * MainWindowViewModel.ItemPerPage;

            this.listingsService.GetFrontFeaturedListings(offset, MainWindowViewModel.ItemPerPage, DetailLevel.Medium);
            string status = string.Format(CultureInfo.InvariantCulture, "Getting {0} front listings on page {1}", MainWindowViewModel.ItemPerPage, value.PageNumber);
            value.StatusText = status;
        }

        /// <summary>
        /// Callback for when Listings data has been received
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event params</param>
        private void FrontFeaturedListingsReceived(object sender, ResultEventArgs<Listings> e)
        {
            // put it onto the Ui thread
            this.dispatcher.Invoke(
                DispatcherPriority.Normal,
                new ResultsReceivedHandler<Listings>(this.FrontFeaturedListingsReceivedSync),
                e);
        }

        /// <summary>
        /// Listings data has been received
        /// </summary>
        /// <param name="listingsReceived">the listings</param>
        private void FrontFeaturedListingsReceivedSync(ResultEventArgs<Listings> listingsReceived)
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

            string status = string.Format(CultureInfo.InvariantCulture, "Loaded {0} front listings on page {1}", this.currentViewModel.Listings.Count, this.currentViewModel.PageNumber);
            this.currentViewModel.StatusText = status;

            CommandLocator.MainWindowCanExecuteChanged();
        }
    }
}
