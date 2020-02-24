using OctopusFramework.V2.Basis;
using OctopusFramework.V2.Web;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace OctopusFramework.V2.MVC
{
    public class DynamicResult : FileResult
    {
        public WebPage Page { get; set; }

        public string HTML
        {
            get
            {
                return this.builder.ToString();
            }
            set
            {
                this.builder = new StringBuilder(value);
            }
        }

        public Encoding encType { get; set; } = Encoding.UTF8;

        public StringBuilder builder = new StringBuilder();

        public StringBuilder Body
        {
            get
            {
                return builder;
            }
            set
            {
                this.builder = value;
            }
        }

        public string Title
        {
            get
            {
                return this.Page.Title;
            }
            set
            {
                this.Page.Title = value;
            }
        }
        public DynamicResult() : base("text/html")
        {
            this.Page = null;
        }

        public DynamicResult(WebPage _page) : base("text/html")
        {
            this.Page = _page;
        }

        public static DynamicResult Create(WebPage _page)
        {
            return new DynamicResult(_page);
        }

        public static DynamicResult Create(WebPage layout, HtmlTag tag, Encoding encoding = null)
        {
            var page = new DynamicResult();
            page.Page = layout;
            page.Page.Body.AppendLine(tag.Write());
            if (encoding != null) page.encType = encoding;
            return page;
        }

        public static DynamicResult Grid(GridComponent grid, Encoding encoding = null)
        {
            var page = new DynamicResult();
            var table = new HtmlTag(Tags.TABLE);
            table.Content = grid.Write();
            page.HTML = table.Write();
            if (encoding != null) page.encType = encoding;
            return page;
        }

        public static DynamicResult Grid<T>(GridPackage<T> grid, HtmlTag tag = null, Encoding encoding = null) where T : ITableBinder
        {
            var page = new DynamicResult();
            if (tag == null)
            {
                page.Page.Body.AppendLine(grid.Write());
            }
            else
            {
                tag.Content = grid.Write();
                page.Page.Body.AppendLine(tag.Write());
            }
            page.Page.Body.AppendLine(grid.Paging.Write());

            if (encoding != null) page.encType = encoding;
            return page;
        }

        public static DynamicResult Grid(WebPage layout, GridComponent grid, Encoding encoding = null)
        {
            var page = new DynamicResult();
            page.Page = layout;
            var table = new HtmlTag(Tags.TABLE);
            table.Content = grid.Write();
            page.Page.Body.AppendLine(table.Write());
            if (encoding != null) page.encType = encoding;
            return page;
        }

        public static DynamicResult Grid<T>(GridPackage<T> pageData, Encoding encoding = null) where T : ITableBinder
        {
            var page = new DynamicResult();
            StringBuilder builder = new StringBuilder(200);
            var table = new HtmlTag(Tags.TABLE);
            table.Content = pageData.Write();
            builder.AppendLine(table.Write());
            builder.AppendLine(pageData.Paging.Write());
            page.HTML = builder.ToString();
            if (encoding != null) page.encType = encoding;
            return page;
        }

        public static DynamicResult Calendar(CalendarHelper calendar, Encoding encoding = null)
        {
            var page = new DynamicResult();
            StringBuilder builder = new StringBuilder(200);
            var table = new HtmlTag(Tags.TABLE);
            HtmlTag header = new HtmlTag(Tags.THEAD);
            header.Content = calendar.header.Values.SummaryString();
            builder.AppendLine(header.Write());
            builder.AppendLine(calendar.Write());
            table.Content = builder.ToString();
            page.HTML = $"<h3>{calendar.Year} / {calendar.Month}</h3>{table.Write()}";
            if (encoding != null) page.encType = encoding;
            return page;
        }

        protected override void WriteFile(HttpResponseBase response)
        {
            string body = (Page == null) ? this.HTML : Page.Write();
            Stream outputStream = response.OutputStream;
            byte[] byteArray = this.encType.GetBytes(body);
            response.HeaderEncoding = this.encType;
            response.ContentEncoding = this.encType;
            response.OutputStream.Write(byteArray, 0, byteArray.GetLength(0));
        }
    }
}
