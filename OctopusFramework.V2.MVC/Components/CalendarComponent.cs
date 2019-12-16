using OctopusFramework.V2.Basis;
using System;
using System.Collections.Generic;
using System.Text;

namespace OctopusFramework.V2.MVC
{
    public class CalendarComponent : ComponentBase, IComponent, IDisposable, IStringWrite
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

        ~CalendarComponent()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            // GC.SuppressFinalize(this);
        }
        #endregion [ IDisposable ]

        public CalendarComponent() : base()
        {
            this.Year = DateTime.Now.Year;
            this.Month = DateTime.Now.Month;
            this.Day = 1;
            this.Set();
        }

        public CalendarComponent(int year, int month) : base()
        {
            this.Year = year;
            this.Month = month;
            this.Day = 1;
            this.Set();
        }

        public void Set()
        {
            this.TargetDate = new DateTime(this.Year, this.Month, this.Day);
            this.FirstDate = this.TargetDate.FirstDay();
            this.LastDate = this.TargetDate.LastDay();
            int week = (int)this.FirstDate.DayOfWeek;
            this.FirstDate = this.FirstDate.AddDays(-week);
            week = (int)this.LastDate.DayOfWeek;
            this.LastDate = this.LastDate.AddDays(6 - week);
            this.List = new List<DateTime>();
            for (DateTime dt = this.FirstDate; dt.Date <= this.LastDate.Date; dt = dt.AddDays(1))
            {
                this.List.Add(dt);
            }
        }

        public bool IsCurrentDate(DateTime tmp)
        {
            if (tmp.Year == this.Year && tmp.Month == this.Month)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        protected DateTime FirstDate { get; set; }
        protected DateTime LastDate { get; set; }

        protected DateTime TargetDate { get; set; }

        public List<DateTime> List { get; set; } = new List<DateTime>();

        public virtual string Write()
        {
            StringBuilder builder = new StringBuilder(200);
            return builder.ToString();
        }
    }
}
