//-----------------------------------------------------------------------
// <copyright file="ShopParseTest.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.DataModel.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Netsy.DataModel.ShopData;
    using Netsy.Helpers;

    /// <summary>
    /// Test parsing of shop data 
    /// </summary>
    [TestClass]
    public class ShopParseTest
    {
        /// <summary>
        /// Data on shops with low detail level
        /// </summary>
        public const string ShopLowDetailData = @"{""count"":1,""results"":[{""user_name"":""Fred"",""user_id"":1234,""url"":""http:\/\/www.etsy.com\/shop.php?user_id=1234"",""image_url_25x25"":""http:\/\/ny-image2.etsy.com\/iusa_25x25.6043378.jpg"",""image_url_30x30"":""http:\/\/ny-image2.etsy.com\/iusa_30x30.6043378.jpg"",""image_url_50x50"":""http:\/\/ny-image2.etsy.com\/iusa_50x50.6043378.jpg"",""image_url_75x75"":""http:\/\/ny-image2.etsy.com\/iusa_75x75.6043378.jpg"",""join_epoch"":1242567472.15,""city"":""London"",""banner_image_url"":""http:\/\/ny-image0.etsy.com\/iusb_760x100.6367692.jpg"",""last_updated_epoch"":1247867743.85,""creation_epoch"":1242575846.16,""listing_count"":8}],""params"":{""user_id"":1234,""detail_level"":""low""},""type"":""shop""}";

        /// <summary>
        /// Data on shops with medium detail level
        /// </summary>
        public const string ShopMediumDetailData = @"{""count"":1,""results"":[{""user_name"":""Fred"",""user_id"":1234,""url"":""http:\/\/www.etsy.com\/shop.php?user_id=1234"",""image_url_25x25"":""http:\/\/ny-image2.etsy.com\/iusa_25x25.6043378.jpg"",""image_url_30x30"":""http:\/\/ny-image2.etsy.com\/iusa_30x30.6043378.jpg"",""image_url_50x50"":""http:\/\/ny-image2.etsy.com\/iusa_50x50.6043378.jpg"",""image_url_75x75"":""http:\/\/ny-image2.etsy.com\/iusa_75x75.6043378.jpg"",""join_epoch"":1242567472.15,""city"":""London"",""gender"":""private"",""lat"":51.4985,""lon"":-0.1318,""transaction_buy_count"":1,""transaction_sold_count"":2,""is_seller"":true,""was_featured_seller"":false,""materials"":[""Fabric"",""buttons"",""cogs"",""brass"",""lace"",""satin"",""cotton""],""last_login_epoch"":1248902764.22,""banner_image_url"":""http:\/\/ny-image0.etsy.com\/iusb_760x100.6367692.jpg"",""last_updated_epoch"":1249293900.49,""creation_epoch"":1242575846.16,""listing_count"":7,""shop_name"":""Fred"",""title"":""Freds clothing"",""sale_message"":""Thank you for purchasing from Fred.""}],""params"":{""user_id"":1234,""detail_level"":""medium""},""type"":""shop""}";

        /// <summary>
        /// Data on shops with high detail level
        /// </summary>
        public const string ShopHighDetailData = @"{""count"":1,""results"":[{""user_name"":""Fred"",""user_id"":1234,""url"":""http:\/\/www.etsy.com\/shop.php?user_id=1234"",""image_url_25x25"":""http:\/\/ny-image2.etsy.com\/iusa_25x25.6043378.jpg"",""image_url_30x30"":""http:\/\/ny-image2.etsy.com\/iusa_30x30.6043378.jpg"",""image_url_50x50"":""http:\/\/ny-image2.etsy.com\/iusa_50x50.6043378.jpg"",""image_url_75x75"":""http:\/\/ny-image2.etsy.com\/iusa_75x75.6043378.jpg"",""join_epoch"":1242567472.15,""city"":""London"",""gender"":""private"",""lat"":51.4985,""lon"":-0.1318,""transaction_buy_count"":1,""transaction_sold_count"":2,""is_seller"":true,""was_featured_seller"":false,""materials"":[""Fabric"",""buttons"",""cogs"",""brass"",""lace"",""satin"",""cotton""],""last_login_epoch"":1248902764.22,""feedback_count"":""1"",""feedback_percent_positive"":""100"",""referred_user_count"":0,""birth_day"":null,""birth_month"":null,""bio"":""Fred was here."",""banner_image_url"":""http:\/\/ny-image0.etsy.com\/iusb_760x100.6367692.jpg"",""last_updated_epoch"":1249293900.49,""creation_epoch"":1242575846.16,""listing_count"":7,""shop_name"":""Fred"",""title"":""Freds clothing"",""sale_message"":""Thank you for purchasing from fred."",""announcement"":""Fred has a shop"",""is_vacation"":"""",""vacation_message"":"""",""currency_code"":""USD"",""policy_welcome"":""Welcome to Freds shop."",""policy_payment"":""We only accept payment."",""policy_shipping"":""All our shipping prices are calculated."",""policy_refunds"":""Fred"",""policy_additional"":""Bespoken."",""sections"":[{""section_id"":6177343,""title"":""Bags"",""listing_count"":3},{""section_id"":6177344,""title"":""Waistcoats"",""listing_count"":3}]}],""params"":{""user_id"":1234,""detail_level"":""high""},""type"":""shop""}";
        
        /// <summary>
        /// Test that the shops can be parsed
        /// </summary>
        [TestMethod]
        public void ShopLowDetailParseTest()
        {
            Shops shops = ShopLowDetailData.Deserialize<Shops>();

            Assert.IsNotNull(shops);
            Assert.AreEqual(1, shops.Count);
            Assert.AreEqual(1234, shops.Params.UserId);
            Assert.AreEqual(DetailLevel.Low, shops.Params.DetailLevelEnum);

            Shop shop1 = shops.Results[0];

            Assert.AreEqual("Fred", shop1.UserName);
            Assert.AreEqual(1234, shop1.UserId);

            Assert.AreEqual("http://www.etsy.com/shop.php?user_id=1234", shop1.Url);
            Assert.AreEqual("http://ny-image0.etsy.com/iusb_760x100.6367692.jpg", shop1.BannerImageUrl);
        }

        /// <summary>
        /// Test that the shops can be parsed
        /// </summary>
        [TestMethod]
        public void ShopMediumDetailParseTest()
        {
            Shops shops = ShopMediumDetailData.Deserialize<Shops>();

            Assert.IsNotNull(shops);
            Assert.AreEqual(1, shops.Count);
            Assert.AreEqual(1234, shops.Params.UserId);
            Assert.AreEqual(DetailLevel.Medium, shops.Params.DetailLevelEnum);

            Shop shop1 = shops.Results[0];

            Assert.AreEqual("Fred", shop1.UserName);
            Assert.AreEqual(1234, shop1.UserId);

            Assert.AreEqual("http://www.etsy.com/shop.php?user_id=1234", shop1.Url);
            Assert.AreEqual("http://ny-image0.etsy.com/iusb_760x100.6367692.jpg", shop1.BannerImageUrl);
        }

        [TestMethod]
        public void ShopHighDetailParseTest()
        {
            Shops shops = ShopHighDetailData.Deserialize<Shops>();

            Assert.IsNotNull(shops);
            Assert.AreEqual(1, shops.Count);
            Assert.AreEqual(1234, shops.Params.UserId);
            Assert.AreEqual(DetailLevel.High, shops.Params.DetailLevelEnum);

            Shop shop1 = shops.Results[0];

            Assert.AreEqual("Fred", shop1.UserName);
            Assert.AreEqual(1234, shop1.UserId);

            Assert.AreEqual("http://www.etsy.com/shop.php?user_id=1234", shop1.Url);
            Assert.AreEqual("http://ny-image0.etsy.com/iusb_760x100.6367692.jpg", shop1.BannerImageUrl);
        }
    }
}
