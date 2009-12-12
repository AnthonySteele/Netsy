//-----------------------------------------------------------------------
// <copyright file="ColorListingsViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.UI.ViewModels.Listings
{
    using System.Globalization;
    using System.Windows.Threading;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.UI.Commands;

    /// <summary>
    /// View model for a collection of listings from the front featured listings service
    /// </summary>
    public class ColorListingsViewModel : PagedCollectionViewModel<ListingViewModel>
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
        /// The color to match
        /// </summary>
        private string colorText;

        /// <summary>
        /// Initializes a new instance of the ColorListingsViewModel class.
        /// </summary>
        /// <param name="listingsService">the listings service</param>
        /// <param name="dispatcher">the thread dispatcher</param>
        public ColorListingsViewModel(IListingsService listingsService, Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
            this.listingsService = listingsService;
            this.listingsService.GetListingsByColorCompleted += this.ListingsReceived;

            this.MakeCommands();
        }

        /// <summary>
        /// Gets or sets the color to match
        /// </summary>
        public string ColorText
        {
            get
            {
                return this.colorText;
            }

            set
            {
                if (this.colorText != value)
                {
                    this.colorText = value;
                    this.OnPropertyChanged("ColorText");
                }
            }
        }

        /// <summary>
        /// Create the load command 
        /// </summary>
        private void MakeCommands()
        {
            this.LoadPageCommand = new DelegateCommand<ListingViewModel>(
                item =>
                {
                    if (!this.HasSearchParams())
                    {
                        this.StatusText = "Enter the color";
                        return;
                    }

                    const int MaxWiggle = 15;
                    RgbColor rgbColor = this.ConvertColor();

                    int offset = (this.PageNumber - 1) * this.ItemsPerPage;

                    this.listingsService.GetListingsByColor(rgbColor, MaxWiggle, offset, this.ItemsPerPage, DetailLevel.Medium);
                    string status = string.Format(CultureInfo.InvariantCulture, "Getting {0} listings by color on page {1}", this.ItemsPerPage, this.PageNumber);
                    this.StatusText = status;
                });
        }

        /// <summary>
        /// Have search params been entered
        /// </summary>
        /// <returns>true if the UI has text entered</returns>
        private bool HasSearchParams()
        {
            if (string.IsNullOrEmpty(this.ColorText))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get the color data object
        /// </summary>
        /// <returns>the color data object</returns>
        private RgbColor ConvertColor()
        {
            RgbColor rgbColor = null;
            string trimedColor = string.Empty;
            if (! string.IsNullOrEmpty(this.ColorText))
            {
                trimedColor = this.ColorText.Trim();
            }

            if (!string.IsNullOrEmpty(trimedColor))
            {   
                rgbColor = new RgbColor(trimedColor);
            }

            return rgbColor;
        }

        /// <summary>
        /// Callback for when Listings data has been received
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event params</param>
        private void ListingsReceived(object sender, ResultEventArgs<Listings> e)
        {
            // put it onto the Ui thread
            this.dispatcher.Invoke(
                DispatcherPriority.Normal,
                new ResultsReceivedHandler<Listings>(this.ListingsReceivedSync),
                e);
        }

        /// <summary>
        /// Listings data has been received
        /// </summary>
        /// <param name="listingsReceived">the listings</param>
        private void ListingsReceivedSync(ResultEventArgs<Listings> listingsReceived)
        {
            if (!listingsReceived.ResultStatus.Success)
            {
                this.StatusText = "Failed to load listings by color " + listingsReceived.ResultStatus.ErrorMessage;
                return;
            }

            this.Items.Clear();
            foreach (Listing item in listingsReceived.ResultValue.Results)
            {
                ListingViewModel viewModel = new ListingViewModel(item);
                this.Items.Add(viewModel);
            }

            string status = string.Format(CultureInfo.InvariantCulture, "Loaded {0} listings by color on page {1}", this.Items.Count, this.PageNumber);
            this.StatusText = status;
        }
    }
}
