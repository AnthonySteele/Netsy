//-----------------------------------------------------------------------
// <copyright file="WebRequestGeneratorTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Test.Requests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.Requests;

    /// <summary>
    /// Test the WebRequestGenerator class
    /// </summary>
    [TestClass]
    public class WebRequestGeneratorTest
    {
        /// <summary>
        /// Test simple creation
        /// </summary>
        [TestMethod]
        public void CreateTest()
        {
            IRequestGenerator requestGenerator = new WebRequestGenerator();

            Assert.IsNotNull(requestGenerator);
        }
    }
}
