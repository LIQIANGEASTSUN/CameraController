using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureStateClick : StateBase
{
    public GestureStateClick(StateMachine stateMachine, FingerGesture fingerGesture) : base(stateMachine, (int)GestureStateEnum.Click, fingerGesture)
    {
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
