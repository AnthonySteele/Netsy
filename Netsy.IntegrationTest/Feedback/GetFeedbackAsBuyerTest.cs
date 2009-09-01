//-----------------------------------------------------------------------
// <copyright file="GetFeedbackAsBuyerTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.IntegrationTest.Feedback
{
    using System.Net;
    using System.Threading;

    using Netsy.Core;
    using Netsy.DataModel;
    using Netsy.DataModel.FeedbackData;
    using Netsy.Helpers;
    using Netsy.Interfaces;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test the GetFeedbackAsBuyer API Function
    /// </summary>
    [TestClass]
    public class GetFeedbackAsBuyerTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetFeedbackAsBuyerMissingApiKeyTest()
        {
            // ARRANGE
            ResultEventArgs<Feedbacks> result = null;
            IFeedbackService feedbackService = new FeedbackService(new EtsyContext(string.Empty));
            feedbackService.GetFeedbackAsBuyerCompleted += (s, e) => result = e;

            // ACT
            feedbackService.GetFeedbackAsBuyer(NetsyData.TestUserId, 0, 10);

            // check the data
            NetsyData.CheckResultFailure(result);
        }

        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetFeedbackAsBuyerByNameMissingApiKeyTest()
        {
            // ARRANGE
            ResultEventArgs<Feedbacks> result = null;
            IFeedbackService feedbackService = new FeedbackService(new EtsyContext(string.Empty));
            feedbackService.GetFeedbackAsBuyerCompleted += (s, e) => result = e;

            // ACT
            feedbackService.GetFeedbackAsBuyer(NetsyData.TestUserName, 0, 10);

            // check the data
            NetsyData.CheckResultFailure(result);
        }

        /// <summary>
        /// Test invalid API key
        /// </summary>
        [TestMethod]
        public void GetFeedbackAsBuyerApiKeyInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Feedbacks> result = null;
                IFeedbackService feedbackService = new FeedbackService(new EtsyContext("InvalidKey"));
                feedbackService.GetFeedbackAsBuyerCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                feedbackService.GetFeedbackAsBuyer(NetsyData.TestUserId, 0, 10);
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
        /// Test invalid API key
        /// </summary>
        [TestMethod]
        public void GetFeedbackAsBuyerByNameApiKeyInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Feedbacks> result = null;
                IFeedbackService feedbackService = new FeedbackService(new EtsyContext("InvalidKey"));
                feedbackService.GetFeedbackAsBuyerCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                feedbackService.GetFeedbackAsBuyer(NetsyData.TestUserName, 0, 10);
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
    }
}
