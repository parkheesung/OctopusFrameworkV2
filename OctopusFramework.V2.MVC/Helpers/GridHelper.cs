using OctopusFramework.V2.Basis;
using OctopusFramework.V2.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OctopusFramework.V2.MVC
{
    public class GridHelper : IDisposable
    {
        #region [ IDisposable ]
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.helper.ViewContext.Writer.WriteLine("</table>");
                }

                disposedValue = true;
            }
        }

        ~GridHelper()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion [ IDisposable ]

        protected GridComponent grid { get; set; }

        public IEnumerable<ITableBinder> Data
        {
            get
            {
                return this.grid.Data;
            }
            set
            {
                this.grid.Data = value;
            }
        }

        protected HtmlHelper helper { get; set; }

        public GridHelper(HtmlHelper _helper, GridComponent _grid)
        {
            this.helper = _helper;
            this.grid = _grid;
            this.helper.ViewContext.Writer.WriteLine("<table>");
        }

        public MvcHtmlString ShowGrid()
        {
            return MvcHtmlString.Create(this.grid.Write());
        }
    }

    public class EntityGridHelper<T> : IDisposable where T : ITableBinder
    {
        #region [ IDisposable ]
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.helper.ViewContext.Writer.WriteLine("</table>");
                }

                disposedValue = true;
            }
        }

        ~EntityGridHelper()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion [ IDisposable ]

        protected GridPackage<T> grid { get; set; }

        protected HtmlHelper helper { get; set; }

        public EntityGridHelper(HtmlHelper _helper, GridPackage<T> _grid)
        {
            this.helper = _helper;
            this.grid = _grid;
            this.helper.ViewContext.Writer.WriteLine("<table>");
        }

        public MvcHtmlString ShowGrid()
        {
            return MvcHtmlString.Create(this.grid.Write());
        }
    }

    public static class ExtendGridHelper
    {
        public static GridHelper Grid(this HtmlHelper helper, GridComponent grid)
        {
            return new GridHelper(helper, grid);
        }

        public static EntityGridHelper<T> Grid<T>(this HtmlHelper helper, GridPackage<T> grid) where T : ITableBinder
        {
            return new EntityGridHelper<T>(helper, grid);
        }
    }
}
