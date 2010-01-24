//-----------------------------------------------------------------------
// <copyright file="FavoritesControlViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Favorites
{
    using System.Windows.Input;

    using Netsy.UI.ViewModels;
    using Netsy.UI.ViewModels.Shops;

    /// <summary>
    /// View model for the main page
    /// todo: test with mock service
    /// </summary>
    public class FavoritesControlViewModel : BaseViewModel
    {
        /// <summary>
        /// The listings displayed
        /// </summary>
        private readonly FavoriteListingsOfUserViewModel favoriteListingsOfUserViewModel;

        /// <summary>
        /// The command to get shop details
        /// </summary>
        private readonly ShopDetailsCommand shopDetailsCommand;

        /// <summary>
        /// the number of columns in the view 
        /// </summary>
        private int columnCount;
        
        /// <summary>
        /// Initializes a new instance of the FavoritesControlViewModel class
        /// </summary>
        /// <param name="favoriteListingsOfUserViewModel">the favorite listings view model</param>
        /// <param name="shopDetailsCommand">the shop details retrieval command</param>
        public FavoritesControlViewModel(FavoriteListingsOfUserViewModel favoriteListingsOfUserViewModel, ShopDetailsCommand shopDetailsCommand) 
        {
            this.favoriteListingsOfUserViewModel = favoriteListingsOfUserViewModel;
            this.shopDetailsCommand = shopDetailsCommand;
        }

        /// <summary>
        /// Gets the viewmodel for the listings displayed
        /// </summary>
        public FavoriteListingsOfUserViewModel Favorites
        {
            get { return this.favoriteListingsOfUserViewModel; }
        }

        /// <summary>
        /// Gets or sets the number of columns in the view 
        /// </summary>
        public int ColumnCount
        {
            get
            {
                return this.columnCount;
            } 

            set
            {
                if (this.columnCount != value)
                {
                    this.columnCount = value;
                    this.OnPropertyChanged("ColumnCount");                    
                }
            }
        }

        /// <summary>
        /// Gets or sets the user id in use
        /// </summary>
        public string UserId
        {
            get
            {
                return this.Favorites.UserId;
            }

            set
            {
                this.Favorites.UserId = value;
            }
        }

        /// <summary>
        /// Gets or sets the shop shown
        /// </summary>
        public ShopViewModel Shop
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the command to get shop details
        /// </summary>
        public ICommand ShopDetailsCommand
        {
            get
            {
                return this.shopDetailsCommand;
            }
        }

        /// <summary>
        /// Load data into this viewmodel
        /// </summary>
        public void Load()
        {
            this.ShopDetailsCommand.Execute(this);
            this.Favorites.LoadPageCommand.Execute(this.Favorites);
        }
    }
}
