using UnityEngine;

public class FingerGesture
{
    public Touch _touch0;
    public Touch _touch1;
    public int _touchCount = 0;

    private StateMachine _stateMachine;

    public FingerGesture()
    {
        _stateMachine = new StateMachine();
        _stateMachine.AddState(new GestureStateNone(_stateMachine, this));
        _stateMachine.AddState(new GestureStateClick(_stateMachine, this));
        _stateMachine.AddState(new GestureStateDrag(_stateMachine, this));
        _stateMachine.AddState(new GestureStatePinch(_stateMachine, this));

        _stateMachine.ChangeState((int)GestureStateEnum.None);
    }

    public void SetTouch(Touch touch)
    {
        _touch0 = touch;
        _touchCount = 1;
    }

    public void SetTouch(Touch touch0, Touch touch1)
    {
        _touch0 = touch0;
        _touch1 = touch1;
        _touchCount = 2;
    }

    public void ClearTouch()
    {
        _touchCount = 0;
    }

    public void Execute()
    {
        _stateMachine.OnExecute();
    }
}