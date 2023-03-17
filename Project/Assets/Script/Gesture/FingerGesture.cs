using UnityEditorInternal;
using UnityEngine;

public class FingerGesture
{
    public Touch _touch0;
    public Touch _touch1;
    public int _touchCount = 0;
    private bool _beginDrag = false;

    private StateMachine _stateMachine;

    public FingerGesture()
    {
        //_touch = new Touch();
        //_touch.fingerId = -1000;
        //_touch.phase = TouchPhase.Canceled;

        _stateMachine = new StateMachine();
        _stateMachine.AddState(new GestureStateNone(_stateMachine, this));
        _stateMachine.AddState(new GestureStateClick(_stateMachine, this));
        _stateMachine.AddState(new GestureStateDrag(_stateMachine, this));
        _stateMachine.AddState(new GestureStatePinch(_stateMachine, this));

        _stateMachine.ChangeState((int)GestureStateEnum.None);
    }

    public void AddTouch(Touch touch)
    {
        //_touchCount++;
        //_touch.fingerId = touch.fingerId;
        //_touch.position = touch.position;
        //_touch.deltaPosition = touch.deltaPosition;
        //_touch.phase = touch.phase;
    }

    public void SetTouch(Touch touch)
    {
        _touch0 = touch;
        _touchCount = 1;
        _stateMachine.CurrentState.SetTouch(touch);
    }

    public void SetTouch(Touch touch0, Touch touch1)
    {
        _touch0 = touch0;
        _touch1 = touch1;
        _touchCount = 2;
        _stateMachine.CurrentState.SetTouch(touch0, touch1);
    }

    public void ClearTouch()
    {
        _touchCount = 0;
    }

    public void Execute()
    {
        _stateMachine.OnExecute();

        //if (_touchCount <= 0)
        //{
        //    return;
        //}

        //if (_touch.phase == TouchPhase.Began)
        //{
        //    Down(_touch);
        //}
        //else if (_touch.phase == TouchPhase.Moved)
        //{
        //    Move(_touch);
        //}
        //else if (_touch.phase == TouchPhase.Ended)
        //{
        //    Up(_touch);
        //}

        //_touchCount--;
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