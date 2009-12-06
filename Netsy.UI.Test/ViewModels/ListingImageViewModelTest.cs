//-----------------------------------------------------------------------
// <copyright file="ListingImageViewModelTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.UI.Test.ViewModels
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.DataModel;
    using Netsy.UI.ViewModels;

    /// <summary>
    /// Tests on the ListingImageViewModel class
    /// </summary>
    [TestClass]
    public class ListingImageViewModelTest
    {
        /// <summary>
        /// Test creating the view model
        /// </summary>
        [TestMethod]
        public void ListingImageViewModelCreateTest()
        {
            ListingImage listingImage = new ListingImage();
            ListingImageViewModel listingImageViewModel = new ListingImageViewModel(listingImage);

            Assert.IsNotNull(listingImageViewModel);
            Assert.IsNotNull(listingImageViewModel.ListingImage);
        }
    }
}
