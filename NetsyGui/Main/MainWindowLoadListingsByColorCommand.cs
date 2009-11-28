//-----------------------------------------------------------------------
// <copyright file="MainWindowLoadListingsByColorCommand.cs" company="AFS">
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
    public class MainWindowLoadListingsByColorCommand : GenericCommandBase<MainWindowViewModel>
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
        /// The color used
        /// </summary>
        private string color;

        /// <summary>
        /// Initializes a new instance of the MainWindowLoadListingsByColorCommand class.
        /// </summary>
        /// <param name="listingsService">the listings service</param>
        /// <param name="dispatcher">the thread dispatcher</param>
        public MainWindowLoadListingsByColorCommand(IListingsService listingsService, Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
            this.listingsService = listingsService;
            this.listingsService.GetListingsByColorCompleted += this.ListingsByColorReceived;
        }

        /// <summary>
        /// Execute the command and move to the next page
        /// </summary>
        /// <param name="value">the view model</param>
        public override void ExecuteOnValue(MainWindowViewModel value)
        {
            this.currentViewModel = value;

            if (string.IsNullOrEmpty(value.ColourText))
            {
                value.StatusText = "Enter a color";
                return;
            }

            const int MaxWiggle = 15;
            this.color = value.ColourText.Trim();
            RgbColor rgbColor = new RgbColor(this.color);
            this.listingsService.GetListingsByColor(rgbColor, MaxWiggle, 0, MainWindowViewModel.ItemPerPage, DetailLevel.Medium);
            string status = string.Format(CultureInfo.InvariantCulture, "Getting {0} listings for color {1}", MainWindowViewModel.ItemPerPage, this.color);
            value.StatusText = status;
        }

        /// <summary>
        /// Callback for when Listings data has been received
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event params</param>
        private void ListingsByColorReceived(object sender, ResultEventArgs<Listings> e)
        {
            // put it onto the Ui thread
            this.dispatcher.Invoke(
                DispatcherPriority.Normal,
                new ResultsReceivedHandler<Listings>(this.ListingsByColorReceivedSync),
                e);
        }

        /// <summary>
        /// Listings data has been received
        /// </summary>
        /// <param name="listingsReceived">the listings</param>
        private void ListingsByColorReceivedSync(ResultEventArgs<Listings> listingsReceived)
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

            string status = string.Format(CultureInfo.InvariantCulture, "Loaded {0} listings for color {1}", this.currentViewModel.Listings.Count, this.color);
            this.currentViewModel.StatusText = status;

            CommandLocator.MainWindowCanExecuteChanged();
        }
    }
}
