//-----------------------------------------------------------------------
// <copyright file="ShopControlViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Shop
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    using Netsy.Interfaces;
    using Netsy.UI.ViewModels;

    /// <summary>
    /// View model object for the Shop control
    /// </summary>
    public class ShopControlViewModel : BaseViewModel
    {
        /// <summary>
        /// The command to get the shop details
        /// </summary>
        private readonly ShopDetailsCommand shopDetailsCommand;

        /// <summary>
        /// The command to get the shop details
        /// </summary>
        private readonly ShopListingsCommand shopListingsCommand;

        /// <summary>
        /// The listings collection
        /// </summary>
        private readonly ObservableCollection<ListingViewModel> listings = new ObservableCollection<ListingViewModel>();

        /// <summary>
        /// The shop data 
        /// </summary>
        private ShopViewModel shop;

        /// <summary>
        /// Initializes a new instance of the ShopControlViewModel class
        /// </summary>
        /// <param name="shopDetailsCommand">the command to get details for a shop</param>
        /// <param name="shopListingsCommand">the command to get listings for a shop</param>
        public ShopControlViewModel(ShopDetailsCommand shopDetailsCommand, ShopListingsCommand shopListingsCommand)
        {
            if (shopDetailsCommand == null)
            {
                throw new ArgumentNullException("shopDetailsCommand");
            }

            if (shopListingsCommand == null)
            {
                throw new ArgumentNullException("shopListingsCommand");
            }

            this.shopDetailsCommand = shopDetailsCommand;
            this.shopListingsCommand = shopListingsCommand;
        }

        /// <summary>
        /// Gets or sets the Id of the user to display
        /// </summary>
        public string UserId
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the shop displayed
        /// </summary>
        public ShopViewModel Shop
        {
            get
            {
                return this.shop;
            }
            set
            {
                if (this.shop != value)
                {
                    this.shop = value;
                    this.OnPropertyChanged("Shop");
                    this.OnPropertyChanged("BannerImageUrl");
                }
            }
        }

        /// <summary>
        /// Gets the shop image Url
        /// </summary>
        public string BannerImageUrl
        {
            get
            {
                if (this.Shop == null)
                {
                    return String.Empty;
                }

                return this.Shop.Shop.BannerImageUrl;
            }
        }

        /// <summary>
        /// Gets the Listings displayed
        /// </summary>
        public ObservableCollection<ListingViewModel> Listings
        {
            get { return this.listings; }
        }

        /// <summary>
        /// Gets the command to get the shop details
        /// </summary>
        public ICommand ShopDetailsCommand
        {
            get { return this.shopDetailsCommand; }
        }

        /// <summary>
        /// Gets the command to get the shop details
        /// </summary>
        public ICommand ShopListingsCommand
        {
            get { return this.shopListingsCommand; }
        }

        /// <summary>
        /// Load the shop
        /// </summary>
        public void LoadShop()
        {
            this.ShopDetailsCommand.Execute(this);
            this.shopListingsCommand.Execute(this);
        }
    }
}
