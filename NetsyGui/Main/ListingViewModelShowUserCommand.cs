//-----------------------------------------------------------------------
// <copyright file="ListingViewModelShowUserCommand.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace NetsyGui.Main
{
    using Shop;

    using ViewModels;

    /// <summary>
    /// Command to show the user
    /// </summary>
    public class ListingViewModelShowUserCommand : GenericCommandBase<ListingViewModel>
    {
        /// <summary>
        /// Must return true for list item binding to work
        /// </summary>
        /// <param name="parameter">the command arguments</param>
        /// <returns>true if the command can execute</returns>
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="value">the listing view model</param>
        public override void ExecuteOnValue(ListingViewModel value)
        {
            ShopWindow shopWindow = new ShopWindow(value.UserId);
            shopWindow.Show();
        }
    }
}
