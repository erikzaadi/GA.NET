<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Google Analytics Html Helper
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Google Analytics Html Helper</h2>
    <h4>
        Usage :
    </h4>
    <pre>
<code>&lt;%= Html.GoogleAnalytics("UA-xxxxxx-x")%&gt;</code></pre>
    <div>
        View source to see the result html</div>
    <div>
        <!-- Default usage, only GoogleAnalyticsID is required-->
        <%= Html.GoogleAnalytics("UA-xxxxxx-x")%>
    </div>
    <h4>
        Alternate Usage (For passing parameters for javascript disabled tracking):
    </h4>
    <pre>
<code>&lt;%= Html.GoogleAnalytics("UA-xxxxxx-x","CustomPageName","custom.domain.com","","userVariable")%&gt;</code></pre>
    <div>
        View source to see the result html</div>
    <div>
        <!-- With custom parameters for javascript disabled tracking -->
        <%= Html.GoogleAnalytics("UA-xxxxxx-x","CustomPageName","custom.domain.com","","userVariable")%>
    </div>
    <p>
        <a href='<%= Url.Content("~/GoogleAnalyticsWebFormsControl.aspx") %>'>Regular Web Forms
            Example</a>
    </p>
</asp:Content>
