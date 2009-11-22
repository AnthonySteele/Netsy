//-----------------------------------------------------------------------
// <copyright file="ShopViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace NetsyGui.ViewModels
{
    using System;
    using System.Collections.ObjectModel;

    using Netsy.DataModel;

    /// <summary>
    /// View model for an Etsy shop
    /// </summary>
    public class ShopViewModel : BaseViewModel
    {
        /// <summary>
        /// the list of shop-section objects
        /// </summary>
        private readonly ObservableCollection<ShopSection> sections = new ObservableCollection<ShopSection>();

        /// <summary>
        /// Creation date
        /// </summary>
        private DateTime? creationDate;

        /// <summary>
        /// the full URL to the shops's banner image.
        /// </summary>
        private string bannerImageUrl;

        /// <summary>
        /// The shop name
        /// </summary>
        private string name;

        /// <summary>
        /// a brief heading for the shop's main page.
        /// </summary>
        private string title;

        /// <summary>
        /// a message that is sent to users who buy from this shop.
        /// </summary>
        private string saleMessage;

        /// <summary>
        /// an announcement to buyers that displays on the shop's homepage.
        /// </summary>
        private string announcement;

        /// <summary>
        /// Is the shop on vacation
        /// </summary>
        private bool isVacation;

        /// <summary>
        /// the vacation message
        /// </summary>
        private string vacationMessage;

        /// <summary>
        /// The shop's currency
        /// </summary>
        private string currencyCode;

        /// <summary>
        /// The the introductory text from the top of the seller's policies page (may be blank).
        /// </summary>
        private string policyWelcome;

        /// <summary>
        /// the seller's policy on payment (may be blank).
        /// </summary>
        private string policyPayment;

        /// <summary>
        /// the seller's policy on shipping (may be blank).
        /// </summary>
        private string policyShipping;

        /// <summary>
        /// the seller's policy on refunds (may be blank).
        /// </summary>
        private string policyRefunds;

        /// <summary>
        /// any additional policy information the seller provides (may be blank).
        /// </summary>
        private string policyAdditional;

        /// <summary>
        /// Gets or sets the Creation date
        /// </summary>
        public DateTime? CreationDate
        {
            get
            {
                return this.creationDate;
            }

            set
            {
                this.creationDate = value;
            }
        }

        /// <summary>
        /// Gets or sets the full URL to the shops's banner image.
        /// </summary>
        public string BannerImageUrl
        {
            get
            {
                return this.bannerImageUrl;
            }

            set
            {
                this.bannerImageUrl = value;
            }
        }

        /// <summary>
        /// Gets or sets the shop name
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
            }
        }

        /// <summary>
        /// Gets or sets a brief heading for the shop's main page.
        /// </summary>
        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                this.title = value;
            }
        }

        /// <summary>
        /// Gets or sets a message that is sent to users who buy from this shop.
        /// </summary>
        public string SaleMessage
        {
            get
            {
                return this.saleMessage;
            }

            set
            {
                this.saleMessage = value;
            }
        }

        /// <summary>
        /// Gets or sets an announcement to buyers that displays on the shop's homepage.
        /// </summary>
        public string Announcement
        {
            get
            {
                return this.announcement;
            }

            set
            {
                this.announcement = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the shop on vacation
        /// </summary>
        public bool IsVacation
        {
            get
            {
                return this.isVacation;
            }

            set
            {
                this.isVacation = value;
            }
        }

        /// <summary>
        /// Gets or sets the vacation message
        /// </summary>
        public string VacationMessage
        {
            get
            {
                return this.vacationMessage;
            }

            set
            {
                this.vacationMessage = value;
            }
        }

        /// <summary>
        /// Gets or sets the shop's currency
        /// </summary>
        public string CurrencyCode
        {
            get
            {
                return this.currencyCode;
            }

            set
            {
                this.currencyCode = value;
            }
        }

        /// <summary>
        /// Gets or sets the introductory text from the top of the seller's policies page (may be blank).
        /// </summary>
        public string PolicyWelcome
        {
            get
            {
                return this.policyWelcome;
            }

            set
            {
                this.policyWelcome = value;
            }
        }

        /// <summary>
        /// Gets or sets the seller's policy on payment (may be blank).
        /// </summary>
        public string PolicyPayment
        {
            get
            {
                return this.policyPayment;
            }

            set
            {
                this.policyPayment = value;
            }
        }

        /// <summary>
        /// Gets or sets the seller's policy on shipping (may be blank).
        /// </summary>
        public string PolicyShipping
        {
            get
            {
                return this.policyShipping;
            }

            set
            {
                this.policyShipping = value;
            }
        }

        /// <summary>
        /// Gets or sets the seller's policy on refunds (may be blank).
        /// </summary>
        public string PolicyRefunds
        {
            get
            {
                return this.policyRefunds;
            }

            set
            {
                this.policyRefunds = value;
            }
        }

        /// <summary>
        /// Gets or sets any additional policy information the seller provides (may be blank).
        /// </summary>
        public string PolicyAdditional
        {
            get
            {
                return this.policyAdditional;
            }

            set
            {
                this.policyAdditional = value;
            }
        }

        /// <summary>
        /// Gets the list of shop-section objects
        /// </summary>
        public ObservableCollection<ShopSection> Sections
        {
            get
            {
                return this.sections;
            }
        }
    }
}
