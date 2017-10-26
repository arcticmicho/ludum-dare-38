using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DictionaryUtils
{
    public delegate P Parser<P>(object obj);
	public static T TryParseValue<T>(this Dictionary<string, object> dict, string key, T defaultValue, Parser<T> customParser = null)
    {
        if(dict.ContainsKey(key))
        {
            try
            {
                if(customParser == null)
                {
                    return (T)dict[key];
                }
                else
                {
                    return customParser(dict[key]);
                }
                
            }catch(Exception e)
            {
                Debug.LogError("Couldn't parse with key: " + key);
                return defaultValue;
            }
        }else
        {
            return defaultValue;
        }
    }
}
