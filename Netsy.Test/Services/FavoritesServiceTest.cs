//-----------------------------------------------------------------------
// <copyright file="FavoritesServiceTest.cs" company="AFS">
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
    public class FavoritesServiceTest
    {
        private const string GetUserDetailsRawResults = @"{""count"":1,""results"":[{""user_name"":""TESTUSER"",""user_id"":1234,""url"":""http:\/\/www.etsy.com\/shop.php?user_id=7572146"",""image_url_25x25"":""http:\/\/ny-image3.etsy.com\/iusa_25x25.6523631.jpg"",""image_url_30x30"":""http:\/\/ny-image3.etsy.com\/iusa_30x30.6523631.jpg"",""image_url_50x50"":""http:\/\/ny-image3.etsy.com\/iusa_50x50.6523631.jpg"",""image_url_75x75"":""http:\/\/ny-image3.etsy.com\/iusa_75x75.6523631.jpg"",""join_epoch"":1245494184.26,""city"":""Testville""}],""params"":{""user_id"":1234,""detail_level"":""low""},""type"":""user""}";

        [TestMethod]
        public void CreateWithMockRequestTest()
        {
            EtsyContext etsyContext = new EtsyContext(string.Empty);
            IRequestGenerator requestGenerator = new MockFixedDataRequestGenerator(string.Empty);
            DataRetriever dataRetriever = new DataRetriever(new NullDataCache(), requestGenerator);
            IFavoritesService service = new FavoritesService(etsyContext, dataRetriever);

            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void GetFavorersOfListingTest()
        {
            EtsyContext etsyContext = new EtsyContext(Constants.DummyEtsyApiKey);
            MockFixedDataRequestGenerator requestGenerator = new MockFixedDataRequestGenerator(GetUserDetailsRawResults);
            DataRetriever dataRetriever = new DataRetriever(new NullDataCache(), requestGenerator);
            IFavoritesService etsyFavoritesService = new FavoritesService(etsyContext, dataRetriever);

            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Users> result = null;
                etsyFavoritesService.GetFavorersOfListingCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                etsyFavoritesService.GetFavorersOfListing(Constants.TestId, 0, 10, DetailLevel.Low);
                bool signalled = waitEvent.WaitOne(Constants.WaitTimeout);

                // ASSERT

                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                TestHelpers.CheckResultSuccess(result);
            }
        }
    }
}
