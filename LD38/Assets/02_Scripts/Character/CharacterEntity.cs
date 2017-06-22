using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEntity : MonoBehaviour
{
    [SerializeField]
    private Animator m_animator;

    [SerializeField]
    private CharacterCanvas m_characterCanvas;
    public CharacterCanvas CharacterCanvas
    {
        get { return m_characterCanvas; }
    }

    public void PlayCastAnimation()
    {
        PlayAnimationWithTrigger("CastAbility");
    }

    public void PlayDeadAnimation()
    {
        PlayAnimationWithTrigger("Dead");
    }

    public void PlayThrowAbilityAnimation()
    {
        PlayAnimationWithTrigger("ThrowAbility");
    }

    public void PlayHitAnimation()
    {
        PlayAnimationWithTrigger("Hit");
    }

    public void PlayAnimationWithTrigger(string trigger)
    {
        if(m_animator != null)
        {
            m_animator.SetTrigger(trigger);
        }
    }
}
