//-----------------------------------------------------------------------
// <copyright file="MaterialsListingsViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.UI.ViewModels.Listings
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows.Threading;

    using Netsy.DataModel;
    using Netsy.Interfaces;
    using Netsy.UI.Commands;

    /// <summary>
    /// View model for a collection of listings from the istings by materials service
    /// </summary>
    public class MaterialsListingsViewModel : ListingsServiceViewModel
    {
        /// <summary>
        /// The materials to match
        /// </summary>
        private string materials;
        
        /// <summary>
        /// Initializes a new instance of the MaterialsListingsViewModel class.
        /// </summary>
        /// <param name="listingsService">the listings service</param>
        /// <param name="dispatcher">the thread dispatcher</param>
        public MaterialsListingsViewModel(IListingsService listingsService, Dispatcher dispatcher)
            : base(listingsService, dispatcher)
        {
            this.ListingsService.GetListingsByMaterialsCompleted += this.ListingsReceived;
            this.MakeCommands();
        }

        /// <summary>
        /// Gets or sets the Materials to match
        /// </summary>
        public string Materials
        {
            get
            {
                return this.materials;
            }

            set
            {
                if (this.materials != value)
                {
                    this.materials = value;
                    this.OnPropertyChanged("Materials");
                }
            }
        }

        /// <summary>
        /// Show the success message
        /// </summary>
        protected override void ShowLoadedSuccessMessage()
        {
            string status = string.Format(CultureInfo.InvariantCulture, "Loaded {0} listings by materials on page {1}", this.Items.Count, this.PageNumber);
            this.StatusText = status;
        }

        /// <summary>
        /// Create the load command 
        /// </summary>
        private void MakeCommands()
        {
            this.LoadPageCommand = new DelegateCommand<ListingViewModel>(
                item =>
                {
                    if (string.IsNullOrEmpty(this.Materials))
                    {
                        this.StatusText = "Enter the materials";
                        return;
                    } 

                    int offset = (this.PageNumber - 1) * this.ItemsPerPage;
                    IEnumerable<string> materialsArray = this.Materials.ToEnumerable();

                    this.ListingsService.GetListingsByMaterials(materialsArray, SortField.Score, SortOrder.Down, offset, this.ItemsPerPage, DetailLevel.Medium);
                    string status = string.Format(CultureInfo.InvariantCulture, "Getting {0} listings by materials on page {1}", this.ItemsPerPage, this.PageNumber);
                    this.StatusText = status;
                });
        }
    }
}
