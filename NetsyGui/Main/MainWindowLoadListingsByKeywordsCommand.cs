//-----------------------------------------------------------------------
// <copyright file="MainWindowLoadListingsByKeywordCommand.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace NetsyGui.Main
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows.Threading;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;

    using ViewModels;

    /// <summary>
    /// Command to load front featured listings on the main window
    /// </summary>
    public class MainWindowLoadListingsByKeywordCommand : GenericCommandBase<MainWindowViewModel>
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
        /// The search terms used
        /// </summary>
        private string searchTerms;

        /// <summary>
        /// Initializes a new instance of the MainWindowLoadListingsByKeywordCommand class.
        /// </summary>
        /// <param name="listingsService">the listings service</param>
        /// <param name="dispatcher">the thread dispatcher</param>
        public MainWindowLoadListingsByKeywordCommand(IListingsService listingsService, Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
            this.listingsService = listingsService;
            this.listingsService.GetListingsByKeywordCompleted += this.ListingsByKeywordReceived;
        }

        /// <summary>
        /// Execute the command and move to the next page
        /// </summary>
        /// <param name="value">the view model</param>
        public override void ExecuteOnValue(MainWindowViewModel value)
        {
            this.currentViewModel = value;
         
            if (string.IsNullOrEmpty(value.ListingKeywords))
            {
                value.StatusText = "Enter one or more keywords";
                return;
            }

            this.searchTerms = value.ListingKeywords.Trim();
            List<string> keywords = new List<string>();
            keywords.AddRange(this.searchTerms.Split(' ', ','));

            if (keywords.Count == 0)
            {
                value.StatusText = "Enter one or more keywords";
                return;                
            }

            this.listingsService.GetListingsByKeyword(keywords, SortField.Score, SortOrder.Down, null, null, true, 0, MainWindowViewModel.ItemPerPage, DetailLevel.Medium);
            string status = string.Format(CultureInfo.InvariantCulture, "Getting {0} listings for keywords {1}", MainWindowViewModel.ItemPerPage, this.searchTerms);
            value.StatusText = status;
        }

        /// <summary>
        /// Callback for when Listings data has been received
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event params</param>
        private void ListingsByKeywordReceived(object sender, ResultEventArgs<Listings> e)
        {
            // put it onto the Ui thread
            this.dispatcher.Invoke(
                DispatcherPriority.Normal, 
                new ResultsReceivedHandler<Listings>(this.ListingsByKeywordReceivedSync),
                e);
        }

        /// <summary>
        /// Listings data has been received
        /// </summary>
        /// <param name="listingsReceived">the listings</param>
        private void ListingsByKeywordReceivedSync(ResultEventArgs<Listings> listingsReceived)
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

            string status = string.Format(CultureInfo.InvariantCulture, "Loaded {0} listings for keyword {1}", this.currentViewModel.Listings.Count, this.searchTerms);
            this.currentViewModel.StatusText = status;

            CommandLocator.MainWindowCanExecuteChanged();
        }
    }
}
