using System.Collections.Generic;
using UnityEngine;

public delegate void FingerTouchDown(int fingerId, Vector2 position);
public delegate void FingerTouchUp(int fingerId, Vector2 position);
public delegate void FingerTouchBeginDrag(int fingerId, Vector2 position);
public delegate void FingerTouchDrag(int fingerId, Vector2 position, Vector2 deltaPosition);
public delegate void FingerTouchDragEnd(int fingerId, Vector2 pisition);
public delegate void FingerTouchBeginPinch(int fingerId, Vector2 position);
public delegate void FingerTouchPinch(int fingerId1, int fingerId2, float pinch);
public delegate void FingerTouchPinchEnd(int fingerId);

public class FingerGestureSystem : SingletonObject<FingerGestureSystem>
{
    private FingerGesture fingerGesture = new FingerGesture();
    private FingerPinch fingerPinch = new FingerPinch();
    private const int _maxTouchCount = 2;

    public FingerTouchDown fingerTouchDown;
    public FingerTouchUp fingerTouchUp;
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

    private void PCReceiveInput()
    {
        foreach(var fingerId in mouseIds)
        {
            Vector2 deltaPosition = Vector2.zero;
            TouchPhase touchPhase = TouchPhase.Canceled;

            if (Input.GetMouseButtonDown(fingerId))
            {
                touchPhase = TouchPhase.Began;
            }
            else if (Input.GetMouseButton(fingerId))
            {
                Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                deltaPosition = mousePosition - fingerGesture.LastPosition();
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

        float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        if (mouseScroll != 0)
        {
            fingerPinch.Pinch(1, 2, mouseScroll);
        }
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

    public void AddCustomTouch(Touch touch)
    {
        fingerGesture.AddTouch(touch);
    }

    private void Execute()
    {
        fingerGesture.Execute();
    }
}