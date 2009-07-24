//-----------------------------------------------------------------------
// <copyright file="NetsyUsersTest.cs" company="AFS">
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
    public class NetsyUsersTest
    {
        /// <summary>
        /// how long to wait before timing out - 100 seconds
        /// </summary>
        private const int WaitTimeout = 100000;

        /// <summary>
        /// The API key to use for testing
        /// </summary>
        private const string EtsyApiKey = "rfc35bh98q3a9hvccfsxe4cc";

        /// <summary>
        /// the use if to test on
        /// </summary>
        private const int TestUserId = 7394192;

        /// <summary>
        /// Synchronisation object to wait until the user details get completes
        /// </summary>
        private AutoResetEvent userDetailsGetCompletedEvent;

        /// <summary>
        /// Synchronisation object to wait until the user search by na,e get completes
        /// </summary>
        private AutoResetEvent userByNameGetCompletedEvent;

        /// <summary>
        /// Result data from the call to get users by id
        /// </summary>
        private Users usersResultData;


        /// <summary>
        /// Result data from the call to get users by name
        /// </summary>
        private Users usersSearchResultData;


        /// <summary>
        /// Result status from the call to get users by id
        /// </summary>
        private ResultStatus usersResultStatus;

        /// <summary>
        /// Result status from the call to get users by name
        /// </summary>
        private ResultStatus userSearchResultStatus;
        
        /// <summary>
        /// Test retrieving etsy users by id
        /// </summary>
        [TestMethod]
        public void EtsyUsersLowDetailRetrievalTest()
        {
            this.userDetailsGetCompletedEvent = new AutoResetEvent(false);
            try
            {
                IUsersService etsyUsers = new UsersService(EtsyApiKey);

                this.usersResultData = null;
                etsyUsers.GetUserDetailsCompleted += this.GetUserDetailsCompleted;
                etsyUsers.GetUserDetails(TestUserId, DetailLevel.Low);

                // wait for up to 20 seconds for it to complete
                bool signalled = this.userDetailsGetCompletedEvent.WaitOne(WaitTimeout);

                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                Assert.IsNotNull(this.usersResultStatus);
                Assert.IsTrue(this.usersResultStatus.Success, "Call failed");
                Assert.IsNotNull(this.usersResultData);
                Assert.IsNotNull(this.usersResultData.Params);
                Assert.IsNotNull(this.usersResultData.Results);
                Assert.AreEqual(1, this.usersResultData.Count);
            }
            finally
            {
                this.userDetailsGetCompletedEvent = null; 
            }
        }

        /// <summary>
        /// Test retrieving etsy users by id
        /// </summary>
        [TestMethod]
        public void EtsyUsersSearchLowDetailRetrievalTest()
        {
            this.userByNameGetCompletedEvent = new AutoResetEvent(false);
            try
            {
                IUsersService etsyUsers = new UsersService(EtsyApiKey);

                this.usersSearchResultData = null;
                etsyUsers.GetUserByNameCompleted += this.GetUserByNameCompleted;
                // the etsy server should have data here - at least 3 shops with "fred" in the name
                etsyUsers.GetUsersByName("Fred", 0, 3, DetailLevel.Low);

                // wait for up to 20 seconds for it to complete
                bool signalled = this.userByNameGetCompletedEvent.WaitOne(WaitTimeout);

                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                Assert.IsNotNull(this.userSearchResultStatus);
                Assert.IsTrue(this.userSearchResultStatus.Success, "Call failed");
                Assert.IsNotNull(this.usersSearchResultData);
                Assert.IsNotNull(this.usersSearchResultData.Params);
                Assert.IsNotNull(this.usersSearchResultData.Results);

                // the etsy server should have at least 3 shops with "fred" in the name
                Assert.IsTrue(this.usersSearchResultData.Count >= 3);
            }
            finally
            {
                this.userByNameGetCompletedEvent = null;
            }
        }

        /// <summary>
        /// User details by Id Retrieve completed
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event params</param>
        private void GetUserDetailsCompleted(object sender, ResultEventArgs<Users> e)
        {
            this.usersResultData = e.ResultValue;
            this.usersResultStatus = e.ResultStatus;

            // signal that the data is retrieved, ready for testing
            this.userDetailsGetCompletedEvent.Set();
        }


        /// <summary>
        /// Users by name retrieve completed
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event params</param>
        private void GetUserByNameCompleted(object sender, ResultEventArgs<Users> e)
        {
            this.usersSearchResultData = e.ResultValue;
            this.userSearchResultStatus = e.ResultStatus;

            // signal that the data is retrieved, ready for testing
            this.userByNameGetCompletedEvent.Set();
        }
    }
}
