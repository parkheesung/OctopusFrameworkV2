using OctopusFramework.V2.Basis;
using OctopusFramework.V2.Web;
using OctopusFramework.V2.MVC;
using System.Collections.Generic;

namespace OctopusFramework.V2.MVC
{
    public class GridPackage<T> : EntityGridComponent<T> where T : ITableBinder
    {
        public PagingComponent Paging { get; set; }

        public GridPackage(List<T> data, int totalCnt, int CurPage, int PageSize = 20) : base(data)
        {
            this.Paging = null;
            this.Data = data;
            this.Paging = new PagingComponent(totalCnt, PageSize, CurPage);
            this.Paging.ActiveClass = "active";
            this.SequenceNumber = totalCnt - ((CurPage - 1) * PageSize);
        }

        public virtual void SetPagingLinkScript(string script)
        {
            this.Paging.SetAttribute("onclick", script);
        }

        public virtual void SetPagingLinkButton(GridPackageHelper.PagingButtonPosition position, string key, string script, string eventName = "onclick")
        {
            switch (position)
            {
                case GridPackageHelper.PagingButtonPosition.Forward:
                    this.Paging.ForwardAppendTag(Tags.LI, key, HtmlTag.CreateAttribute(eventName, script));
                    break;
                case GridPackageHelper.PagingButtonPosition.Back:
                    this.Paging.BackAppendTag(Tags.LI, key, HtmlTag.CreateAttribute(eventName, script));
                    break;
            }
        }

        public int LastPage
        {
            get
            {
                return this.Paging.LastPage;
            }
        }

        public int PreviousPage
        {
            get
            {
                return this.Paging.PreviousPage;
            }
        }

        public int NextPage
        {
            get
            {
                return this.Paging.NextPage;
            }
        }
    }

    public class GridPackageHelper
    {
        public const string PagingNumberReplaceKeyword = "{PageNumber}";

        public enum PagingButtonPosition
        {
            Forward, Back
        }
    }
}
