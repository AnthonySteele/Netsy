//-----------------------------------------------------------------------
// <copyright file="ShowListtingWindowCommand.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.WpfUI.Windows.Main
{
    using Netsy.DataModel;
    using Netsy.UI.Commands;
    using Netsy.WpfUI.Windows.Listing;

    /// <summary>
    /// Command to show the listing window for a listing
    /// </summary>
    public class ShowListingWindowCommand : GenericCommandBase<Listing>
    {
        /// <summary>
        /// Show the shop window for the listing
        /// </summary>
        /// <param name="value">the listing data</param>
        public override void ExecuteOnValue(Listing value)
        {
            ListingWindow listingWindow = new ListingWindow();

            ListingWindowViewModel viewModel = Locator.Resolve<ListingWindowViewModel>();
            listingWindow.DataContext = viewModel;

            viewModel.ListingId = value.ListingId;
            viewModel.LoadListingCommand.Execute(viewModel);

            listingWindow.Show();
        }
    }
}
