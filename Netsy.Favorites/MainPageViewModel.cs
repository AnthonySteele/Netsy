namespace Netsy.Favorites
{
    using System;
    using System.Windows.Threading;
    using System.Collections.ObjectModel;

    using Netsy.UI.ViewModels;
    using Netsy.Interfaces;
    using Netsy.DataModel;
    using Netsy.Helpers;

    /// <summary>
    /// todo: test with a mock service
    /// </summary>
    public class MainPageViewModel: BaseViewModel
    {
        private int listingCount = Constants.DefaultItemsPerPage;

        private string errorMessage;

        private IFavoritesService favoritesService;
        
        private ObservableCollection<ListingViewModel> listings = new ObservableCollection<ListingViewModel>();

        public MainPageViewModel(IFavoritesService favoritesService, Dispatcher dispatcher) 
        {
            if (favoritesService == null || dispatcher == null)
            {
                throw new ArgumentNullException();
            }
            this.favoritesService = favoritesService;
            this.Dispatcher = dispatcher;
            this.favoritesService.GetFavoriteListingsOfUserCompleted += CompletedGetFavorites;

        }

        public int ListingCount
        {
            get
            {
                return this.listingCount;
            }
            set
            {
                this.listingCount = value;
            }
        }

        public Dispatcher Dispatcher { get; private set; }
        public string UserId { get; set; }

        public ObservableCollection<ListingViewModel> Listings
        {
            get
            {
                return this.listings;
            }

        }

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

        public void BeginGetFavorites() 
        {
            if (string.IsNullOrEmpty(this.UserId))
            {
                this.ErrorMessage = "No user id for favorites";
                return;
            }

            // todo: put tests on api to show wrong count returned
            favoritesService.GetFavoriteListingsOfUser(UserId, 0, ListingCount + 1, DetailLevel.High);
        }

        private void CompletedGetFavorites(object sender, ResultEventArgs<Listings> e)
        {
            this.Dispatcher.BeginInvoke(
                new ResultsReceivedHandler<Listings>(CompletedGetFavoritesSync), e);
        }

        void CompletedGetFavoritesSync(ResultEventArgs<Listings> e)
        {
            if (!e.ResultStatus.Success)
            {
                this.ErrorMessage = e.ResultStatus.ErrorMessage;
                return;
            }

            this.listings.Clear();

            foreach (Listing listing in e.ResultValue.Results)
            {
                this.listings.Add(new ListingViewModel(listing));
            }

            this.ErrorMessage = this.listings.Count.ToString() + " listings loaded";
        }
    }

}
