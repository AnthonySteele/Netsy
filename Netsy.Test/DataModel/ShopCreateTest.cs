//-----------------------------------------------------------------------
// <copyright file="ShopCreateTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Test.DataModel
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Netsy.DataModel;

    /// <summary>
    /// Test creating the shop details
    /// </summary>
    [TestClass]
    public class ShopCreateTest
    {
        /// <summary>
        /// Test simple creation of a shop
        /// </summary>
        [TestMethod]
        public void ShopSimpleCreateTest()
        {
            Shop shop = new Shop();
            Assert.IsNotNull(shop);
        }

        /// <summary>
        /// Test that that shop is not on vacation by default
        /// </summary>
        [TestMethod]
        public void ShopVacationFalseByDefaultTest()
        {
            Shop shop = new Shop();
            Assert.IsTrue(string.IsNullOrEmpty(shop.IsVacation));
            Assert.IsFalse(shop.IsVacationFlag);
        }

        /// <summary>
        /// Test that that shop is on vacation when set
        /// </summary>
        [TestMethod]
        public void ShopVacationTrueWhenSetTest()
        {
            Shop shop = new Shop();
            shop.IsVacation = "1";
            Assert.AreEqual("1", shop.IsVacation);
            Assert.IsTrue(shop.IsVacationFlag);
        }

        /// <summary>
        /// Test that that shop count is readable as int when set 
        /// </summary>
        [TestMethod]
        public void ShopListingCountSetTest()
        {
            Shop shop = new Shop();
            shop.ListingCount = "1";
            Assert.IsTrue(shop.ListingCountInt.HasValue);
            Assert.AreEqual(1, shop.ListingCountInt.Value);
        }

        /// <summary>
        /// Test that that shop count is not readable as int when not set 
        /// </summary>
        [TestMethod]
        public void ShopListingCountSetNullTest()
        {
            Shop shop = new Shop();
            shop.ListingCount = "null";
            Assert.IsFalse(shop.ListingCountInt.HasValue);
        }

        /// <summary>
        /// Test simple creation of a shop section
        /// </summary>
        [TestMethod]
        public void ShopSectionSimpleCreateTest()
        {
            ShopSection shopSection = new ShopSection();
            Assert.IsNotNull(shopSection);
        }

        /// <summary>
        /// Test simple creation of shops
        /// </summary>
        [TestMethod]
        public void ShopsSimpleCreateTest()
        {
            Shops shops = new Shops();
            Assert.IsNotNull(shops);
        }
    }
}
