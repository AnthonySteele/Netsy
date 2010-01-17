//-----------------------------------------------------------------------
// <copyright file="FavoritesControlViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Favorites
{
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
        /// Initializes a new instance of the FavoritesControlViewModel class
        /// </summary>
        /// <param name="favoriteListingsOfUserViewModel">the favorite listings view model</param>
        public FavoritesControlViewModel(FavoriteListingsOfUserViewModel favoriteListingsOfUserViewModel) 
        {
            this.favoriteListingsOfUserViewModel = favoriteListingsOfUserViewModel;
        }

        /// <summary>
        /// Gets the viewmodel for the listings displayed
        /// </summary>
        public FavoriteListingsOfUserViewModel Favorites
        {
            get { return this.favoriteListingsOfUserViewModel; }
        }
    }
}
