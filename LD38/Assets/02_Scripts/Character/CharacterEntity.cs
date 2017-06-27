using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEntity : MonoBehaviour
{
    protected EDirection m_currentDirection;
    public EDirection CurrentDirection
    {
        get { return m_currentDirection; }
    }

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

    public void PlayIdleAnimation()
    {
        PlayAnimationWithTrigger("Idle");
    }

    public void PlayAnimationWithTrigger(string trigger)
    {
        if(m_animator != null)
        {
            m_animator.SetTrigger(trigger);
        }
    }

    public void SetDirection(EDirection direction)
    {
        if (m_currentDirection != direction)
        {
            m_currentDirection = direction;
            transform.localScale = transform.localScale * -1;
        }
    }

    public void TranslateEntity(Vector3 position)
    {
        transform.position = position;
    }
}
