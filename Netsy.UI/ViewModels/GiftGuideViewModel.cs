//-----------------------------------------------------------------------
// <copyright file="GiftGuideViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.UI.ViewModels
{
    using Netsy.DataModel;

    /// <summary>
    /// View model for a gift guide
    /// </summary>
    public class GiftGuideViewModel
    {
        /// <summary>
        /// the gift guide data transfer object
        /// </summary>
        private readonly GiftGuide giftGuide;

        /// <summary>
        /// Initializes a new instance of the GiftGuideViewModel class
        /// </summary>
        /// <param name="giftGuide">the gift guide Data transfer object</param>
        public GiftGuideViewModel(GiftGuide giftGuide)
        {
            this.giftGuide = giftGuide;
        }

        /// <summary>
        /// Gets the gift guide data transfer object
        /// </summary>
        public GiftGuide GiftGuide
        {
            get { return this.giftGuide; }
        }
    }
}
