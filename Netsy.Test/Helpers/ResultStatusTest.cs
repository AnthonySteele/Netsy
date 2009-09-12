//-----------------------------------------------------------------------
// <copyright file="ResultStatusTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Test.Helpers
{
    using System.Net;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.Helpers;

    /// <summary>
    /// Test result Status
    /// </summary>
    [TestClass]
    public class ResultStatusTest
    {
        /// <summary>
        /// Test the result status success creation
        /// </summary>
        [TestMethod]
        public void ResultStatusCreateSuccessTest()
        {
            ResultStatus status = new ResultStatus(true);

            Assert.IsNotNull(status);
            Assert.IsTrue(status.Success);
            Assert.AreEqual(WebExceptionStatus.Success, status.WebStatus);
        }

        /// <summary>
        /// Test the result status error creation
        /// </summary>
        [TestMethod]
        public void ResultStatusCreateErrorTest()
        {
            ResultStatus status = new ResultStatus("test fail", null);

            Assert.IsNotNull(status);
            Assert.IsFalse(status.Success);
            Assert.AreEqual("test fail", status.ErrorMessage);
        }
    }
}
