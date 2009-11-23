//-----------------------------------------------------------------------
// <copyright file="ShopViewModelTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace NetsyGui.Test.ViewModels
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.DataModel;
    using NetsyGui.ViewModels;

    /// <summary>
    /// Test the  ShopViewModel
    /// </summary>
    [TestClass]
    public class ShopViewModelTest
    {
        /// <summary>
        /// Test creating a Listing view model
        /// </summary>
        [TestMethod]
        public void ShopViewModelCreateTest()
        {
            Shop shop = new Shop();
            ShopViewModel viewModel = new ShopViewModel(shop);

            Assert.IsNotNull(viewModel);
        }
    }
}
