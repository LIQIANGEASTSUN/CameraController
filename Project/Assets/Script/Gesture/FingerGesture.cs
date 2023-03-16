﻿using UnityEngine;

public class FingerGesture
{
    private Touch _touch;
    private int _touchCount = 0;
    private bool _beginDrag = false;

    public FingerGesture(int fingerId)
    {
        _touch = new Touch();
        _touch.fingerId = fingerId;
        _touch.phase = TouchPhase.Canceled;
    }

    public void AddTouch(Touch touch)
    {
        _touchCount++;
        _touch.fingerId = touch.fingerId;
        _touch.position = touch.position;
        _touch.deltaPosition = touch.deltaPosition;
        _touch.phase = touch.phase;
    }

    public int FingerId
    {
        get { return _touch.fingerId; }
    }

    public Vector2 LastPosition()
    {
        return _touch.position;
    }

    public void Execute()
    {
        if (_touchCount <= 0)
        {
            return;
        }

        if (_touch.phase == TouchPhase.Began)
        {
            Down(_touch);
        }
        else if (_touch.phase == TouchPhase.Moved)
        {
            Move(_touch);
        }
        else if (_touch.phase == TouchPhase.Ended)
        {
            Up(_touch);
        }

        _touchCount--;
    }

    private void Down(Touch touch)
    {
        _beginDrag = false;
        //Debug.LogError("Down");
        FingerGestureSystem.GetInstance().fingerTouchDown?.Invoke(touch.fingerId, touch.position);
    }

    private void Move(Touch touch)
    {
        if (!_beginDrag)
        {
            _beginDrag = true;
            //Debug.LogError("BeginDrag");
            FingerGestureSystem.GetInstance().fingerTouchBeginDrag?.Invoke(touch.fingerId, touch.position);
        }
        else
        {
            //Debug.LogError("Drag:" + touch.position + "    " + touch.deltaPosition);
            FingerGestureSystem.GetInstance().fingerTouchDrag?.Invoke(touch.fingerId, touch.position, touch.deltaPosition);
        }
    }

    private void Up(Touch touch)
    {
        //Debug.LogError("Up");
        FingerGestureSystem.GetInstance().fingerTouchUp?.Invoke(touch.fingerId, touch.position);

        if (_beginDrag)
        {
            _beginDrag = false;
            //Debug.LogError("EndDrag");
            FingerGestureSystem.GetInstance().fingerTouchDragEnd?.Invoke(touch.fingerId, touch.position);
        }
    }
}