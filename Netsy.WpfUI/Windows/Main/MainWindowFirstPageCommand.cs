//-----------------------------------------------------------------------
// <copyright file="MainWindowFirstPageCommand.cs" company="AFS">
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
    public class MainWindowFirstPageCommand : GenericCommandBase<MainWindowViewModel>
    {
        /// <summary>
        /// Execute the command and move to the next page
        /// </summary>
        /// <param name="value">the view model</param>
        public override void ExecuteOnValue(MainWindowViewModel value)
        {
            if (value.PageNumber > 1)
            {
                value.PageNumber = 1;
                CommandLocator.MainWindowLoadFrontFeaturedListingsCommand.Execute(value);
            }
        }

        /// <summary>
        /// Can the command execute
        /// </summary>
        /// <param name="value">the view model</param>
        /// <returns>true if it's not already on the first page</returns>
        public override bool CanExecuteOnValue(MainWindowViewModel value)
        {
            return value.PageNumber > 1;
        }
    }
}
