<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Test
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Test</h2>
    <%= Html.GoogleAnalytics("UA-XXXXXX-X","/Home/Test[noscript]","domain.com", "","") %>
</asp:Content>
