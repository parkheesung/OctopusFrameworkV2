using System;
using System.Collections.Generic;
using System.Text;

namespace OctopusFramework.V2.Web
{
    public class TagPosition
    {
        public string Name { get; set; } = string.Empty;
        public int StartPoint { get; set; } = -1;
        public int EndPoint { get; set; } = -1;

        public int Length
        {
            get
            {
                if (EndPoint > StartPoint)
                {
                    return EndPoint - StartPoint;
                }
                else
                {
                    return 1;
                }
            }
        }

        public TagPosition()
        {
        }

        public TagPosition(string name)
        {
            this.Name = name;
        }
    }
}
