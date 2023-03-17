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

        float pinch = Vector2.Distance(_fingerGesture._touch0.position, _fingerGesture._touch1.position);
        FingerGestureSystem.GetInstance().fingerTouchBeginPinch?.Invoke(_fingerGesture._touch0.fingerId, _fingerGesture._touch1.fingerId, pinch);
    }

    public override void OnExecute()
    {
        base.OnExecute();

        if (_fingerGesture._touchCount <= 1)
        {
            _stateMachine.ChangeState((int)GestureStateEnum.None);
            return;
        }

        if (_fingerGesture._touch0.phase == TouchPhase.Moved || _fingerGesture._touch1.phase == TouchPhase.Moved)
        {
            float pinch = Vector2.Distance(_fingerGesture._touch0.position, _fingerGesture._touch1.position);
            FingerGestureSystem.GetInstance().fingerTouchPinch?.Invoke(_fingerGesture._touch0.fingerId, _fingerGesture._touch1.fingerId, pinch);
        }
        else if (_fingerGesture._touch0.phase == TouchPhase.Ended || _fingerGesture._touch0.phase == TouchPhase.Canceled
            || _fingerGesture._touch1.phase == TouchPhase.Ended || _fingerGesture._touch1.phase == TouchPhase.Canceled)
        {
            float pinch = Vector2.Distance(_fingerGesture._touch0.position, _fingerGesture._touch1.position);
            FingerGestureSystem.GetInstance().fingerTouchPinchEnd?.Invoke(_fingerGesture._touch0.fingerId, _fingerGesture._touch1.fingerId, pinch);
            _stateMachine.ChangeState((int)GestureStateEnum.None);
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
