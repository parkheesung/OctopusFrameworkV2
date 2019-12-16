using System.Web.Mvc;

namespace OctopusFramework.V2.MVC
{
    public class PagingHelper
    {
    }

    public static class ExtendPagingHelper
    {
        public static MvcHtmlString Paging(this HtmlHelper html, PagingComponent paging)
        {
            return MvcHtmlString.Create(paging.Write());
        }
    }
}
