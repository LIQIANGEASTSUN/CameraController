using UnityEngine;

public class StateBase : IState
{
    protected StateMachine _stateMachine;
    protected int _state;

    protected FingerGesture _fingerGesture;

    public StateBase(StateMachine stateMachine, int state, FingerGesture fingerGesture)
    {
        _stateMachine = stateMachine;
        _state = state;
        _fingerGesture = fingerGesture;
    }

    public int State
    {
        get
        {
            return _state;
        }
    }

    public virtual void OnEnter()
    {

    }

    public virtual void OnExecute()
    {

    }

    public virtual void OnExit()
    {

    }
}
