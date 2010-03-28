//-----------------------------------------------------------------------
// <copyright file="ListingImageViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.UI.ViewModels
{
    using Netsy.DataModel;

    /// <summary>
    /// View model for an image in a listing
    /// </summary>
    public class ListingImageViewModel : BaseViewModel
    {
        /// <summary>
        /// the listing data transfer object
        /// </summary>
        private readonly ListingImage listingImage;

        /// <summary>
        /// Initializes a new instance of the ListingImageViewModel class
        /// </summary>
        /// <param name="listingImage">the listing image Data transfer object</param>
        public ListingImageViewModel(ListingImage listingImage)
        {
            this.listingImage = listingImage;
        }

        /// <summary>
        /// Gets the gift guide data transfer object
        /// </summary>
        public ListingImage ListingImage
        {
            get { return this.listingImage; }
        }
    }
}
