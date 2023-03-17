using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureStateNone : StateBase
{
    public GestureStateNone(StateMachine stateMachine, FingerGesture fingerGesture) : base(stateMachine, (int)GestureStateEnum.None, fingerGesture)
    {
    }

    public override void SetTouch(Touch touch)
    {
        base.SetTouch(touch);
    }

    public override void SetTouch(Touch touch0, Touch touch1)
    {
        base.SetTouch(touch0, touch1);
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExecute()
    {
        base.OnExecute();
        if (_fingerGesture._touchCount == 1)
        {
            _stateMachine.ChangeState((int)GestureStateEnum.Click);
        }
        else if (_fingerGesture._touchCount == 2)
        {
            _stateMachine.ChangeState((int)GestureStateEnum.Pinch);
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }

}
