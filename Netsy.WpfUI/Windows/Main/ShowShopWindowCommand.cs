//-----------------------------------------------------------------------
// <copyright file="ShowShopCommand.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.WpfUI.Windows.Main
{
    using System;

    using Netsy.DataModel;

    using Netsy.UI.Commands;

    using Shop;

    /// <summary>
    /// Command to show the shop window for a shop
    /// </summary>
    public class ShowShopWindowCommand : GenericCommandBase<Listing>
    {
        /// <summary>
        /// Show the shop window for the listing
        /// </summary>
        /// <param name="value">the listing data</param>
        public override void ExecuteOnValue(Listing value)
        {
            ShopWindow shopWindow = new ShopWindow();

            ShopWindowViewModel shopViewModel = Locator.Resolve<ShopWindowViewModel>();
            shopWindow.DataContext = shopViewModel;

            shopViewModel.UserId = value.UserId;

            shopViewModel.ShopWindowLoadShopCommand.Execute(shopViewModel);
            shopViewModel.ShopWindowLoadListingsCommand.Execute(shopViewModel);
            shopWindow.Show();
        }
    }
}
