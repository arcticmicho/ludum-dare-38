using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Collections;

public class SerializeHelper
{
    public static List<object> SerializeTypedList<T>(List<T> list)
    {
        List<object> outList = new List<object>();

        for (int a = 0; a < list.Count; ++a)
        {
            outList.Add(list[a]);
        }

        return outList;
    }

    public static List<T> DeserializeTypedList<T>(List<object> list)
    {
        List<T> outList = new List<T>();

        for (int a = 0; a < list.Count; ++a)
        {
            outList.Add(Interpret<T>(list[a]));
        }

        return outList;
    }


    public static List<object> SerializeIntArray(int[] list)
    {
        List<object> outList = new List<object>();

        for (int a = 0; a < list.Length; ++a)
        {
            outList.Add(list[a]);
        }

        return outList;
    }

    public static int[] DeserializeIntArray(List<object> data)
    {
        if (data.Count > 0)
        {
            int[] output = new int[data.Count];
            for (int a = 0; a < data.Count; ++a)
            {
                output[a] = (int)System.Convert.ChangeType(data[a], typeof(int));
            }
            return output;
        }

        return new int[0];
    }

    public static List<object> SerializeFloatArray(float[] list)
    {
        List<object> outList = new List<object>();

        for (int a = 0; a < list.Length; ++a)
        {
            outList.Add(list[a]);
        }

        return outList;
    }

    public static float[] DeserializeFloatArray(List<object> data)
    {
        if (data.Count > 0)
        {
            float[] output = new float[data.Count];
            for (int a = 0; a < data.Count; ++a)
            {
                output[a] = (float)System.Convert.ChangeType(data[a], typeof(float));
            }
            return output;
        }

        return new float[0];
    }


    public static List<object> SerializeList<T>(List<T> list) where T : ISerialized
    {
        List<object> outList = new List<object>();

        for (int a = 0; a < list.Count; ++a)
        {
            outList.Add(list[a].Serialize());
        }

        return outList;
    }

    public static List<object> SerializeDicionaryAsList<T, V>(Dictionary<T, V> dictionary) where V : ISerialized
    {
        List<object> outList = new List<object>();

        foreach (var keyvalue in dictionary)
        {
            outList.Add(keyvalue.Value.Serialize());
        }

        return outList;
    }

    public static List<T> DeserializeList<T>(Dictionary<string, object> dic, string field) where T : ISerialized, new()
    {
        List<T> outList = new List<T>();

        object obj;

        if (dic.TryGetValue(field, out obj))
        {
            List<object> serializedList = (List<object>)dic[field];

            for (int a = 0; a < serializedList.Count; ++a)
            {
                var dataObj = new T();
                dataObj.Deserialize(serializedList[a] as Dictionary<string, object>);
                outList.Add(dataObj);
            }
        }

        return outList;
    }


    public static Dictionary<string, object> GetDic(Dictionary<string, object> dic, string field)
    {
        object obj;
        if (dic.TryGetValue(field, out obj))
        {
            return obj as Dictionary<string, object>;
        }
        return null;
    }

    public static List<object> GetList(Dictionary<string, object> dic, string field)
    {
        object obj;
        if (dic.TryGetValue(field, out obj))
        {
            return obj as List<object>;
        }
        return null;
    }

    public static List<T> GetTypedList<T>(Dictionary<string, object> dic, string field)
    {
        List<T> outList = new List<T>();
        object obj;
        if (dic.TryGetValue(field, out obj))
        {
            List<object> serializedList = (List<object>)obj;

            for (int a = 0; a < serializedList.Count; ++a)
            {
                outList.Add((T)serializedList[a]);
            }
        }

        return outList;
    }


    public static T GetValue<T>(Dictionary<string, object> dic, string field)
    {
        object obj;
        if (dic.TryGetValue(field, out obj))
        {
            return Interpret<T>(obj);
        }

        Debug.LogWarning("Failed To get field: " + field + " from the dictionary");
        return default(T);
    }

    public static T GetValue<T>(Dictionary<string, object> dic, string field, T defaultValue)
    {
        object obj;
        if (dic.TryGetValue(field, out obj))
        {
            return Interpret<T>(obj);
        }

        return defaultValue;
    }

    public static T Interpret<T>(object obj)
    {
        if (obj is T)
        {
            return (T)obj;
        }
        else
        {
            try
            {
                return (T)System.Convert.ChangeType(obj, typeof(T));
            }
            catch (System.InvalidCastException)
            {
                return default(T);
            }
        }
    }

    public static byte[] StringToBytes(string str)
    {
        byte[] bytes = new byte[str.Length * sizeof(char)];
        System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
        return bytes;
    }

    public static string BytesToString(byte[] bytes)
    {
        char[] chars = new char[bytes.Length / sizeof(char)];
        System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
        return new string(chars);
    }

    public static byte[] DataToBytes(Dictionary<string, object> data)
    {
        string json = MiniJSON.Json.Serialize(data);
        return StringToBytes(json);
    }

    public static Dictionary<string, object> BytesToData(byte[] bytes)
    {
        string json = BytesToString(bytes);
        return MiniJSON.Json.Deserialize(json) as Dictionary<string, object>;
    }

    public static string DataToString(Dictionary<string, object> data)
    {
        return MiniJSON.Json.Serialize(data);
    }

    public static Dictionary<string, object> StringToData(string json)
    {
        return MiniJSON.Json.Deserialize(json) as Dictionary<string, object>;
    }
}
