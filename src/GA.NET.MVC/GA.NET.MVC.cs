using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace GA.NET.MVC
{
    public static class GoogleAnalyticsHtmlHelper
    {
        public static string GoogleAnalytics(this HtmlHelper helper, string GoogleAnalyticsID)
        {
            return helper.GoogleAnalytics(GoogleAnalyticsID, null);
        }

        public static string GoogleAnalytics(this HtmlHelper helper,
            string GoogleAnalyticsID,
            string PageName)
        {
            return helper.GoogleAnalytics(GoogleAnalyticsID,
                PageName,
                null);
        }

        public static string GoogleAnalytics(this HtmlHelper helper,
            string GoogleAnalyticsID,
            string PageName,
            string Domain)
        {
            return helper.GoogleAnalytics(GoogleAnalyticsID,
                PageName,
                Domain,
                null);
        }

        public static string GoogleAnalytics(this HtmlHelper helper,
            string GoogleAnalyticsID,
            string PageName,
            string Domain,
            string Referer)
        {
            return helper.GoogleAnalytics(GoogleAnalyticsID,
                PageName,
                Domain,
                Referer,
                null);
        }

        public static string GoogleAnalytics(this HtmlHelper helper,
            string GoogleAnalyticsID,
            string PageName,
            string Domain,
            string Referer,
            string UserVariable)
        {
            string domain = string.IsNullOrEmpty(Domain) ? helper.ViewContext.HttpContext.Request.Url.Host : Domain;
            string referer = string.IsNullOrEmpty(Referer) ? (helper.ViewContext.HttpContext.Request.UrlReferrer != null ? helper.ViewContext.HttpContext.Request.UrlReferrer.ToString() : "") : Referer;
            string pagename = string.IsNullOrEmpty(PageName) ? helper.ViewContext.HttpContext.Request.Url.PathAndQuery + "[noscript]" : PageName;
            string userVariable = string.IsNullOrEmpty(UserVariable) ? "" : UserVariable;
            return GA.NET.Core.Engine.GetGoogleAnalytics(GoogleAnalyticsID, domain, referer, pagename, userVariable);
        }
    }
}
