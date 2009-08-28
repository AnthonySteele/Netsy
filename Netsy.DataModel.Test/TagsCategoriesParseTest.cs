//-----------------------------------------------------------------------
// <copyright file="TagsParseTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel.Test
{
    using Netsy.Helpers;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test parsing of tags data 
    /// </summary>
    [TestClass]
    public class TagsCategoriesParseTest
    {
        /// <summary>
        /// The sample server response top-level tags data 
        /// </summary>
        private const string TagsData = @"{""count"":31,""results"":[""accessories"",""art"",""bags_and_purses"",""bath_and_beauty"",""books_and_zines"",""candles"",""ceramics_and_pottery"",""children"",""clothing"",""crochet"",""dolls_and_miniatures"",""everything_else"",""furniture"",""geekery"",""glass"",""holidays"",""housewares"",""jewelry"",""knitting"",""music"",""needlecraft"",""paper_goods"",""patterns"",""pets"",""plants_and_edibles"",""quilts"",""supplies"",""toys"",""vintage"",""weddings"",""woodworking""],""params"":null,""type"":""tag""}";

        /// <summary>
        /// The sample server response child tags data 
        /// </summary>
        private const string ChildTagsData = @"{""count"":14,""results"":[""aceo"",""collage"",""drawing"",""fiber_art"",""illustration"",""mixed_media"",""original_illustration"",""original_painting"",""painting"",""photography"",""print"",""printmaking"",""reproduction"",""sculpture""],""params"":{""tag"":""art""},""type"":""tag""}";

        /// <summary>
        /// The sample server response Categories data 
        /// </summary>
        private const string CategoriesData = @"{""count"":31,""results"":[""accessories"",""art"",""bags_and_purses"",""bath_and_beauty"",""books_and_zines"",""candles"",""ceramics_and_pottery"",""children"",""clothing"",""crochet"",""dolls_and_miniatures"",""everything_else"",""furniture"",""geekery"",""glass"",""holidays"",""housewares"",""jewelry"",""knitting"",""music"",""needlecraft"",""paper_goods"",""patterns"",""pets"",""plants_and_edibles"",""quilts"",""supplies"",""toys"",""vintage"",""weddings"",""woodworking""],""params"":null,""type"":""category""}";
        
        /// <summary>
        /// The sample server response child categories data 
        /// </summary>
        private const string ChildCategoriesData = @"{""count"":12,""results"":[""art:painting"",""art:photography"",""art:print"",""art:printmaking"",""art:reproduction"",""art:sculpture"",""art:aceo"",""art:collage"",""art:drawing"",""art:fiber_art"",""art:illustration"",""art:mixed_media""],""params"":{""category"":""art""},""type"":""category""}";

        /// <summary>
        /// Test parsing top-level tags from data
        /// </summary>
        [TestMethod]
        public void TopLevelTagsParseTest()
        {
            StringResults results = TagsData.Deserialize<StringResults>();
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count > 0);
            Assert.AreEqual(results.Count, results.Results.Length);
        }

        /// <summary>
        /// Test parsing top-level categories from data
        /// </summary>
        [TestMethod]
        public void CategoriesParseTest()
        {
            StringResults results = CategoriesData.Deserialize<StringResults>();
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count > 0);
            Assert.AreEqual(results.Count, results.Results.Length);
        }

        /// <summary>
        /// Test parsing child tags from data
        /// </summary>
        [TestMethod]
        public void ChildTagsParseTest()
        {
            StringResults results = ChildTagsData.Deserialize<StringResults>();
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count > 0);
            Assert.AreEqual(results.Count, results.Results.Length);
        }

        /// <summary>
        /// Test parsing child categories from data
        /// </summary>
        [TestMethod]
        public void ChildCategoriesDataParseTest()
        {
            StringResults results = ChildCategoriesData.Deserialize<StringResults>();
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count > 0);
            Assert.AreEqual(results.Count, results.Results.Length);
        }
    }
}
