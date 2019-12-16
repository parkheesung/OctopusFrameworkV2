using OctopusFramework.V2.Web;
using System;
using System.Collections.Generic;
using System.Text;

namespace OctopusFramework.V2.MVC
{
    public class LinkTag : HtmlTag
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

        public string Target
        {
            get
            {
                return this.GetValue("target");
            }
            set
            {
                this.SetAttribute("target", value);
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

        public LinkTag() : base("a")
        {
        }

        public LinkTag(HtmlTag tag) : base("a")
        {
            this.Content = tag.Content;
            this.Attributes = tag.Attributes;
        }

        public static LinkTag CreateTag(string url, string text, string target = "")
        {
            LinkTag result = new LinkTag();
            result.Href = url;
            result.Content = text;
            if (!String.IsNullOrWhiteSpace(target))
            {
                result.Target = target;
            }
            return result;
        }
    }
}
