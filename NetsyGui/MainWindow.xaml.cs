//-----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace NetsyGui
{
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Threading;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.Services;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// the listings shown on the gui
        /// </summary>
        private readonly ObservableCollection<Listing> listings = new ObservableCollection<Listing>();

        /// <summary>
        /// Initializes a new instance of the MainWindow class
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.listingsDisplay.DataContext = this.listings;
            this.RequestFrontFeaturedListings();
        }

        /// <summary>
        /// Request Listings data
        /// </summary>
        private void RequestFrontFeaturedListings()
        {
            const string EtsyApiKey = "rfc35bh98q3a9hvccfsxe4cc";

            IListingsService listingsService = new ListingsService(new EtsyContext(EtsyApiKey));
            listingsService.GetFrontFeaturedListingsCompleted += this.FrontFeaturedListingsReceived;

            // ACT
            listingsService.GetFrontFeaturedListings(0, 10, DetailLevel.Medium);
        }

        /// <summary>
        /// Callback for when Listings data has been received
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event params</param>
        private void FrontFeaturedListingsReceived(object sender, ResultEventArgs<Listings> e)
        {
            this.Dispatcher.Invoke(
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
                this.listings.Add(item);
            }
        }
    }
}
