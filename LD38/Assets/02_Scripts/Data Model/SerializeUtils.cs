using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class SerializeUtils
{
    private static RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

    public static string ToJSON(Dictionary<string, object> dict)
    {
        return MiniJSON.Json.Serialize(dict);
    }

    public static string EncodeValue(string json)
    {
        return EncodeValue(json, false, 0);
    }

    public static string EncodeValue(string json, bool shift, int shiftValue)
    {
        string encodedString = json;
        if(shift)
        {
            encodedString = ShiftString(json, shiftValue);
        }
        byte[] encodeBytes = Encoding.UTF8.GetBytes(encodedString);
        return Convert.ToBase64String(encodeBytes);
    }

    public static string DecodeValue(string encodedData, bool shift, int shiftValue)
    {
        string decodedString = encodedData;
        byte[] decodedBytes = Convert.FromBase64String(decodedString);
        decodedString = Encoding.UTF8.GetString(decodedBytes);
        if(shift)
        {
            decodedString = ShiftString(decodedString, shiftValue);
        }
        return decodedString;
    }

    public static string DecodeValue(string encodedData)
    {
        return DecodeValue(encodedData, false, 0);
    }

    private static string ShiftString(string baseString, int shiftValue)
    {
        char[] chars = baseString.ToCharArray();
        for (int i = 0, count = chars.Length; i < count; i++)
        {
            chars[i] = (char)(chars[i] + shiftValue);
        }
        return new string(chars);
    }

    internal static int GetShiftValue()
    {
        return 5;
    }

    public static byte[] RSAEncrypt(byte[] dataToEncrypt)
    {
        RSA.ImportParameters(RSA.ExportParameters(false));
        byte[] encrypted = RSA.Encrypt(dataToEncrypt, false);
        return encrypted;
    }

    public static string RSAEncryptString(byte[] dataToEncrypt)
    {
        return Convert.ToBase64String(RSAEncrypt(dataToEncrypt));
    }    

    public static byte[] RSADecrypt(byte[] dataToDecrypt)
    {
        RSA.ImportParameters(RSA.ExportParameters(true));
        byte[] decrypted = RSA.Decrypt(dataToDecrypt, false);
        return decrypted;
    }

    public static string RSADecryptString(byte[] dataToDecrypt)
    {
        return Convert.ToBase64String(RSADecrypt(dataToDecrypt));
    }

    private const string c_saveFormat = "{0}/{1}";

    public static bool SaveToPersistentPath(string dataToSave, string saveName, out ESerializeError error)
    {
        StreamWriter stream = File.CreateText(string.Format(c_saveFormat, Application.persistentDataPath, saveName));
        stream.Write(dataToSave);
        error = ESerializeError.None;
        stream.Close();
        return true;
    }

    public static bool LoadFromPersistentDataPath(string saveName, out string savedData, out ESerializeError error)
    {
        try
        {
            StreamReader stream = File.OpenText(string.Format(c_saveFormat, Application.persistentDataPath, saveName));
            savedData = stream.ReadToEnd();
            error = ESerializeError.None;
            return true;
        }
        catch (UnauthorizedAccessException e)
        {
            savedData = string.Empty;
            error = ESerializeError.UnauthorizedAccess;
            return false;
        }
        catch (FileNotFoundException e)
        {
            savedData = string.Empty;
            error = ESerializeError.FileNotFound;
            return false;
        }
        catch (NotSupportedException e)
        {
            savedData = string.Empty;
            error = ESerializeError.UnauthorizedAccess;
            return false;
        }               
    }

    public enum ESerializeError
    {
        None = 0,
        FileNotFound = 1,
        PathDoesntExist = 2,
        UnauthorizedAccess = 3
    }
	
}
