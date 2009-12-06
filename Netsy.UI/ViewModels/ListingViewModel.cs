//-----------------------------------------------------------------------
// <copyright file="ListingViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.UI.ViewModels
{
    using Netsy.DataModel;

    /// <summary>
    /// View model for a listing
    /// </summary>
    public class ListingViewModel
    {
        /// <summary>
        /// the listing data transfer object
        /// </summary>
        private readonly Listing listing;

        /// <summary>
        /// Initializes a new instance of the ListingViewModel class
        /// </summary>
        /// <param name="listing">the listing Data transfer object</param>
        public ListingViewModel(Listing listing)
        {
            this.listing = listing;
        }

        /// <summary>
        /// Gets the Listing data transfer object
        /// </summary>
        public Listing Listing
        {
            get { return this.listing; }
        }
    }
}
