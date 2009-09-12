//-----------------------------------------------------------------------
// <copyright file="ListingParseTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.Test.DataModel
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Netsy.DataModel;
    using Netsy.Helpers;

    /// <summary>
    /// Test parsing string Json data into listing details
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
        /// A sample response text containing one listing, high detail level
        /// </summary>
        private const string ListingsHighDetailResponse = @"{""count"":7,""results"":[{""listing_id"":1234,""state"":""active"",""title"":""Fred"",""url"":""http:\/\/www.etsy.com\/view_listing.php?listing_id=25452130"",""image_url_25x25"":""http:\/\/ny-image0.etsy.com\/il_25x25.72088048.jpg"",""image_url_50x50"":""http:\/\/ny-image0.etsy.com\/il_50x50.72088048.jpg"",""image_url_75x75"":""http:\/\/ny-image0.etsy.com\/il_75x75.72088048.jpg"",""image_url_155x125"":""http:\/\/ny-image0.etsy.com\/il_155x125.72088048.jpg"",""image_url_200x200"":""http:\/\/ny-image0.etsy.com\/il_200x200.72088048.jpg"",""image_url_430xN"":""http:\/\/ny-image0.etsy.com\/il_430xN.72088048.jpg"",""creation_epoch"":1243108221.07,""views"":361,""tags"":[""clothing"",""costume"",""men"",""vest"",""gothic"",""victorian"",""waistcoat"",""goth"",""cravat"",""steampunk"",""uk"",""geekery"",""bfsc"",""etsydarkteam""],""materials"":[""buttons"",""satin"",""lining""],""price"":130,""currency_code"":""USD"",""ending_epoch"":1253735421.07,""hsv_color"":""0;0;100"",""rgb_color"":""#FFFFFF"",""user_id"":1234,""user_name"":""Fred"",""quantity"":1,""description"":""The Fred."",""section_id"":6177344,""section_title"":""Waistcoats"",""lat"":51.4985,""lon"":-0.1318,""user_image_id"":6043378,""city"":""London"",""all_images"":[{""image_url_25x25"":""http:\/\/ny-image0.etsy.com\/il_25x25.72088048.jpg"",""image_url_50x50"":""http:\/\/ny-image0.etsy.com\/il_50x50.72088048.jpg"",""image_url_75x75"":""http:\/\/ny-image0.etsy.com\/il_75x75.72088048.jpg"",""image_url_155x125"":""http:\/\/ny-image0.etsy.com\/il_155x125.72088048.jpg"",""image_url_200x200"":""http:\/\/ny-image0.etsy.com\/il_200x200.72088048.jpg"",""image_url_430xN"":""http:\/\/ny-image0.etsy.com\/il_430xN.72088048.jpg"",""hsv_color"":""0;0;100"",""rgb_color"":""#FFFFFF""},{""image_url_25x25"":""http:\/\/ny-image1.etsy.com\/il_25x25.72088193.jpg"",""image_url_50x50"":""http:\/\/ny-image1.etsy.com\/il_50x50.72088193.jpg"",""image_url_75x75"":""http:\/\/ny-image1.etsy.com\/il_75x75.72088193.jpg"",""image_url_155x125"":""http:\/\/ny-image1.etsy.com\/il_155x125.72088193.jpg"",""image_url_200x200"":""http:\/\/ny-image1.etsy.com\/il_200x200.72088193.jpg"",""image_url_430xN"":""http:\/\/ny-image1.etsy.com\/il_430xN.72088193.jpg"",""hsv_color"":""0;0;100"",""rgb_color"":""#FFFFFF""},{""image_url_25x25"":""http:\/\/ny-image3.etsy.com\/il_25x25.72088239.jpg"",""image_url_50x50"":""http:\/\/ny-image3.etsy.com\/il_50x50.72088239.jpg"",""image_url_75x75"":""http:\/\/ny-image3.etsy.com\/il_75x75.72088239.jpg"",""image_url_155x125"":""http:\/\/ny-image3.etsy.com\/il_155x125.72088239.jpg"",""image_url_200x200"":""http:\/\/ny-image3.etsy.com\/il_200x200.72088239.jpg"",""image_url_430xN"":""http:\/\/ny-image3.etsy.com\/il_430xN.72088239.jpg"",""hsv_color"":""0;0;100"",""rgb_color"":""#FFFFFF""}]}],""params"":{""user_id"":1234,""sort_on"":""created"",""sort_order"":""up"",""section_id"":null,""offset"":0,""limit"":1,""detail_level"":""high""},""type"":""listing""}";

        /// <summary>
        /// A sample response text containing three listings, high detail level
        /// </summary>
        private const string ThreeListingsHighDetailResponse = @"{""count"":3,""results"":[{""listing_id"":26448486,""state"":""active"",""title"":""Fred1"",""url"":""http:\/\/www.etsy.com\/view_listing.php?listing_id=26448486"",""image_url_25x25"":""http:\/\/ny-image1.etsy.com\/il_25x25.75420265.jpg"",""image_url_50x50"":""http:\/\/ny-image1.etsy.com\/il_50x50.75420265.jpg"",""image_url_75x75"":""http:\/\/ny-image1.etsy.com\/il_75x75.75420265.jpg"",""image_url_155x125"":""http:\/\/ny-image1.etsy.com\/il_155x125.75420265.jpg"",""image_url_200x200"":""http:\/\/ny-image1.etsy.com\/il_200x200.75420265.jpg"",""image_url_430xN"":""http:\/\/ny-image1.etsy.com\/il_430xN.75420265.jpg"",""creation_epoch"":1249293900.49,""views"":351,""tags"":[""clothing"",""geekery"",""skirt"",""women"",""stars"",""goth"",""gothic"",""uk"",""black"",""custom"",""red"",""purple"",""bfsc"",""etsydarkteam""],""materials"":[""tulle"",""satin"",""elastic""],""price"":33,""currency_code"":""USD"",""ending_epoch"":1259834700.49,""hsv_color"":""0;0;100"",""rgb_color"":""#FFFFFF"",""user_id"":1234,""user_name"":""Fred"",""quantity"":1,""description"":""Made from real fred"",""section_id"":null,""section_title"":null,""lat"":51.4985,""lon"":-0.1318,""user_image_id"":6043378,""city"":""London"",""all_images"":[{""image_url_25x25"":""http:\/\/ny-image1.etsy.com\/il_25x25.75420265.jpg"",""image_url_50x50"":""http:\/\/ny-image1.etsy.com\/il_50x50.75420265.jpg"",""image_url_75x75"":""http:\/\/ny-image1.etsy.com\/il_75x75.75420265.jpg"",""image_url_155x125"":""http:\/\/ny-image1.etsy.com\/il_155x125.75420265.jpg"",""image_url_200x200"":""http:\/\/ny-image1.etsy.com\/il_200x200.75420265.jpg"",""image_url_430xN"":""http:\/\/ny-image1.etsy.com\/il_430xN.75420265.jpg"",""hsv_color"":""0;0;100"",""rgb_color"":""#FFFFFF""},{""image_url_25x25"":""http:\/\/ny-image2.etsy.com\/il_25x25.75420206.jpg"",""image_url_50x50"":""http:\/\/ny-image2.etsy.com\/il_50x50.75420206.jpg"",""image_url_75x75"":""http:\/\/ny-image2.etsy.com\/il_75x75.75420206.jpg"",""image_url_155x125"":""http:\/\/ny-image2.etsy.com\/il_155x125.75420206.jpg"",""image_url_200x200"":""http:\/\/ny-image2.etsy.com\/il_200x200.75420206.jpg"",""image_url_430xN"":""http:\/\/ny-image2.etsy.com\/il_430xN.75420206.jpg"",""hsv_color"":""0;0;100"",""rgb_color"":""#FFFFFF""},{""image_url_25x25"":""http:\/\/ny-image2.etsy.com\/il_25x25.75420230.jpg"",""image_url_50x50"":""http:\/\/ny-image2.etsy.com\/il_50x50.75420230.jpg"",""image_url_75x75"":""http:\/\/ny-image2.etsy.com\/il_75x75.75420230.jpg"",""image_url_155x125"":""http:\/\/ny-image2.etsy.com\/il_155x125.75420230.jpg"",""image_url_200x200"":""http:\/\/ny-image2.etsy.com\/il_200x200.75420230.jpg"",""image_url_430xN"":""http:\/\/ny-image2.etsy.com\/il_430xN.75420230.jpg"",""hsv_color"":""0;0;100"",""rgb_color"":""#FFFFFF""},{""image_url_25x25"":""http:\/\/ny-image0.etsy.com\/il_25x25.75420248.jpg"",""image_url_50x50"":""http:\/\/ny-image0.etsy.com\/il_50x50.75420248.jpg"",""image_url_75x75"":""http:\/\/ny-image0.etsy.com\/il_75x75.75420248.jpg"",""image_url_155x125"":""http:\/\/ny-image0.etsy.com\/il_155x125.75420248.jpg"",""image_url_200x200"":""http:\/\/ny-image0.etsy.com\/il_200x200.75420248.jpg"",""image_url_430xN"":""http:\/\/ny-image0.etsy.com\/il_430xN.75420248.jpg"",""hsv_color"":""0;0;100"",""rgb_color"":""#FFFFFF""},{""image_url_25x25"":""http:\/\/ny-image2.etsy.com\/il_25x25.75420274.jpg"",""image_url_50x50"":""http:\/\/ny-image2.etsy.com\/il_50x50.75420274.jpg"",""image_url_75x75"":""http:\/\/ny-image2.etsy.com\/il_75x75.75420274.jpg"",""image_url_155x125"":""http:\/\/ny-image2.etsy.com\/il_155x125.75420274.jpg"",""image_url_200x200"":""http:\/\/ny-image2.etsy.com\/il_200x200.75420274.jpg"",""image_url_430xN"":""http:\/\/ny-image2.etsy.com\/il_430xN.75420274.jpg"",""hsv_color"":""0;0;100"",""rgb_color"":""#FFFFFF""}]},{""listing_id"":25806131,""state"":""active"",""title"":""Durban - Mens Stumpunk waistcoat"",""url"":""http:\/\/www.etsy.com\/view_listing.php?listing_id=25806131"",""image_url_25x25"":""http:\/\/ny-image0.etsy.com\/il_25x25.73270868.jpg"",""image_url_50x50"":""http:\/\/ny-image0.etsy.com\/il_50x50.73270868.jpg"",""image_url_75x75"":""http:\/\/ny-image0.etsy.com\/il_75x75.73270868.jpg"",""image_url_155x125"":""http:\/\/ny-image0.etsy.com\/il_155x125.73270868.jpg"",""image_url_200x200"":""http:\/\/ny-image0.etsy.com\/il_200x200.73270868.jpg"",""image_url_430xN"":""http:\/\/ny-image0.etsy.com\/il_430xN.73270868.jpg"",""creation_epoch"":1247867743.85,""views"":452,""tags"":[""clothing"",""costume"",""victorian"",""steampunk"",""men"",""vest"",""waistcoat"",""uk"",""embroidery"",""cogs"",""red"",""hand_embroidery"",""geekery"",""bfsc""],""materials"":[""cotton"",""copper_buttons"",""corduroy"",""thread"",""stranded_cotton""],""price"":140,""currency_code"":""USD"",""ending_epoch"":1258494943.85,""hsv_color"":""0;0;100"",""rgb_color"":""#FFFFFF"",""user_id"":1234,""user_name"":""Fred"",""quantity"":1,""description"":""The Fredster"",""section_id"":6177344,""section_title"":""Waistcoats"",""lat"":51.4985,""lon"":-0.1318,""user_image_id"":6043378,""city"":""London"",""all_images"":[{""image_url_25x25"":""http:\/\/ny-image0.etsy.com\/il_25x25.73270868.jpg"",""image_url_50x50"":""http:\/\/ny-image0.etsy.com\/il_50x50.73270868.jpg"",""image_url_75x75"":""http:\/\/ny-image0.etsy.com\/il_75x75.73270868.jpg"",""image_url_155x125"":""http:\/\/ny-image0.etsy.com\/il_155x125.73270868.jpg"",""image_url_200x200"":""http:\/\/ny-image0.etsy.com\/il_200x200.73270868.jpg"",""image_url_430xN"":""http:\/\/ny-image0.etsy.com\/il_430xN.73270868.jpg"",""hsv_color"":""0;0;100"",""rgb_color"":""#FFFFFF""},{""image_url_25x25"":""http:\/\/ny-image2.etsy.com\/il_25x25.73270914.jpg"",""image_url_50x50"":""http:\/\/ny-image2.etsy.com\/il_50x50.73270914.jpg"",""image_url_75x75"":""http:\/\/ny-image2.etsy.com\/il_75x75.73270914.jpg"",""image_url_155x125"":""http:\/\/ny-image2.etsy.com\/il_155x125.73270914.jpg"",""image_url_200x200"":""http:\/\/ny-image2.etsy.com\/il_200x200.73270914.jpg"",""image_url_430xN"":""http:\/\/ny-image2.etsy.com\/il_430xN.73270914.jpg"",""hsv_color"":""0;0;100"",""rgb_color"":""#FFFFFF""},{""image_url_25x25"":""http:\/\/ny-image1.etsy.com\/il_25x25.73270985.jpg"",""image_url_50x50"":""http:\/\/ny-image1.etsy.com\/il_50x50.73270985.jpg"",""image_url_75x75"":""http:\/\/ny-image1.etsy.com\/il_75x75.73270985.jpg"",""image_url_155x125"":""http:\/\/ny-image1.etsy.com\/il_155x125.73270985.jpg"",""image_url_200x200"":""http:\/\/ny-image1.etsy.com\/il_200x200.73270985.jpg"",""image_url_430xN"":""http:\/\/ny-image1.etsy.com\/il_430xN.73270985.jpg"",""hsv_color"":""0;0;100"",""rgb_color"":""#FFFFFF""},{""image_url_25x25"":""http:\/\/ny-image2.etsy.com\/il_25x25.73271102.jpg"",""image_url_50x50"":""http:\/\/ny-image2.etsy.com\/il_50x50.73271102.jpg"",""image_url_75x75"":""http:\/\/ny-image2.etsy.com\/il_75x75.73271102.jpg"",""image_url_155x125"":""http:\/\/ny-image2.etsy.com\/il_155x125.73271102.jpg"",""image_url_200x200"":""http:\/\/ny-image2.etsy.com\/il_200x200.73271102.jpg"",""image_url_430xN"":""http:\/\/ny-image2.etsy.com\/il_430xN.73271102.jpg"",""hsv_color"":""0;0;100"",""rgb_color"":""#FFFFFF""},{""image_url_25x25"":""http:\/\/ny-image1.etsy.com\/il_25x25.73271169.jpg"",""image_url_50x50"":""http:\/\/ny-image1.etsy.com\/il_50x50.73271169.jpg"",""image_url_75x75"":""http:\/\/ny-image1.etsy.com\/il_75x75.73271169.jpg"",""image_url_155x125"":""http:\/\/ny-image1.etsy.com\/il_155x125.73271169.jpg"",""image_url_200x200"":""http:\/\/ny-image1.etsy.com\/il_200x200.73271169.jpg"",""image_url_430xN"":""http:\/\/ny-image1.etsy.com\/il_430xN.73271169.jpg"",""hsv_color"":""0;0;100"",""rgb_color"":""#FFFFFF""}]},{""listing_id"":25452560,""state"":""active"",""title"":""Jeparit - Mens period waistcoat"",""url"":""http:\/\/www.etsy.com\/view_listing.php?listing_id=25452560"",""image_url_25x25"":""http:\/\/ny-image2.etsy.com\/il_25x25.72089194.jpg"",""image_url_50x50"":""http:\/\/ny-image2.etsy.com\/il_50x50.72089194.jpg"",""image_url_75x75"":""http:\/\/ny-image2.etsy.com\/il_75x75.72089194.jpg"",""image_url_155x125"":""http:\/\/ny-image2.etsy.com\/il_155x125.72089194.jpg"",""image_url_200x200"":""http:\/\/ny-image2.etsy.com\/il_200x200.72089194.jpg"",""image_url_430xN"":""http:\/\/ny-image2.etsy.com\/il_430xN.72089194.jpg"",""creation_epoch"":1246625892.37,""views"":312,""tags"":[""clothing"",""sewing"",""costume"",""vest"",""men"",""gothic"",""victorian"",""necktie"",""cravat"",""goth"",""steampunk"",""uk"",""geekery"",""bfsc""],""materials"":[""satin"",""fabric"",""buttons"",""lining""],""price"":130,""currency_code"":""USD"",""ending_epoch"":1257253092.37,""hsv_color"":""0;0;100"",""rgb_color"":""#FFFFFF"",""user_id"":1234,""user_name"":""Fred"",""quantity"":1,""description"":""Fred2"",""section_id"":6177344,""section_title"":""Waistcoats"",""lat"":51.4985,""lon"":-0.1318,""user_image_id"":6043378,""city"":""London"",""all_images"":[{""image_url_25x25"":""http:\/\/ny-image2.etsy.com\/il_25x25.72089194.jpg"",""image_url_50x50"":""http:\/\/ny-image2.etsy.com\/il_50x50.72089194.jpg"",""image_url_75x75"":""http:\/\/ny-image2.etsy.com\/il_75x75.72089194.jpg"",""image_url_155x125"":""http:\/\/ny-image2.etsy.com\/il_155x125.72089194.jpg"",""image_url_200x200"":""http:\/\/ny-image2.etsy.com\/il_200x200.72089194.jpg"",""image_url_430xN"":""http:\/\/ny-image2.etsy.com\/il_430xN.72089194.jpg"",""hsv_color"":""0;0;100"",""rgb_color"":""#FFFFFF""},{""image_url_25x25"":""http:\/\/ny-image0.etsy.com\/il_25x25.72089268.jpg"",""image_url_50x50"":""http:\/\/ny-image0.etsy.com\/il_50x50.72089268.jpg"",""image_url_75x75"":""http:\/\/ny-image0.etsy.com\/il_75x75.72089268.jpg"",""image_url_155x125"":""http:\/\/ny-image0.etsy.com\/il_155x125.72089268.jpg"",""image_url_200x200"":""http:\/\/ny-image0.etsy.com\/il_200x200.72089268.jpg"",""image_url_430xN"":""http:\/\/ny-image0.etsy.com\/il_430xN.72089268.jpg"",""hsv_color"":""0;0;100"",""rgb_color"":""#FFFFFF""},{""image_url_25x25"":""http:\/\/ny-image0.etsy.com\/il_25x25.72089288.jpg"",""image_url_50x50"":""http:\/\/ny-image0.etsy.com\/il_50x50.72089288.jpg"",""image_url_75x75"":""http:\/\/ny-image0.etsy.com\/il_75x75.72089288.jpg"",""image_url_155x125"":""http:\/\/ny-image0.etsy.com\/il_155x125.72089288.jpg"",""image_url_200x200"":""http:\/\/ny-image0.etsy.com\/il_200x200.72089288.jpg"",""image_url_430xN"":""http:\/\/ny-image0.etsy.com\/il_430xN.72089288.jpg"",""hsv_color"":""0;0;100"",""rgb_color"":""#FFFFFF""}]}],""params"":{""user_id"":1234,""detail_level"":""high""},""type"":""listing""}";

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

        /// <summary>
        /// Test parsing three listings high detail response
        /// </summary>
        [TestMethod]
        public void ThreeListingsHighDetailResponseParse()
        {
            Listings listings = ThreeListingsHighDetailResponse.Deserialize<Listings>();

            Assert.IsNotNull(listings);
            Assert.AreEqual(3, listings.Count);

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
