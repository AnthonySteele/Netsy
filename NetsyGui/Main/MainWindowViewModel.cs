//-----------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace NetsyGui.Main
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Windows.Threading;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;

    using NetsyGui.ViewModels;

    /// <summary>
    /// View model for the main window
    /// </summary>
    public class MainWindowViewModel : BaseViewModel
    {
        /// <summary>
        /// the listings shown on the gui
        /// </summary>
        private readonly ObservableCollection<ListingViewModel> listings = new ObservableCollection<ListingViewModel>();

        /// <summary>
        /// The service to get listings from
        /// </summary>
        private readonly IListingsService listingsService;

        /// <summary>
        /// The thread dispatcher
        /// </summary>
        private readonly Dispatcher dispatcher;

        /// <summary>
        /// Number of items to retrieve
        /// </summary>
        private const int ItemPerPage = 12;

        /// <summary>
        /// the page index into the results
        /// </summary>
        private int pageNumber = 1;

        /// <summary>
        /// The text to display on the status bar
        /// </summary>
        private string statusText;

        /// <summary>
        /// Initializes a new instance of the MainWindowViewModel class
        /// </summary>
        /// <param name="listingsService">the listing service to use</param>
        /// <param name="dispatcher">the dispatcher to use</param>
        public MainWindowViewModel(IListingsService listingsService, Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
            this.listingsService = listingsService;

            this.listingsService.GetFrontFeaturedListingsCompleted += this.FrontFeaturedListingsReceived;

            this.StatusText = "Etsy Gui";
        }

        /// <summary>
        /// Gets the Listings 
        /// </summary>
        public ObservableCollection<ListingViewModel> Listings
        {
            get { return this.listings; }
        }

        /// <summary>
        /// Gets or sets the page index into the results
        /// </summary>
        public int PageNumber
        {
            get
            {
                return this.pageNumber;
            }

            set
            {
                if (this.pageNumber != value)
                {
                    this.pageNumber = value;
                    this.OnPropertyChanged("PageNumber");
                }
            }
        }

        /// <summary>
        /// Gets or sets the status bar text
        /// </summary>
        public string StatusText
        {
            get
            {
                return this.statusText;
            }

            set
            {
                if (this.statusText != value)
                {
                    this.statusText = value;
                    this.OnPropertyChanged("StatusText");
                }
            }
        }

        /// <summary>
        /// Request Listings data
        /// </summary>
        public void RequestFrontFeaturedListings()
        {
            int offset = (this.PageNumber - 1) * ItemPerPage;

            this.listingsService.GetFrontFeaturedListings(offset, ItemPerPage, DetailLevel.Medium);
            this.StatusText = "Getting front featured listings";
        }

        /// <summary>
        /// Show the next page of results
        /// </summary>
        public void NextPage()
        {
            this.PageNumber++;
            this.RequestFrontFeaturedListings();
        }

        /// <summary>
        /// Show the previous page of results
        /// </summary>
        public void PreviousPage()
        {
            if (this.pageNumber > 1)
            {
                this.PageNumber--;
                this.RequestFrontFeaturedListings();
            }
        }

        /// <summary>
        /// Show the first page of results
        /// </summary>
        public void FirstPage()
        {
            if (this.PageNumber > 1)
            {
                this.pageNumber = 1;
                this.RequestFrontFeaturedListings();
            }
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
                this.StatusText = "Failed to load listings";
                return;
            }

            this.DisplayListings(listingsReceived.ResultValue.Results);
            this.StatusText = string.Format(CultureInfo.InvariantCulture, "Loaded {0} front listings", this.listings.Count);
            CommandLocator.MainWindowCanExecuteChanged();
        }

        /// <summary>
        /// Diplay the listings
        /// </summary>
        /// <param name="listingsData">the listings to display</param>
        private void DisplayListings(IEnumerable<Listing> listingsData)
        {
            this.listings.Clear();
            foreach (Listing item in listingsData)
            {
                ListingViewModel viewModel = new ListingViewModel(item);
                this.listings.Add(viewModel);
            }
        }
    }
}
