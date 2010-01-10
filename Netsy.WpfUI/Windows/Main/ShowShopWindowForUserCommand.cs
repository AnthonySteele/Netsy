//-----------------------------------------------------------------------
// <copyright file="ShowShopWindowForUserCommand.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.WpfUI.Windows.Main
{
    using Netsy.DataModel;
    using Netsy.UI.Commands;
    using Netsy.WpfUI.Windows.Shop;

    /// <summary>
    /// Command to show the shop window for a user
    /// </summary>
    public class ShowShopWindowForUserCommand : GenericCommandBase<User>
    {
        /// <summary>
        /// Show the shop window for the listing
        /// </summary>
        /// <param name="value">the listing data</param>
        public override void ExecuteOnValue(User value)
        {
            ShopWindow shopWindow = new ShopWindow();

            ShopWindowViewModel viewModel = Locator.Resolve<ShopWindowViewModel>();
            shopWindow.DataContext = viewModel;

            viewModel.UserId = value.UserId;

            viewModel.ShopWindowLoadShopCommand.Execute(viewModel);
            viewModel.LoadAll();
            shopWindow.Show();
        }
    }
}
