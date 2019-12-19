using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using OctopusFramework.V2.Basis;

namespace OctopusFramework.V2.Web
{
    /// <summary>
    /// 태그의 기본 형태를 정의합니다.
    /// </summary>
    public class HtmlTag : IStringWrite
    {
        /// <summary>
        /// 태그가 가지고 있는 어트리뷰트 목록
        /// </summary>
        protected ConcurrentDictionary<string, string> attributes { get; set; } = new ConcurrentDictionary<string, string>();

        public ConcurrentDictionary<string, string> Attributes
        {
            get
            {
                return this.attributes;
            }
            set
            {
                this.attributes = value;
            }
        }

        /// <summary>
        /// 태그의 형태
        /// </summary>
        public TagType Type { get; set; } = TagType.PairTag;

        /// <summary>
        /// 태그 명칭
        /// </summary>
        public string TagName { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// 초기화
        /// </summary>
        public HtmlTag()
        {
        }

        public HtmlTag(string name)
        {
            this.TagName = name;
        }

        public HtmlTag(string name, TagType type)
        {
            this.TagName = name;
            this.Type = type;
        }

        public static HtmlTag Create(string name, TagType type = TagType.PairTag)
        {
            return new HtmlTag(name, type);
        }

        public static HtmlTag Create(string name, string content, TagType type = TagType.PairTag)
        {
            var result = new HtmlTag(name, type);
            result.Content = content;
            return result;
        }

        public virtual void SetAttribute(string key, string value)
        {
            this.attributes.AddOrUpdate(key.Trim().ToLower(), value, (oldKey, oldValue) => value);
        }

        public virtual void AppendAttribute(string key, string value, char splitChar = ',')
        {
            string temp = string.Empty;
            if (this.attributes.TryGetValue(key, out temp))
            {
                if (!string.IsNullOrWhiteSpace(temp))
                {
                    value = $"{temp}{splitChar}{value}";
                }

                this.attributes.AddOrUpdate(key.Trim().ToLower(), value, (oldKey, oldValue) => value);
            }
            else
            {
                this.attributes.AddOrUpdate(key.Trim().ToLower(), value, (oldKey, oldValue) => value);
            }
        }

        public virtual void RemoveAttribute(string key, string value, char splitChar = ',')
        {
            string temp = string.Empty;
            if (this.attributes.TryGetValue(key, out temp))
            {
                if (!string.IsNullOrWhiteSpace(temp))
                {
                    StringBuilder builder = new StringBuilder(200);
                    string[] values = temp.Split(splitChar);
                    int num = 0;
                    foreach(string val in values)
                    {
                        if (value.Equals(val, System.StringComparison.OrdinalIgnoreCase) == false)
                        {
                            if (num > 0) builder.Append(splitChar);
                            builder.Append(val);
                            num++;
                        }
                    }
                    this.attributes.AddOrUpdate(key.Trim().ToLower(), builder.ToString(), (oldKey, oldValue) => builder.ToString());
                }
            }
        }

        public virtual void ClearAttribute()
        {
            this.attributes.Clear();
        }

        public string GetValue(string key)
        {
            string result = string.Empty;
            if (this.attributes.TryGetValue(key.Trim().ToLower(), out result))
            {
                return result;
            }
            else
            {
                return string.Empty;
            }
        }

        public virtual string Write()
        {
            StringBuilder builder = new StringBuilder(200);

            switch (this.Type)
            {
                case TagType.PairTag:
                    builder.Append($"<{this.TagName}");
                    foreach(var attr in this.attributes)
                    {
                        if (!string.IsNullOrWhiteSpace(attr.Value))
                        {
                            builder.Append($" {attr.Key}=\"{attr.Value}\"");
                        }
                        else
                        {
                            builder.Append($" {attr.Key}");
                        }
                    }
                    builder.AppendLine(">");
                    builder.AppendLine(this.Content);
                    builder.AppendLine($"</{this.TagName}>");
                    break;
                case TagType.SingleTag:
                    builder.Append($"<{this.TagName}");
                    foreach (var attr in this.attributes)
                    {
                        if (!string.IsNullOrWhiteSpace(attr.Value))
                        {
                            builder.Append($" {attr.Key}=\"{attr.Value}\"");
                        }
                        else
                        {
                            builder.Append($" {attr.Key}");
                        }
                    }
                    builder.Append(" />");
                    break;
            }

            return builder.ToString();
        }

        /// <summary>
        /// 태그의 형태 옵션
        /// </summary>
        public enum TagType
        {
            SingleTag = 1, PairTag = 2
        }

        public static KeyValuePair<string, string> CreateAttribute(string key, string value)
        {
            return new KeyValuePair<string, string>(key, value);
        }
    }
}
