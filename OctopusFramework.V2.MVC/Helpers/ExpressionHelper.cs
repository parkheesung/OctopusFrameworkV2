using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace OctopusFramework.V2.MVC
{
    public class ExpressionHelper
    {
        public ExpressionHelper()
        {
        }
    }

    public static class ExtendExpressionHelper
    {
        public static string PropertyName(this Expression expression)
        {
            string result = String.Empty;

            if (expression != null)
            {
                Regex filter = new Regex("(\\w\\s?\\=\\>\\s?)?\\w\\.[\\w\\d]{1,100}");
                Regex cut = new Regex("(\\w\\s?\\=\\>\\s?)?\\w\\.");

                var temp = filter.Match(expression.ToString());
                if (temp.Success)
                {
                    result = cut.Replace(temp.Value, "");
                }
            }

            return result;
        }
    }
}
