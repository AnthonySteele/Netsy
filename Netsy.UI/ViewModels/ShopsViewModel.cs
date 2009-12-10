//-----------------------------------------------------------------------
// <copyright file="ShopsViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.UI.ViewModels
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// View model for Front listings
    /// </summary>
    public class ShopsViewModel : BaseViewModel
    {
        /// <summary>
        /// the listings shown on the gui
        /// </summary>
        private readonly ObservableCollection<ShopViewModel> items = new ObservableCollection<ShopViewModel>();

        /// <summary>
        /// Number of items to retrieve
        /// </summary>
        private int itemsPerPage = Constants.DefaultItemsPerPage;

        /// <summary>
        /// the page index into the results
        /// </summary>
        private int pageNumber = 1;

        /// <summary>
        /// Gets the Items 
        /// </summary>
        public ObservableCollection<ShopViewModel> Items
        {
            get { return this.items; }
        }

        /// <summary>
        /// Gets or sets the page index into the results
        /// </summary>
        public int PageNumber
        {
            get
            {
                return this.pageNumber;
            }

            set
            {
                if (this.pageNumber != value)
                {
                    this.pageNumber = value;
                    this.OnPropertyChanged("PageNumber");
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of items to retrieve
        /// </summary>
        public int ItemsPerPage
        {
            get
            {
                return this.itemsPerPage;
            }

            set
            {
                if (this.itemsPerPage != value)
                {
                    this.itemsPerPage = value;
                    this.OnPropertyChanged("ItemsPerPage");
                }
            }
        }
    }
}
