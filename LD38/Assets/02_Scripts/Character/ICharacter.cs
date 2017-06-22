using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    void ApplyDamage(float damage);
    void KillCharacter();

    float HealthPoints
    {
        get;
    }

    bool IsDeath
    {
        get;
    }

    CharacterEntity Entity
    {
        get;
    }
}
