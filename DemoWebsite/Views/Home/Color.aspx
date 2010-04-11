<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ColorModel>" %>
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

<% Html.BeginForm("Color", "Home"); %>
    <label for="Color">Color:</label> <%= Html.TextBoxFor(model => model.Color) %>
    <input type="submit" value="Show listings for color" />
<% Html.EndForm(); %>
    
</asp:Content>
