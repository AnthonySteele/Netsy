//-----------------------------------------------------------------------
// <copyright file="FavoritesControl.xaml.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Favorites
{
    using System.Windows.Controls;

    using Netsy.DataModel;
    using Netsy.Interfaces;
    using Netsy.Services;

    /// <summary>
    /// Display Favourites
    /// </summary>
    public partial class FavoritesControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the FavoritesControl class
        /// </summary>
        public FavoritesControl()
        {
            InitializeComponent();

            IFavoritesService favoritesService = new FavoritesService(new EtsyContext(Constants.EtsyApiKey));
 
            FavoritesControlViewModel viewModel = new FavoritesControlViewModel(favoritesService, this.Dispatcher);
            this.DataContext = viewModel;

            viewModel.UserId = "5007275";
            viewModel.BeginGetFavorites();
        }

    }
}
