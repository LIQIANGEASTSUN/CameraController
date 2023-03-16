using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StateMachine
{

    private Dictionary<int, StateBase> _stateDic = new Dictionary<int, StateBase>();
    private StateBase _currentState;

    public StateMachine()
    {

    }

    public void AddState(StateBase stateBase)
    {
        _stateDic[stateBase.State] = stateBase;
    }

    public void ChangeState(int state)
    {
        if (null != _currentState && _currentState.State == state)
        {
            return;
        }

        if (null != _currentState)
        {
            _currentState.OnExit();
        }

        StateBase stateBase = null;
        if (!_stateDic.TryGetValue(state, out stateBase))
        {
            return;
        }

        _currentState = stateBase;
        _currentState.OnEnter();
    }

    public void OnExecute()
    {
        if (null != _currentState)
        {
            _currentState.OnExecute();
        }
    }

}
