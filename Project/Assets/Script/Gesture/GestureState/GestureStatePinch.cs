using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureStatePinch : StateBase
{
    public GestureStatePinch(StateMachine stateMachine, FingerGesture fingerGesture) : base(stateMachine, (int)GestureStateEnum.Pinch, fingerGesture)
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
