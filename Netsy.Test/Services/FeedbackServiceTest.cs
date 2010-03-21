//-----------------------------------------------------------------------
// <copyright file="FeedbackServiceTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Test.Services
{
    using System.Threading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.Requests;
    using Netsy.Services;
    using Netsy.Test.Requests;

    [TestClass]
    public class FeedbackServiceTest
    {
        private const string GetFeedbackRawResults = @"{""count"":1,""results"":[{""feedback_id"":1,""listing_id"":1234,""title"":""Test data"",""url"":""http:\/\/www.etsy.com\/view_listing.php?listing_id=7209"",""creation_epoch"":1125463478.12,""author_user_id"":1234,""subject_user_id"":1234,""seller_user_id"":1234,""buyer_user_id"":1234,""message"":""Test Data"",""disposition"":""positive"",""value"":1,""image_url_25x25"":null,""image_url_fullxfull"":null}],""params"":{""feedback_id"":1},""type"":""feedback""}";

        [TestMethod]
        public void CreateWithMockRequestTest()
        {
            EtsyContext etsyContext = new EtsyContext(string.Empty);
            IRequestGenerator requestGenerator = new MockFixedDataRequestGenerator(string.Empty);
            DataRetriever dataRetriever = new DataRetriever(new NullDataCache(), requestGenerator);
            IFeedbackService service = new FeedbackService(etsyContext, dataRetriever);

            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void GetFeedbackTest()
        {
            EtsyContext etsyContext = new EtsyContext(Constants.DummyEtsyApiKey);
            MockFixedDataRequestGenerator requestGenerator = new MockFixedDataRequestGenerator(GetFeedbackRawResults);
            DataRetriever dataRetriever = new DataRetriever(new NullDataCache(), requestGenerator);
            IFeedbackService etsyShopsService = new FeedbackService(etsyContext, dataRetriever);

            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Feedbacks> result = null;
                etsyShopsService.GetFeedbackCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                etsyShopsService.GetFeedback(Constants.TestId);
                bool signalled = waitEvent.WaitOne(Constants.WaitTimeout);

                // ASSERT

                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                TestHelpers.CheckResultSuccess(result);
            }
        }
    }
}
