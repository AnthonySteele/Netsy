//-----------------------------------------------------------------------
// <copyright file="ShopCreateTest.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel.Test
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Netsy.DataModel.ShopData;

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
        public void ShopDetailsSimpleCreateTest()
        {
            Shop shop = new Shop();
            Assert.IsNotNull(shop);
        }

        /// <summary>
        /// Test that that shop is not on vacation by default
        /// </summary>
        [TestMethod]
        public void ShopDetailsVacationFalseByDefaultTest()
        {
            Shop shop = new Shop();
            Assert.AreEqual(0, shop.IsVacation);
            Assert.IsFalse(shop.IsVacationFlag);
        }

        /// <summary>
        /// Test that that shop is on vacation when set
        /// </summary>
        [TestMethod]
        public void ShopDetailsVacationTrueWhenSetTest()
        {
            Shop shop = new Shop();
            shop.IsVacation = 1;
            Assert.AreEqual(1, shop.IsVacation);
            Assert.IsTrue(shop.IsVacationFlag);
        }

        /// <summary>
        /// Test simple creation of a shop section
        /// </summary>
        [TestMethod]
        public void ShopSectionDetailsSimpleCreateTest()
        {
            ShopSection shopSection = new ShopSection();
            Assert.IsNotNull(shopSection);
        }
    }
}
