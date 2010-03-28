//-----------------------------------------------------------------------
// <copyright file="ShopViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.UI.ViewModels
{
    using System.Globalization;
    using System.Windows;
    using System.Windows.Input;

    using Commands;

    using Netsy.DataModel;
    using Netsy.Helpers;

    /// <summary>
    /// View model for a shop
    /// </summary>
    public class ShopViewModel : BaseViewModel
    {
        /// <summary>
        /// the listing data transfer object
        /// </summary>
        private readonly Shop shop;

        /// <summary>
        /// Initializes a new instance of the ShopViewModel class
        /// </summary>
        /// <param name="shop">the shop Data transfer object</param>
        public ShopViewModel(Shop shop)
        {
            this.shop = shop;

            this.WebLinkClickCommand = new HyperlinkNavigateCommand();
        }

        /// <summary>
        /// Gets the shop data transfer object
        /// </summary>
        public Shop Shop
        {
            get { return this.shop; }
        }

        /// <summary>
        /// Gets data data for display
        /// </summary>
        public string DateDisplay
        {
            get
            {
                if (this.Shop.CreationDate.HasValue)
                {
                    return this.Shop.CreationDate.Value.ToShortDateString();
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the image url for the largest image
        /// </summary>
        public string LargestUserImageUrl
        {
            get
            {
                if (Shop.ImageUrl75X75.HasContent())
                {
                    return Shop.ImageUrl75X75;
                }

                if (Shop.ImageUrl50X50.HasContent())
                {
                    return Shop.ImageUrl50X50;
                }

                if (Shop.ImageUrl30X30.HasContent())
                {
                    return Shop.ImageUrl30X30;
                }

                if (Shop.ImageUrl25X25.HasContent())
                {
                    return Shop.ImageUrl25X25;
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets data data for display
        /// </summary>
        public string ListingsCountDisplay
        {
            get
            {
                int? listingCount = this.Shop.ListingCountInt;
                if (listingCount.HasValue)
                {
                    switch (listingCount.Value)
                    {
                        case 0: return "No listings";
                        case 1: return "1 listing";
                        default: return string.Format(CultureInfo.InvariantCulture, "{0} listings", listingCount.Value);
                    }
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the shop has a sale message
        /// </summary>
        public Visibility SaleMessageVisibility
        {
            get
            {
                return this.Shop.SaleMessage.HasContent().ToVisibility();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the shop has an announcement
        /// </summary>
        public Visibility AnnouncementVisibility
        {
            get
            {
                return this.Shop.Announcement.HasContent().ToVisibility();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the shop has a vacation message
        /// </summary>
        public Visibility VacationMessageVisibility
        {
            get
            {
                return this.Shop.VacationMessage.HasContent().ToVisibility();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the shop has a policy welcome messsage
        /// </summary>
        public Visibility PolicyWelcomeVisibility
        {
            get
            {
                return this.Shop.PolicyWelcome.HasContent().ToVisibility();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the shop has a policy payments message
        /// </summary>
        public Visibility PolicyPaymentVisibility
        {
            get
            {
                return this.Shop.PolicyPayment.HasContent().ToVisibility();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the shop has a policy shipping message
        /// </summary>
        public Visibility PolicyShippingVisibility
        {
            get
            {
                return this.Shop.PolicyShipping.HasContent().ToVisibility();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the shop has a policy refunds message
        /// </summary>
        public Visibility PolicyRefundsVisibility
        {
            get
            {
                return this.Shop.PolicyRefunds.HasContent().ToVisibility();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the shop has a policy additional message
        /// </summary>
        public Visibility PolicyAdditionalVisibility
        {
            get
            {
                return this.Shop.PolicyAdditional.HasContent().ToVisibility();
            }
        }

        /// <summary>
        /// Gets or sets the command to show the shop in a seperate display
        /// </summary>
        public ICommand ShowShopCommand { get; set; }

        /// <summary>
        /// Gets the command to show the listing in a web browser
        /// </summary>
        public ICommand WebLinkClickCommand { get; private set; }
    }
}
