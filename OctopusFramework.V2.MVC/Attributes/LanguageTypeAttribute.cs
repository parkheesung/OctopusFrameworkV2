using System;

namespace OctopusFramework.V2.MVC
{
    public class LanguageTypeAttribute : Attribute
    {
        public string LanguageType { get; set; } = string.Empty;

        public LanguageTypeAttribute(string Value) : base()
        {
            this.LanguageType = Value;
        }

        public string Value
        {
            get
            {
                return this.LanguageType;
            }
        }
    }
}
