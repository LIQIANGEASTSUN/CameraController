using UnityEngine;

public class FingerPinch
{
    private bool pinchValid = false;
    private float pinchDistance = 0;

    private Touch _touch0;
    private Touch _touch1;

    public void Pinch(int fingerId1, int fingerId2, float delta)
    {
        FingerGestureSystem.GetInstance().fingerTouchPinch?.Invoke(fingerId1, fingerId2, delta);
    }

    public void SetTouch(Touch touch0, Touch touch1)
    {
        if (NeedClear(touch0, touch1))
        {
            Clear();
            return;
        }

        if (!pinchValid)
        {
            pinchValid = true;
            pinchDistance = TouchDistance(touch0, touch1);
            return;
        }

        if (!ValidTouch(touch0) && !ValidTouch(touch1))
        {
            return;
        }

        float distance = TouchDistance(touch0, touch1);
        float delta = distance - pinchDistance;
        pinchDistance = distance;
        Pinch(touch0.fingerId, touch1.fingerId, delta);
    }

    public void Clear()
    {
        pinchValid = false;
        pinchDistance = 0;
    }

    private float TouchDistance(Touch touch0, Touch touch1)
    {
        return Vector2.Distance(touch0.position, touch1.position);
    }

    private bool NeedClear(Touch touch0, Touch touch1)
    {
        if (pinchValid)
        {
            if (_touch0.fingerId != touch0.fingerId || _touch1.fingerId != touch1.fingerId)
            {
                return true;
            }
        }

        if (!ValidTouch(touch0) || !ValidTouch(touch1))
        {
            return true;
        }
        return false;
    }

    private bool ValidTouch(Touch touch)
    {
        return touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary;
    }

    private bool ValidDragTouch(Touch touch)
    {
        return touch.phase == TouchPhase.Moved;
    }

}