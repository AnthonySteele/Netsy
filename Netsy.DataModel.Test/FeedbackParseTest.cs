//-----------------------------------------------------------------------
// <copyright file="ListingParseTest.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.DataModel.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Netsy.DataModel.FeedbackData;
    using Netsy.Helpers;

    /// <summary>
    /// Test parsing string Json data into user details
    /// </summary>
    [TestClass]
    public class FeedbackParseTest
    {

        /// <summary>
        /// A sample response text containing one feedback
        /// </summary>
        private const string FeedbackResponse = @"{""count"":1,""results"":[{""feedback_id"":1234,""listing_id"":2345,""title"":""Special Listing for Fred"",""url"":""http:\/\/www.etsy.com\/view_listing.php?listing_id=1234"",""creation_epoch"":1246330888.32,""author_user_id"":6228895,""subject_user_id"":1234,""seller_user_id"":3456,""buyer_user_id"":7890,""message"":""Thank you for an easy, pleasant transaction Excellent buyer. A++++++."",""disposition"":""positive"",""value"":1,""image_url_25x25"":null,""image_url_fullxfull"":null}],""params"":{""user_id"":1234,""limit"":10,""offset"":0},""type"":""feedback""}";

        /// <summary>
        /// Test parsing feedbackresponse
        /// </summary>
        [TestMethod]
        public void FeedbackResponseParse()
        {
            Feedbacks feedbacks = FeedbackResponse.Deserialize<Feedbacks>();

            Assert.IsNotNull(feedbacks);
            Assert.AreEqual(1, feedbacks.Count);

            Feedback feedback1 = feedbacks.Results[0];

            Assert.IsNotNull(feedback1);

            QueryParams queryParams = feedbacks.Params;
            Assert.IsNotNull(queryParams);

            // feedback doesn't use detail level, only one version of it
            Assert.AreEqual(DetailLevel.Unknown, queryParams.DetailLevelEnum);
            Assert.AreEqual(1234, queryParams.UserId);
        }
    }
}
