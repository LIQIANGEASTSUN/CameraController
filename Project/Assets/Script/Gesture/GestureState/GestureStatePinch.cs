﻿using UnityEngine;

public class GestureStatePinch : StateBase
{
    private float _lastPinchDistance = 0;
    private float _coefficient = 0.01f;
    public GestureStatePinch(StateMachine stateMachine, FingerGesture fingerGesture) : base(stateMachine, (int)GestureStateEnum.Pinch, fingerGesture)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        _lastPinchDistance = Vector2.Distance(_fingerGesture._touch0.position, _fingerGesture._touch1.position);
        FingerGestureSystem.GetInstance().fingerTouchBeginPinch?.Invoke(_fingerGesture._touch0.fingerId, _fingerGesture._touch1.fingerId, 0);
    }

    public override void OnExecute()
    {
        base.OnExecute();

        if (_fingerGesture._touchCount <= 1)
        {
            FingerGestureSystem.GetInstance().fingerTouchPinchEnd?.Invoke(_fingerGesture._touch0.fingerId, _fingerGesture._touch1.fingerId, 0);
            _stateMachine.ChangeState((int)GestureStateEnum.None);
            return;
        }

        if (_fingerGesture._touch0.phase == TouchPhase.Moved || _fingerGesture._touch1.phase == TouchPhase.Moved)
        {
            float pinch = Pinch();
            FingerGestureSystem.GetInstance().fingerTouchPinch?.Invoke(_fingerGesture._touch0.fingerId, _fingerGesture._touch1.fingerId, pinch);
        }
        else if (_fingerGesture._touch0.phase == TouchPhase.Ended || _fingerGesture._touch0.phase == TouchPhase.Canceled
            || _fingerGesture._touch1.phase == TouchPhase.Ended || _fingerGesture._touch1.phase == TouchPhase.Canceled)
        {
            float pinch = Pinch();
            FingerGestureSystem.GetInstance().fingerTouchPinchEnd?.Invoke(_fingerGesture._touch0.fingerId, _fingerGesture._touch1.fingerId, pinch);
            _stateMachine.ChangeState((int)GestureStateEnum.None);
        }
    }

    private float Pinch()
    {
        float pinch = Vector2.Distance(_fingerGesture._touch0.position, _fingerGesture._touch1.position);
        float value = (pinch - _lastPinchDistance) * _coefficient;
        _lastPinchDistance = pinch;
        return value;
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
