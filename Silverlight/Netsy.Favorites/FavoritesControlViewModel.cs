//-----------------------------------------------------------------------
// <copyright file="FavoritesControlViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Favorites
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Threading;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.UI.ViewModels;

    /// <summary>
    /// View model for the main page
    /// todo: test with mock service
    /// </summary>
    public class FavoritesControlViewModel : BaseViewModel
    {
        /// <summary>
        /// The service to get favorites
        /// </summary>
        private readonly IFavoritesService favoritesService;

        /// <summary>
        /// The listings displayed
        /// </summary>
        private readonly ObservableCollection<ListingViewModel> listings = new ObservableCollection<ListingViewModel>();
        
        /// <summary>
        /// Number of listings per page
        /// </summary>
        private int listingCount = Constants.DefaultItemsPerPage;

        /// <summary>
        /// The error test to display
        /// </summary>
        private string errorMessage;
        
        /// <summary>
        /// Initializes a new instance of the MainPageViewModel class
        /// </summary>
        /// <param name="favoritesService">the favorites service</param>
        /// <param name="dispatcher">the Ui dispatecher</param>
        public FavoritesControlViewModel(IFavoritesService favoritesService, Dispatcher dispatcher) 
        {
            if (favoritesService == null)
            {
                throw new ArgumentNullException("favoritesService");
            }

            if (dispatcher == null)
            {
                throw new ArgumentNullException("dispatcher");
            }

            this.favoritesService = favoritesService;
            this.Dispatcher = dispatcher;

            this.favoritesService.GetFavoriteListingsOfUserCompleted += this.CompletedGetFavorites;
        }

        /// <summary>
        /// Gets or sets the number of listings per page
        /// </summary>
        public int ListingCount
        {
            get
            {
                return this.listingCount;
            }

            set
            {
                if (this.listingCount != value)
                {
                    this.listingCount = value;
                    this.OnPropertyChanged("ListingCount");
                }
            }
        }

        /// <summary>
        /// Gets the Error message text
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }

            private set
            {
                if (this.errorMessage != value)
                {
                    this.errorMessage = value;
                    this.OnPropertyChanged("ErrorMessage");
                }
            }
        }

        /// <summary>
        /// Gets the listings displayed
        /// </summary>
        public ObservableCollection<ListingViewModel> Listings
        {
            get { return this.listings; }
        }

        /// <summary>
        /// Gets the UI dispatcher
        /// </summary>
        public Dispatcher Dispatcher { get; private set; }

        /// <summary>
        /// Gets or sets the user to get favoorites for
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Retrieve favorites from the service
        /// </summary>
        public void BeginGetFavorites() 
        {
            if (string.IsNullOrEmpty(this.UserId))
            {
                this.ErrorMessage = "No user id for favorites";
                return;
            }

            // todo: put tests on api to show wrong count returned
            this.favoritesService.GetFavoriteListingsOfUser(this.UserId, 0, this.ListingCount + 1, DetailLevel.High);
        }

        /// <summary>
        /// Event handler for when favorites have been received
        /// </summary>
        /// <param name="sender">the vent sender</param>
        /// <param name="e">the event params</param>
        private void CompletedGetFavorites(object sender, ResultEventArgs<Listings> e)
        {
            this.Dispatcher.BeginInvoke(new ResultsReceivedHandler<Listings>(this.UpdateForReceivedListings), e);
        }

        /// <summary>
        /// Process favorites that have been received
        /// </summary>
        /// <param name="receivedListings">the listings data received</param>
        private void UpdateForReceivedListings(ResultEventArgs<Listings> receivedListings)
        {
            if (!receivedListings.ResultStatus.Success)
            {
                this.ErrorMessage = receivedListings.ResultStatus.ErrorMessage;
                return;
            }

            this.listings.Clear();
            foreach (Listing listing in receivedListings.ResultValue.Results)
            {
                this.listings.Add(new ListingViewModel(listing));
            }

            this.ErrorMessage = this.listings.Count + " Listings loaded";
        }
    }
}
