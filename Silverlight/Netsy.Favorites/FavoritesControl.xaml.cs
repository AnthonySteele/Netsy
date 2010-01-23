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
        }

        /// <summary>
        /// Move to previous page
        /// </summary>
        /// <param name="sender">the event page</param>
        /// <param name="e">the event params</param>
        private void PrevButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            FavoritesControlViewModel viewModel = this.DataContext as FavoritesControlViewModel;
            if (viewModel == null)
            {
                return;
            }

            if (viewModel.Favorites.PreviousPageCommand.CanExecute(viewModel.Favorites))
            {
                viewModel.Favorites.PreviousPageCommand.Execute(viewModel.Favorites);
            }
        }

        /// <summary>
        /// Move to next page
        /// </summary>
        /// <param name="sender">the event page</param>
        /// <param name="e">the event params</param>
        private void NextButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            FavoritesControlViewModel viewModel = this.DataContext as FavoritesControlViewModel;
            if (viewModel == null)
            {
                return;
            }

            if (viewModel.Favorites.NextPageCommand.CanExecute(viewModel.Favorites))
            {
                viewModel.Favorites.NextPageCommand.Execute(viewModel.Favorites);
            }
        }
    }
}
