using OctopusFramework.V2.Basis;
using OctopusFramework.V2.Web;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace OctopusFramework.V2.MVC
{
    public static class TagHelper
    {
        public static string ToEncTypeString<T>(this T enumValue) where T : Enum
        {
            string result = String.Empty;

            Type type = enumValue.GetType();
            if (type.IsEnum)
            {
                FieldInfo fi = type.GetField(enumValue.ToString());
                EncTypeAttribute[] attrs = fi.GetCustomAttributes(typeof(EncTypeAttribute), false) as EncTypeAttribute[];
                if (attrs.Length > 0)
                {
                    result = attrs[0].Value;
                }
            }

            return result;
        }

        public static string ToLanguageString<T>(this T enumValue) where T : Enum
        {
            string result = String.Empty;

            Type type = enumValue.GetType();
            if (type.IsEnum)
            {
                FieldInfo fi = type.GetField(enumValue.ToString());
                LanguageTypeAttribute[] attrs = fi.GetCustomAttributes(typeof(LanguageTypeAttribute), false) as LanguageTypeAttribute[];
                if (attrs.Length > 0)
                {
                    result = attrs[0].Value;
                }
            }

            return result;
        }

        public static HtmlTag ToTable<T>(this List<T> list) where T : IEntity
        {
            HtmlTag result = new HtmlTag(Tags.TABLE);
            T target = default(T);

            HtmlTag header = new HtmlTag(Tags.THEAD);
            HtmlTag tr = new HtmlTag(Tags.TR);
            HtmlTag tmp = null;
            foreach (EntityObject obj in target.GetEntities())
            {
                tmp = new HtmlTag(Tags.TH);
                tmp.Content = obj.LogicalName;
                tr.Content += tmp.Write();
            }
            header.Content = tr.Write();

            HtmlTag body = new HtmlTag(Tags.TBODY);
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    tr = new HtmlTag(Tags.TR);
                    foreach (EntityObject obj in target.GetEntities())
                    {
                        tmp = new HtmlTag(Tags.TD);
                        tmp.Content = item.GetValue(obj.PhysicalName).ToString();
                        tr.Content += tmp.Write();
                    }
                    body.Content += tr.Write();
                }
            }

            result.Content = header.Write();
            result.Content += body.Write();

            return result;
        }

        public static HtmlTag ToInput(this EntityObject entity, Tags.InputType type, object Value = null)
        {
            HtmlTag result = new HtmlTag(Tags.INPUT);
            result.Type = HtmlTag.TagType.SingleTag;
            result.SetAttribute(Tags.Attrubutes.TYPE, type.ToString());
            if (entity != null)
            {
                result.SetAttribute(Tags.Attrubutes.NAME, entity.PhysicalName);
                result.SetAttribute(Tags.Attrubutes.ID, entity.PhysicalName);
                result.SetAttribute(Tags.Attrubutes.PLACEHOLDER, entity.LogicalName);
                result.SetAttribute(Tags.Attrubutes.VALUE, $"{Value}");
                if (entity.Size > 0)
                {
                    result.SetAttribute(Tags.Attrubutes.MAXLENGTH, $"{entity.Size}");
                }
            }
            return result;
        }

        public static HtmlTag CreateInput<T>(this T target, Expression<Func<T, object>> p, Tags.InputType type) where T : IEntity
        {
            var result = new HtmlTag();
            string propertyName = String.Empty;
            if (p != null && p.Body != null)
            {
                propertyName = p.Body.PropertyName();
            }
            if (!String.IsNullOrWhiteSpace(propertyName))
            {
                var entity = target.FindEntity(propertyName);
                if (entity != null)
                {
                    result = entity.ToInput(type, target.GetValue(entity.PhysicalName));
                }
            }
            return result;
        }

        public static HtmlTag ListToTag(this List<LinkTag> list)
        {
            var result = new HtmlTag(Tags.UL);

            if (list != null && list.Count > 0)
            {
                HtmlTag temp = null;
                foreach (var item in list)
                {
                    temp = new HtmlTag(Tags.LI);
                    temp.Content = item.Write();
                    result.Content += temp.Write();
                }
            }

            return result;
        }

        public static LinkTag CreateToLink(this string htmlString)
        {
            var result = new LinkTag();

            if (!string.IsNullOrWhiteSpace(htmlString))
            {
                Regex test = new Regex("<a(([\\s]{1})?[\\w\\d]{1,100}=([\"\']{1})+([^>]{1,100})([\"\']{1})+)?>([^<]{0,1000})?</a>");
                Match check = test.Match(htmlString);
                if (check.Success)
                {
                    result = new LinkTag(check.Value.TagFind("a"));
                }
            }

            return result;
        }
    }
}
