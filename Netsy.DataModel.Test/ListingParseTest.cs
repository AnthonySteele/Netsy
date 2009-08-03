//-----------------------------------------------------------------------
// <copyright file="ListingParseTest.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.DataModel.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Netsy.DataModel.ListingData;
    using Netsy.Helpers;

    /// <summary>
    /// Test parsing string Json data into user details
    /// </summary>
    [TestClass]
    public class ListingParseTest
    {

        /// <summary>
        /// A sample response text containing one listing, low detail level
        /// </summary>
        private const string ListingsLowDetailResponse = @"{""count"":7,""results"":[{""listing_id"":1234,""state"":""active"",""title"":""Fred"",""url"":""http:\/\/www.etsy.com\/view_listing.php?listing_id=25452130"",""image_url_25x25"":""http:\/\/ny-image0.etsy.com\/il_25x25.72088048.jpg"",""image_url_50x50"":""http:\/\/ny-image0.etsy.com\/il_50x50.72088048.jpg"",""image_url_75x75"":""http:\/\/ny-image0.etsy.com\/il_75x75.72088048.jpg"",""image_url_155x125"":""http:\/\/ny-image0.etsy.com\/il_155x125.72088048.jpg"",""image_url_200x200"":""http:\/\/ny-image0.etsy.com\/il_200x200.72088048.jpg"",""image_url_430xN"":""http:\/\/ny-image0.etsy.com\/il_430xN.72088048.jpg"",""creation_epoch"":1243108221.07}],""params"":{""user_id"":1234,""sort_on"":""created"",""sort_order"":""up"",""section_id"":null,""offset"":0,""limit"":1,""detail_level"":""low""},""type"":""listing""}";

        /// <summary>
        /// A sample response text containing one listing, medium detail level
        /// </summary>
        private const string ListingsMediumDetailResponse = @"{""count"":7,""results"":[{""listing_id"":1234,""state"":""active"",""title"":""Fred"",""url"":""http:\/\/www.etsy.com\/view_listing.php?listing_id=25452130"",""image_url_25x25"":""http:\/\/ny-image0.etsy.com\/il_25x25.72088048.jpg"",""image_url_50x50"":""http:\/\/ny-image0.etsy.com\/il_50x50.72088048.jpg"",""image_url_75x75"":""http:\/\/ny-image0.etsy.com\/il_75x75.72088048.jpg"",""image_url_155x125"":""http:\/\/ny-image0.etsy.com\/il_155x125.72088048.jpg"",""image_url_200x200"":""http:\/\/ny-image0.etsy.com\/il_200x200.72088048.jpg"",""image_url_430xN"":""http:\/\/ny-image0.etsy.com\/il_430xN.72088048.jpg"",""creation_epoch"":1243108221.07,""views"":361,""tags"":[""clothing"",""costume"",""men"",""vest"",""gothic"",""victorian"",""waistcoat"",""goth"",""cravat"",""steampunk"",""uk"",""geekery"",""bfsc"",""etsydarkteam""],""materials"":[""buttons"",""satin"",""lining""],""price"":130,""currency_code"":""USD"",""ending_epoch"":1253735421.07,""hsv_color"":""0;0;100"",""rgb_color"":""#FFFFFF"",""user_id"":1234,""user_name"":""Fred"",""quantity"":1}],""params"":{""user_id"":1234,""sort_on"":""created"",""sort_order"":""up"",""section_id"":null,""offset"":0,""limit"":1,""detail_level"":""medium""},""type"":""listing""}";

        /// <summary>
        /// A sample response text containing one user, high detail level
        /// </summary>
        private const string ListingsHighDetailResponse = @"{""count"":7,""results"":[{""listing_id"":1234,""state"":""active"",""title"":""Fred"",""url"":""http:\/\/www.etsy.com\/view_listing.php?listing_id=25452130"",""image_url_25x25"":""http:\/\/ny-image0.etsy.com\/il_25x25.72088048.jpg"",""image_url_50x50"":""http:\/\/ny-image0.etsy.com\/il_50x50.72088048.jpg"",""image_url_75x75"":""http:\/\/ny-image0.etsy.com\/il_75x75.72088048.jpg"",""image_url_155x125"":""http:\/\/ny-image0.etsy.com\/il_155x125.72088048.jpg"",""image_url_200x200"":""http:\/\/ny-image0.etsy.com\/il_200x200.72088048.jpg"",""image_url_430xN"":""http:\/\/ny-image0.etsy.com\/il_430xN.72088048.jpg"",""creation_epoch"":1243108221.07,""views"":361,""tags"":[""clothing"",""costume"",""men"",""vest"",""gothic"",""victorian"",""waistcoat"",""goth"",""cravat"",""steampunk"",""uk"",""geekery"",""bfsc"",""etsydarkteam""],""materials"":[""buttons"",""satin"",""lining""],""price"":130,""currency_code"":""USD"",""ending_epoch"":1253735421.07,""hsv_color"":""0;0;100"",""rgb_color"":""#FFFFFF"",""user_id"":1234,""user_name"":""Fred"",""quantity"":1,""description"":""The Fred."",""section_id"":6177344,""section_title"":""Waistcoats"",""lat"":51.4985,""lon"":-0.1318,""user_image_id"":6043378,""city"":""London"",""all_images"":[{""image_url_25x25"":""http:\/\/ny-image0.etsy.com\/il_25x25.72088048.jpg"",""image_url_50x50"":""http:\/\/ny-image0.etsy.com\/il_50x50.72088048.jpg"",""image_url_75x75"":""http:\/\/ny-image0.etsy.com\/il_75x75.72088048.jpg"",""image_url_155x125"":""http:\/\/ny-image0.etsy.com\/il_155x125.72088048.jpg"",""image_url_200x200"":""http:\/\/ny-image0.etsy.com\/il_200x200.72088048.jpg"",""image_url_430xN"":""http:\/\/ny-image0.etsy.com\/il_430xN.72088048.jpg"",""hsv_color"":""0;0;100"",""rgb_color"":""#FFFFFF""},{""image_url_25x25"":""http:\/\/ny-image1.etsy.com\/il_25x25.72088193.jpg"",""image_url_50x50"":""http:\/\/ny-image1.etsy.com\/il_50x50.72088193.jpg"",""image_url_75x75"":""http:\/\/ny-image1.etsy.com\/il_75x75.72088193.jpg"",""image_url_155x125"":""http:\/\/ny-image1.etsy.com\/il_155x125.72088193.jpg"",""image_url_200x200"":""http:\/\/ny-image1.etsy.com\/il_200x200.72088193.jpg"",""image_url_430xN"":""http:\/\/ny-image1.etsy.com\/il_430xN.72088193.jpg"",""hsv_color"":""0;0;100"",""rgb_color"":""#FFFFFF""},{""image_url_25x25"":""http:\/\/ny-image3.etsy.com\/il_25x25.72088239.jpg"",""image_url_50x50"":""http:\/\/ny-image3.etsy.com\/il_50x50.72088239.jpg"",""image_url_75x75"":""http:\/\/ny-image3.etsy.com\/il_75x75.72088239.jpg"",""image_url_155x125"":""http:\/\/ny-image3.etsy.com\/il_155x125.72088239.jpg"",""image_url_200x200"":""http:\/\/ny-image3.etsy.com\/il_200x200.72088239.jpg"",""image_url_430xN"":""http:\/\/ny-image3.etsy.com\/il_430xN.72088239.jpg"",""hsv_color"":""0;0;100"",""rgb_color"":""#FFFFFF""}]}],""params"":{""user_id"":1234,""sort_on"":""created"",""sort_order"":""up"",""section_id"":null,""offset"":0,""limit"":1,""detail_level"":""high""},""type"":""listing""}";

        /// <summary>
        /// Test parsing listings low detail response
        /// </summary>
        [TestMethod]
        public void ListingsLowDetailResponseParse()
        {
            Listings listings = ListingsLowDetailResponse.Deserialize<Listings>();

            Assert.IsNotNull(listings);
            Assert.AreEqual(7, listings.Count);

            Listing listing1 = listings.Results[0];

            Assert.IsNotNull(listing1);

            QueryParams queryParams = listings.Params;
            Assert.IsNotNull(queryParams);
            Assert.AreEqual(DetailLevel.Low, queryParams.DetailLevelEnum);
            Assert.AreEqual(1234, queryParams.UserId);
        }

        /// <summary>
        /// Test parsing a listings medium detail response
        /// </summary>
        [TestMethod]
        public void ListingsMediumDetailResponseParse()
        {
            Listings listings = ListingsMediumDetailResponse.Deserialize<Listings>();

            Assert.IsNotNull(listings);
            Assert.AreEqual(7, listings.Count);

            Listing listing1 = listings.Results[0];

            Assert.IsNotNull(listing1);
            Assert.AreEqual("Fred", listing1.UserName);
            Assert.AreEqual(1234, listing1.UserId);

            QueryParams queryParams = listings.Params;
            Assert.IsNotNull(queryParams);
            Assert.AreEqual(DetailLevel.Medium, queryParams.DetailLevelEnum);
            Assert.AreEqual(1234, queryParams.UserId);
        }

        /// <summary>
        /// Test parsing a listings high detail response
        /// </summary>
        [TestMethod]
        public void ListingsHighDetailResponseParse()
        {
            Listings listings = ListingsHighDetailResponse.Deserialize<Listings>();

            Assert.IsNotNull(listings);
            Assert.AreEqual(7, listings.Count);

            Listing listing1 = listings.Results[0];

            Assert.IsNotNull(listing1);
            Assert.AreEqual("Fred", listing1.UserName);
            Assert.AreEqual("London", listing1.City);
            Assert.AreEqual(1234, listing1.UserId);

            QueryParams queryParams = listings.Params;
            Assert.IsNotNull(queryParams);
            Assert.AreEqual(DetailLevel.High, queryParams.DetailLevelEnum);
            Assert.AreEqual(1234, queryParams.UserId);
        }
    }
}
