using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEditorInternal;
using UnityEngine;

public class StateBase : IState
{
    private StateMachine _stateMachine;
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

    public virtual void SetTouch(Touch touch)
    {

    }

    public virtual void SetTouch(Touch touch0, Touch touch1)
    {

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
