<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<HomeModel>" %>
<%@ Import Namespace="Netsy.DemoWeb.Models"%>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PageTitle" runat="server">
    <h1><% = Model.Title %></h1>
</asp:Content>

<asp:Content ID="TopText" ContentPlaceHolderID="TopText" runat="server">
    <p> <% = Model.TopText %> </p>
</asp:Content>


<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" src="/Scripts/Silverlight.js"></script>
    <script type="text/javascript">
        function onSilverlightError(sender, args) {
            var appSource = "";
            if (sender != null && sender != 0) {
                appSource = sender.getHost().Source;
            }

            var errorType = args.ErrorType;
            var iErrorCode = args.ErrorCode;

            if (errorType == "ImageError" || errorType == "MediaError") {
                return;
            }

            var errMsg = "Unhandled Error in Silverlight Application " + appSource + "\n";

            errMsg += "Code: " + iErrorCode + "    \n";
            errMsg += "Category: " + errorType + "       \n";
            errMsg += "Message: " + args.ErrorMessage + "     \n";

            if (errorType == "ParserError") {
                errMsg += "File: " + args.xamlFile + "     \n";
                errMsg += "Line: " + args.lineNumber + "     \n";
                errMsg += "Position: " + args.charPosition + "     \n";
            }
            else if (errorType == "RuntimeError") {
                if (args.lineNumber != 0) {
                    errMsg += "Line: " + args.lineNumber + "     \n";
                    errMsg += "Position: " + args.charPosition + "     \n";
                }
                errMsg += "MethodName: " + args.methodName + "     \n";
            }

            throw new Error(errMsg);
        }
    </script>
    
    <h2 class="center"><%= Model.SilverlightHeading %></h2>
    
    <div id="silverlightControlHost">
    <object data="data:application/x-silverlight-2," type="application/x-silverlight-2" width="660" height="560">
	  <param name="source" value="/ClientBin/Netsy.Listings.xap"/>
	  <param name="onError" value="onSilverlightError" />
	  <param name="background" value="white" />
	  <param name="minRuntimeVersion" value="3.0.40624.0" />
	  <param name="autoUpgrade" value="true" />
	  <param name="initParams" value="<%= Model.SilverlightParams %>" />
	  <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=3.0.40624.0" style="text-decoration:none">
		  <img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight" style="border-style:none"/>
	  </a>
    </object><iframe id="_sl_historyFrame" style="visibility:hidden;height:0px;width:0px;border:0px"></iframe></div>

</asp:Content>
