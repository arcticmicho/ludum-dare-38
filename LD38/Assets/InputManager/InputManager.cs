using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace GameModules
{
    public enum DragStatus
    {
        Begin,
        Moving,
        End,
    }

    [System.Flags]
    public enum SwipeDirection
    {
        None = 0,
        Left = 0x1,
        Up = 0x2,
        Right = 0x4,
        Down = 0x8
    }

    public sealed class InputManager : MonoSingleton<InputManager>
    {
        private class TouchInfo
        {
            public float beginTime;
            public Vector3 beginPosition;
            public bool valid = true;
        }

        [Header("Touch Setup")]
        [SerializeField]
        private float _longTapMinTime = 0.5f;
        [SerializeField]
        private float _longTapMaxTime = 1.0f;
        [SerializeField]
        private float _swipeMaxTime = 0.4f; 
        [SerializeField]
        private float _dragDeadZone = 15;

        [SerializeField]
        private bool _mouseSimulation = true;

        public delegate void OnTapDownDelegate(Vector3 position);
        public event OnTapDownDelegate OnTapDownEvent;

        public delegate void OnTapUpDelegate(Vector3 position);
        public event OnTapUpDelegate OnTapUpEvent;

        public delegate void OnLongTapDelegate(Vector3 position);
        public event OnLongTapDelegate OnLongTapEvent;

        public delegate void OnDragDelegate(DragStatus status, Vector3 position, Vector3 last);
        public event OnDragDelegate OnDragEvent;

        public delegate void OnSwipeDelegate(SwipeDirection direction,Vector3 realDirection);
        public event OnSwipeDelegate OnSwipeEvent;

        public delegate void OnPinchZoomDelegate(float delta);
        public event OnPinchZoomDelegate OnPinchZoomEvent;

        private Dictionary<int, TouchInfo> _touchInfoList = new Dictionary<int, TouchInfo>(10);

        private TouchInfo _currentDragTouch;
        private Vector3 _lastMousePosition;

        private ObjectPool<TouchInfo> _infoPool = new ObjectPool<TouchInfo>(16);

        #region Get/Set

        public float LongTapMinTime
        {
            get { return _longTapMinTime; }
        }

        public float LongTapMaxTime
        {
            get { return _longTapMaxTime; }
        }

        public float SwipeMaxTime
        {
            get { return _swipeMaxTime; }
            set { _swipeMaxTime = value; }
        }

        public float DragDeadZone
        {
            get { return _dragDeadZone; }
            set { _dragDeadZone = value; }
        }
        #endregion

        //protected override bool DestroyOnLoad()
        //{
        //    return false;
        //}

        void Update()
        {
            // Tap / Drag / Swipe
            for (int a = 0; a < Input.touchCount; ++a)
            {
                Touch touch = Input.GetTouch(a);
                HandleTouch(touch.fingerId, touch.position, touch.deltaPosition, touch.phase);
            }

            // Pinch Zoom
            if (Input.touchCount == 2)
            {
                if (OnPinchZoomEvent != null)
                {
                    Touch touchZero = Input.GetTouch(0);
                    Touch touchOne = Input.GetTouch(1);

                    Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                    Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                    float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                    float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                    float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                    OnPinchZoomEvent(deltaMagnitudeDiff);
                }
            }

            // Mouse Touch Simulation
#if UNITY_EDITOR
            if (_mouseSimulation)
            {
                if (Input.touchCount == 0)
                {
                    for (int a = 0; a < 3; ++a)
                    {
                        if (Input.GetMouseButtonDown(a))
                        {
                            HandleTouch( - (20 + a), Input.mousePosition, Input.mousePosition - _lastMousePosition, TouchPhase.Began);
                        }

                        if (Input.GetMouseButton(a))
                        {
                            HandleTouch( - (20 + a), Input.mousePosition, Input.mousePosition - _lastMousePosition, TouchPhase.Moved);
                        }

                        if (Input.GetMouseButtonUp(a))
                        {
                            HandleTouch( - (20 + a), Input.mousePosition, Input.mousePosition - _lastMousePosition, TouchPhase.Ended);
                        }
                    }

                    _lastMousePosition = Input.mousePosition;

                    if (OnPinchZoomEvent != null)
                    {
                        if (Input.GetAxis("Mouse ScrollWheel") > 0)
                        {
                            OnPinchZoomEvent(-1);
                        }

                        if (Input.GetAxis("Mouse ScrollWheel") < 0)
                        {
                            OnPinchZoomEvent(1);
                        }
                    }
                }
            }
            #endif
        }

        private void ProcessTapDown(Vector3 touchPosition)
        {
            if (OnTapDownEvent != null)
            {
                OnTapDownEvent(touchPosition);
            }
        }

        private void ProcessTapUP(Vector3 touchPosition)
        {
            if (OnTapDownEvent != null)
            {
                OnTapUpEvent(touchPosition);
            }
        }

        private void ProcessLongTap(Vector3 touchPosition)
        {
            if (OnLongTapEvent != null)
            {
                OnLongTapEvent(touchPosition);
            }
        }

        private void ProcessDrag(DragStatus status, Vector3 position, Vector3 last)
        {
            if (OnDragEvent != null)
            {
                OnDragEvent(status, position, last);
            }
        }

        private void ProcessSwipe(SwipeDirection direction,Vector3 realDirection)
        {
            if (OnSwipeEvent != null)
            {
                OnSwipeEvent(direction, realDirection);
            }
        }

        private void HandleTouch(int touchFingerId, Vector3 touchPosition, Vector3 delta, TouchPhase touchPhase)
        {
            TouchInfo info = null;

            if (!_touchInfoList.TryGetValue(touchFingerId, out info))
            {
                info = _infoPool.GetInstance();
                _touchInfoList.Add(touchFingerId, info);
            }

            switch (touchPhase)
            {
                case TouchPhase.Began:

                    info.beginTime = Time.time;
                    info.beginPosition = touchPosition;
                    info.valid = true;

                    if (EventSystem.current == null || !EventSystem.current.IsPointerOverGameObject())
                    {
                        ProcessTapDown(touchPosition);
                    }
                    else
                    {
                        info.valid = false;
                    }

                    break;
                case TouchPhase.Stationary:
                case TouchPhase.Moved:

                    if (_currentDragTouch == null)
                    {
                        if (Vector3.Distance(touchPosition, info.beginPosition) > DragDeadZone)
                        {
                            _currentDragTouch = info;
                            if (info.valid)
                            {
                                ProcessDrag(DragStatus.Begin, touchPosition, delta);
                            }
                        }
                    }
                    else if (_currentDragTouch == info)
                    {
                        if (info.valid)
                        {
                            ProcessDrag(DragStatus.Moving, touchPosition, delta);
                        }
                    }

                    break;
                case TouchPhase.Ended:

                    if (_currentDragTouch != null && _currentDragTouch == info)
                    {
                        if (info.valid)
                        {
                            ProcessDrag(DragStatus.End, touchPosition, delta);
                        }
                        _currentDragTouch = null;
                    }

                    if (Vector3.Distance(touchPosition, info.beginPosition) > DragDeadZone)
                    {
                        if ((Time.time - info.beginTime) < SwipeMaxTime)
                        {
                            if (OnSwipeEvent != null)
                            {
                                SwipeDirection direction = SwipeDirection.None;
                                if (Mathf.Abs(touchPosition.x - info.beginPosition.x) > _dragDeadZone)
                                {
                                    if (touchPosition.x - info.beginPosition.x < 0)
                                    {
                                        direction |= SwipeDirection.Left;
                                    }
                                    else
                                    {
                                        direction |= SwipeDirection.Right;
                                    }

                                }
                                if (Mathf.Abs(touchPosition.y - info.beginPosition.y) > _dragDeadZone)
                                {
                                    if (touchPosition.y - info.beginPosition.y < 0)
                                    {
                                        direction |= SwipeDirection.Down;
                                    }
                                    else
                                    {
                                        direction |= SwipeDirection.Up;
                                    }
                                }

                                if (info.valid && direction != SwipeDirection.None)
                                {
                                    ProcessSwipe(direction, (touchPosition - info.beginPosition).normalized);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (EventSystem.current == null || !EventSystem.current.IsPointerOverGameObject())
                        {
                            float elapsed = Time.time - info.beginTime;
                            if (elapsed < _longTapMaxTime)
                            {
                                ProcessTapUP(touchPosition);
                            }
                            else if (elapsed > _longTapMinTime)
                            {
                                ProcessLongTap(touchPosition);
                            }
                        }
                    }
                    _infoPool.ReleaseInstance(info);
                    _touchInfoList.Remove(touchFingerId);
                    break;
                case TouchPhase.Canceled:
                    if (_currentDragTouch != null && _currentDragTouch == info)
                    {
                        ProcessDrag(DragStatus.End, touchPosition, delta);
                        _currentDragTouch = null;
                    }
                    _touchInfoList.Remove(touchFingerId);

                    _infoPool.ReleaseInstance(info);
                    break;
            }
        }
    }
}