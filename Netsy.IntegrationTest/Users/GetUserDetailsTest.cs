//-----------------------------------------------------------------------
// <copyright file="GetUserDetailsTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.IntegrationTest.Users
{
    using System.Net;
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
    public class GetUserDetailsTest
    {
        /// <summary>
        /// Test the GetUserDetails funciton on the users service
        /// </summary>
        [TestMethod]
        public void GetUserDetailsApiKeyMissingTest()
        {
            ResultEventArgs<Users> result = null;
            IUsersService etsyUsers = new UsersService(new EtsyContext(string.Empty));
            etsyUsers.GetUserDetailsCompleted += (s, e) => result = e;

            // ACT
            etsyUsers.GetUserDetails(NetsyData.TestUserId, DetailLevel.Low);

            // check the data
            NetsyData.CheckResultFailure(result);
        }

        /// <summary>
        /// test what happens with an invalid api key
        /// </summary>
        [TestMethod]
        public void GetUserDetailsApiKeyFailTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Users> result = null;
                IUsersService etsyUsers = new UsersService(new EtsyContext("InvalidKey"));
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

                // check the data - should fail
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.ResultStatus);
                Assert.IsFalse(result.ResultStatus.Success);
                Assert.AreEqual(WebExceptionStatus.ProtocolError, result.ResultStatus.WebStatus);
            }
        }

        /// <summary>
        /// Test retrieving etsy users by id, low detail
        /// </summary>
        [TestMethod]
        public void GetUserDetailsLowDetailRetrievalTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Users> result = null;
                IUsersService etsyUsers = new UsersService(new EtsyContext(NetsyData.EtsyApiKey));
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
        /// Test retrieving etsy users by id, all detail levels
        /// </summary>
        [TestMethod]
        public void GetUserDetailsAllDetailRetrievalTest()
        {
            TestGetUserDetails(DetailLevel.Low);
            TestGetUserDetails(DetailLevel.Medium);
            TestGetUserDetails(DetailLevel.High);
        }

        /// <summary>
        /// Test getting users with the given detail level
        /// </summary>
        /// <param name="detailLevel">the detail level to use</param>
        private static void TestGetUserDetails(DetailLevel detailLevel)
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Users> result = null;
                IUsersService etsyUsers = new UsersService(new EtsyContext(NetsyData.EtsyApiKey));
                etsyUsers.GetUserDetailsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                etsyUsers.GetUserDetails(NetsyData.TestUserId, detailLevel);
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
    }
}
