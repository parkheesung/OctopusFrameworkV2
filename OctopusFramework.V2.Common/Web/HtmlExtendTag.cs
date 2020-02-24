using System;
using System.Collections.Generic;
using System.Text;

namespace OctopusFramework.V2.Web
{
    public class HtmlExtendTag : HtmlTag
    {
        public HtmlExtendTag() : base()
        {
        }

        public HtmlExtendTag(string name) : base(name)
        {
        }

        public HtmlExtendTag(string name, TagType type) : base(name, type)
        {
        }

        public string ID
        {
            get
            {
                return this.GetValue("ID");
            }
            set
            {
                SetAttribute("ID", value);
            }
        }

        public string Name
        {
            get
            {
                return this.GetValue("Name");
            }
            set
            {
                SetAttribute("Name", value);
            }
        }

        public string NameAndID
        {
            set
            {
                SetAttribute("Name", value);
                SetAttribute("ID", value);
            }
        }

        public string Class
        {
            get
            {
                return this.GetValue("Class");
            }
            set
            {
                SetAttribute("Class", value);
            }
        }

        public string Style
        {
            get
            {
                return this.GetValue("Style");
            }
            set
            {
                SetAttribute("Style", value);
            }
        }

        public static new HtmlExtendTag Create(string name, TagType type = TagType.PairTag)
        {
            return new HtmlExtendTag(name, type);
        }

        public static new HtmlExtendTag Create(string name, string content, TagType type = TagType.PairTag)
        {
            var result = new HtmlExtendTag(name, type);
            result.Content = content;
            return result;
        }

        public static HtmlExtendTag Create(string name, string content, params KeyValuePair<string, string>[] attributes)
        {
            var result = new HtmlExtendTag(name);
            result.Content = content;
            foreach (var attr in attributes)
            {
                result.SetAttribute(attr.Key, attr.Value);
            }
            return result;
        }


    }
}
