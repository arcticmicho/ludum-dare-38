using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEntity : MonoBehaviour
{
    [SerializeField]
    private Animator m_animator;

    public void PlayCastAnimation()
    {
        PlayAnimationWithTrigger("CastAbility");
    }

    public void PlayAnimationWithTrigger(string trigger)
    {
        if(m_animator != null)
        {
            m_animator.SetTrigger(trigger);
        }
    }
}
