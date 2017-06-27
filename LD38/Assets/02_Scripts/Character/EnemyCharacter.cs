using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{    

    public EnemyCharacter(CharacterTemplate template) : base(template)
    {
                
    }

    public override void KillCharacter()
    {
        base.KillCharacter();
        GameManager.Instance.ActiveGameSession.NotifyEnemyDeath(this);
    }
}
