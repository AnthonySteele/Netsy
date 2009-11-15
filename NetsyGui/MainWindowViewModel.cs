namespace NetsyGui
{
    using System.Collections.ObjectModel;
    using System.Windows.Threading;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;

    public class MainWindowViewModel
    {
        /// <summary>
        /// the listings shown on the gui
        /// </summary>
        private readonly ObservableCollection<ListingViewModel> listings = new ObservableCollection<ListingViewModel>();

        private IListingsService listingsService;

        private Dispatcher dispatcher;

        public MainWindowViewModel(IListingsService listingsService, Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;

            this.listingsService = listingsService;
            this.listingsService.GetFrontFeaturedListingsCompleted += this.FrontFeaturedListingsReceived;
        }

        public ObservableCollection<ListingViewModel> Listings
        {
            get { return this.listings; }
        }

        /// <summary>
        /// Request Listings data
        /// </summary>
        public void RequestFrontFeaturedListings()
        {
            this.listingsService.GetFrontFeaturedListings(0, 10, DetailLevel.Medium);
        }

        /// <summary>
        /// Callback for when Listings data has been received
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event params</param>
        private void FrontFeaturedListingsReceived(object sender, ResultEventArgs<Listings> e)
        {
            this.dispatcher.Invoke(
                DispatcherPriority.Normal,
                new ResultsReceivedHandler<Listings>(this.FrontFeaturedListingsReceivedSync),
                e);
        }

        /// <summary>
        /// Listings data has been received, on the right thread
        /// </summary>
        /// <param name="listingsReceived">the listings</param>
        private void FrontFeaturedListingsReceivedSync(ResultEventArgs<Listings> listingsReceived)
        {
            this.listings.Clear();
            foreach (Listing item in listingsReceived.ResultValue.Results)
            {
                ListingViewModel viewModel = new ListingViewModel();
                viewModel.Listing = item;

                this.listings.Add(viewModel);
            }
        }


    }
}
