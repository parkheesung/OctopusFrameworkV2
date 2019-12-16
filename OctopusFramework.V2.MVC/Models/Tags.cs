namespace OctopusFramework.V2.MVC
{
    public class Tags
    {
        public const string UL = "ul";
        public const string OL = "ul";
        public const string LI = "li";
        public const string DL = "dl";
        public const string DT = "dt";
        public const string DD = "dd";
        public const string TABLE = "table";
        public const string THEAD = "thead";
        public const string TBODY = "tbody";
        public const string TFOOT = "tfoot";
        public const string TR = "tr";
        public const string TH = "th";
        public const string TD = "td";
        public const string A = "a";
        public const string DIV = "div";
        public const string NAV = "nav";
        public const string BODY = "body";
        public const string SCRIPT = "script";
        public const string LINK = "link";
        public const string META = "meta";
        public const string HTML = "html";
        public const string TITLE = "title";
        public const string HEAD = "head";
        public const string STYLE = "style";
        public const string FIELDSET = "fieldset";
        public const string IMG = "img";
        public const string BUTTON = "button";
        public const string INPUT = "input";
        public const string SELECT = "select";
        public const string LEGEND = "legend";
        public const string TEXTAREA = "textarea";
        public const string H1 = "h1";
        public const string H2 = "h2";
        public const string H3 = "h3";
        public const string H4 = "h4";
        public const string H5 = "h5";
        public const string H6 = "h6";
        public const string HR = "hr";
        public const string LABEL = "label";
        public const string SPAN = "span";
        public const string SMALL = "small";
        public const string P = "p";
        public const string HEADER = "header";
        public const string FOOTER = "footer";
        public const string ARTICLE = "article";
        public const string SECTION = "section";
        public const string SVG = "svg";
        public const string CANVAS = "canvas";
        public const string AUDIO = "audio";
        public const string VIDEO = "video";
        public const string NOSCRIPT = "noscript";
        public const string IFRAME = "iframe";
        public const string OBJECT = "object";
        public const string ABBR = "abbr";

        public static TagAttribute Attrubutes { get; set; } = new TagAttribute();

        public enum InputType
        {
            Text = 1,
            Hidden = 2,
            Password = 3,
            Email = 4,
            Number = 5,
            Date = 6
        }

    }

    public class TagAttribute
    {
        public readonly string TYPE = "type";
        public readonly string PLACEHOLDER = "placeholder";
        public readonly string VALUE = "value";
        public readonly string MAXLENGTH = "maxlength";
        public readonly string TARGET = "target";
        public readonly string HREF = "href";
        public readonly string ID = "id";
        public readonly string NAME = "name";
    }
}
