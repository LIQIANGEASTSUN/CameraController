using System.Collections.Generic;
using UnityEngine;

public class FingerGestureGet
{
    private FingerGesture fingerGesture = null;

    public FingerGestureGet() {
        fingerGesture = new FingerGesture();
    }

    public void Update()
    {
        ReceiveInput();
        Execute();

        fingerGesture.ClearTouch();
    }

    private int[] mouseIds = new int[] { 0, }; // { 0, 1, 2 };
    private void ReceiveInput()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        PCReceiveInput();
#endif

#if (!UNITY_EDITOR) && (UNITY_IOS || UNITY_ANDROID)
        MobileReceiveInput();
#endif
    }

    private Vector3 _lastMousePosition = Vector3.zero;
    private void PCReceiveInput()
    {
        if (ScrollWheel())
        {
            return;
        }

        foreach (var fingerId in mouseIds)
        {
            Vector2 deltaPosition = Vector2.zero;
            TouchPhase touchPhase = TouchPhase.Canceled;

            if (Input.GetMouseButtonDown(fingerId))
            {
                touchPhase = TouchPhase.Began;
                _lastMousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(fingerId))
            {
                Vector3 offset = Input.mousePosition - _lastMousePosition;
                _lastMousePosition = Input.mousePosition;
                deltaPosition = new Vector2(offset.x, offset.y);
                touchPhase = (deltaPosition.sqrMagnitude > 0) ? TouchPhase.Moved : TouchPhase.Stationary;
            }
            else if (Input.GetMouseButtonUp(fingerId))
            {
                touchPhase = TouchPhase.Ended;
            }

            if (touchPhase != TouchPhase.Canceled)
            {
                Touch touch = new Touch();
                touch.fingerId = fingerId;
                touch.position = Input.mousePosition;
                touch.deltaPosition = deltaPosition;
                touch.phase = touchPhase;
                if (ValidTouch(touch))
                {
                    fingerGesture.SetTouch(touch);
                }
            }
            else
            {
                ClearInvalid();
            }
        }
    }

    private float _scrollWheel = 10000;
    private bool ScrollWheel()
    {
        float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        // 模拟两个手指输入
        if (mouseScroll != 0)
        {
            _scrollWheel += mouseScroll * 150;

            Touch touch0 = new Touch();
            touch0.fingerId = 0;
            touch0.position = Input.mousePosition;
            touch0.deltaPosition = Vector2.zero;
            touch0.phase = TouchPhase.Stationary;

            Touch touch1 = new Touch();
            touch1.fingerId = 1;
            touch1.position = Input.mousePosition + Vector3.one * _scrollWheel;
            touch1.deltaPosition = Vector2.zero;
            touch1.phase = TouchPhase.Moved;

            fingerGesture.SetTouch(touch0, touch1);
            return true;
        }
        else
        {
            _scrollWheel = 10000;
        }
        return false;
    }

    private void MobileReceiveInput()
    {
        if (Input.touchCount == 0)
        {
            ClearInvalid();
        }
        else if (Input.touchCount == 1)
        {
            Touch touch0 = Input.GetTouch(0);
            if (ValidTouch(touch0))
            {
                fingerGesture.SetTouch(touch0);
            }
        }
        else if (Input.touchCount >= 2)
        {
            // 只取前两个
            Touch touch0 = Input.GetTouch(0);
            bool valid0 = ValidTouch(touch0);

            Touch touch1 = Input.GetTouch(1);
            bool valie1 = ValidTouch(touch1);

            if (valid0 && valie1)
            {
                fingerGesture.SetTouch(touch0, touch1);
            }
            else if (valid0)
            {
                fingerGesture.SetTouch(touch0);
            }
        }
    }

    private void Execute()
    {
        fingerGesture.Execute();
    }

    private HashSet<int> _invalidHash = new HashSet<int>();
    // 是否有效的输入，如果按下时在 UI 上则为无效的输入
    private bool ValidTouch(Touch touch)
    {
        if (touch.phase == TouchPhase.Began)
        {
            bool isOverUI = TouchUtil.IsPointerOverGameObject(touch.position);
            if (isOverUI && !_invalidHash.Contains(touch.fingerId))
            {
                _invalidHash.Add(touch.fingerId);
            }
        }

        return !_invalidHash.Contains(touch.fingerId);
    }

    private void ClearInvalid()
    {
        _invalidHash.Clear();
    }
}