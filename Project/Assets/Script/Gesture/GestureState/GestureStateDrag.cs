using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureStateDrag : StateBase
{
    public GestureStateDrag(StateMachine stateMachine, FingerGesture fingerGesture) : base(stateMachine, (int)GestureStateEnum.Drag, fingerGesture)
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
