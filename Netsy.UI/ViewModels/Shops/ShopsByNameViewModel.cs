//-----------------------------------------------------------------------
// <copyright file="ShopsByNameViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.UI.ViewModels.Shops
{
    using System.Globalization;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.UI.Commands;

    /// <summary>
    /// View model for a collection of shops from the shops by name service
    /// </summary>
    public class ShopsByNameViewModel : ShopsServiceViewModel
    {
        /// <summary>
        /// The shop name to search for
        /// </summary>
        private string name;

        /// <summary>
        /// Initializes a new instance of the ShopsByNameViewModel class.
        /// </summary>
        /// <param name="shopsService">the shopsService service</param>
        public ShopsByNameViewModel(IShopService shopsService)
            : base(shopsService)
        {
            this.ShopService.GetShopsByNameCompleted += this.ShopsReceived;
            this.MakeCommands();
        }

        /// <summary>
        /// Gets or sets the shop name to search for
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (this.Name != value)
                {
                    this.name = value;
                    this.OnPropertyChanged("Name");                    
                }
            }
        }

        /// <summary>
        /// Show the success message
        /// </summary>
        protected override void ShowLoadedSuccessMessage()
        {
            string status = string.Format(CultureInfo.InvariantCulture, "Loaded {0} shops by keyword on page {1}", this.Items.Count, this.PageNumber);
            this.StatusText = status;
        }

        /// <summary>
        /// Create the load command 
        /// </summary>
        private void MakeCommands()
        {
            this.LoadPageCommand = new DelegateCommand<ShopViewModel>(
                item =>
                {
                    if (this.Name.IsNullEmptyOrWhiteSpace())
                    {
                        this.StatusText = "Enter the shop name";
                        return;
                    }

                    int offset = (this.PageNumber - 1) * this.ItemsPerPage;

                    this.ShopService.GetShopsByName(this.Name.Trim(), SortOrder.Down, offset, this.ItemsPerPage, DetailLevel.Medium);
                    string status = string.Format(CultureInfo.InvariantCulture, "Getting {0} shops by keyword on page {1}", this.ItemsPerPage, this.PageNumber);
                    this.StatusText = status;
                });
        }
    }
}