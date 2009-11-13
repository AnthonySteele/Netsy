//-----------------------------------------------------------------------
// <copyright file="ListingCreateTest.cs" company="AFS">
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
    /// Test creating the listing
    /// </summary>
    [TestClass]
    public class ListingCreateTest
    {
        /// <summary>
        /// Test simple creation of a listing
        /// </summary>
        [TestMethod]
        public void ListingSimpleCreateTest()
        {
            Listing listing = new Listing();
            Assert.IsNotNull(listing);
        }

        /// <summary>
        /// Test that the listing has no CreationDate by default
        /// </summary>
        [TestMethod]
        public void ListingDatesNullByDefaultTest()
        {
            Listing shop = new Listing();

            Assert.IsTrue(string.IsNullOrEmpty(shop.CreationEpoch));
            Assert.IsFalse(shop.CreationDate.HasValue);

            Assert.IsTrue(string.IsNullOrEmpty(shop.FavoriteCreationEpoch));
            Assert.IsFalse(shop.FavoriteCreationDate.HasValue);
            
            Assert.IsTrue(string.IsNullOrEmpty(shop.EndingEpoch));
            Assert.IsFalse(shop.EndingDate.HasValue);
        }

        /// <summary>
        /// Test setting the user status to public
        /// </summary>
        [TestMethod]
        public void ListingSetStateActiveTest()
        {
            Listing listing = new Listing();
            listing.State = "active";

            Assert.AreEqual(ListingState.Active, listing.StateEnum);
        }

        /// <summary>
        /// Test setting the user status to private
        /// </summary>
        [TestMethod]
        public void ListingSetStateExpiredTest()
        {
            Listing listing = new Listing();
            listing.State = "expired";

            Assert.AreEqual(ListingState.Expired, listing.StateEnum);
        }

        /// <summary>
        /// Test setting the user status to a bad value
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ListingSetStateBadValueTest()
        {
            Listing listing = new Listing();
            listing.State = "goofy";
        }

        /// <summary>
        /// Test setting creation epoch
        /// </summary>
        [TestMethod]
        public void CreationEpochTest()
        {
            Listing listing = new Listing();
            listing.CreationEpoch = "1";

            Assert.AreEqual("1", listing.CreationEpoch);
            Helper.AssertDateIs(listing.CreationDate.Value, 1970, 1, 1, 0, 0, 1);
        }

        /// <summary>
        /// Test that the creation date can be set to a null date
        /// </summary>
        [TestMethod]
        public void ListingEpochNullTest()
        {
            Listing listing = new Listing();
            listing.CreationEpoch = string.Empty;

            Assert.IsFalse(listing.CreationDate.HasValue);
        }

        /// <summary>
        /// Test setting creation date
        /// </summary>
        [TestMethod]
        public void CreationDateTest()
        {
            Listing listing = new Listing();
            listing.CreationDate = new DateTime(1970, 1, 1);

            Assert.AreEqual(new DateTime(1970, 1, 1), listing.CreationDate);
            Assert.AreEqual("0", listing.CreationEpoch);
        }

        /// <summary>
        /// Test setting creation epoch
        /// </summary>
        [TestMethod]
        public void EndingEpochTest()
        {
            Listing listing = new Listing();
            listing.EndingEpoch = "1";

            Assert.AreEqual("1", listing.EndingEpoch);
            Helper.AssertDateIs(listing.EndingDate.Value, 1970, 1, 1, 0, 0, 1);
        }

        /// <summary>
        /// Test that the ending date can be set to a null date
        /// </summary>
        [TestMethod]
        public void ListingEndingEpochNullTest()
        {
            Listing listing = new Listing();
            listing.EndingEpoch = string.Empty;

            Assert.IsFalse(listing.EndingDate.HasValue);
        }

        /// <summary>
        /// Test setting creation date
        /// </summary>
        [TestMethod]
        public void EndingDateTest()
        {
            Listing listing = new Listing();
            listing.EndingDate = new DateTime(1970, 1, 1);

            Assert.AreEqual(new DateTime(1970, 1, 1), listing.EndingDate);
            Assert.AreEqual("0", listing.EndingEpoch);
        }

        /// <summary>
        /// Test setting creation epoch
        /// </summary>
        [TestMethod]
        public void FavoriteCreationEpochTest()
        {
            Listing listing = new Listing();
            listing.FavoriteCreationEpoch = "1";

            Assert.AreEqual("1", listing.FavoriteCreationEpoch);
            Helper.AssertDateIs(listing.FavoriteCreationDate.Value, 1970, 1, 1, 0, 0, 1);
        }

        /// <summary>
        /// Test that the ending date can be set to a null date
        /// </summary>
        [TestMethod]
        public void ListingFavoriteCreationEpochNullTest()
        {
            Listing listing = new Listing();
            listing.FavoriteCreationEpoch = string.Empty;

            Assert.IsFalse(listing.FavoriteCreationDate.HasValue);
        }

        /// <summary>
        /// Test setting creation date
        /// </summary>
        [TestMethod]
        public void FavoriteCreationDateTest()
        {
            Listing listing = new Listing();
            listing.FavoriteCreationDate = new DateTime(1970, 1, 1);

            Assert.AreEqual(new DateTime(1970, 1, 1), listing.FavoriteCreationDate);
            Assert.AreEqual("0", listing.FavoriteCreationEpoch);
        }
    }
}
