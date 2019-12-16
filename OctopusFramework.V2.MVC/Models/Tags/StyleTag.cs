using OctopusFramework.V2.Web;
using System;
using System.Collections.Generic;
using System.Text;

namespace OctopusFramework.V2.MVC
{
    public class StyleTag : HtmlTag
    {
        public string Href
        {
            get
            {
                return this.GetValue("href");
            }
            set
            {
                this.SetAttribute("href", value);
            }
        }

        public string Rel
        {
            get
            {
                return this.GetValue("rel");
            }
            set
            {
                this.SetAttribute("rel", value);
            }
        }

        public string StyleType
        {
            get
            {
                return this.GetValue("type");
            }
            set
            {
                this.SetAttribute("type", value);
            }
        }

        public string ID
        {
            get
            {
                return this.GetValue("id");
            }
            set
            {
                this.SetAttribute("id", value);
            }
        }

        public StyleTag() : base("link")
        {
            this.StyleType = "text/css";
            this.Rel = "stylesheet";
        }

        public StyleTag(string url) : base("link")
        {
            this.StyleType = "text/css";
            this.Rel = "stylesheet";
            this.Href = url;
        }

        public override string Write()
        {
            StringBuilder builder = new StringBuilder(200);
            builder.Append("<link");
            if (!String.IsNullOrWhiteSpace(this.Href)) builder.Append($" href=\"{this.Href}\" ");
            if (!String.IsNullOrWhiteSpace(this.Rel)) builder.Append($" rel=\"{this.Rel}\" ");
            if (!String.IsNullOrWhiteSpace(this.StyleType)) builder.Append($" type=\"{this.StyleType}\" ");
            if (!String.IsNullOrWhiteSpace(this.ID)) builder.Append($" id=\"{this.ID}\" ");
            builder.Append(" />");
            return builder.ToString();
        }
    }
}
