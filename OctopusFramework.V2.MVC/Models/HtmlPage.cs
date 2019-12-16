using OctopusFramework.V2.Basis;
using OctopusFramework.V2.Web;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OctopusFramework.V2.MVC
{
    public class HtmlPage : IStringWrite
    {
        public HtmlPageOptions.Languages Language { get; set; } = HtmlPageOptions.Languages.KOREAN;

        protected ConcurrentDictionary<string, MetaTag> metas { get; set; } = new ConcurrentDictionary<string, MetaTag>();
        protected ConcurrentDictionary<string, StyleTag> styles { get; set; } = new ConcurrentDictionary<string, StyleTag>();
        protected ConcurrentDictionary<string, ScriptTag> scripts { get; set; } = new ConcurrentDictionary<string, ScriptTag>();

        public HtmlTag Body { get; set; } = new HtmlTag("body");
        public string Title { get; set; } = String.Empty;

        public HtmlPage()
        {
        }

        public virtual string Write()
        {
            StringBuilder builder = new StringBuilder(500);
            builder.AppendLine("<!doctype html>");
            builder.AppendLine($"<html lang=\"{this.Language.ToLanguageString()}\">");
            builder.AppendLine("<head>");
            builder.AppendLine($"<meta charset=\"{this.Language.ToEncTypeString()}\">");
            if (this.metas != null && this.metas.Count > 0)
            {
                foreach(var meta in this.metas)
                {
                    builder.AppendLine(meta.Value.Write());
                }
            }
            if (this.styles != null && this.styles.Count > 0)
            {
                foreach (var style in this.styles)
                {
                    builder.AppendLine(style.Value.Write());
                }
            }
            builder.AppendLine($"<title>{this.Title}</title>");
            builder.AppendLine("</head>");
            if (this.scripts != null && this.scripts.Count > 0)
            {
                this.Body.Content += $"{Environment.NewLine}";
                if (this.scripts != null && this.scripts.Count > 0)
                {
                    this.Body.Content += this.scripts.Values.SummaryString();
                }
            }
            builder.AppendLine(this.Body.Write());
            builder.AppendLine("</html>");
            return builder.ToString();
        }
    }

    public class HtmlLanguage
    {
        public const string KOREAN = "ko";
        public const string ENGLISH = "en";
    }

    public class HtmlEncoding
    {
        public const string UTF8 = "utf-8";
        public const string KOREAN = "euc-kr";
    }

    public class HtmlPageOptions
    {
        public enum Languages
        {
            [LanguageType(HtmlLanguage.KOREAN)]
            [EncType(HtmlEncoding.UTF8)]
            KOREAN = 1,

            [LanguageType(HtmlLanguage.ENGLISH)]
            [EncType(HtmlEncoding.KOREAN)]
            ENGLISH = 2
        }
    }

}
