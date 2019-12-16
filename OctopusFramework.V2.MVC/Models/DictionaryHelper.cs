using System;
using System.Collections.Concurrent;

namespace OctopusFramework.V2.MVC
{
    public static class DictionaryHelper
    {
        public static bool AppendAddOrUpdate(this ConcurrentDictionary<string, string> list, string key, string value)
        {
            try
            {
                string tmp = string.Empty;
                if (list.TryGetValue(key, out tmp))
                {
                    if (!String.IsNullOrWhiteSpace(tmp))
                    {
                        list.AddOrUpdate(key, $"{tmp} {value}", (oldKey, oldValue) => $"{tmp} {value}");
                    }
                    else
                    {
                        list.AddOrUpdate(key, value, (oldKey, oldValue) => value);
                    }
                }
                else
                {
                    list.AddOrUpdate(key, value, (oldKey, oldValue) => value);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
