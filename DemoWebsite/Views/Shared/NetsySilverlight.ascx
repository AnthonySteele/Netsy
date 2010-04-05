<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NetsySilverlightModel>" %>
<%@ Import Namespace="Netsy.DemoWeb.Models"%>

    <% if (Model.HasHeading) {%>
    <h2 class="center"><%=Model.Heading%></h2>
    <%  } %>
    
 <div id="silverlightControlHost">
    <object data="data:application/x-silverlight-2," type="application/x-silverlight-2" width="<%=Model.Width%>" height="<%=Model.Height%>">
	  <param name="source" value="/ClientBin/Netsy.Listings.xap"/>
	  <param name="onError" value="onSilverlightError" />
	  <param name="background" value="white" />
	  <param name="minRuntimeVersion" value="3.0.40624.0" />
	  <param name="autoUpgrade" value="true" />
	  <param name="initParams" value="<%= Model.Params %>" />
	  <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=3.0.40624.0" style="text-decoration:none">
		  <img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight" style="border-style:none"/>
	  </a>
    </object><iframe id="_sl_historyFrame" style="visibility:hidden;height:0px;width:0px;border:0px"></iframe></div>
