using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    void ApplyDamage(float damage);

    float HealthPoints
    {
        get;
    }

    bool IsDeath
    {
        get;
    }
}
