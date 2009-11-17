//-----------------------------------------------------------------------
// <copyright file="MainWindowReloadCommand.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace NetsyGui.Main
{
    /// <summary>
    /// Command to move the viewModel to the first page of results
    /// </summary>
    public class MainWindowReloadCommand : GenericCommandBase<MainWindowViewModel>
    {
        /// <summary>
        /// Execute the command and move to the next page
        /// </summary>
        /// <param name="value">the view model</param>
        public override void ExecuteOnValue(MainWindowViewModel value)
        {
            value.RequestFrontFeaturedListings();
        }
    }
}
