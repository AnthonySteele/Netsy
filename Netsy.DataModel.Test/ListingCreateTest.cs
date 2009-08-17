//-----------------------------------------------------------------------
// <copyright file="ListingCreateTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel.Test
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Netsy.DataModel.ListingData;

    /// <summary>
    /// Test creating the listing
    /// </summary>
    [TestClass]
    public class ListingCreateTest
    {
        /// <summary>
        /// Test simple creation of a user
        /// </summary>
        [TestMethod]
        public void ListingSimpleCreateTest()
        {
            Listing listing = new Listing();
            Assert.IsNotNull(listing);
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
    }
}
