using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;

static class CollectionExtensionMethods
{
    public static string Expand<T>(this IEnumerable<T> enumerable)
    {
        StringBuilder sb = new StringBuilder("");
        foreach (T element in enumerable)
        {
            sb.Append(element.ToString());
            sb.Append(", ");
        }
        return sb.ToString();
    }
}
