//-----------------------------------------------------------------------
// <copyright file="ITagCategoryService.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.Interfaces
{
    using System;
    using DataModel;

    using Helpers;

    /// <summary>
    /// Interface to Tag and Category Commands on the Etsy API
    /// </summary>
    public interface ITagCategoryService
    {
        event EventHandler<ResultEventArgs<string>> GetTopCategoriesCompleted;
        event EventHandler<ResultEventArgs<string>> GetChildCategoriesCompleted;
        event EventHandler<ResultEventArgs<string>> GetTopTagsCompleted;
        event EventHandler<ResultEventArgs<string>> GetChildTagsCompleted;

         IAsyncResult GetTopCategories();
         IAsyncResult GetChildCategories(string category);

         IAsyncResult GetTopTags();
         IAsyncResult GetChildTags(string category);
    }
}
