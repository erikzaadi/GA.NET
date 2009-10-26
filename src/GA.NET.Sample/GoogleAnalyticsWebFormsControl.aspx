<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoogleAnalyticsWebFormsControl.aspx.cs"
    Inherits="GA.NET.Sample.GoogleAnalyticsWebFormsControl" %>

<%@ Register Assembly="GA.NET.ASPNET" Namespace="GA.NET.ASPNET" TagPrefix="GoogleAnalytics" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Google Analytics Web Control</title>
    <link href="Content/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div class="page">
        <div id="header">
            <div>
                &nbsp;</div>
            <div id="menucontainer">
            </div>
        </div>
        <div id="main">
            <form id="form1" runat="server">
            <h2>
                Google Analytics Web Control</h2>
            <h4>
                Usage :
            </h4>
            <pre><code>&lt;GoogleAnalytics:GAControl ID="GAControlID" runat="server" GoogleAnalyticsID="UA-xxxxxx-x"
                /&gt;</code></pre>
            <div>
                View source to see the result html</div>
            <div>
                <!-- Default usage, only GoogleAnalyticsID is required-->
                <GoogleAnalytics:GAControl ID="GAControlID" runat="server" GoogleAnalyticsID="UA-xxxxxx-x" />
            </div>
            <h4>
                Alternate Usage (For passing parameters for javascript disabled tracking):
            </h4>
            <pre><code>&lt;GoogleAnalytics:GAControl ID="GAControl1" Domain="customdomain" PageName="CustomPageName"
                Referer="" UserVariable="SomethingSpecial" runat="server" GoogleAnalyticsID="UA-xxxxxx-x"
                /&gt;</code></pre>
            <div>
                View source to see the result html</div>
            <div>
                <!-- With custom parameters for javascript disabled tracking -->
                <GoogleAnalytics:GAControl ID="GAControl1" Domain="customdomain" PageName="CustomPageName"
                    Referer="" UserVariable="SomethingSpecial" runat="server" GoogleAnalyticsID="UA-xxxxxx-x" />
            </div>
            </form>
            <div id="footer">
            </div>
            <p>
                <a href='Home/Index'>ASP.NET MVC Sample</a></p>
        </div>
    </div>
</body>
</html>
