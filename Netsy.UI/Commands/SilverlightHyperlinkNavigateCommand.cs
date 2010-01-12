//-----------------------------------------------------------------------
// <copyright file="HyperlinkNavigateCommand.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.UI.Commands
{
    using Netsy.Helpers;

    /// <summary>
    /// Command to navigate a hyperlink
    /// </summary>
    public class HyperlinkNavigateCommand : GenericCommandBase<string>
    {
        /// <summary>
        /// Execute the command with a ViewModel as parameter
        /// </summary>
        /// <param name="value">the view model</param>
        public override void ExecuteOnValue(string value)
        {
            if (!value.HasContent())
            {
                return;
            }

            // todo
        }
    }
}
