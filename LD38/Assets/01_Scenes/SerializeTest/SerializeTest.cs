using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializeTest : MonoBehaviour
{
    private GameSerializer m_serializer;
    private PlayerData m_playerData;
	// Use this for initialization
	void Start ()
    {
        m_serializer = new GameSerializer();
        m_playerData = new PlayerData(m_serializer);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyUp(KeyCode.A))
        {
            //Serialize
            m_serializer.PlayerData.PlayerLevel = (int) Time.time;
            Debug.Log("Player Level pre Serialize: " + m_serializer.PlayerData.PlayerLevel);
            m_serializer.ShiftData = true;
            m_serializer.EncrypData = true;
            m_serializer.SerializeData();
        }else if(Input.GetKeyUp(KeyCode.S))
        {
            //Deserialize
            m_serializer.ShiftData = true;
            m_serializer.EncrypData = true;
            m_serializer.DeserializeData();
            Debug.Log("Player Level post Deserialize: " + m_serializer.PlayerData.PlayerLevel);
        }
	}
}
