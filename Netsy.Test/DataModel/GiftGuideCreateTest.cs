//-----------------------------------------------------------------------
// <copyright file="GiftGuideTest.cs" company="AFS">
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
    /// Test creating gift guides
    /// </summary>
    [TestClass]
    public class GiftGuideTest
    {
        /// <summary>
        /// Test simple creation
        /// </summary>
        [TestMethod]
        public void GiftGuideSimpleCreateTest()
        {
            GiftGuide giftGuide = new GiftGuide();
            Assert.IsNotNull(giftGuide);
        }

        /// <summary>
        /// Test that the gift guide has no CreationDate by default
        /// </summary>
        [TestMethod]
        public void GiftGuideDatesNullByDefaultTest()
        {
            GiftGuide giftGuide = new GiftGuide();

            Assert.IsFalse(giftGuide.CreationEpoch.HasValue);
            Assert.IsFalse(giftGuide.CreationDate.HasValue);
        }

        /// <summary>
        /// Test setting creation epoch
        /// </summary>
        [TestMethod]
        public void GiftGuideCreationEpochTest()
        {
            GiftGuide giftGuide = new GiftGuide();
            giftGuide.CreationEpoch = 1;

            Assert.AreEqual(1, giftGuide.CreationEpoch);
            Helper.AssertDateIs(giftGuide.CreationDate.Value, 1970, 1, 1, 0, 0, 1);
        }

        /// <summary>
        /// Test that the creation date can be set to a null date
        /// </summary>
        [TestMethod]
        public void GiftGuideEpochNullTest()
        {
            GiftGuide giftGuide = new GiftGuide();
            giftGuide.CreationEpoch = null;

            Assert.IsFalse(giftGuide.CreationDate.HasValue);
        }

        /// <summary>
        /// Test setting creation date
        /// </summary>
        [TestMethod]
        public void GiftGuideCreationDateTest()
        {
            GiftGuide giftGuide = new GiftGuide();
            giftGuide.CreationDate = new DateTime(1970, 1, 1);

            Assert.AreEqual(new DateTime(1970, 1, 1), giftGuide.CreationDate);
            Assert.AreEqual(0, giftGuide.CreationEpoch);
        }
    }
}
