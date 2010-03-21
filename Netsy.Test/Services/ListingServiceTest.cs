//-----------------------------------------------------------------------
// <copyright file="ListingServiceTest.cs" company="AFS">
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
    public class ListingServiceTest
    {
        private const string GetFrontFeaturedListingsRawResults = @"{""count"":50000,""results"":[{""listing_id"":41987372,""state"":""active"",""title"":""Jellybean. . . necklace"",""url"":""http:\/\/www.etsy.com\/view_listing.php?listing_id=41987372"",""image_url_25x25"":""http:\/\/ny-image3.etsy.com\/il_25x25.127799487.jpg"",""image_url_50x50"":""http:\/\/ny-image3.etsy.com\/il_50x50.127799487.jpg"",""image_url_75x75"":""http:\/\/ny-image3.etsy.com\/il_75x75.127799487.jpg"",""image_url_155x125"":""http:\/\/ny-image3.etsy.com\/il_155x125.127799487.jpg"",""image_url_200x200"":""http:\/\/ny-image3.etsy.com\/il_200x200.127799487.jpg"",""image_url_430xN"":""http:\/\/ny-image3.etsy.com\/il_430xN.127799487.jpg"",""creation_epoch"":1267727964.32,""user_id"":5342773,""user_name"":""staceywinters""},{""listing_id"":41319542,""state"":""active"",""title"":""Cotton 8 x 8 Print"",""url"":""http:\/\/www.etsy.com\/view_listing.php?listing_id=41319542"",""image_url_25x25"":""http:\/\/ny-image3.etsy.com\/il_25x25.125526351.jpg"",""image_url_50x50"":""http:\/\/ny-image3.etsy.com\/il_50x50.125526351.jpg"",""image_url_75x75"":""http:\/\/ny-image3.etsy.com\/il_75x75.125526351.jpg"",""image_url_155x125"":""http:\/\/ny-image3.etsy.com\/il_155x125.125526351.jpg"",""image_url_200x200"":""http:\/\/ny-image3.etsy.com\/il_200x200.125526351.jpg"",""image_url_430xN"":""http:\/\/ny-image3.etsy.com\/il_430xN.125526351.jpg"",""creation_epoch"":1266947407.32,""user_id"":8201757,""user_name"":""LolasRoom""},{""listing_id"":36227845,""state"":""active"",""title"":""2 to 4T Daisy Earflap Beanie - pastel pink, rose pink, yellow white"",""url"":""http:\/\/www.etsy.com\/view_listing.php?listing_id=36227845"",""image_url_25x25"":""http:\/\/ny-image0.etsy.com\/il_25x25.108291692.jpg"",""image_url_50x50"":""http:\/\/ny-image0.etsy.com\/il_50x50.108291692.jpg"",""image_url_75x75"":""http:\/\/ny-image0.etsy.com\/il_75x75.108291692.jpg"",""image_url_155x125"":""http:\/\/ny-image0.etsy.com\/il_155x125.108291692.jpg"",""image_url_200x200"":""http:\/\/ny-image0.etsy.com\/il_200x200.108291692.jpg"",""image_url_430xN"":""http:\/\/ny-image0.etsy.com\/il_430xN.108291692.jpg"",""creation_epoch"":1268158905.85,""user_id"":96642,""user_name"":""pdxbeanies""},{""listing_id"":42112614,""state"":""active"",""title"":""Pastel Pink Hoodie"",""url"":""http:\/\/www.etsy.com\/view_listing.php?listing_id=42112614"",""image_url_25x25"":""http:\/\/ny-image1.etsy.com\/il_25x25.128224305.jpg"",""image_url_50x50"":""http:\/\/ny-image1.etsy.com\/il_50x50.128224305.jpg"",""image_url_75x75"":""http:\/\/ny-image1.etsy.com\/il_75x75.128224305.jpg"",""image_url_155x125"":""http:\/\/ny-image1.etsy.com\/il_155x125.128224305.jpg"",""image_url_200x200"":""http:\/\/ny-image1.etsy.com\/il_200x200.128224305.jpg"",""image_url_430xN"":""http:\/\/ny-image1.etsy.com\/il_430xN.128224305.jpg"",""creation_epoch"":1267863564.13,""user_id"":8469639,""user_name"":""firuze""},{""listing_id"":39380392,""state"":""active"",""title"":""Dollhouse Miniature 1\/12 Scale Shabby Chic Mint Green Bread Box"",""url"":""http:\/\/www.etsy.com\/view_listing.php?listing_id=39380392"",""image_url_25x25"":""http:\/\/ny-image3.etsy.com\/il_25x25.118919955.jpg"",""image_url_50x50"":""http:\/\/ny-image3.etsy.com\/il_50x50.118919955.jpg"",""image_url_75x75"":""http:\/\/ny-image3.etsy.com\/il_75x75.118919955.jpg"",""image_url_155x125"":""http:\/\/ny-image3.etsy.com\/il_155x125.118919955.jpg"",""image_url_200x200"":""http:\/\/ny-image3.etsy.com\/il_200x200.118919955.jpg"",""image_url_430xN"":""http:\/\/ny-image3.etsy.com\/il_430xN.118919955.jpg"",""creation_epoch"":1264554271.46,""user_id"":5787747,""user_name"":""miniaturepatisserie""},{""listing_id"":42557961,""state"":""active"",""title"":""ANGELIQUE - Fully Reversible Modern Everyday Canvas Tote Handbag"",""url"":""http:\/\/www.etsy.com\/view_listing.php?listing_id=42557961"",""image_url_25x25"":""http:\/\/ny-image2.etsy.com\/il_25x25.129752834.jpg"",""image_url_50x50"":""http:\/\/ny-image2.etsy.com\/il_50x50.129752834.jpg"",""image_url_75x75"":""http:\/\/ny-image2.etsy.com\/il_75x75.129752834.jpg"",""image_url_155x125"":""http:\/\/ny-image2.etsy.com\/il_155x125.129752834.jpg"",""image_url_200x200"":""http:\/\/ny-image2.etsy.com\/il_200x200.129752834.jpg"",""image_url_430xN"":""http:\/\/ny-image2.etsy.com\/il_430xN.129752834.jpg"",""creation_epoch"":1268601287.58,""user_id"":9045640,""user_name"":""THEBAGSHOP""},{""listing_id"":41755260,""state"":""active"",""title"":""Miniature bunny"",""url"":""http:\/\/www.etsy.com\/view_listing.php?listing_id=41755260"",""image_url_25x25"":""http:\/\/ny-image2.etsy.com\/il_25x25.127011086.jpg"",""image_url_50x50"":""http:\/\/ny-image2.etsy.com\/il_50x50.127011086.jpg"",""image_url_75x75"":""http:\/\/ny-image2.etsy.com\/il_75x75.127011086.jpg"",""image_url_155x125"":""http:\/\/ny-image2.etsy.com\/il_155x125.127011086.jpg"",""image_url_200x200"":""http:\/\/ny-image2.etsy.com\/il_200x200.127011086.jpg"",""image_url_430xN"":""http:\/\/ny-image2.etsy.com\/il_430xN.127011086.jpg"",""creation_epoch"":1267469766.32,""user_id"":5768132,""user_name"":""Elze""},{""listing_id"":40708274,""state"":""active"",""title"":""Shabby chic - 8x10 fine art print"",""url"":""http:\/\/www.etsy.com\/view_listing.php?listing_id=40708274"",""image_url_25x25"":""http:\/\/ny-image2.etsy.com\/il_25x25.128561766.jpg"",""image_url_50x50"":""http:\/\/ny-image2.etsy.com\/il_50x50.128561766.jpg"",""image_url_75x75"":""http:\/\/ny-image2.etsy.com\/il_75x75.128561766.jpg"",""image_url_155x125"":""http:\/\/ny-image2.etsy.com\/il_155x125.128561766.jpg"",""image_url_200x200"":""http:\/\/ny-image2.etsy.com\/il_200x200.128561766.jpg"",""image_url_430xN"":""http:\/\/ny-image2.etsy.com\/il_430xN.128561766.jpg"",""creation_epoch"":1266180357.44,""user_id"":5064563,""user_name"":""irenesuchocki""},{""listing_id"":40823816,""state"":""active"",""title"":""Pretty In Pink Ring"",""url"":""http:\/\/www.etsy.com\/view_listing.php?listing_id=40823816"",""image_url_25x25"":""http:\/\/ny-image2.etsy.com\/il_25x25.123829322.jpg"",""image_url_50x50"":""http:\/\/ny-image2.etsy.com\/il_50x50.123829322.jpg"",""image_url_75x75"":""http:\/\/ny-image2.etsy.com\/il_75x75.123829322.jpg"",""image_url_155x125"":""http:\/\/ny-image2.etsy.com\/il_155x125.123829322.jpg"",""image_url_200x200"":""http:\/\/ny-image2.etsy.com\/il_200x200.123829322.jpg"",""image_url_430xN"":""http:\/\/ny-image2.etsy.com\/il_430xN.123829322.jpg"",""creation_epoch"":1268427932.22,""user_id"":6422478,""user_name"":""linkeldesigns""},{""listing_id"":39351472,""state"":""active"",""title"":""Rosebud and Leaf hair pins"",""url"":""http:\/\/www.etsy.com\/view_listing.php?listing_id=39351472"",""image_url_25x25"":""http:\/\/ny-image0.etsy.com\/il_25x25.118820568.jpg"",""image_url_50x50"":""http:\/\/ny-image0.etsy.com\/il_50x50.118820568.jpg"",""image_url_75x75"":""http:\/\/ny-image0.etsy.com\/il_75x75.118820568.jpg"",""image_url_155x125"":""http:\/\/ny-image0.etsy.com\/il_155x125.118820568.jpg"",""image_url_200x200"":""http:\/\/ny-image0.etsy.com\/il_200x200.118820568.jpg"",""image_url_430xN"":""http:\/\/ny-image0.etsy.com\/il_430xN.118820568.jpg"",""creation_epoch"":1268841348.56,""user_id"":7075630,""user_name"":""jewllori""}],""params"":{""offset"":0,""limit"":10,""detail_level"":""low""},""type"":""listing""}";

        [TestMethod]
        public void CreateWithMockRequestTest()
        {
            EtsyContext etsyContext = new EtsyContext(string.Empty);
            IRequestGenerator requestGenerator = new MockFixedDataRequestGenerator(string.Empty);
            DataRetriever dataRetriever = new DataRetriever(new NullDataCache(), requestGenerator);
            IListingsService service = new ListingsService(etsyContext, dataRetriever);

            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void GetFrontFeaturedListingsTest()
        {
            EtsyContext etsyContext = new EtsyContext(Constants.DummyEtsyApiKey);
            MockFixedDataRequestGenerator requestGenerator = new MockFixedDataRequestGenerator(GetFrontFeaturedListingsRawResults);
            DataRetriever dataRetriever = new DataRetriever(new NullDataCache(), requestGenerator);
            IListingsService etsyListingsService = new ListingsService(etsyContext, dataRetriever);

            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                etsyListingsService.GetFrontFeaturedListingsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                etsyListingsService.GetFrontFeaturedListings(0, 10, DetailLevel.Low);
                bool signalled = waitEvent.WaitOne(Constants.WaitTimeout);

                // ASSERT

                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                TestHelpers.CheckResultSuccess(result);
            }
        }
    }
}
