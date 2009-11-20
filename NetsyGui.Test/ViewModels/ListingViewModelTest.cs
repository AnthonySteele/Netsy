//-----------------------------------------------------------------------
// <copyright file="ListingViewModelTest.cs" company="AFS">
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
    /// Tests on the listing view models
    /// </summary>
    [TestClass]
    public class ListingViewModelTest
    {
        /// <summary>
        /// Test creating a Listing view model
        /// </summary>
        [TestMethod]
        public void CreateTest()
        {
            ListingViewModel viewModel = new ListingViewModel(new Listing());
            
            Assert.IsNotNull(viewModel);
        }
    }
}
