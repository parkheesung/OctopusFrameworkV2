using OctopusFramework.V2.Basis;
using OctopusFramework.V2.Web;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace OctopusFramework.V2.MVC
{
    public class GridColumn : IStringWrite
    {
        public string Title { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;

        public int Seq { get; set; } = 0;

        protected ConcurrentDictionary<string, string> attributes { get; set; } = new ConcurrentDictionary<string, string>();

        public Func<object, object> ExpressionColumn { get; set; }

        public GridColumn()
        {
            this.ExpressionColumn = null;
        }

        public void AttributeSet(string key, string value)
        {
            this.attributes.AddOrUpdate(key, value, (oldKey, oldValue) => value);
        }


        public object GetValue(object obj)
        {
            if (this.ExpressionColumn != null)
            {
                return ExpressionColumn(obj);
            }
            else
            {
                return obj;
            }
        }

        public string AttributeGet(string key)
        {
            string result = string.Empty;
            if (this.attributes.TryGetValue(key, out result))
            {
                return result;
            }
            else
            {
                return string.Empty;
            }
        }

        public string Write()
        {
            HtmlTag tag = new HtmlTag(Tags.TH);
            tag.Attributes = this.attributes;
            tag.Content = this.Title;
            tag.SetAttribute("column-id", this.Key);
            return tag.Write();
        }
    }
}
