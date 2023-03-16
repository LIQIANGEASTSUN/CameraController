using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEditorInternal;
using UnityEngine;

public class StateBase : IState
{
    protected StateMachine _stateMachine;
    protected int _state;

    protected FingerGesture _fingerGesture;

    protected int _touchCount;
    protected Touch _touch0;
    protected Touch _touch1;

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
        _touchCount = 1;
        _touch0 = touch;
    }

    public virtual void SetTouch(Touch touch0, Touch touch1)
    {
        _touchCount = 2;
        _touch0 = touch0;
        _touch1 = touch1;
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
