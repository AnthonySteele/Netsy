//-----------------------------------------------------------------------
// <copyright file="MainWindowPreviousPageCommand.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace NetsyGui.Main
{
    /// <summary>
    /// Command to move the viewModel to the previous page of results
    /// </summary>
    public class MainWindowPreviousPageCommand : GenericCommandBase<MainWindowViewModel>
    {
        /// <summary>
        /// Execute the command and move to the next page
        /// </summary>
        /// <param name="value">the view model</param>
        public override void ExecuteOnValue(MainWindowViewModel value)
        {
            if (value.PageNumber > 1)
            {
                value.PageNumber--;
                CommandLocator.MainWindowLoadFrontFeaturedListingsCommand.Execute(value);
            }
        }

        /// <summary>
        /// Can the command execute
        /// </summary>
        /// <param name="value">the view model</param>
        /// <returns>true if there is a previous page</returns>
        public override bool CanExecuteOnValue(MainWindowViewModel value)
        {
            return value.PageNumber > 1;
        }
    }
}
