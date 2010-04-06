<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SearchModel>" %>
<%@ Import Namespace="Netsy.DemoWeb.Models"%>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PageTitle" runat="server">
    <h1><% = Model.Title %></h1>
</asp:Content>

<asp:Content ID="TopText" ContentPlaceHolderID="TopText" runat="server">
    <p> <% = Model.TopText %> </p>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<br/>
<% Html.BeginForm("Shop", "Home"); %>
    Shop name: <%= Html.TextBoxFor(model => model.SearchTerm) %>
    <input type="submit" value="Show listings for shop" />
<% Html.EndForm(); %>
<%
    if (Model.NetsyControl != null)
    {
        Html.RenderPartial("SilverlightErrorHandler", Model);
        Html.RenderPartial("NetsySilverlight", Model.NetsyControl);
    }
%>
    
</asp:Content>
