using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OctopusFramework.V2.Web
{
    public static class ExtendHtmlTagParser
    {
        public static string TagFind(this string original, TagPosition position)
        {
            string result = String.Empty;

            if (!string.IsNullOrWhiteSpace(original))
            {
                if (position.EndPoint > -1)
                {
                    result = original.Substring(position.StartPoint, position.Length);
                }
                else
                {
                    result = original.Substring(position.StartPoint);
                    if (result.IndexOf("/>") > -1)
                    {
                        result.Substring(0, result.IndexOf("/>") + 2);
                    }
                }
            }

            return result;
        }

        public static HtmlTag TagFindToCreate(this string original, TagPosition position)
        {
            HtmlTag result = new HtmlTag();
            result.TagName = position.Name;

            if (!string.IsNullOrWhiteSpace(original))
            {
                string temp = String.Empty;
                if (position.EndPoint > -1)
                {
                    temp = original.Substring(position.StartPoint, position.Length);
                }
                else
                {
                    temp = original.Substring(position.StartPoint);
                    if (temp.IndexOf("/>") > -1)
                    {
                        temp.Substring(0, temp.IndexOf("/>") + 2);
                    }
                }
                if (temp.IndexOf(">") > -1)
                {
                    string attrStr = temp.Substring(position.Name.Length + 1, temp.IndexOf(">") - position.Name.Length).Replace("<", "").Replace(">", "").Trim();
                    temp = temp.Substring(temp.IndexOf(">") + 1);

                    if (!String.IsNullOrWhiteSpace(attrStr))
                    {
                        Regex attrEx = new Regex("([\\w\\d]{1,100}=([\"\']{1})+([^>^\"]{1,100})([\"\']{1})+)?");
                        foreach (Match match in attrEx.Matches(attrStr))
                        {
                            if (match.Success && match.Value.IndexOf("=") > -1)
                            {
                                result.SetAttribute(match.Value.Split('=')[0], match.Value.Split('=')[1].Replace("\"","").Replace("'","").Trim());
                            }
                        }
                    }
                }
                if (temp.LastIndexOf("<") > -1)
                {
                    result.Content = temp.Substring(0, temp.LastIndexOf("<"));
                    result.Type = HtmlTag.TagType.PairTag;
                }
                else
                {
                    result.Type = HtmlTag.TagType.SingleTag;
                }
            }

            return result;
        }

        public static List<TagPosition> FindPosition(this string original)
        {
            var result = new List<TagPosition>();

            if (!string.IsNullOrWhiteSpace(original))
            {
                Regex reg = new Regex("<\\w+\\s?");
                var matchs = reg.Matches(original);
                if (matchs != null && matchs.Count > 0)
                {
                    foreach(Match match in matchs)
                    {
                        result.Add(new TagPosition(match.Value.Replace("<", "").Trim()));
                    }
                }

                if (result != null && result.Count > 0)
                {
                    for(int i = 0; i < result.Count; i++)
                    {
                        if (i > 0)
                        {
                            result[i].StartPoint = original.Substring(original.IndexOf(">")).IndexOf("<" + result[i].Name) + original.IndexOf(">");
                            result[i].EndPoint = original.Substring(0, original.LastIndexOf("<")).IndexOf("</" + result[i].Name);
                            if (result[i].EndPoint > -1)
                            {
                                result[i].EndPoint += result[i].Name.Length + 3;
                            }
                        }
                        else
                        {
                            result[i].StartPoint = original.IndexOf("<" + result[i].Name);
                            result[i].EndPoint = original.LastIndexOf("</" + result[i].Name);
                            if (result[i].EndPoint > -1)
                            {
                                result[i].EndPoint += result[i].Name.Length + 3;
                            }
                        }
                    }
                }
            }

            return result;
        }

        public static HtmlTag TagFind(this string original, string Name)
        {
            var result = new HtmlTag();

            if (!String.IsNullOrWhiteSpace(original) && !String.IsNullOrWhiteSpace(Name))
            {
                foreach(var pos in original.FindPosition())
                {
                    if (pos.Name.Equals(Name, StringComparison.OrdinalIgnoreCase))
                    {
                        result = original.TagFindToCreate(pos);
                    }
                }
            }

            return result;
        }
    }
}
