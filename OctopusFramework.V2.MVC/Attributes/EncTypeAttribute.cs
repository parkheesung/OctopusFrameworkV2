using System;

namespace OctopusFramework.V2.MVC
{
    public class EncTypeAttribute : Attribute
    {
        public string EncType { get; set; } = string.Empty;

        public EncTypeAttribute(string Value) : base()
        {
            this.EncType = Value;
        }

        public string Value
        {
            get
            {
                return this.EncType;
            }
        }
    }
}
