using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Web;

namespace GA.NET.Core
{
    public class Engine
    {
        private const string SnippetTemplateResourceName = "GA.NET.Core.Resources.GaSnippet.include";
        private const string GoogleAnalyticsImageURLPlaceHolder = "GA_IMG";
        private const string GoogleAnalyticsUrchinPlaceHolder = "GA_CODE";

        public static string GetGA(string UrchinCode, System.Web.HttpContext Context)
        {
            HttpContext current = Context ?? HttpContext.Current;
            string domain = current.Request.Url.Host;
            string referer = current.Request.UrlReferrer != null ? current.Request.UrlReferrer.ToString() : "";
            string pagename = System.IO.Path.GetFileName(current.Request.Url.LocalPath);

            return GetGA(UrchinCode, domain, referer, pagename, "");
        }

        public static string GetGA(string UrchinCode, string Domain, string Referer, string PageName, string UserVariable)
        {
            string snippetTemplate = GetSnippetTemplate();

            string imageURL = BuildImageURL(UrchinCode,
                Domain,
                Referer,
                PageName,
                UserVariable);

            return snippetTemplate
                .Replace(GoogleAnalyticsImageURLPlaceHolder, imageURL)
                .Replace(GoogleAnalyticsUrchinPlaceHolder, UrchinCode);
        }

        private static string GetSnippetTemplate()
        {
            string snippetTemplate = "";
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (TextReader tr = new StreamReader(assembly.GetManifestResourceStream(SnippetTemplateResourceName)))
            {
                snippetTemplate = tr.ReadToEnd();
            }
            return snippetTemplate;
        }

        private static string BuildImageURL(string UrchinCode,
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
                    + "{5}.{7}2.2.utmccn%3D(direct)%7Cutmcsr%3D(direct)%7Cutmcmd%3D(none)%3B%2B__utmv%3D"
                    + "{5}.{8}%3B";
            return string.Format(toReturn,
                requestRandom,//0
                HttpUtility.HtmlEncode(Domain),//1 
                HttpUtility.HtmlEncode(Referer),//2
                HttpUtility.HtmlEncode(PageName),//3 
                UrchinCode,//4 
                cookieRandom,//5
                random,//6
                today,//7 
                HttpUtility.HtmlEncode(UserVar));//8 
            //'http://www.google-analytics.com/__utm.gif?utmwv=1&utmn='.$var_utmn.'&utmsr=-&utmsc=-&utmul=-&utmje=0&utmfl=-&utmdt=-&utmhn='.$var_utmhn.'&utmr='.$var_referer.'&utmp='.$var_utmp.'&utmac='.$var_utmac.'&utmcc=__utma%3D'.$var_cookie.'.'.$var_random.'.'.$var_today.'.'.$var_today.'.'.$var_today.'.2%3B%2B__utmb%3D'.$var_cookie.'%3B%2B__utmc%3D'.$var_cookie.'%3B%2B__utmz%3D'.$var_cookie.'.'.$var_today.'.2.2.utmccn%3D(direct)%7Cutmcsr%3D(direct)%7Cutmcmd%3D(none)%3B%2B__utmv%3D'.$var_cookie.'.'.$var_uservar.'%3B';
            //Reference - http://code.google.com/apis/analytics/docs/tracking/gaTrackingTroubleshooting.html
        }

        //http://sigabrt.blogspot.com/2007/07/c-datetime-tofrom-unix-epoch.html
        public static readonly DateTime JAN_01_1970 =
            DateTime.SpecifyKind(new DateTime(1970, 1, 1, 0, 0, 0), DateTimeKind.Utc);
        // Get Unix Timestamp for given DateTime
        public static long SecondsFromEpoch(DateTime date)
        {
            DateTime dt = date.ToUniversalTime();
            TimeSpan ts = dt.Subtract(JAN_01_1970);
            return (long)ts.TotalSeconds;
        }
    }
}
