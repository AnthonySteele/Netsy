//-----------------------------------------------------------------------
// <copyright file="ListingWindowViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.WpfUI.Windows.Listing
{
    using System.Windows.Input;

    using Netsy.DataModel;
    using Netsy.UI.ViewModels;

    /// <summary>
    /// View model for the window to show a listing
    /// </summary>
    public class ListingWindowViewModel : BaseViewModel
    {
        /// <summary>
        /// The text to display on the status bar
        /// </summary>
        private string statusText;

        /// <summary>
        /// Initializes a new instance of the ListingWindowViewModel class 
        /// </summary>
        /// <param name="loadListingCommand">the command to load the listing</param>
        public ListingWindowViewModel(ListingWindowLoadListingCommand loadListingCommand)
        {
            this.LoadListingCommand = loadListingCommand;
        }

        /// <summary>
        /// Gets or sets the identifier of the listing
        /// </summary>
        public int ListingId { get; set; }

        /// <summary>
        /// Gets or sets the listing shown
        /// </summary>
        public Listing Listing { get; set; }

        /// <summary>
        /// Gets or sets the commnad to load the listing
        /// </summary>
        public ICommand LoadListingCommand { get; set; }

        /// <summary>
        /// Gets or sets the status bar text
        /// </summary>
        public string StatusText
        {
            get
            {
                return this.statusText;
            }

            set
            {
                if (this.statusText != value)
                {
                    this.statusText = value;
                    this.OnPropertyChanged("StatusText");
                }
            }
        }
    }
}
