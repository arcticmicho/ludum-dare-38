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
    private GameObject m_doll;

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

    public void PlayWalkAnimation()
    {
        PlayAnimationWithTrigger("Walk");
    }

    public void PlayAnimationWithTrigger(string trigger)
    {
        if(m_animator != null && !string.IsNullOrEmpty(trigger))
        {
            m_animator.SetTrigger(trigger);
        }
    }

    public void SetDirection(EDirection direction)
    {
        m_currentDirection = direction;
        m_doll.transform.localScale = new Vector3(Mathf.Abs(m_doll.transform.localScale.x) * (direction == EDirection.Right ? 1 : -1), m_doll.transform.localScale.y, m_doll.transform.localScale.z);
    }

    public void TranslateEntity(Vector3 position)
    {
        transform.position = position;
    }

    public void SetAnimatorTimeScale(float timeScale)
    {
        float normalizedValue = Mathf.Clamp01(timeScale);
        m_animator.speed = normalizedValue;
    }

    public void ResetAnimatorTimeScale()
    {
        SetAnimatorTimeScale(1f);
    }
}
