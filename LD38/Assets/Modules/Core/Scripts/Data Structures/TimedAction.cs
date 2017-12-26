using System;
using System.Collections.Generic;

public abstract class BaseTimedAction
{
    private bool _ignoreTimeScale;
    private bool _paused;
    private float _remainingSeconds;

    public BaseTimedAction(float seconds, bool ignoreTimeScale = false)
    {
        _remainingSeconds = seconds;
        _ignoreTimeScale = ignoreTimeScale;
    }

    public bool Update(float deltaTime, float timeScale = 1)
    {
        if (_remainingSeconds > 0 && !_paused)
        {
            if (_ignoreTimeScale)
            {
                _remainingSeconds -= deltaTime;
            }
            else
            {
                _remainingSeconds -= deltaTime * timeScale;
            }
            if (_remainingSeconds <= 0)
            {
                _remainingSeconds = 0;
                Finish();
                return true;
            }
        }
        return false;
    }

    public void Pause()
    {
        _paused = true;
    }

    public void Resume()
    {
        _paused = false;
    }

    public abstract void Finish();
}

public static class TimedAction
{
    #region SubClasses
    public class TimedAction0 : BaseTimedAction
    {
        private Action OnFinish;

        public TimedAction0(Action onFinish, float seconds, bool ignoreTimeScale = false)
            : base(seconds, ignoreTimeScale)
        {
            OnFinish = onFinish;
        }

        public override void Finish()
        {
            OnFinish.SafeInvoke();
        }
    }

    public class TimedAction1<T> : BaseTimedAction
    {
        private Action<T> OnFinish;
        private T _param;

        public TimedAction1(Action<T> onFinish, float seconds, T param, bool ignoreTimeScale = false)
            : base(seconds, ignoreTimeScale)
        {
            OnFinish = onFinish;
            _param = param;
        }

        public override void Finish()
        {
            OnFinish.SafeInvoke(_param);
        }
    }

    public class TimedAction2<T1, T2> : BaseTimedAction
    {
        private Action<T1, T2> OnFinish;
        private T1 _param1;
        private T2 _param2;

        public TimedAction2(Action<T1, T2> onFinish, float seconds, T1 param1, T2 param2, bool ignoreTimeScale = false)
            : base(seconds, ignoreTimeScale)
        {
            OnFinish = onFinish;
            _param1 = param1;
            _param2 = param2;
        }

        public override void Finish()
        {
            OnFinish.SafeInvoke(_param1, _param2);
        }
    }

    public class TimedAction3<T1, T2, T3> : BaseTimedAction
    {
        private Action<T1, T2, T3> OnFinish;
        private T1 _param1;
        private T2 _param2;
        private T3 _param3;

        public TimedAction3(Action<T1, T2, T3> onFinish, float seconds, T1 param1, T2 param2, T3 param3, bool ignoreTimeScale = false)
            : base(seconds, ignoreTimeScale)
        {
            OnFinish = onFinish;
            _param1 = param1;
            _param2 = param2;
            _param3 = param3;
        }

        public override void Finish()
        {
            OnFinish.SafeInvoke(_param1, _param2, _param3);
        }
    }

    public class TimedAction4<T1, T2, T3, T4> : BaseTimedAction
    {
        private Action<T1, T2, T3, T4> OnFinish;
        private T1 _param1;
        private T2 _param2;
        private T3 _param3;
        private T4 _param4;

        public TimedAction4(Action<T1, T2, T3, T4> onFinish, float seconds, T1 param1, T2 param2, T3 param3, T4 param4, bool ignoreTimeScale = false)
            : base(seconds, ignoreTimeScale)
        {
            OnFinish = onFinish;
            _param1 = param1;
            _param2 = param2;
            _param3 = param3;
            _param4 = param4;
        }

        public override void Finish()
        {
            OnFinish.SafeInvoke(_param1, _param2, _param3, _param4);
        }
    }
    #endregion

    #region Static
    private static List<BaseTimedAction> _timedActions = new List<BaseTimedAction>();

    public static BaseTimedAction Start(Action onFinish, float seconds, bool ignoreTimeScale = false)
    {
        TimedAction0 newAction = new TimedAction0(onFinish, seconds, ignoreTimeScale);
        _timedActions.Add(newAction);
        return newAction;
    }

    public static BaseTimedAction Start<T>(Action<T> onFinish, float seconds, T param, bool ignoreTimeScale = false)
    {
        TimedAction1<T> newAction = new TimedAction1<T>(onFinish, seconds, param, ignoreTimeScale);
        _timedActions.Add(newAction);
        return newAction;
    }

    public static BaseTimedAction Start<T1, T2>(Action<T1, T2> onFinish, float seconds, T1 param1, T2 param2, bool ignoreTimeScale = false)
    {
        TimedAction2<T1, T2> newAction = new TimedAction2<T1, T2>(onFinish, seconds, param1, param2, ignoreTimeScale);
        _timedActions.Add(newAction);
        return newAction;
    }

    public static BaseTimedAction Start<T1, T2, T3>(Action<T1, T2, T3> onFinish, float seconds, T1 param1, T2 param2, T3 param3, bool ignoreTimeScale = false)
    {
        TimedAction3<T1, T2, T3> newAction = new TimedAction3<T1, T2, T3>(onFinish, seconds, param1, param2, param3, ignoreTimeScale);
        _timedActions.Add(newAction);
        return newAction;
    }

    public static BaseTimedAction Start<T1, T2, T3, T4>(Action<T1, T2, T3, T4> onFinish, float seconds, T1 param1, T2 param2, T3 param3, T4 param4, bool ignoreTimeScale = false)
    {
        TimedAction4<T1, T2, T3, T4> newAction = new TimedAction4<T1, T2, T3, T4>(onFinish, seconds, param1, param2, param3, param4, ignoreTimeScale);
        _timedActions.Add(newAction);
        return newAction;
    }

    public static void CancelAll(BaseTimedAction action)
    {
        foreach (var timedAction in _timedActions)
        {
            timedAction.Cancel();
        }
    }

    public static void Cancel(this BaseTimedAction action)
    {
        _timedActions.Remove(action);
    }

    public static void AccelerateAll(BaseTimedAction action)
    {
        foreach (var timedAction in _timedActions)
        {
            timedAction.Accelerate();
        }
    }

    public static void Accelerate(this BaseTimedAction action)
    {
        if (action != null)
        {
            action.Finish();
            _timedActions.Remove(action);
        }
        else
        {
            Debug.LogWarning("Trying to accelerate a null action!");
        }
    }

    public static void PauseAll(BaseTimedAction action)
    {
        foreach (var timedAction in _timedActions)
        {
            timedAction.Pause();
        }
    }

    public static void ResumeAll(BaseTimedAction action)
    {
        foreach (var timedAction in _timedActions)
        {
            timedAction.Resume();
        }
    }

    public static void UpdateAll(float deltaTime, float timeScale = 1)
    {
        for (int i = 0; i < _timedActions.Count; i++)
        {
            BaseTimedAction action = _timedActions[i];
            if (action.Update(deltaTime, timeScale))
            {
                _timedActions.Remove(action);
                i--;
            }
        }
    }
    #endregion
}
