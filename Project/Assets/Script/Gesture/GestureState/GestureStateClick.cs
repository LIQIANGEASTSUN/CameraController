using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureStateClick : StateBase
{
    private Vector2 _downPosition = Vector2.zero;
    public GestureStateClick(StateMachine stateMachine, FingerGesture fingerGesture) : base(stateMachine, (int)GestureStateEnum.Click, fingerGesture)
    {
    }

    public override void SetTouch(Touch touch)
    {
        if (_touch0.fingerId != touch.fingerId)
        {
            _stateMachine.ChangeState((int)GestureStateEnum.None);
        }
        base.SetTouch(touch);
    }

    public override void SetTouch(Touch touch0, Touch touch1)
    {
        base.SetTouch(touch0, touch1);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        if (_touchCount == 1 && _touch0.phase == TouchPhase.Began)
        {
            _downPosition = _touch0.position;
            FingerGestureSystem.GetInstance().fingerTouchDown?.Invoke(_touch0.fingerId, _touch0.position);
        }
    }

    public override void OnExecute()
    {
        base.OnExecute();

        if (_touch0.phase == TouchPhase.Moved)
        {
            _stateMachine.ChangeState((int)GestureStateEnum.Drag);
        }
        else if (_touch0.phase == TouchPhase.Stationary)
        {
            FingerGestureSystem.GetInstance().fingerTouchPress?.Invoke(_touch0.fingerId, _touch0.position);
        }
        else if (_touch0.phase == TouchPhase.Ended)
        {
            FingerGestureSystem.GetInstance().fingerTouchUp?.Invoke(_touch0.fingerId, _touch0.position);
            if (Vector2.Distance(_downPosition, _touch0.position) <= 0.1f)
            {
                FingerGestureSystem.GetInstance().fingerTouchClick?.Invoke(_touch0.fingerId, _touch0.position);
            }

            _stateMachine.ChangeState((int)GestureStateEnum.None);
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }

}
