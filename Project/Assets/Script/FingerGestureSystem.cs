using System.Collections.Generic;
using UnityEngine;

public delegate void FingerTouchDown(int fingerId, Vector2 position);
public delegate void FingerTouchUp(int fingerId, Vector2 position);
public delegate void FingerTouchBeginDrag(int fingerId, Vector2 position);
public delegate void FingerTouchDrag(int fingerId, Vector2 position, Vector2 deltaPosition);
public delegate void FingerTouchDragEnd(int fingerId, Vector2 pisition);
public delegate void FingerTouchBeginPinch(int fingerId, Vector2 position);
public delegate void FingerTouchPinch(int fingerId, float pinch);
public delegate void FingerTouchPinchEnd(int fingerId);

public class FingerGestureSystem : SingletonObject<FingerGestureSystem>
{
    private List<FingerGesture> fingerGesturesList = new List<FingerGesture>();
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
                FingerGesture fingerGesture = GetFingerGestureIfNullCreate(fingerId);
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
                FingerGesture fingerGesture = GetFingerGestureIfNullCreate(fingerId);
                fingerGesture.AddTouch(touch);
            }
        }
    }

    private void MobileReceiveInput()
    {
        for (int i = 0; i < Input.touchCount && i < _maxTouchCount; ++i)
        {
            Touch touch = Input.GetTouch(i);
            FingerGesture fingerGesture = GetFingerGestureIfNullCreate(touch.fingerId);
            fingerGesture.AddTouch(touch);
        }
    }

    public void AddCustomTouch(Touch touch)
    {
        FingerGesture fingerGesture = GetFingerGestureIfNullCreate(touch.fingerId);
        fingerGesture.AddTouch(touch);
    }

    private void Execute()
    {
        foreach (var gesture in fingerGesturesList)
        {
            gesture.Execute();
        }
    }

    private FingerGesture GetFingerGestureIfNullCreate(int fingerId)
    {
        for (int i = 0; i < fingerGesturesList.Count; ++i)
        {
            FingerGesture temp = fingerGesturesList[i];
            if (temp.FingerId == fingerId)
            {
                return temp;
            }
        }

        FingerGesture fingerGesture = new FingerGesture(fingerId);
        fingerGesturesList.Add(fingerGesture);
        while (fingerGesturesList.Count > 2)
        {
            fingerGesturesList.RemoveAt(0);
        }
        return fingerGesture;
    }
}