//-----------------------------------------------------------------------
// <copyright file="ShopWindowViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace NetsyGui.Shop
{
    using System.Collections.ObjectModel;

    using ViewModels;

    /// <summary>
    /// View model for the shop Window
    /// </summary>
    public class ShopWindowViewModel : BaseViewModel
    {
        /// <summary>
        /// the number of listings to show at once
        /// </summary>
        public const int ListingsPerPage = 12;

        /// <summary>
        /// The shop's listings
        /// </summary>
        private readonly ObservableCollection<ListingViewModel> listings = new ObservableCollection<ListingViewModel>();

        /// <summary>
        /// the Id of the user/shop being shown
        /// </summary>
        private int userId;

        /// <summary>
        /// the status bar text
        /// </summary>
        private string statusText;

        /// <summary>
        /// The shop to display
        /// </summary>
        private ShopViewModel shop; 

        /// <summary>
        /// Gets or sets the Id of the user/shop being shown
        /// </summary>
        public int UserId
        {
            get
            {
                return this.userId;
            }

            set
            {
                if (this.userId != value)
                {
                    this.userId = value;
                    this.OnPropertyChanged("UserId");
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
        /// Gets or sets the shop's data
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
                }                
            }
        }

        /// <summary>
        /// Gets the shop's listings
        /// </summary>
        public ObservableCollection<ListingViewModel> Listings
        {
            get
            {
                return this.listings;
            }
        }
    }
}
