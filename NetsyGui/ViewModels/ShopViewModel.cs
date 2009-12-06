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
        /// the full URL to the shops's banner image.
        /// </summary>
        private string bannerImageUrl;

        /// <summary>
        /// last updated date
        /// </summary>
        private DateTime? lastUpdated;

        /// <summary>
        /// Creation date
        /// </summary>
        private DateTime? creationDate;

        /// <summary>
        /// The number of active listings in the shop.
        /// </summary>
        private int ListingCount;

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
        /// the list of shop-section objects
        /// </summary>
        private readonly ObservableCollection<ShopSection> sections = new ObservableCollection<ShopSection>();

        /// <summary>
        /// Initializes a new instance of the ShopViewModel class
        /// </summary>
        /// <param name="shop">the shop data to read</param>
        public ShopViewModel(Shop shop)
        {
            this.CreationDate = shop.CreationDate;
            this.BannerImageUrl = shop.BannerImageUrl;
            this.Name = shop.ShopName;
            this.Title = shop.Title;

            this.SaleMessage = shop.SaleMessage;
            this.Announcement = shop.Announcement;
            this.IsVacation = shop.IsVacationFlag;
            this.VacationMessage = shop.VacationMessage;
            this.CurrencyCode = shop.CurrencyCode;

            this.PolicyWelcome = shop.PolicyWelcome;
            this.PolicyPayment = shop.PolicyPayment;
            this.PolicyShipping = shop.PolicyShipping;
            this.PolicyRefunds = shop.PolicyRefunds;
            this.PolicyAdditional = shop.PolicyAdditional;
        }

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
                if (this.creationDate != value)
                {
                    this.creationDate = value;
                    this.OnPropertyChanged("CreationDate");
                }
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
                if (this.bannerImageUrl != value)
                {
                    this.bannerImageUrl = value;
                    this.OnPropertyChanged("BannerImageUrl");
                }
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
                if (this.name != value)
                {
                    this.name = value;
                    this.OnPropertyChanged("Name");
                } 
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
                if (this.title != value)
                {
                    this.title = value;
                    this.OnPropertyChanged("Title");
                }
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
                if (this.saleMessage != value)
                {
                    this.saleMessage = value;
                    this.OnPropertyChanged("SaleMessage");
                }
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
                if (this.announcement != value)
                {
                    this.announcement = value;
                    this.OnPropertyChanged("Announcement");
                }
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
                if (this.isVacation != value)
                {
                    this.isVacation = value;
                    this.OnPropertyChanged("IsVacation");
                }
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
                if (this.vacationMessage != value)
                {
                    this.vacationMessage = value;
                    this.OnPropertyChanged("VacationMessage");
                }
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
                if (this.currencyCode != value)
                {
                    this.currencyCode = value;
                    this.OnPropertyChanged("CurrencyCode");
                }
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
                if (this.policyWelcome != value)
                {
                    this.policyWelcome = value;
                    this.OnPropertyChanged("PolicyWelcome");
                }
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
                if (this.policyPayment != value)
                {
                    this.policyPayment = value;
                    this.OnPropertyChanged("PolicyPayment");
                }
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
                if (this.policyShipping != value)
                {
                    this.policyShipping = value;
                    this.OnPropertyChanged("PolicyShipping");
                }
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
                if (this.policyRefunds != value)
                {
                    this.policyRefunds = value;
                    this.OnPropertyChanged("PolicyRefunds");
                }
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
                if (this.policyAdditional != value)
                {
                    this.policyAdditional = value;
                    this.OnPropertyChanged("PolicyAdditional");
                }
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
