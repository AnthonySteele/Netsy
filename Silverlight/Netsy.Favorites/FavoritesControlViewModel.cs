//-----------------------------------------------------------------------
// <copyright file="FavoritesControlViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Favorites
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.UI.ViewModels;

    /// <summary>
    /// View model for the main page
    /// todo: test with mock service
    /// </summary>
    public class FavoritesControlViewModel : BaseViewModel
    {
        /// <summary>
        /// The listings displayed
        /// </summary>
        private readonly ObservableCollection<ListingViewModel> listings = new ObservableCollection<ListingViewModel>();
        
        /// <summary>
        /// The error or sucess status text to display
        /// </summary>
        private string statusMessage;
        
        /// <summary>
        /// Initializes a new instance of the FavoritesControlViewModel class
        /// </summary>
        /// <param name="loadCommand">the command to load listings</param>
        public FavoritesControlViewModel(LoadFavoritesCommand loadCommand) 
        {
            if (loadCommand == null)
            {
                throw new ArgumentNullException("loadCommand");
            }

            this.ItemsPerPage = Constants.DefaultItemsPerPage;
            this.LoadCommand = loadCommand;
        }

        /// <summary>
        /// Gets or sets the Error message text
        /// </summary>
        public string StatusMessage
        {
            get
            {
                return this.statusMessage;
            }

            set
            {
                if (this.statusMessage != value)
                {
                    this.statusMessage = value;
                    this.OnPropertyChanged("StatusMessage");
                }
            }
        }

        /// <summary>
        /// Gets the listings displayed
        /// </summary>
        public ObservableCollection<ListingViewModel> Listings
        {
            get { return this.listings; }
        }

        /// <summary>
        /// Gets or sets the user to get favoorites for
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets the number of listings per page
        /// </summary>
        public int ItemsPerPage { get; private set; }

        /// <summary>
        /// Gets the commnad to load listings
        /// </summary>
        public ICommand LoadCommand { get; private set; }
    }
}
