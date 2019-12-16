using OctopusFramework.V2.Web;
using System;
using System.Text;

namespace OctopusFramework.V2.MVC
{
    public class MetaTag : HtmlTag
    {
        public string HttpEquiv
        {
            get
            {
                return this.GetValue("http-equiv");
            }
            set
            {
                this.SetAttribute("http-equiv", value);
            }
        }

        public string Name
        {
            get
            {
                return this.GetValue("name");
            }
            set
            {
                this.SetAttribute("name", value);
            }
        }

        public string Property
        {
            get
            {
                return this.GetValue("property");
            }
            set
            {
                this.SetAttribute("property", value);
            }
        }

        public string MetaContent
        {
            get
            {
                return this.GetValue("content");
            }
            set
            {
                this.SetAttribute("content", value);
            }
        }

        public MetaTag() : base("meta")
        {
        }

        public override string Write()
        {
            StringBuilder builder = new StringBuilder(200);
            builder.Append("<meta");
            if (!String.IsNullOrWhiteSpace(this.HttpEquiv)) builder.Append($" http-equiv=\"{this.HttpEquiv}\" ");
            if (!String.IsNullOrWhiteSpace(this.Name)) builder.Append($" name=\"{this.Name}\" ");
            if (!String.IsNullOrWhiteSpace(this.Property)) builder.Append($" property=\"{this.Property}\" ");
            if (!String.IsNullOrWhiteSpace(this.MetaContent)) builder.Append($" content=\"{this.MetaContent}\" ");
            builder.Append(" />");
            return builder.ToString();
        }
    }
}
