<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CategoryModel>" %>
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

<% Html.BeginForm("Category", "Home"); %>
    <label for="Category">Category:</label> <%= Html.TextBoxFor(model => model.Category) %>
    <input type="submit" value="Show listings for category" />
<% Html.EndForm(); %>
    
</asp:Content>
