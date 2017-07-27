using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustomizableMenuItems
{

    [MenuItem("Assets/Create/ScriptableObjects/DamageType")]
    public static void CreateDamageType()
    {
        ScriptableObjectCreator.CreateAsset(typeof(DamageType));
    }

    [MenuItem("Assets/Create/ScriptableObjects/Skill")]
    public static void CreateSkill()
    {
        ScriptableObjectCreator.CreateAsset(typeof(SkillDefinition));
    }

    [MenuItem("Assets/Create/ScriptableObjects/CharacterDef")]
    public static void CreateCharacterDefinition()
    {
        ScriptableObjectCreator.CreateAsset(typeof(CharacterTemplate));
    }

    [MenuItem("Assets/Create/ScriptableObjects/RoomSessionData")]
    public static void CreateRoomDefinition()
    {
        ScriptableObjectCreator.CreateAsset(typeof(RoomSessionData));
    }

}
