using UnityEngine;
using System.Collections;

namespace GameModules
{
    public class UIPanelClipAnimation : UIPanelAnimation
    {
        [SerializeField]
        private Animator _animator;

        [Header("Animations")]
        [SerializeField]
        private AnimationClip _showClip;

        [SerializeField]
        private bool _showReverse = false;

        [SerializeField]
        private AnimationClip _hideClip;

        [SerializeField]
        private bool _hideReverse = false;

        public void Awake()
        {
            AnimatorOverrideController overrideAnimatorController = new AnimatorOverrideController(_animator.runtimeAnimatorController);
            _animator.runtimeAnimatorController = overrideAnimatorController;

            if (_showClip != null)
            {
                overrideAnimatorController["BaseShow"] = _showClip;
            }

            if (_hideClip != null)
            {
                overrideAnimatorController["BaseHide"] = _hideClip;
            }
        }

        public override void OnAnimateShow()
        {
            if (_showClip != null)
            {
                if(_showReverse)
                {
                    _animator.Play("Show_R", 0, 0);
                }
                else
                {
                    _animator.Play("Show", 0, 0);
                }
                
                UIManager.Instance.StartCoroutine(WaitToComplete(_showClip.length));
            }
            else
            {
                CallOnComplete();
            }
        }

        public override void OnAnimateHide()
        {
            if (_hideClip != null)
            {
                if (_hideReverse)
                {
                    _animator.Play("Hide_R", 0, 0);
                }
                else
                {
                    _animator.Play("Hide", 0, 0);
                }

                UIManager.Instance.StartCoroutine(WaitToComplete(_hideClip.length));
            }
            else
            {
                CallOnComplete();
            }
        }


        public IEnumerator WaitToComplete(float time)
        {
            yield return new WaitForSeconds(time);
            CallOnComplete();
        }
    }
}