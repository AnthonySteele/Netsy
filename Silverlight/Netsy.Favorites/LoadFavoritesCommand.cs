//-----------------------------------------------------------------------
// <copyright file="LoadFavoritesCommand.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Favorites
{
    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.UI.Commands;
    using Netsy.UI.ViewModels;

    /// <summary>
    /// Command to load favorites 
    /// </summary>
    public class LoadFavoritesCommand : GenericCommandBase<FavoritesControlViewModel>
    {
        /// <summary>
        /// The service to get favorites
        /// </summary>
        private readonly IFavoritesService favoritesService;

        /// <summary>
        /// the view modeol being acted on 
        /// </summary>
        private FavoritesControlViewModel currentViewModel;

        /// <summary>
        /// Initializes a new instance of the LoadFavoritesCommand class
        /// </summary>
        /// <param name="favoritesService">the service to load from</param>
        public LoadFavoritesCommand(IFavoritesService favoritesService)
        {
            this.favoritesService = favoritesService;
            this.favoritesService.GetFavoriteListingsOfUserCompleted += this.UpdateForReceivedListings;
        }

        /// <summary>
        /// Execute the command with a ViewModel as parameter
        /// </summary>
        /// <param name="value">the view model</param>
        public override void ExecuteOnValue(FavoritesControlViewModel value)
        {
            this.currentViewModel = value;

            if (string.IsNullOrEmpty(value.UserId))
            {
                value.StatusMessage = "No user id for favorites";
                return;
            }

            // todo: put tests on api to show wrong count returned
            this.favoritesService.GetFavoriteListingsOfUser(value.UserId, 0, value.ItemsPerPage + 1, DetailLevel.High);        
        }

        /// <summary>
        /// Event handler for when favorites have been received
        /// </summary>
        /// <param name="sender">the vent sender</param>
        /// <param name="e">the event params</param>
        private void UpdateForReceivedListings(object sender, ResultEventArgs<Listings> e)
        {
            if (!e.ResultStatus.Success)
            {
                this.currentViewModel.StatusMessage = "Error loading: " + e.ResultStatus.ErrorMessage;
                return;
            }

            this.currentViewModel.Listings.Clear();
            foreach (Listing listing in e.ResultValue.Results)
            {
                this.currentViewModel.Listings.Add(new ListingViewModel(listing));
            }

            this.currentViewModel.StatusMessage =
                this.currentViewModel.Listings.Count + " Listings favorites loaded for user " + this.currentViewModel.UserId;
        }
    }
}
