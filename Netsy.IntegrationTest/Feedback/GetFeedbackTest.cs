//-----------------------------------------------------------------------
// <copyright file="GetFeedbackTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.IntegrationTest.Feedback
{
    using System.Net;
    using System.Threading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.Services;
    using Netsy.Test;

    /// <summary>
    /// Test the GetFeedback API Function
    /// </summary>
    [TestClass]
    public class GetFeedbackTest
    {
        /// <summary>
        /// Test invalid API key
        /// </summary>
        [TestMethod]
        public void GetFeedbackApiKeyInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Feedbacks> result = null;
                IFeedbackService feedbackService = new FeedbackService(new EtsyContext("InvalidKey"));
                feedbackService.GetFeedbackCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                feedbackService.GetFeedback(NetsyData.TestFeedbackId);
                bool signalled = waitEvent.WaitOne(Constants.WaitTimeout);

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
        /// Test invalid Feedback id
        /// </summary>
        [TestMethod]
        public void GetFeedbackIdInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Feedbacks> result = null;
                IFeedbackService feedbackService = new FeedbackService(new EtsyContext(NetsyData.EtsyApiKey));
                feedbackService.GetFeedbackCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                feedbackService.GetFeedback(NetsyData.TestBadFeedbackId);
                bool signalled = waitEvent.WaitOne(Constants.WaitTimeout);

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
        /// test valid retrieval
        /// </summary>
        [TestMethod]
        public void GetFeedbackGetTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Feedbacks> result = null;
                IFeedbackService feedbackService = new FeedbackService(new EtsyContext(NetsyData.EtsyApiKey));
                feedbackService.GetFeedbackCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                feedbackService.GetFeedback(NetsyData.TestFeedbackId);
                bool signalled = waitEvent.WaitOne(Constants.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                TestHelpers.CheckResultSuccess(result);
                Assert.AreEqual(1, result.ResultValue.Count);
            }
        }
    }
}
