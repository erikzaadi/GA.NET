using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.UI.WebControls;

[assembly: TagPrefix("GA.NET.ASPNET", "GoogleAnalytics")]
namespace GA.NET.ASPNET
{
    /// <summary>
    /// 
    /// </summary>
    [ToolboxData("<{0}:GAControl ID='GAControlID' GoogleAnalyticsID='' runat=\"server\"> </{0}:GAControl>")]
    [DefaultProperty("GoogleAnalyticsID")]
    [Description("")]
    public class GAControl : WebControl
    {
        [Category("Mandatory")]
        [Description("Google Analytics Account ID (UA-xxxxxx-x)")]
        public string GoogleAnalyticsID { get; set; }
        [Category("NoScript")]
        public string PageName { get; set; }
        [Category("NoScript")]
        public string Domain { get; set; }
        [Category("NoScript")]
        public string Referer { get; set; }
        [Category("NoScript")]
        public string UserVariable { get; set; }


        protected override void Render(HtmlTextWriter writer)
        {
            if (DesignMode)
            {
                writer.Write(DesignTimeRenderHTML);
                return;
            }
            if (string.IsNullOrEmpty(GoogleAnalyticsID))
                throw new ArgumentNullException("GoogleAnalyticsID");
            string text = "";
            if (!string.IsNullOrEmpty(PageName) ||
              !string.IsNullOrEmpty(Domain) ||
              !string.IsNullOrEmpty(Referer) ||
              !string.IsNullOrEmpty(UserVariable))
                text = GA.NET.Core.Engine.GetGoogleAnalytics(GoogleAnalyticsID, Domain, Referer, PageName, UserVariable);
            else
                text = GA.NET.Core.Engine.GetGoogleAnalytics(GoogleAnalyticsID, HttpContext.Current);
            writer.Write(text);
        }

        private string DesignTimeRenderHTML
        {
            get
            {
                string prefix = "<p style='border:1px dotted black;padding:2px;'>";
                if (!string.IsNullOrEmpty(GoogleAnalyticsID))
                    return string.Format("{0}Google Analytics - ID : <span style='color:blue;'>'{1}'</span></p>", prefix, GoogleAnalyticsID);
                else
                    return string.Format("{0}Google Analytics - <span style='color:red;'>Please add an Account ID (UA-xxxxxx-x)</span></p>", prefix);
            }
        }

    }
}
