//-----------------------------------------------------------------------
// <copyright file="ShopWindowViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.WpfUI.Windows.Shop
{
    using System.Windows.Input;

    using Main;

    using Netsy.DataModel;
    using Netsy.UI.ViewModels;
    using Netsy.UI.ViewModels.Shops;

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
        /// The shop listings being displayed
        /// </summary>
        private readonly ShopListingsViewModel shopListingsViewModel;

        /// <summary>
        /// The user's favorite shops 
        /// </summary>
        private readonly FavoriteShopsOfUserViewModel favoriteShopsOfUserViewModel;

        /// <summary>
        /// The user's favorite shops 
        /// </summary>
        private readonly FavoriteListingsOfUserViewModel favoriteListingsOfUserViewModel;

        /// <summary>
        /// Favorers of the shop
        /// </summary>
        private readonly FavorersOfShopViewModel favorersOfShopViewModel;

        /// <summary>
        /// the shop being diplayed
        /// </summary>
        private ShopViewModel shopViewModel;

        /// <summary>
        /// the shop id used
        /// </summary>
        private int userId;

        /// <summary>
        /// Initializes a new instance of the ShopWindowViewModel class
        /// </summary>
        /// <param name="shopListingsViewModel">viewmodel for the listings</param>
        /// <param name="favoriteShopsOfUserViewModel">viewmodel for the favorite shops</param>
        /// <param name="favoriteListingsOfUserViewModel">viewmodel for the favorite listings</param>
        /// <param name="favorersOfShopViewModel">viewmodel for the favorers of the shop</param>
        /// <param name="shopWindowLoadShopCommand">Command to load the shop</param>
        /// <param name="showShopWindowForShopCommand">Command to show the shop details for a shop</param>
        /// <param name="showShopWindowForListingCommand">Command to show the shop details for a listing</param>
        /// <param name="showListingWindowCommand">Command to show the listing details</param>
        public ShopWindowViewModel(
            ShopListingsViewModel shopListingsViewModel, 
            FavoriteShopsOfUserViewModel favoriteShopsOfUserViewModel,
            FavoriteListingsOfUserViewModel favoriteListingsOfUserViewModel,
            FavorersOfShopViewModel favorersOfShopViewModel,
            ShopWindowLoadShopCommand shopWindowLoadShopCommand,
            ShowShopWindowForShopCommand showShopWindowForShopCommand,
            ShowShopWindowForListingCommand showShopWindowForListingCommand,
            ShowListingWindowCommand showListingWindowCommand)
        {
            this.shopListingsViewModel = shopListingsViewModel;
            this.favoriteShopsOfUserViewModel = favoriteShopsOfUserViewModel;
            this.favoriteListingsOfUserViewModel = favoriteListingsOfUserViewModel;
            this.favorersOfShopViewModel = favorersOfShopViewModel;

            this.ShopListingsViewModel.ShowListingCommand = showListingWindowCommand;

            this.FavoriteListingsOfUserViewModel.ShowListingCommand = showListingWindowCommand;
            this.FavoriteListingsOfUserViewModel.ShowShopCommand = showShopWindowForListingCommand;
            
            this.FavoriteShopsOfUserViewModel.ShowListingCommand = showListingWindowCommand;
            this.FavoriteShopsOfUserViewModel.ShowShopCommand = showShopWindowForShopCommand;

            this.FavorersOfShopViewModel.ShowListingCommand = showListingWindowCommand;

            this.shopWindowLoadShopCommand = shopWindowLoadShopCommand;
        }

        /// <summary>
        /// Gets or sets the user id used 
        /// </summary>
        public int UserId
        {
            get
            {
                return this.userId;
            }

            set
            {
                this.userId = value;
                this.ShopListingsViewModel.ShopId = value;
                this.FavoriteListingsOfUserViewModel.UserId = value;
                this.FavoriteShopsOfUserViewModel.UserId = value;
                this.FavorersOfShopViewModel.UserId = value;
            }
        }

        /// <summary>
        /// Gets or sets the viewmodel of the shop being displayed
        /// </summary>
        public ShopViewModel Shop
        {
            get
            {
                return this.shopViewModel;
            }

            set
            {
                if (this.shopViewModel != value)
                {
                  this.shopViewModel = value;
                  this.OnPropertyChanged("Shop");
                  this.OnPropertyChanged("ShopData");
              }
            }
        }

        /// <summary>
        /// Gets the data of the shop being displayed
        /// </summary>
        public Shop ShopData
        {
            get
            {
                if (this.shopViewModel == null)
                {
                    return null;
                }

                return this.shopViewModel.Shop;
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
        /// Gets the shop listings being displayed
        /// </summary>
        public ShopListingsViewModel ShopListingsViewModel
        {
            get
            {
                return this.shopListingsViewModel;
            }
        }

        /// <summary>
        /// Gets the viewmodel for the user's favorite shops 
        /// </summary>
        public FavoriteShopsOfUserViewModel FavoriteShopsOfUserViewModel
        {
            get
            {
                return this.favoriteShopsOfUserViewModel;
            }
        }

        /// <summary>
        /// Gets the viewmodel for the user's favorite shops 
        /// </summary>
        public FavoriteListingsOfUserViewModel FavoriteListingsOfUserViewModel
        {
            get
            {
                return this.favoriteListingsOfUserViewModel;
            }
        }

        /// <summary>
        /// Gets the favorers of the shop
        /// </summary>
        public FavorersOfShopViewModel FavorersOfShopViewModel
        {
            get
            {
                return this.favorersOfShopViewModel;
            }
        }

        /// <summary>
        /// Load all data 
        /// </summary>
        public void LoadAll()
        {
            this.ShopWindowLoadShopCommand.Execute(this);
            this.ShopListingsViewModel.LoadPageCommand.Execute(this);
            this.FavoriteListingsOfUserViewModel.LoadPageCommand.Execute(this);
            this.FavoriteShopsOfUserViewModel.LoadPageCommand.Execute(this);
            this.FavorersOfShopViewModel.LoadPageCommand.Execute(this);
        }
    }
}
