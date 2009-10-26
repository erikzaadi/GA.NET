﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Web;

namespace GA.NET.Core
{
    /// <summary>
    /// Google Analytics Include Generator (With support for tracking browsers with javascript disabled)
    /// </summary>
    public class Engine
    {
        /// <summary>
        /// Suffix for page name (shown in Google Analytics dashboard)
        /// </summary>
        public const string NoScriptPageNameSuffix = "[NOSCRIPT]";

        /// <summary>
        /// Script template (you can edit the file)
        /// </summary>
        private const string ScriptTemplateResourceName = "GA.NET.Core.Resources.Script.template";
        
        /// <summary>
        /// No Script template (you can edit the file)
        /// </summary>
        private const string NoScriptTemplateResourceName = "GA.NET.Core.Resources.NoScript.template";
        
        /// <summary>
        /// Include Prefix
        /// </summary>
        private const string Prefix = "\n<!-- Google Analytics -->\n";
        
        /// <summary>
        /// Include Suffix
        /// </summary>
        private const string Suffix = "\n<!-- Google Analytics End -->\n";
       
        /// <summary>
        /// Image place holder in No Script template
        /// </summary>
        private const string ImageURLPlaceHolder = "GA_IMG";
        
        /// <summary>
        /// Google Account ID Place Holder in both NoScript and Script templates
        /// </summary>
        private const string GoogleAccountIDPlaceHolder = "GA_CODE";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="GoogleAccountID"></param>
        /// <param name="Context"></param>
        /// <returns></returns>
        public static string GetGoogleAnalytics(string GoogleAccountID, System.Web.HttpContext Context)
        {
            HttpContext current = Context ?? HttpContext.Current;
            string domain = current.Request.Url.Host;
            string referer = current.Request.UrlReferrer != null ? current.Request.UrlReferrer.ToString() : "";
            string pagename = System.IO.Path.GetFileName(current.Request.Url.LocalPath);

            return GetGoogleAnalytics(GoogleAccountID, domain, referer, pagename, "");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="GoogleAccountID"></param>
        /// <param name="Domain"></param>
        /// <param name="Referer"></param>
        /// <param name="PageName"></param>
        /// <param name="UserVariable"></param>
        /// <returns></returns>
        public static string GetGoogleAnalytics(string GoogleAccountID, string Domain, string Referer, string PageName, string UserVariable)
        {
            string scriptTemplate = GetScriptSnippetTemplate();
            string noScriptTemplate = GetNoScriptSnippetTemplate();

            string imageURL = BuildImageURL(GoogleAccountID,
                Domain,
                Referer,
                PageName,
                UserVariable);

            scriptTemplate = scriptTemplate.Replace(GoogleAccountIDPlaceHolder, GoogleAccountID);
            noScriptTemplate = noScriptTemplate
                .Replace(ImageURLPlaceHolder, imageURL)
                .Replace(GoogleAccountIDPlaceHolder, GoogleAccountID);

            return string.Format("{0}{1}{2}{3}", Prefix, scriptTemplate, noScriptTemplate, Suffix);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="GoogleAccountID"></param>
        /// <param name="Domain"></param>
        /// <param name="Referer"></param>
        /// <param name="PageName"></param>
        /// <param name="UserVariable"></param>
        /// <returns></returns>
        public static string GetGoogleAnalyticsNoScriptOnly(string GoogleAccountID, string Domain, string Referer, string PageName, string UserVariable)
        {
            string scriptTemplate = GetScriptSnippetTemplate();

            scriptTemplate = scriptTemplate
              .Replace(GoogleAccountIDPlaceHolder, GoogleAccountID);

            return string.Format("{0}{1}{2}", Prefix, scriptTemplate, Suffix);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="GoogleAccountID"></param>
        /// <param name="Domain"></param>
        /// <param name="Referer"></param>
        /// <param name="PageName"></param>
        /// <param name="UserVariable"></param>
        /// <returns></returns>
        public static string GetGoogleAnalyticsScriptOnly(string GoogleAccountID, string Domain, string Referer, string PageName, string UserVariable)
        {
            string noScriptTemplate = GetNoScriptSnippetTemplate();

            string imageURL = BuildImageURL(GoogleAccountID,
                Domain,
                Referer,
                PageName,
                UserVariable);

            noScriptTemplate = noScriptTemplate
                .Replace(ImageURLPlaceHolder, imageURL)
                .Replace(GoogleAccountIDPlaceHolder, GoogleAccountID);

            return string.Format("{0}{1}{2}", Prefix, noScriptTemplate, Suffix);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ResourceName"></param>
        /// <returns></returns>
        private static string GetResource(string ResourceName)
        {
            string resource = "";
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (TextReader tr = new StreamReader(assembly.GetManifestResourceStream(ResourceName)))
            {
                resource = tr.ReadToEnd();
            }
            return resource;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static string GetNoScriptSnippetTemplate()
        {
            return GetResource(NoScriptTemplateResourceName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static string GetScriptSnippetTemplate()
        {
            return GetResource(ScriptTemplateResourceName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="GoogleAccountID"></param>
        /// <param name="Domain"></param>
        /// <param name="Referer"></param>
        /// <param name="PageName"></param>
        /// <param name="UserVar"></param>
        /// <returns></returns>
        private static string BuildImageURL(string GoogleAccountID,
                                            string Domain,
                                            string Referer,
                                            string PageName,
                                            string UserVar)
        {
            int requestRandom = new Random().Next(100000000, 999999999);
            int cookieRandom = new Random().Next(100000000, 999999999);
            int random = new Random().Next(1000000000, 2147483647);
            long today = SecondsFromEpoch(DateTime.Now);

            string toReturn = "http://www.google-analytics.com/__utm.gif?utmwv=1&utmn={0}"
                    + "&utmsr=-&utmsc=-&utmul=-&utmje=0&utmfl=-&utmdt=-&utmhn={1}&utmr="
                    + "{2}&utmp={3}&utmac={4}&utmcc=__utma%3D"
                    + "{5}.{6}.{7}.{7}.{7}"
                    + ".2%3B%2B__utmb%3D{5}%3B%2B__utmc%3D{5}%3B%2B__utmz%3D"
                    + "{5}.{7}.2.2.utmccn%3D({9})%7Cutmcsr%3D({9})%7Cutmcmd%3D(none)%3B%2B__utmv%3D"
                    + "{5}.{8}%3B";
            return string.Format(toReturn,
                requestRandom,//0
                string.IsNullOrEmpty(Domain) ? "-" : HttpUtility.HtmlEncode(Domain),//1 
                string.IsNullOrEmpty(Referer) ? "-" : HttpUtility.HtmlEncode(Referer),//2
                string.IsNullOrEmpty(PageName) ? NoScriptPageNameSuffix : HttpUtility.HtmlEncode(PageName),//3 
                GoogleAccountID,//4 
                cookieRandom,//5
                random,//6
                today,//7 
                string.IsNullOrEmpty(UserVar) ? "-" : HttpUtility.HtmlEncode(UserVar),//8
                string.IsNullOrEmpty(Referer) ? "direct" : "referral");//

            //Reference - http://code.google.com/apis/analytics/docs/tracking/gaTrackingTroubleshooting.html
        }

        //http://sigabrt.blogspot.com/2007/07/c-datetime-tofrom-unix-epoch.html
        /// <summary>
        /// Unix Start Date
        /// </summary>
        public static readonly DateTime JAN_01_1970 =
            DateTime.SpecifyKind(new DateTime(1970, 1, 1, 0, 0, 0), DateTimeKind.Utc);
        /// <summary>
        /// Get Unix Timestamp for given DateTime
        /// </summary>
        /// <param name="date">DateTime to convert</param>
        /// <returns>long Unix Timestamp</returns>
        public static long SecondsFromEpoch(DateTime date)
        {
            DateTime dt = date.ToUniversalTime();
            TimeSpan ts = dt.Subtract(JAN_01_1970);
            return (long)ts.TotalSeconds;
        }
    }
}