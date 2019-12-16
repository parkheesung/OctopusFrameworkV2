using System.Collections.Concurrent;
using System.Text;

namespace OctopusFramework.V2.MVC
{
    public class WebPage : HtmlPage
    {
        public WebPage() : base()
        {
        }

        public static WebPage Create()
        {
            return new WebPage();
        }

        public static WebPage Create(GridComponent grid)
        {
            var result = new WebPage();
            result.Body.Append(grid.Write());
            return result;
        }

        protected StringBuilder bodyContent { get; set; } = new StringBuilder(1000);

        public new StringBuilder Body 
        { 
            get
            {
                return this.bodyContent;
            }
            set
            {
                this.bodyContent = value;
            }
        }

        public void Clear()
        {
            this.bodyContent.Clear();
        }

        public ConcurrentDictionary<string, MetaTag> Metas
        {
            get
            {
                return this.metas;
            }
            set
            {
                this.metas = value;
            }
        }


        public ConcurrentDictionary<string, StyleTag> Styles
        {
            get
            {
                return this.styles;
            }
            set
            {
                this.styles = value;
            }
        }


        public ConcurrentDictionary<string, ScriptTag> Scripts
        {
            get
            {
                return this.scripts;
            }
            set
            {
                this.scripts = value;
            }
        }

        public override string Write()
        {
            base.Body.Content = this.bodyContent.ToString();
            return base.Write();
        }

        public void SetBodyAttribute(string key, string value)
        {
            base.Body.SetAttribute(key, value);
        }

    }
}
