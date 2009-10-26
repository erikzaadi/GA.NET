using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace GA.NET.MVC
{
    /// <summary>
    /// 
    /// </summary>
    public static class GoogleAnalyticsHtmlHelper
    {
        /// <summary>
        /// Renders the html and javascript needed to include for Google Analytics Tracking
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="GoogleAnalyticsID">Google Analytics ID (UA-xxxxxx-x)</param>
        /// <returns></returns>
        public static string GoogleAnalytics(this HtmlHelper helper, string GoogleAnalyticsID)
        {
            return helper.GoogleAnalytics(GoogleAnalyticsID, null);
        }

        /// <summary>
        /// Renders the html and javascript needed to include for Google Analytics Tracking
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="GoogleAnalyticsID">Google Analytics ID (UA-xxxxxx-x)</param>
        /// <param name="PageName">Name of page (shown in the Google Analytics Dashboard)</param>
        /// <returns></returns>
        public static string GoogleAnalytics(this HtmlHelper helper,
            string GoogleAnalyticsID,
            string PageName)
        {
            return helper.GoogleAnalytics(GoogleAnalyticsID,
                PageName,
                null);
        }

        /// <summary>
        /// Renders the html and javascript needed to include for Google Analytics Tracking
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="GoogleAnalyticsID">Google Analytics ID (UA-xxxxxx-x)</param>
        /// <param name="PageName">Name of page (shown in the Google Analytics Dashboard)</param>
        /// <param name="Domain">Domain to track <example>sub.domain.com</example></param>
        /// <returns></returns>
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

        /// <summary>
        /// Renders the html and javascript needed to include for Google Analytics Tracking
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="GoogleAnalyticsID">Google Analytics ID (UA-xxxxxx-x)</param>
        /// <param name="PageName">Name of page (shown in the Google Analytics Dashboard)</param>
        /// <param name="Domain">Domain to track <example>sub.domain.com</example></param>
        /// <param name="Referer">Refering page</param>
        /// <returns></returns>
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

        /// <summary>
        /// Renders the html and javascript needed to include for Google Analytics Tracking
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="GoogleAnalyticsID">Google Analytics ID (UA-xxxxxx-x)</param>
        /// <param name="PageName">Name of page (shown in the Google Analytics Dashboard)</param>
        /// <param name="Domain">Domain to track <example>sub.domain.com</example></param>
        /// <param name="Referer">Refering page</param>
        /// <param name="UserVariable">Special variable to pass to analytics</param>
        /// <returns></returns>
        public static string GoogleAnalytics(this HtmlHelper helper,
            string GoogleAnalyticsID,
            string PageName,
            string Domain,
            string Referer,
            string UserVariable)
        {
            string domain = string.IsNullOrEmpty(Domain) ? helper.ViewContext.HttpContext.Request.Url.Host : Domain;
            string referer = string.IsNullOrEmpty(Referer) ? (helper.ViewContext.HttpContext.Request.UrlReferrer != null ? helper.ViewContext.HttpContext.Request.UrlReferrer.ToString() : "") : Referer;
            string pagename = string.IsNullOrEmpty(PageName) ? helper.ViewContext.HttpContext.Request.Url.PathAndQuery + GA.NET.Core.Engine.NoScriptPageNameSuffix : PageName;
            string userVariable = string.IsNullOrEmpty(UserVariable) ? "" : UserVariable;
            return GA.NET.Core.Engine.GetGoogleAnalytics(GoogleAnalyticsID, domain, referer, pagename, userVariable);
        }
    }
}
