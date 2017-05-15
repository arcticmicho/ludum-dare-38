using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
public class UIAnimationSync
{
    [SerializeField]
    private string m_openAnimation;
    [SerializeField]
    private string m_closeAnimation;

    private List<UIAnimationTask> m_activeTasks;

    private Animation m_animations;

    public Action OnAnimationFinished;

    public bool HasActiveAnimations
    {
        get
        {
            return m_activeTasks.Count > 0;
        }
    }

    public void Initialize(Animation anim)
    {
        m_animations = anim;
        m_activeTasks = new List<UIAnimationTask>();
    }

    public void UpdateAnimationSync(float deltaTime)
    {
        foreach(UIAnimationTask task in m_activeTasks.ToArray())
        {
            if(task.ProcessTask(deltaTime))
            {
                m_activeTasks.Remove(task);
            }
        }
    }

    public UIAnimationTask PlayAnimation(string clipName, Action callback = null)
    {
        AnimationClip clip = m_animations.GetClip(clipName);
        UIAnimationTask newTask = null;
        if (clip != null)
        {
            m_animations.Play(clipName);
            newTask = new UIAnimationTask(clip, callback);
            m_activeTasks.Add(newTask);
        }
        return newTask;
    }

    public bool TryPlayOpenAnimation(out UIAnimationTask task, Action callback = null)
    {
        return TryPlayAnimation(m_openAnimation, out task, callback);        
    }

    public bool TryPlayCloseAnimation(out UIAnimationTask task, Action callback = null)
    {
        return TryPlayAnimation(m_closeAnimation, out task, callback);        
    }

    public bool TryPlayAnimation(string clipName, out UIAnimationTask task, Action callback = null)
    {
        UIAnimationTask newTask = PlayAnimation(clipName, callback);
        if(newTask != null)
        {
            task = newTask;
            return true;
        }else
        {
            task = null;
            return false;
        }
    }


    public class UIAnimationTask
    {
        [SerializeField]
        private AnimationClip m_playedAnimation;

        private float m_animDuration;
        private float m_elapsedTime;

        public Action OnAnimationFinished;

        public UIAnimationTask(AnimationClip animation, Action callback = null)
        {
            m_playedAnimation = animation;
            m_animDuration = m_playedAnimation.length;
            OnAnimationFinished += callback;
        }

        public bool ProcessTask(float deltaTime)
        {
            m_elapsedTime += deltaTime;

            if (m_elapsedTime >= m_animDuration)
            {
                if (OnAnimationFinished != null)
                {
                    OnAnimationFinished();
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
