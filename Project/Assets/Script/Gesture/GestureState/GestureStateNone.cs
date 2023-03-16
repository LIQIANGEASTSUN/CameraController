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
        _stateMachine.ChangeState((int)GestureStateEnum.Click);
    }

    public override void SetTouch(Touch touch0, Touch touch1)
    {
        base.SetTouch(touch0, touch1);
        _stateMachine.ChangeState((int)GestureStateEnum.Pinch);
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExecute()
    {
        base.OnExecute();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

}
