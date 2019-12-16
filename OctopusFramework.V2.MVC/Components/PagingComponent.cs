using OctopusFramework.V2.Basis;
using OctopusFramework.V2.Web;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace OctopusFramework.V2.MVC
{
    public class PagingComponent : ComponentBase, IComponent, IDisposable, IStringWrite
    {
        #region [ IDisposable ]
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                disposedValue = true;
            }
        }

        ~PagingComponent()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            // GC.SuppressFinalize(this);
        }
        #endregion [ IDisposable ]

        public int TotalCount { get; set; } = 0;
        public int ShowCount { get; set; } = 0;
        public int CurPage { get; set; } = 0;

        public int LastPage { get; set; } = 0;
        public int PreviousPage { get; set; } = 0;
        public int NextPage { get; set; } = 0;

        public int PagingCount { get; set; } = 10;

        public string ActiveClass { get; set; } = string.Empty;

        protected HtmlTag parentTag { get; set; } = new HtmlTag(Tags.UL);
        protected HtmlTag itemTag { get; set; } = new HtmlTag(Tags.LI);

        protected string _parentTag { get; set; } = Tags.UL;
        protected string _tag { get; set; } = Tags.LI;

        protected ConcurrentDictionary<string, HtmlTag> ForwardTags = new ConcurrentDictionary<string, HtmlTag>();
        protected ConcurrentDictionary<string, HtmlTag> BackTags = new ConcurrentDictionary<string, HtmlTag>();

        public string ParentTag
        {
            get
            {
                return this._parentTag;
            }
            set
            {
                this._parentTag = value;
                this.parentTag = new HtmlTag(this._parentTag);
            }
        }

        public string Tag
        {
            get
            {
                return this._tag;
            }
            set
            {
                this._tag = value;
                this.itemTag = new HtmlTag(this._tag);
            }
        }

        public PagingComponent() : base()
        {
        }

        public PagingComponent(int totalCnt, int showCnt = 10, int curPage = 1, string activeClass = "") : this(totalCnt, showCnt, curPage)
        {
            this.ActiveClass = activeClass;
        }

        public PagingComponent(int totalCnt, int showCnt = 10, int curPage = 1) : base()
        {
            this.TotalCount = totalCnt;
            this.ShowCount = showCnt;
            this.CurPage = curPage;

            if (this.TotalCount > this.ShowCount)
            {
                this.LastPage = this.TotalCount / this.ShowCount;  // 601/20 = 30
                int tmp = this.TotalCount % this.ShowCount;  // 601 % 20 = 18
                if (tmp > 0) this.LastPage += 1;   //31
            }
            else
            {
                this.LastPage = 1;
            }

            if (this.CurPage > this.LastPage) this.CurPage = this.LastPage;

            if (this.CurPage > 10)
            {
                this.PreviousPage = (((this.CurPage / 10) - 1) * 10) + 1;
                this.NextPage = this.PreviousPage + 20;
                if (this.NextPage > this.LastPage) this.NextPage = 1;
            }
            else
            {
                this.PreviousPage = 1;
                this.NextPage = 11;
                if (this.NextPage > this.LastPage) this.NextPage = this.LastPage;
            }
        }

        public void SetAttribute(string key, string value)
        {
            this.itemTag.SetAttribute(key, value);
        }

        public void SetParentAttribute(string key, string value)
        {
            this.parentTag.SetAttribute(key, value);
        }

        public void ForwardAppendTag(string index, HtmlTag attrStr)
        {
            this.ForwardTags.AddOrUpdate(index, attrStr, (oldKey, oldValue) => attrStr);
        }

        public void ForwardAppendTag(string tagName, string content, KeyValuePair<string, string> attr)
        {
            var tag = HtmlExtendTag.Create(tagName, content, attr);
            this.ForwardTags.AddOrUpdate(content, tag, (oldKey, oldValue) => tag);
        }


        public HtmlTag GetForwardTag(string index)
        {
            HtmlTag result = null;
            if (this.ForwardTags.TryGetValue(index, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public void BackAppendTag(string index, HtmlTag attrStr)
        {
            this.BackTags.AddOrUpdate(index, attrStr, (oldKey, oldValue) => attrStr);
        }

        public void BackAppendTag(string tagName, string content, KeyValuePair<string, string> attr)
        {
            var tag = HtmlExtendTag.Create(tagName, content, attr);
            this.BackTags.AddOrUpdate(content, tag, (oldKey, oldValue) => tag);
        }

        public HtmlTag GetBackTag(string index)
        {
            HtmlTag result = null;
            if (this.BackTags.TryGetValue(index, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        protected virtual List<int> GetArray()
        {
            var result = new List<int>();

            int st = (this.CurPage > this.PagingCount) ? ((this.CurPage / this.PagingCount) * this.PagingCount) + 1 : 1;
            int ed = (st + this.PagingCount) - 1;
            if (ed > this.LastPage) ed = this.LastPage;

            for(int i = 0; i < this.PagingCount; i++)
            {
                if (st <= ed)
                {
                    result.Add(st++);
                }
                else
                {
                    break;
                }
            }

            return result;
        }

        public string Write()
        {
            StringBuilder builder = new StringBuilder(200);
            builder.Append(this.ForwardTags.Values.SummaryString());

            if (this.TotalCount > 0)
            {
                foreach (int pageNo in GetArray())
                {
                    itemTag.Content = $"{pageNo}";
                    if (this.CurPage == pageNo)
                    {
                        string normalClass = string.Empty;
                        if (!String.IsNullOrWhiteSpace(ActiveClass))
                        {
                            string tmp = string.Empty;
                            if (itemTag.Attributes.TryGetValue("class", out tmp))
                            {
                                normalClass = tmp;
                                itemTag.SetAttribute("class", $"{tmp} {this.ActiveClass}");
                            }
                            else
                            {
                                itemTag.SetAttribute("class", this.ActiveClass);
                            }
                        }
                        builder.Append(itemTag.Write().Replace("{PageNumber}", $"{pageNo}"));
                        if (!String.IsNullOrWhiteSpace(ActiveClass))
                        {
                            string tmp = string.Empty;
                            if (!String.IsNullOrWhiteSpace(normalClass))
                            {
                                itemTag.SetAttribute("class", $"{normalClass}");
                                normalClass = string.Empty;
                            }
                            else
                            {
                                itemTag.SetAttribute("class", "");
                            }
                        }
                    }
                    else
                    {
                        builder.Append(itemTag.Write().Replace("{PageNumber}", $"{pageNo}"));
                    }
                }
            }
            else
            {
                builder.Append(itemTag.Write().Replace("{PageNumber}", "1"));
            }
            builder.Append(this.BackTags.Values.SummaryString());
            this.parentTag.Content = builder.ToString();
            return this.parentTag.Write();
        }
    }
}
