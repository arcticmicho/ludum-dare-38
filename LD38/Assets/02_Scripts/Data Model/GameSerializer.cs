using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameSerializer
{
    private bool m_shiftData;
    public bool ShiftData
    {
        get { return m_shiftData; }
        set { m_shiftData = value; }
    }
    private bool m_encryptData;
    public bool EncrypData
    {
        get { return m_encryptData; }
        set { m_encryptData = value; }
    }
    private const string c_saveName = "";

    private bool m_isDirty;
    public bool IsDirty
    {
        get { return m_isDirty; }
    }

    public void SetDirty()
    {
        m_isDirty = true;
    }

    public void Serialize()
    {
        
    }

    public void Deserialize()
    {

    }

    private bool SaveGame(Dictionary<string,object> dataToSave, string saveName, out SerializeUtils.ESerializeError error)
    {
        string encodedData = EncodeData(dataToSave);
        return SerializeUtils.SaveToPersistentPath(encodedData, saveName, out error);
    }

    private bool LoadGame(string saveName, out Dictionary<string,object> data, out SerializeUtils.ESerializeError error)
    {
        string savedData;
        bool loaded = SerializeUtils.LoadFromPersistentDataPath(saveName, out savedData, out error);
        if(!loaded)
        {
            data = null;
            return loaded;
        }
        data = DecodeData(savedData);
        return true;
    }

    private string EncodeData(Dictionary<string, object> dataToSave)
    {
        string encodedData = SerializeUtils.ToJSON(dataToSave);

        if (m_shiftData)
        {
            encodedData = SerializeUtils.EncodeValue(encodedData, true, SerializeUtils.GetShiftValue());
        }else
        {
            encodedData = SerializeUtils.EncodeValue(encodedData);
        }

        if(m_encryptData)
        {
            encodedData = SerializeUtils.RSAEncryptString(System.Text.Encoding.UTF8.GetBytes(encodedData));             
        }
        return encodedData;
    }
    
    private Dictionary<string, object> DecodeData(string encodedString)
    {
        string decodedData = encodedString;
        if(m_encryptData)
        {
            decodedData = SerializeUtils.RSADecryptString(Convert.FromBase64String(decodedData));
        }

        if(m_shiftData)
        {
            decodedData = SerializeUtils.DecodeValue(decodedData, true, -1 * SerializeUtils.GetShiftValue());
        }else
        {
            decodedData = SerializeUtils.DecodeValue(decodedData);
        }

        Dictionary<string, object> dataDict = (Dictionary<string,object>) MiniJSON.Json.Deserialize(decodedData);
        return dataDict;
    }
}
