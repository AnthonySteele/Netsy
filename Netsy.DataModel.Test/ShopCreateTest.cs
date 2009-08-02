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
            Assert.AreEqual(0, shop.IsVacation);
            Assert.IsFalse(shop.IsVacationFlag);
        }

        /// <summary>
        /// Test that that shop is on vacation when set
        /// </summary>
        [TestMethod]
        public void ShopVacationTrueWhenSetTest()
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
