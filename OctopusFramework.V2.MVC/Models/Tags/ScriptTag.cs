using OctopusFramework.V2.Web;
using System;
using System.Collections.Generic;
using System.Text;

namespace OctopusFramework.V2.MVC
{
    public class ScriptTag : HtmlTag
    {
        public string Src
        {
            get
            {
                return this.GetValue("src");
            }
            set
            {
                this.SetAttribute("src", value);
            }
        }

        public string Integrity
        {
            get
            {
                return this.GetValue("integrity");
            }
            set
            {
                this.SetAttribute("integrity", value);
            }
        }

        public string Crossorigin
        {
            get
            {
                return this.GetValue("crossorigin");
            }
            set
            {
                this.SetAttribute("crossorigin", value);
            }
        }

        public string ScriptType
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

        public ScriptTag() : base("script")
        {
            this.ScriptType = "text/javascript";
        }

        public ScriptTag(string url) : base("script")
        {
            this.ScriptType = "text/javascript";
            this.Src = url;
        }

        public override string Write()
        {
            StringBuilder builder = new StringBuilder(200);
            builder.Append("<script");
            if (!String.IsNullOrWhiteSpace(this.Src)) builder.Append($" src=\"{this.Src}\" ");
            if (!String.IsNullOrWhiteSpace(this.Integrity)) builder.Append($" integrity=\"{this.Integrity}\" ");
            if (!String.IsNullOrWhiteSpace(this.Crossorigin)) builder.Append($" crossorigin=\"{this.Crossorigin}\" ");
            if (!String.IsNullOrWhiteSpace(this.ScriptType)) builder.Append($" type=\"{this.ScriptType}\" ");
            if (!String.IsNullOrWhiteSpace(this.ID)) builder.Append($" id=\"{this.ID}\" ");
            builder.Append("></script>");
            return builder.ToString();
        }
    }
}
