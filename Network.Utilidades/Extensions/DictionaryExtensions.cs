using System.Collections.Generic;
using System.Linq;

public static class DictionaryExtensions
{
    public static T ToObject<T>(this IDictionary<string, object> source)
        where T : class, new()
    {
        var someObject = new T();
        var someObjectType = someObject.GetType();

        foreach (var item in source)
        {
            string keyCap = item.Key.First().ToString().ToUpper() + item.Key.Substring(1);
            someObjectType
                     .GetProperty(keyCap)
                     .SetValue(someObject, item.Value, null);
        }

        return someObject;
    }

}