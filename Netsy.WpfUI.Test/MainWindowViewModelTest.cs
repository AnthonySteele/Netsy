//-----------------------------------------------------------------------
// <copyright file="MainWindowViewModelTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.WpfUI.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests on the MainWindowViewModelTest
    /// </summary>
    [TestClass]
    public class MainWindowViewModelTest
    {
        /// <summary>
        /// Test creating a view model
        /// </summary>
        [TestMethod]
        public void MainWindowViewModelCreateTest()
        {
            MainWindowViewModel viewModel = new MainWindowViewModel(null);
            Assert.IsNotNull(viewModel);
        }
    }
}
