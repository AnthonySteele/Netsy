<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FavoritesModel>" %>
<%@ Import Namespace="Netsy.DemoWeb.Models"%>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PageTitle" runat="server">
    <h1><% = Model.Title %></h1>
</asp:Content>

<asp:Content ID="TopText" ContentPlaceHolderID="TopText" runat="server">
    <p> <% = Model.TopText %> </p>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<br/>
<%
    if (Model.NetsyControl != null)
    {
        Html.RenderPartial("SilverlightErrorHandler", Model);
        Html.RenderPartial("NetsySilverlight", Model.NetsyControl);
    }
%>

<% Html.BeginForm("Favorites", "Home"); %>
    <label for="Shop">Shop name:</label> <%= Html.TextBoxFor(model => model.Shop) %>
    <input type="submit" value="Show favorites for shop" />
<% Html.EndForm(); %>
    
</asp:Content>
