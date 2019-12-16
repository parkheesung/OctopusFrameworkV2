using OctopusFramework.V2.Basis;
using OctopusFramework.V2.Web;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace OctopusFramework.V2.MVC
{
    public class EntityGridComponent<T> : ComponentBase, IComponent, IDisposable, IStringWrite where T : IEntity, ITableBinder
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

        ~EntityGridComponent()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            // GC.SuppressFinalize(this);
        }
        #endregion [ IDisposable ]

        protected ConcurrentDictionary<string, GridColumn> columns { get; set; } = new ConcurrentDictionary<string, GridColumn>();

        protected List<T> list { get; set; } = new List<T>();

        public List<T> Data
        {
            get
            {
                return this.list;
            }
            set
            {
                this.list = value;
            }
        }

        public EntityGridComponent() : base()
        {
        }

        public int SequenceNumber { get; set; } = 0;


        public EntityGridComponent(List<T> data) : base()
        {
            this.list = data;
        }

        public EntityGridComponent(List<T> data, int seqNumber) : base()
        {
            this.list = data;
            this.SequenceNumber = seqNumber;
        }

        public virtual void ColumnSet(Expression<Func<T, object>> p, int seq, string title, Func<object, object> columnExpression = null)
        {
            string key = p.PropertyName();
            this.ColumnSet(key, seq, title, columnExpression);
        }

        public virtual void ColumnSet(Expression<Func<T, object>> p, string title, Func<object, object> columnExpression = null)
        {
            string key = p.PropertyName();
            this.ColumnSet(key, this.GetLastNumber + 1, title, columnExpression);
        }

        public virtual void ColumnSet(Expression<Func<T, object>> p, Func<object, object> columnExpression = null)
        {
            string key = p.PropertyName();
            T target = default(T);
            var entity = target.FindEntity(key);
            string title = (entity != null) ? entity.LogicalName : string.Empty;
            this.ColumnSet(key, this.GetLastNumber + 1, title, columnExpression);
        }

        public virtual void ColumnSet(string key, int seq, string title, Func<object, object> columnExpression = null)
        {
            GridColumn target = ColumnGet(key);
            if (target == null) target = new GridColumn();
            target.Key = key;
            target.Title = title;
            target.Seq = seq;
            target.ExpressionColumn = columnExpression;
            this.ColumnSet(key, target);
        }

        public virtual void ColumnSet(string key, string title, Func<object, object> columnExpression = null)
        {
            GridColumn target = ColumnGet(key);
            if (target == null) target = new GridColumn();
            target.Key = key;
            target.Title = title;
            target.Seq = this.GetLastNumber + 1;
            target.ExpressionColumn = columnExpression;
            this.ColumnSet(key, target);
        }

        public int GetLastNumber
        {
            get
            {
                int result = 0;
                if (this.Columns != null && this.Columns.Count() > 0)
                {
                    result = this.Columns.Max(x => x.Seq);
                }
                return result;
            }
        }

        public IEnumerable<GridColumn> Columns
        {
            get
            {
                return this.columns.Values.OrderBy(x => x.Seq);
            }
        }

        public virtual GridColumn ColumnGet(int seq)
        {
            GridColumn result = null;
            foreach (var column in this.columns)
            {
                if (column.Value.Seq == seq)
                {
                    result = column.Value;
                    break;
                }
            }
            return result;
        }

        public virtual void ColumnSet(string key, GridColumn column)
        {
            column.Key = key;
            this.columns.AddOrUpdate(key, column, (oldKey, oldValue) => column);
        }

        public virtual void ColumnAttributeSet(string key, string attr_key, string attr_value)
        {
            GridColumn target = ColumnGet(key);
            if (target != null)
            {
                target.AttributeSet(attr_key, attr_value);
                this.ColumnSet(key, target);
            }
        }


        public virtual GridColumn ColumnGet(string key)
        {
            GridColumn result = null;
            if (this.columns.TryGetValue(key, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public virtual string Write()
        {
            StringBuilder builder = new StringBuilder(200);
            if (this.Columns.Count() > 0)
            {
                HtmlTag thead = new HtmlTag(Tags.THEAD);
                thead.Content = this.Columns.SummaryString();
                builder.AppendLine(thead.Write());
            }

            HtmlTag tbody = new HtmlTag(Tags.TBODY);
            HtmlTag tr = null;
            HtmlTag td = null;
            EntityObject obj = null;
            StringBuilder tr_tags = new StringBuilder(200);
            StringBuilder row = new StringBuilder(200);
            if (this.Data != null && this.Data.Count() > 0)
            {
                foreach (var item in this.Data)
                {
                    tr = new HtmlTag(Tags.TR);
                    tr_tags.Clear();
                    KeyValuePair<int, ITableBinder> paramData = new KeyValuePair<int, ITableBinder>(this.SequenceNumber, item);
                    foreach (GridColumn col in this.Columns)
                    {
                        td = new HtmlTag(Tags.TD);
                        obj = item.GetColumns().Where(x => x.PhysicalName.Equals(col.Key, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                        if (obj != null)
                        {
                            td.Content = $"{col.GetValue(item.GetValue(obj.PhysicalName))}";
                        }
                        else
                        {
                            td.Content = (col.ExpressionColumn != null) ? Convert.ToString(col.GetValue(paramData)) : "&nbsp;";
                        }
                        tr_tags.Append(td.Write());
                    }
                    tr.Content = tr_tags.ToString();
                    row.Append(tr.Write());
                    this.SequenceNumber--;
                }
                tbody.Content = row.ToString();
            }
            else
            {
                tbody.Content = $"<tr><td colspan=\"{this.Columns.Count() + 1}\">no data</td></tr>";
            }
            builder.Append(tbody.Write());

            return builder.ToString();
        }

        public int GetSequenceNumber(object obj)
        {
            int result = 0;
            if (obj != null)
            {
                try
                {
                    KeyValuePair<int, ITableBinder> item = (KeyValuePair<int, ITableBinder>)obj;
                    result = item.Key;
                }
                catch
                {
                    result = -1;
                }
            }
            return result;
        }

        public ITableBinder GetDataObject(object obj)
        {
            if (obj != null)
            {
                try
                {
                    KeyValuePair<int, ITableBinder> item = (KeyValuePair<int, ITableBinder>)obj;
                    return item.Value;
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
