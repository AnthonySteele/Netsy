//-----------------------------------------------------------------------
// <copyright file="ShopWindowViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.WpfUI.Windows.Shop
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    using Netsy.UI.ViewModels;

    /// <summary>
    /// View model for the shop window
    /// </summary>
    public class ShopWindowViewModel : BaseViewModel
    {
        /// <summary>
        /// Command to load the shop
        /// </summary>
        private readonly ICommand shopWindowLoadShopCommand;

        /// <summary>
        /// Command to load the listings
        /// </summary>
        private readonly ICommand shopWindowLoadListingsCommand;

        /// <summary>
        /// The text to display on the status bar
        /// </summary>
        private string statusText;

        /// <summary>
        /// The number of listings per page
        /// </summary>
        private int listingsPerPage = Constants.DefaultItemsPerPage;

        /// <summary>
        /// Initializes a new instance of the ShopWindowViewModel class
        /// </summary>
        /// <param name="shopWindowLoadShopCommand">Command to load the shop</param>
        /// <param name="shopWindowLoadListingsCommand">Command to load the listings</param>
        public ShopWindowViewModel(ShopWindowLoadShopCommand shopWindowLoadShopCommand, ShopWindowLoadListingsCommand shopWindowLoadListingsCommand)
        {
            this.shopWindowLoadShopCommand = shopWindowLoadShopCommand;
            this.shopWindowLoadListingsCommand = shopWindowLoadListingsCommand;

            this.Listings = new ObservableCollection<ListingViewModel>();
        }

        /// <summary>
        /// Gets or sets the user id used 
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the shop being displayed
        /// </summary>
        public ShopViewModel Shop { get; set; }

        /// <summary>
        /// Gets or sets the number of listings per page
        /// </summary>
        public int ListingsPerPage
        {
            get { return this.listingsPerPage; }
            set { this.listingsPerPage = value; }
        }

        /// <summary>
        /// Gets the listings in the shop
        /// </summary>
        public ObservableCollection<ListingViewModel> Listings { get; private set; }

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
        /// Gets the command to load the shop
        /// </summary>
        public ICommand ShopWindowLoadShopCommand
        {
            get
            {
                return this.shopWindowLoadShopCommand;
            }
        }

        /// <summary>
        /// Gets the command to load the listings
        /// </summary>
        public ICommand ShopWindowLoadListingsCommand
        {
            get
            {
                return this.shopWindowLoadListingsCommand;
            }
        }
    }
}
