using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public delegate void FingerTouchDown(int fingerId, Vector2 position);
public delegate void FingerTouchUp(int fingerId, Vector2 position);
public delegate void FingerTouchClick(int fingerId, Vector2 position);
public delegate void FingerTouchPress(int fingerId, Vector2 position);
public delegate void FingerTouchBeginDrag(int fingerId, Vector2 position);
public delegate void FingerTouchDrag(int fingerId, Vector2 position, Vector2 deltaPosition);
public delegate void FingerTouchDragEnd(int fingerId, Vector2 pisition);
public delegate void FingerTouchBeginPinch(int fingerId1, int fingerId2, float pinch);
public delegate void FingerTouchPinch(int fingerId1, int fingerId2, float pinch);
public delegate void FingerTouchPinchEnd(int fingerId1, int fingerId2, float pinch);

public class FingerGestureSystem : SingletonObject<FingerGestureSystem>
{
    private FingerGesture fingerGesture = new FingerGesture();

    public FingerTouchDown fingerTouchDown;
    public FingerTouchUp fingerTouchUp;
    public FingerTouchClick fingerTouchClick;
    public FingerTouchPress fingerTouchPress;
    public FingerTouchBeginDrag fingerTouchBeginDrag;
    public FingerTouchDrag fingerTouchDrag;
    public FingerTouchDragEnd fingerTouchDragEnd;
    public FingerTouchBeginPinch fingerTouchBeginPinch;
    public FingerTouchPinch fingerTouchPinch;
    public FingerTouchPinchEnd fingerTouchPinchEnd;

    public FingerGestureSystem() {    }

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
    private float _scrollWheel = 0;
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
                fingerGesture.SetTouch(touch);
            }
        }
    }

    private bool ScrollWheel()
    {
        float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        if (mouseScroll != 0)
        {
            //_scrollWheel += mouseScroll * 50;
            //Debug.LogError(_scrollWheel);

            //Touch touch0 = new Touch();
            //touch0.fingerId = 1;
            //touch0.position = Input.mousePosition;
            //touch0.deltaPosition = Vector2.zero;
            //touch0.phase = TouchPhase.Stationary;

            //Touch touch1 = new Touch();
            //touch1.fingerId = 2;
            //touch1.position = Input.mousePosition + Vector3.one * _scrollWheel;
            //touch1.deltaPosition = Vector2.zero;
            //touch1.phase = TouchPhase.Moved;

            //fingerGesture.SetTouch(touch0, touch1);

            FingerGestureSystem.GetInstance().fingerTouchPinch?.Invoke(1, 2, mouseScroll);
            return true;
        }
        else
        {
            _scrollWheel = 0;
        }
        return false;
    }

    private void MobileReceiveInput()
    {
        if (Input.touchCount == 1)
        {
            Touch touch0 = Input.GetTouch(0);
            fingerGesture.SetTouch(touch0);
        }
        else if (Input.touchCount >= 2)
        {
            // 只取前两个
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);
            fingerGesture.SetTouch(touch0, touch1);
        }
    }

    private void Execute()
    {
        fingerGesture.Execute();
    }
}