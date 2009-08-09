//-----------------------------------------------------------------------
// <copyright file="UserParseTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.DataModel.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Netsy.DataModel.UserData;
    using Netsy.Helpers;

    /// <summary>
    /// Test parsing string Json data into user details
    /// </summary>
    [TestClass]
    public class UserParseTest
    {
        /// <summary>
        /// Data for one user
        /// </summary>
        private const string UserDetails = @"{""user_name"":""Fred"",""user_id"":1234,""url"":""http:\/\/www.etsy.com\/shop.php?user_id=1234"",""image_url_25x25"":""http:\/\/ny-image2.etsy.com\/iusa_25x25.6043378.jpg"",""image_url_30x30"":""http:\/\/ny-image2.etsy.com\/iusa_30x30.6043378.jpg"",""image_url_50x50"":""http:\/\/ny-image2.etsy.com\/iusa_50x50.6043378.jpg"",""image_url_75x75"":""http:\/\/ny-image2.etsy.com\/iusa_75x75.6043378.jpg"",""join_epoch"":142567472.16,""city"":""London""}";

        /// <summary>
        /// A sample response text containing one user, low detail level
        /// </summary>
        private const string UsersLowDetailResponse = @"{""count"":1,""results"":[{""user_name"":""Fred"",""user_id"":1234,""url"":""http:\/\/www.etsy.com\/shop.php?user_id=1234"",""image_url_25x25"":""http:\/\/ny-image2.etsy.com\/iusa_25x25.6043378.jpg"",""image_url_30x30"":""http:\/\/ny-image2.etsy.com\/iusa_30x30.6043378.jpg"",""image_url_50x50"":""http:\/\/ny-image2.etsy.com\/iusa_50x50.6043378.jpg"",""image_url_75x75"":""http:\/\/ny-image2.etsy.com\/iusa_75x75.6043378.jpg"",""join_epoch"":142567472.16,""city"":""London""}],""params"":{""user_id"":1234,""detail_level"":""low""},""type"":""user""}";

        /// <summary>
        /// A sample response text containing one user, medium detail level
        /// </summary>
        private const string UsersMediumDetailResponse = @"{""count"":1,""results"":[{""user_name"":""Fred"",""user_id"":1234,""url"":""http:\/\/www.etsy.com\/shop.php?user_id=1234"",""image_url_25x25"":""http:\/\/ny-image2.etsy.com\/iusa_25x25.6043378.jpg"",""image_url_30x30"":""http:\/\/ny-image2.etsy.com\/iusa_30x30.6043378.jpg"",""image_url_50x50"":""http:\/\/ny-image2.etsy.com\/iusa_50x50.6043378.jpg"",""image_url_75x75"":""http:\/\/ny-image2.etsy.com\/iusa_75x75.6043378.jpg"",""join_epoch"":1242567472.15,""city"":""London"",""gender"":""private"",""lat"":51.4985,""lon"":-0.1318,""transaction_buy_count"":1,""transaction_sold_count"":2,""is_seller"":true,""was_featured_seller"":false,""materials"":[""Fabric"",""buttons"",""cogs"",""brass"",""lace"",""satin"",""cotton""],""last_login_epoch"":1248902764.22}],""params"":{""user_id"":1234,""detail_level"":""medium""},""type"":""user""}";

        /// <summary>
        /// A sample response text containing one user, medium detail level, with no lat or long
        /// </summary>
        private const string UsersMediumDetailNoLatLongResponse = @"{""count"":1,""results"":[{""user_name"":""Fred"",""user_id"":1234,""url"":""http:\/\/www.etsy.com\/shop.php?user_id=1234"",""image_url_25x25"":""http:\/\/ny-image2.etsy.com\/iusa_25x25.6043378.jpg"",""image_url_30x30"":""http:\/\/ny-image2.etsy.com\/iusa_30x30.6043378.jpg"",""image_url_50x50"":""http:\/\/ny-image2.etsy.com\/iusa_50x50.6043378.jpg"",""image_url_75x75"":""http:\/\/ny-image2.etsy.com\/iusa_75x75.6043378.jpg"",""join_epoch"":1242567472.15,""city"":""London"",""gender"":""private"",""lat"":null,""lon"":null,""transaction_buy_count"":1,""transaction_sold_count"":2,""is_seller"":true,""was_featured_seller"":false,""materials"":[""Fabric"",""buttons"",""cogs"",""brass"",""lace"",""satin"",""cotton""],""last_login_epoch"":1248902764.22}],""params"":{""user_id"":1234,""detail_level"":""medium""},""type"":""user""}";

        /// <summary>
        /// A sample response text containing three users, medium detail level
        /// </summary>
        private const string ThreeUsersMediumDetailResponse = @"{""count"":654,""results"":[{""user_name"":""freddae1"",""user_id"":7836538,""url"":""http:\/\/www.etsy.com\/profile.php?user_id=7836538"",""image_url_25x25"":""http:\/\/www.etsy.com\/images\/grey.gif"",""image_url_30x30"":""http:\/\/www.etsy.com\/images\/grey.gif"",""image_url_50x50"":""http:\/\/www.etsy.com\/images\/grey.gif"",""image_url_75x75"":""http:\/\/www.etsy.com\/images\/grey.gif"",""join_epoch"":1249680875.83,""city"":"""",""gender"":""private"",""lat"":null,""lon"":null,""transaction_buy_count"":1,""transaction_sold_count"":0,""is_seller"":false,""was_featured_seller"":false,""materials"":[],""last_login_epoch"":1249680895.31},{""user_name"":""freddiemarie"",""user_id"":7836190,""url"":""http:\/\/www.etsy.com\/profile.php?user_id=7836190"",""image_url_25x25"":""http:\/\/www.etsy.com\/images\/grey.gif"",""image_url_30x30"":""http:\/\/www.etsy.com\/images\/grey.gif"",""image_url_50x50"":""http:\/\/www.etsy.com\/images\/grey.gif"",""image_url_75x75"":""http:\/\/www.etsy.com\/images\/grey.gif"",""join_epoch"":1249677230.11,""city"":"""",""gender"":""private"",""lat"":null,""lon"":null,""transaction_buy_count"":0,""transaction_sold_count"":0,""is_seller"":false,""was_featured_seller"":false,""materials"":[],""last_login_epoch"":null},{""user_name"":""FrederickandCameos"",""user_id"":7825591,""url"":""http:\/\/www.etsy.com\/shop.php?user_id=7825591"",""image_url_25x25"":""http:\/\/www.etsy.com\/images\/grey.gif"",""image_url_30x30"":""http:\/\/www.etsy.com\/images\/grey.gif"",""image_url_50x50"":""http:\/\/www.etsy.com\/images\/grey.gif"",""image_url_75x75"":""http:\/\/www.etsy.com\/images\/grey.gif"",""join_epoch"":1249522655.16,""city"":"""",""gender"":""private"",""lat"":null,""lon"":null,""transaction_buy_count"":0,""transaction_sold_count"":0,""is_seller"":true,""was_featured_seller"":false,""materials"":[],""last_login_epoch"":1249522702.26}],""params"":{""search_name"":""Fred"",""offset"":0,""limit"":3,""detail_level"":""medium""},""type"":""user""}";
        
        /// <summary>
        /// A sample response text containing one user, high detail level
        /// </summary>
        private const string UsersHighDetailResponse = @"{""count"":1,""results"":[{""user_name"":""Fred"",""user_id"":1234,""url"":""http:\/\/www.etsy.com\/shop.php?user_id=1234"",""image_url_25x25"":""http:\/\/ny-image2.etsy.com\/iusa_25x25.6043378.jpg"",""image_url_30x30"":""http:\/\/ny-image2.etsy.com\/iusa_30x30.6043378.jpg"",""image_url_50x50"":""http:\/\/ny-image2.etsy.com\/iusa_50x50.6043378.jpg"",""image_url_75x75"":""http:\/\/ny-image2.etsy.com\/iusa_75x75.6043378.jpg"",""join_epoch"":1242567472.15,""city"":""London"",""gender"":""private"",""lat"":51.4985,""lon"":-0.1318,""transaction_buy_count"":1,""transaction_sold_count"":2,""is_seller"":true,""was_featured_seller"":false,""materials"":[""Fabric"",""buttons"",""cogs"",""brass"",""lace"",""satin"",""cotton""],""last_login_epoch"":1248902764.22,""feedback_count"":""1"",""feedback_percent_positive"":""100"",""referred_user_count"":0,""birth_day"":null,""birth_month"":null,""bio"":""Fred was here""}],""params"":{""user_id"":1234,""detail_level"":""high""},""type"":""user""}";

        /// <summary>
        /// A sample response containing three users, high detail level
        /// </summary>
        private const string ThreeUsersHighDetailResponse = @"{""count"":654,""results"":[{""user_name"":""freddae1"",""user_id"":7836538,""url"":""http:\/\/www.etsy.com\/profile.php?user_id=7836538"",""image_url_25x25"":""http:\/\/www.etsy.com\/images\/grey.gif"",""image_url_30x30"":""http:\/\/www.etsy.com\/images\/grey.gif"",""image_url_50x50"":""http:\/\/www.etsy.com\/images\/grey.gif"",""image_url_75x75"":""http:\/\/www.etsy.com\/images\/grey.gif"",""join_epoch"":1249680875.83,""city"":null,""gender"":""private"",""lat"":null,""lon"":null,""transaction_buy_count"":1,""transaction_sold_count"":0,""is_seller"":false,""was_featured_seller"":false,""materials"":[],""last_login_epoch"":1249680895.31,""feedback_count"":""0"",""feedback_percent_positive"":null,""referred_user_count"":0,""birth_day"":null,""birth_month"":null,""bio"":null},{""user_name"":""freddiemarie"",""user_id"":7836190,""url"":""http:\/\/www.etsy.com\/profile.php?user_id=7836190"",""image_url_25x25"":""http:\/\/www.etsy.com\/images\/grey.gif"",""image_url_30x30"":""http:\/\/www.etsy.com\/images\/grey.gif"",""image_url_50x50"":""http:\/\/www.etsy.com\/images\/grey.gif"",""image_url_75x75"":""http:\/\/www.etsy.com\/images\/grey.gif"",""join_epoch"":1249677230.11,""city"":null,""gender"":""private"",""lat"":null,""lon"":null,""transaction_buy_count"":0,""transaction_sold_count"":0,""is_seller"":false,""was_featured_seller"":false,""materials"":[],""last_login_epoch"":null,""feedback_count"":""0"",""feedback_percent_positive"":null,""referred_user_count"":0,""birth_day"":null,""birth_month"":null,""bio"":null},{""user_name"":""FrederickandCameos"",""user_id"":7825591,""url"":""http:\/\/www.etsy.com\/shop.php?user_id=7825591"",""image_url_25x25"":""http:\/\/www.etsy.com\/images\/grey.gif"",""image_url_30x30"":""http:\/\/www.etsy.com\/images\/grey.gif"",""image_url_50x50"":""http:\/\/www.etsy.com\/images\/grey.gif"",""image_url_75x75"":""http:\/\/www.etsy.com\/images\/grey.gif"",""join_epoch"":1249522655.16,""city"":null,""gender"":""private"",""lat"":null,""lon"":null,""transaction_buy_count"":0,""transaction_sold_count"":0,""is_seller"":true,""was_featured_seller"":false,""materials"":[],""last_login_epoch"":1249522702.26,""feedback_count"":""0"",""feedback_percent_positive"":null,""referred_user_count"":0,""birth_day"":null,""birth_month"":null,""bio"":null}],""params"":{""search_name"":""Fred"",""offset"":0,""limit"":3,""detail_level"":""high""},""type"":""user""}";

        /// <summary>
        /// Test parsing a user details
        /// </summary>
        [TestMethod]
        public void UserLowDetailParseTest()
        {
            User user = UserDetails.Deserialize<User>();

            Assert.IsNotNull(user);
            Assert.AreEqual("Fred", user.UserName);
            Assert.AreEqual(1234, user.UserId);

            Assert.AreEqual(@"http://www.etsy.com/shop.php?user_id=1234", user.Url);
            Assert.AreEqual(@"http://ny-image2.etsy.com/iusa_25x25.6043378.jpg", user.ImageUrl25x25);
            Assert.AreEqual(@"http://ny-image2.etsy.com/iusa_30x30.6043378.jpg", user.ImageUrl30x30);
            Assert.AreEqual(@"http://ny-image2.etsy.com/iusa_50x50.6043378.jpg", user.ImageUrl50x50);
            Assert.AreEqual(@"http://ny-image2.etsy.com/iusa_75x75.6043378.jpg", user.ImageUrl75x75);

            Assert.AreEqual(142567472.16, user.JoinEpoch);
            Assert.AreEqual("London", user.City);
        }

        /// <summary>
        /// Test parsing a users low details response
        /// </summary>
        [TestMethod]
        public void UsersLowDetailResponseParse()
        {
            Users users = UsersLowDetailResponse.Deserialize<Users>();

            Assert.IsNotNull(users);
            Assert.AreEqual(1, users.Count);

            User user1 = users.Results[0];

            Assert.IsNotNull(user1);
            Assert.AreEqual("Fred", user1.UserName);
            Assert.AreEqual("London", user1.City);
            Assert.AreEqual(1234, user1.UserId);

            QueryParams queryParams = users.Params;
            Assert.IsNotNull(queryParams);
            Assert.AreEqual(DetailLevel.Low, queryParams.DetailLevelEnum);
            Assert.AreEqual(1234, queryParams.UserId);
        }

        /// <summary>
        /// Test parsing a users medium detail response
        /// </summary>
        [TestMethod]
        public void UsersMediumDetailResponseParse()
        {
            Users users = UsersMediumDetailResponse.Deserialize<Users>();

            Assert.IsNotNull(users);
            Assert.AreEqual(1, users.Count);

            User user1 = users.Results[0];

            Assert.IsNotNull(user1);
            Assert.AreEqual("Fred", user1.UserName);
            Assert.AreEqual("London", user1.City);
            Assert.AreEqual(1234, user1.UserId);

            QueryParams queryParams = users.Params;
            Assert.IsNotNull(queryParams);
            Assert.AreEqual(DetailLevel.Medium, queryParams.DetailLevelEnum);
            Assert.AreEqual(1234, queryParams.UserId);
        }

        /// <summary>
        /// Test parsing a users med details response with null lat and long
        /// </summary>
        [TestMethod]
        public void UsersMedDetailResponseNoLatLongParse()
        {
            Users users = UsersMediumDetailNoLatLongResponse.Deserialize<Users>();

            Assert.IsNotNull(users);
            Assert.AreEqual(1, users.Count);

            User user1 = users.Results[0];

            Assert.IsNotNull(user1);
            Assert.AreEqual("Fred", user1.UserName);
            Assert.AreEqual("London", user1.City);
            Assert.AreEqual(1234, user1.UserId);

            QueryParams queryParams = users.Params;
            Assert.IsNotNull(queryParams);
            Assert.AreEqual(DetailLevel.Medium, queryParams.DetailLevelEnum);
            Assert.AreEqual(1234, queryParams.UserId);
        }

        /// <summary>
        /// Test parsing three users med details response
        /// </summary>
        [TestMethod]
        public void ThreeUsersMedDetailResponseParse()
        {
            Users users = ThreeUsersMediumDetailResponse.Deserialize<Users>();

            Assert.IsNotNull(users);
            Assert.AreEqual(654, users.Count);

            User user1 = users.Results[0];

            Assert.IsNotNull(user1);
            Assert.AreEqual("freddae1", user1.UserName);
            Assert.IsTrue(string.IsNullOrEmpty(user1.City));
            Assert.AreEqual(7836538, user1.UserId);

            QueryParams queryParams = users.Params;
            Assert.IsNotNull(queryParams);
            Assert.AreEqual(DetailLevel.Medium, queryParams.DetailLevelEnum);
            Assert.AreEqual(0, queryParams.UserId);
        }
        
        /// <summary>
        /// Test parsing a users high detail response
        /// </summary>
        [TestMethod]
        public void UsersHighDetailResponseParse()
        {
            Users users = UsersHighDetailResponse.Deserialize<Users>();

            Assert.IsNotNull(users);
            Assert.AreEqual(1, users.Count);

            User user1 = users.Results[0];

            Assert.IsNotNull(user1);
            Assert.AreEqual("Fred", user1.UserName);
            Assert.AreEqual("London", user1.City);
            Assert.AreEqual(1234, user1.UserId);

            QueryParams queryParams = users.Params;
            Assert.IsNotNull(queryParams);
            Assert.AreEqual(DetailLevel.High, queryParams.DetailLevelEnum);
            Assert.AreEqual(1234, queryParams.UserId);
        }

        /// <summary>
        /// Test parsing three users med details response
        /// </summary>
        [TestMethod]
        public void ThreeUsersHighDetailResponseParse()
        {
            Users users = ThreeUsersHighDetailResponse.Deserialize<Users>();

            Assert.IsNotNull(users);
            Assert.AreEqual(654, users.Count);

            User user1 = users.Results[0];

            Assert.IsNotNull(user1);
            Assert.AreEqual("freddae1", user1.UserName);
            Assert.IsTrue(string.IsNullOrEmpty(user1.City));
            Assert.AreEqual(7836538, user1.UserId);

            QueryParams queryParams = users.Params;
            Assert.IsNotNull(queryParams);
            Assert.AreEqual(DetailLevel.High, queryParams.DetailLevelEnum);
            Assert.AreEqual(0, queryParams.UserId);
        }
    }
}
