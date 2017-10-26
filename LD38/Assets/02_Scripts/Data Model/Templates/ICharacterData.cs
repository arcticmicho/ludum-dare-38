using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterData
{
    string Name { get; }

	float HealthPoints { get; }

    DamageTable ResistanceTable { get; }

    List<SkillDefinition> Skills { get; }

    float MovementSpeed { get; }


}
