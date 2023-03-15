using System.Collections.Generic;
using UnityEngine;

public class FingerGesture
{
    private int _fingerId;
    private Touch _lastTouch;
    private Queue<Touch> _queueTouch = new Queue<Touch>();

    private bool _beginDrag = false;

    public FingerGesture(int fingerId)
    {
        _fingerId = fingerId;
    }

    public void AddTouch(Touch touch)
    {
        if (touch.fingerId == _fingerId)
        {
            _queueTouch.Enqueue(touch);
        }
        else
        {
            Debug.LogError("Different touch:" + touch.fingerId);
        }
    }

    public Vector2 LastPosition()
    {
        return _lastTouch.position;
    }

    public void Execute()
    {
        if (_queueTouch.Count <= 0)
        {
            return;
        }

        Touch touch = _queueTouch.Dequeue();
        if (touch.phase == TouchPhase.Began)
        {
            Down(touch);
        }
        else if (touch.phase == TouchPhase.Moved)
        {
            Move(touch);
        }
        else if (touch.phase == TouchPhase.Stationary)
        {

        }
        else if (touch.phase == TouchPhase.Ended)
        {
            Up(touch);
        }

        _lastTouch = touch;
    }

    private void Down(Touch touch)
    {
        _beginDrag = false;
        //Debug.LogError("Down");
        if (null != FingerGestureSystem.GetInstance().fingerTouchDown)
        {
            FingerGestureSystem.GetInstance().fingerTouchDown(_fingerId, touch.position);
        }
    }

    private void Move(Touch touch)
    {
        if (!_beginDrag)
        {
            _beginDrag = true;
            //Debug.LogError("BeginDrag");
            if (null != FingerGestureSystem.GetInstance().fingerTouchBeginDrag)
            {
                FingerGestureSystem.GetInstance().fingerTouchBeginDrag(_fingerId, touch.position);
            }
        }
        else
        {
            //Debug.LogError("Drag:" + touch.position + "    " + touch.deltaPosition);
            if (null != FingerGestureSystem.GetInstance().fingerTouchDrag)
            {
                FingerGestureSystem.GetInstance().fingerTouchDrag(_fingerId, touch.position, touch.deltaPosition);
            }
        }
    }

    private void Up(Touch touch)
    {
        //Debug.LogError("Up");
        if (null != FingerGestureSystem.GetInstance().fingerTouchUp)
        {
            FingerGestureSystem.GetInstance().fingerTouchUp(_fingerId, touch.position);
        }
        if (_beginDrag)
        {
            _beginDrag = false;
            //Debug.LogError("EndDrag");
            if (null != FingerGestureSystem.GetInstance().fingerTouchDragEnd)
            {
                FingerGestureSystem.GetInstance().fingerTouchDragEnd(_fingerId, touch.position);
            }
        }
    }

    public int FingerId
    {
        get { return _fingerId; }
    }
}