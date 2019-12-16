using OctopusFramework.V2.Basis;
using OctopusFramework.V2.Web;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace OctopusFramework.V2.MVC
{
    public class CalendarHelper : IDisposable, IStringWrite
    {
        #region [ IDisposable ]
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.helper?.ViewContext.Writer.WriteLine("</table>");
                }

                disposedValue = true;
            }
        }

        ~CalendarHelper()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion [ IDisposable ]

        protected HtmlHelper helper { get; set; }

        protected CalendarComponent calendar { get; set; }

        public ConcurrentDictionary<int, HtmlTag> header { get; set; }  = new ConcurrentDictionary<int, HtmlTag>();

        protected ConcurrentDictionary<string, string> in_attribute { get; set; } = new ConcurrentDictionary<string, string>();
        protected ConcurrentDictionary<string, string> out_attribute { get; set; } = new ConcurrentDictionary<string, string>();

        protected ConcurrentDictionary<string, string> today_attribute { get; set; } = new ConcurrentDictionary<string, string>();

        public int Year
        {
            get
            {
                return this.calendar.Year;
            }
            set
            {
                this.calendar.Year = value;
            }
        }

        public int Month
        {
            get
            {
                return this.calendar.Month;
            }
            set
            {
                this.calendar.Month = value;
            }
        }

        public CalendarHelper(HtmlHelper _helper, int year, int month)
        {
            this.helper = _helper;
            this.calendar = new CalendarComponent(year, month);

            this.header.TryAdd(0, new HtmlTag(Tags.TH) { Content = "일" });
            this.header.TryAdd(1, new HtmlTag(Tags.TH) { Content = "월" });
            this.header.TryAdd(2, new HtmlTag(Tags.TH) { Content = "화" });
            this.header.TryAdd(3, new HtmlTag(Tags.TH) { Content = "수" });
            this.header.TryAdd(4, new HtmlTag(Tags.TH) { Content = "목" });
            this.header.TryAdd(5, new HtmlTag(Tags.TH) { Content = "금" });
            this.header.TryAdd(6, new HtmlTag(Tags.TH) { Content = "토" });

            this.helper?.ViewContext.Writer.WriteLine("<table>");
        }

        public void SetHeader(params string[] list)
        {
            int num = 0;
            HtmlTag tmp = null;
            foreach(string item in list)
            {
                if (this.header.TryGetValue(num, out tmp))
                {
                    tmp.Content = item;
                    this.header.AddOrUpdate(num, tmp, (oldKey, oldValue) => tmp);
                }

                num++;
            }
        }

        public enum AttributeType
        {
            COMMON,
            IN,
            OUT,
            TODAY
        }

        public void AddAttribute(AttributeType type, string key, string value)
        {
            switch (type)
            {
                case AttributeType.COMMON:
                    this.in_attribute.AppendAddOrUpdate(key, value);
                    this.out_attribute.AppendAddOrUpdate(key, value);
                    this.today_attribute.AppendAddOrUpdate(key, value);
                    break;
                case AttributeType.IN:
                    this.in_attribute.AppendAddOrUpdate(key, value);
                    break;
                case AttributeType.OUT:
                    this.out_attribute.AppendAddOrUpdate(key, value);
                    break;
                case AttributeType.TODAY:
                    this.today_attribute.AppendAddOrUpdate(key, value);
                    break;
            }
            
        }

        public List<DateTime> List
        {
            get
            {
                return this.calendar.List;
            }
        }


        public bool IsCurrentDate(DateTime dt)
        {
            return this.calendar.IsCurrentDate(dt);
        }

        public virtual MvcHtmlString CalendarWrite()
        {
            return MvcHtmlString.Create(this.Write());
        }

        public virtual MvcHtmlString HeaderWrite()
        {
            HtmlTag result = new HtmlTag(Tags.THEAD);
            result.Content = this.header.Values.SummaryString();
            return MvcHtmlString.Create(result.Write());
        }

        public virtual string Write()
        {
            HtmlTag result = new HtmlTag(Tags.TBODY);
            StringBuilder builder = new StringBuilder(200);
            HtmlTag tr = null;
            HtmlTag td = null;
            foreach(DateTime dt in this.List)
            {
                if ((int)dt.DayOfWeek == 0) tr = new HtmlTag(Tags.TR);
                td = new HtmlTag(Tags.TD);
                if (IsCurrentDate(dt))
                {
                    td.Content = dt.Day.ToString();
                    if (dt.Date == DateTime.Now.Date)
                    {
                        td.Attributes = today_attribute;
                    }
                    else
                    {
                        td.Attributes = in_attribute;
                    }
                }
                else
                {
                    td.Attributes = out_attribute;
                }
                tr.Content += td.Write();
                if ((int)dt.DayOfWeek == 6) builder.AppendLine(tr.Write());
            }
            result.Content = builder.ToString();
            return result.Write();
        }
    }

    public static class ExtendCalendarHelper
    {
        public static CalendarHelper Calendar(this HtmlHelper helper, int year, int month)
        {
            return new CalendarHelper(helper, year, month);
        }
    }
}
