//-----------------------------------------------------------------------
// <copyright file="UsersTest.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.IntegrationTest
{
    using System.Threading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Netsy.Core;
    using Netsy.DataModel;
    using Netsy.DataModel.UserData;
    using Netsy.Helpers;
    using Netsy.Interfaces;

    /// <summary>
    /// Test etsy users retrieval
    /// </summary>
    [TestClass]
    public class UsersTest
    {
        /// <summary>
        /// Test retrieving etsy users by id
        /// </summary>
        [TestMethod]
        public void UsersLowDetailRetrievalTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Users> result = null;
                IUsersService etsyUsers = new UsersService(NetsyData.EtsyApiKey);
                etsyUsers.GetUserDetailsCompleted += (s, e) =>
                    {
                        result = e;
                        waitEvent.Set();
                    };

                // ACT
                etsyUsers.GetUserDetails(NetsyData.TestUserId, DetailLevel.Low);
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

                // ASSERT

                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data
                NetsyData.CheckResultSuccess(result);

                Assert.IsNotNull(result.ResultValue.Params);
                Assert.IsNotNull(result.ResultValue.Results);
                Assert.AreEqual(1, result.ResultValue.Count);
            }
        }

        /// <summary>
        /// Test retrieving etsy users by id
        /// </summary>
        [TestMethod]
        public void UsersSearchLowDetailRetrievalTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Users> result = null;
                IUsersService etsyUsers = new UsersService(NetsyData.EtsyApiKey);
                etsyUsers.GetUserByNameCompleted += (s, e) =>
                    {
                        result = e;
                        waitEvent.Set();
                    };

                // ACT
                // the etsy server should have data here - at least 3 shops with "fred" in the name
                etsyUsers.GetUsersByName("Fred", 0, 3, DetailLevel.Low);
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data
                NetsyData.CheckResultSuccess(result);

                Assert.IsNotNull(result.ResultValue.Params);
                Assert.IsNotNull(result.ResultValue.Results);

                // the etsy server should have at least 3 shops with "fred" in the name
                Assert.IsTrue(result.ResultValue.Count >= 3);
            }
        }
    }
}
