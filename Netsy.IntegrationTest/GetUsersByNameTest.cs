//-----------------------------------------------------------------------
// <copyright file="GetUsersByNameTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.IntegrationTest
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
    public class GetUsersByNameTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetUsersByNameApiKeyMissingTest()
        {
            ResultEventArgs<Users> result = null;
            IUsersService etsyUsers = new UsersService(new EtsyContext(string.Empty));
            etsyUsers.GetUsersByNameCompleted += (s, e) => result = e;

            // ACT
            etsyUsers.GetUsersByName("Fred", 0, 3, DetailLevel.Low);

            // check the data
            NetsyData.CheckResultFailure(result);
        }

        /// <summary>
        /// Test invalid API key
        /// </summary>
        [TestMethod]
        public void GetUsersByNameApiKeyInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Users> result = null;
                IUsersService etsyUsers = new UsersService(new EtsyContext("InvalidKey"));
                etsyUsers.GetUsersByNameCompleted += (s, e) =>
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

                // check the data - should fail
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.ResultStatus);
                Assert.IsFalse(result.ResultStatus.Success);
                Assert.AreEqual(WebExceptionStatus.ProtocolError, result.ResultStatus.WebStatus);
            }
        }

        /// <summary>
        /// Test retrieving etsy users by id
        /// </summary>
        [TestMethod]
        public void GetUsersByNameLowDetailRetrievalTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Users> result = null;
                IUsersService etsyUsers = new UsersService(new EtsyContext(NetsyData.EtsyApiKey));
                etsyUsers.GetUsersByNameCompleted += (s, e) =>
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

        /// <summary>
        /// Test retrieving etsy users by id, all detail levels
        /// </summary>
        [TestMethod]
        public void GetUsersByNameAllDetailLevelsRetrievalTest()
        {
            TestGetUsersByName(DetailLevel.Low);
            TestGetUsersByName(DetailLevel.Medium);
            TestGetUsersByName(DetailLevel.High);
        }

        /// <summary>
        /// Test getting users by name with the given detail level
        /// </summary>
        /// <param name="detailLevel">the detail level to use</param>
        private static void TestGetUsersByName(DetailLevel detailLevel)
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Users> result = null;
                IUsersService etsyUsers = new UsersService(new EtsyContext(NetsyData.EtsyApiKey));
                etsyUsers.GetUsersByNameCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                // the etsy server should have data here - at least 3 shops with "fred" in the name
                etsyUsers.GetUsersByName("Fred", 0, 3, detailLevel);
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
