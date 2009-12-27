//-----------------------------------------------------------------------
// <copyright file="GiftGuideViewModelTest.cs" company="AFS">
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
    /// Tests on the GiftGuideViewModel class
    /// </summary>
    [TestClass]
    public class GiftGuideViewModelTest
    {
        /// <summary>
        /// Test creating the view model
        /// </summary>
        [TestMethod]
        public void GiftGuideViewModelCreateTest()
        {
            GiftGuide giftGuide = new GiftGuide();
            GiftGuideViewModel giftGuideViewModel = new GiftGuideViewModel(giftGuide);

            Assert.IsNotNull(giftGuideViewModel);
            Assert.IsNotNull(giftGuideViewModel.GiftGuide);
        }
    }
}
