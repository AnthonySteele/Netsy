<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<HomeModel>" %>
<%@ Import Namespace="Netsy.DemoWeb.Models"%>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PageTitle" runat="server">
    <h1><% = Model.Title %></h1>
</asp:Content>

<asp:Content ID="TopText" ContentPlaceHolderID="TopText" runat="server">
    <p> <% = Model.TopText %> </p>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<%
        Html.RenderPartial("SilverlightErrorHandler", Model); 
    
        foreach (NetsySilverlightModel netsyControl in Model.NetsyControls)
        {
            Html.RenderPartial("NetsySilverlight", netsyControl);            
        }
 %>
    
</asp:Content>
