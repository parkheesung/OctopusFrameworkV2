using System;
using System.Collections.Generic;
using System.Text;

namespace OctopusFramework.V2.Web
{
    public class HtmlTagParser
    {
        protected string original { get; set; } = string.Empty;

        public HtmlTagParser()
        {
        }

        public HtmlTagParser(string _original)
        {
            this.original = (String.IsNullOrWhiteSpace(_original)) ? String.Empty : _original.Trim();
        }

        public virtual List<HtmlTag> GetTags()
        {
            var result = new List<HtmlTag>();

            if (!String.IsNullOrWhiteSpace(this.original))
            {
                foreach(var pos in this.original.FindPosition())
                {
                    result.Add(this.original.TagFindToCreate(pos));
                }
            }

            return result;
        }

        public virtual HtmlTag FirstTag()
        {
            var result = new HtmlTag();

            if (!String.IsNullOrWhiteSpace(this.original))
            {
                var list = this.GetTags();
                if (list != null && list.Count > 0)
                {
                    result = list[0];
                }
            }

            return result;
        }
    }
}
