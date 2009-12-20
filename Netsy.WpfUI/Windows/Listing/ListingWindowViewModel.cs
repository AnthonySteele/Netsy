//-----------------------------------------------------------------------
// <copyright file="ListingWindowViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.WpfUI.Windows.Listing
{
    using Netsy.UI.ViewModels;

    /// <summary>
    /// View model for the window to shw a listing
    /// </summary>
    public class ListingWindowViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets or sets the identifier of the listing
        /// </summary>
        public int ListingId { get; set; }
    }
}
