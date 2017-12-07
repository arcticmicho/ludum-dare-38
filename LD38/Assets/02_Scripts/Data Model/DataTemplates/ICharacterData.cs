using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterData
{
    string Name { get; set; }

    int Level { get; set; }

    float HealthPoints { get; }

    DamageTable ResistanceTable { get; set; }

    List<SkillData> Skills { get; set; }

    float MovementSpeed { get; set; }

    string EntityTemplateId { get; set; }


}
