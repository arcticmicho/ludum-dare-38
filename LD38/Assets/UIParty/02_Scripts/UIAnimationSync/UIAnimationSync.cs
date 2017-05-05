using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
public class UIAnimationSync
{
    [SerializeField]
    private AnimationClip m_openAnimation;
    [SerializeField]
    private AnimationClip m_closeAnimation;

    private List<UIAnimationTask> m_activeTasks;

    private Animation m_animations;

    public Action OnAnimationFinished;

    private bool m_animationPlaying;

    public void Initialize(Animation anim)
    {
        m_animations = anim;
        m_activeTasks = new List<UIAnimationTask>();
        m_animationPlaying = false;
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

    public UIAnimationTask PlayOpenAnimation(Action callback = null)
    {
        return PlayAnimation(m_openAnimation.name, callback);
    }

    public UIAnimationTask PlayCloseAnimation(Action callback = null)
    {
        return PlayAnimation(m_closeAnimation.name, callback);
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
