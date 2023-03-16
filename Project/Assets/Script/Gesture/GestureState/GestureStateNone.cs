using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureStateNone : StateBase
{
    public GestureStateNone(StateMachine stateMachine, FingerGesture fingerGesture) : base(stateMachine, (int)GestureStateEnum.None, fingerGesture)
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
