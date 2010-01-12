//-----------------------------------------------------------------------
// <copyright file="ColorKeywordsListingsViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.WpfUI.ColorPicker
{
    using Netsy.Helpers;
    using Netsy.UI.Commands;
    using Netsy.UI.ViewModels.Listings;

    /// <summary>
    /// Command to use the color dialog to populate the ColorListingsViewModel
    /// </summary>
    public class ColorKeywordsListingColorDialogCommand : GenericCommandBase<ColorKeywordsListingsViewModel>
    {
        /// <summary>
        /// Execute the command with a ViewModel as parameter
        /// </summary>
        /// <param name="value">the view model</param>
        public override void ExecuteOnValue(ColorKeywordsListingsViewModel value)
        {
            string color = ColorDialog.ShowColorDialog();
            if (color.HasContent())
            {
                value.ColorText = color;
            }
        }
    }
}
