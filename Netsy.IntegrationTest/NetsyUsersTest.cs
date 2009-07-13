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
        /// Synchronisation object to wait until the suceeding login attempt completes
        /// </summary>
        private AutoResetEvent userDetailsGetCompletedEvent;

        /// <summary>
        /// Result data from the call to get users
        /// </summary>
        private Users resultData;

        /// <summary>
        /// Result status from the call to get users
        /// </summary>
        private ResultStatus resultStatus;
        
        /// <summary>
        /// Test retrieving etsy users
        /// </summary>
        [TestMethod]
        public void EtsyUsersLowDetailRetrievalTest()
        {
            this.userDetailsGetCompletedEvent = new AutoResetEvent(false);
            try
            {
                IUsersService etsyUsers = new UsersService(EtsyApiKey);

                this.resultData = null;
                etsyUsers.GetUserDetailsCompleted += this.GetUserDetailsCompleted;
                etsyUsers.GetUserDetails(TestUserId, DetailLevel.Low);

                // wait for up to 20 seconds for it to complete
                bool signalled = this.userDetailsGetCompletedEvent.WaitOne(WaitTimeout);

                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                Assert.IsNotNull(this.resultStatus);
                Assert.IsTrue(this.resultStatus.Success, "Call failed");
                Assert.IsNotNull(this.resultData);
                Assert.IsNotNull(this.resultData.Params);
                Assert.IsNotNull(this.resultData.Results);
                Assert.AreEqual(1, this.resultData.Count);
            }
            finally
            {
                this.userDetailsGetCompletedEvent = null; 
            }
        }

        /// <summary>
        /// Retrieve completed
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event params</param>
        private void GetUserDetailsCompleted(object sender, ResultEventArgs<Users, ResultStatus> e)
        {
            this.resultData = e.ResultValue;
            this.resultStatus = e.ResultStatus;

            this.userDetailsGetCompletedEvent.Set();
        }
    }
}
