using UnityEngine;

public delegate void FingerTouchDown(int fingerId, Vector2 position);
public delegate void FingerTouchUp(int fingerId, Vector2 position);
public delegate void FingerTouchClick(int fingerId, Vector2 position);
public delegate void FingerTouchPress(int fingerId, Vector2 position);
public delegate void FingerTouchBeginDrag(int fingerId, Vector2 position);
public delegate void FingerTouchDrag(int fingerId, Vector2 position, Vector2 deltaPosition);
public delegate void FingerTouchDragEnd(int fingerId, Vector2 pisition, Vector2 deltaPosition);
public delegate void FingerTouchBeginPinch(int fingerId1, int fingerId2, float pinch);
public delegate void FingerTouchPinch(int fingerId1, int fingerId2, float pinch);
public delegate void FingerTouchPinchEnd(int fingerId1, int fingerId2, float pinch);

public class FingerInputController : SingletonObject<FingerInputController>
{
    private FingerGestureGet _fingerGestureGet = new FingerGestureGet();

    private FingerTouchDown fingerTouchDown;
    private FingerTouchUp fingerTouchUp;
    private FingerTouchClick fingerTouchClick;
    private FingerTouchPress fingerTouchPress;
    private FingerTouchBeginDrag fingerTouchBeginDrag;
    private FingerTouchDrag fingerTouchDrag;
    private FingerTouchDragEnd fingerTouchDragEnd;
    private FingerTouchBeginPinch fingerTouchBeginPinch;
    private FingerTouchPinch fingerTouchPinch;
    private FingerTouchPinchEnd fingerTouchPinchEnd;

    public FingerInputController()
    {
    }

    public void Update()
    {
        _fingerGestureGet.Update();
    }

    public void AddTouchDown(FingerTouchDown down)
    {
        fingerTouchDown += down;
    }

    public void RemoveTouchDown(FingerTouchDown down)
    {
        fingerTouchDown -= down;
    }

    public void NotifyTouchDown(int fingerId, Vector2 position)
    {
        fingerTouchDown?.Invoke(fingerId, position);
    }

    public void AddTouchUp(FingerTouchUp up)
    {
        fingerTouchUp += up;
    }

    public void RemoveTouchUp(FingerTouchUp up)
    {
        fingerTouchUp -= up;
    }

    public void NotifyTouchUp(int fingerId, Vector2 position)
    {
        fingerTouchUp?.Invoke(fingerId, position);
    }

    public void AddTouchClick(FingerTouchClick click)
    {
        fingerTouchClick += click;
    }

    public void RemoveTouchClick(FingerTouchClick click)
    {
        fingerTouchClick -= click;
    }

    public void NotifyTouchClick(int fingerId, Vector2 position)
    {
        fingerTouchClick?.Invoke(fingerId, position);
    }

    public void AddTouchPress(FingerTouchPress press)
    {
        fingerTouchPress += press;
    }

    public void RemoveTouchPress(FingerTouchPress press)
    {
        fingerTouchPress -= press;
    }

    public void NotifyTouchPress(int fingerId, Vector2 position)
    {
        fingerTouchPress?.Invoke(fingerId, position);
    }

    public void AddBeginDrag(FingerTouchBeginDrag beginDrag)
    {
        fingerTouchBeginDrag += beginDrag;
    }

    public void RemoveBeginDrag(FingerTouchBeginDrag beginDrag)
    {
        fingerTouchBeginDrag -= beginDrag;
    }

    public void NotifyBeginDrag(int fingerId, Vector2 position)
    {
        fingerTouchBeginDrag?.Invoke(fingerId, position);
    }

    public void AddTouchDrag(FingerTouchDrag drag)
    {
        fingerTouchDrag += drag;
    }

    public void RemoveTouchDrag(FingerTouchDrag drag)
    {
        fingerTouchDrag -= drag;
    }

    public void NotifyTouchDrag(int fingerId, Vector2 position, Vector2 deltaPosition)
    {
        fingerTouchDrag?.Invoke(fingerId, position, deltaPosition);
    }

    public void AddDragEnd(FingerTouchDragEnd dragEnd)
    {
        fingerTouchDragEnd += dragEnd;
    }

    public void RemoveDragEnd(FingerTouchDragEnd dragEnd)
    {
        fingerTouchDragEnd -= dragEnd;
    }

    public void NotifyDragEnd(int fingerId, Vector2 pisition, Vector2 deltaPosition)
    {
        fingerTouchDragEnd?.Invoke(fingerId, pisition, deltaPosition);
    }

    public void AddBeginPinch(FingerTouchBeginPinch beginPinch)
    {
        fingerTouchBeginPinch += beginPinch;
    }

    public void RemoveBeginPinch(FingerTouchBeginPinch beginPinch)
    {
        fingerTouchBeginPinch -= beginPinch;
    }

    public void NofifyBeginPinch(int fingerId1, int fingerId2, float pinch)
    {
        fingerTouchBeginPinch?.Invoke(fingerId1, fingerId2, pinch);
    }

    public void AddTouchPinch(FingerTouchPinch pinch)
    {
        fingerTouchPinch += pinch;
    }

    public void RemoveTouchPinch(FingerTouchPinch pinch)
    {
        fingerTouchPinch -= pinch;
    }

    public void NotifyTouchPinch(int fingerId1, int fingerId2, float pinch)
    {
        fingerTouchPinch?.Invoke(fingerId1, fingerId2, pinch);
    }

    public void AddEndPinch(FingerTouchPinchEnd endPinch)
    {
        fingerTouchPinchEnd += endPinch;
    }

    public void RemoveEndPinch(FingerTouchPinchEnd endPinch)
    {
        fingerTouchPinchEnd -= endPinch;
    }

    public void NotifyEndPinch(int fingerId1, int fingerId2, float pinch)
    {
        fingerTouchPinchEnd?.Invoke(fingerId1, fingerId2, pinch);
    }
}