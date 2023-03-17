using UnityEngine;

public class GestureStateClick : StateBase
{
    private Vector2 _downPosition = Vector2.zero;
    public GestureStateClick(StateMachine stateMachine, FingerGesture fingerGesture) : base(stateMachine, (int)GestureStateEnum.Click, fingerGesture)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        if (_fingerGesture._touch0.phase == TouchPhase.Began)
        {
            FingerGestureSystem.GetInstance().fingerTouchDown?.Invoke(_fingerGesture._touch0.fingerId, _fingerGesture._touch0.position);
        }
    }

    public override void OnExecute()
    {
        base.OnExecute();

        if (_fingerGesture._touchCount <= 0)
        {
            _stateMachine.ChangeState((int)GestureStateEnum.None);
            return;
        }

        if (_fingerGesture._touchCount == 1)
        {
            if (_fingerGesture._touch0.phase == TouchPhase.Moved)
            {
                _stateMachine.ChangeState((int)GestureStateEnum.Drag);
            }
            else if (_fingerGesture._touch0.phase == TouchPhase.Stationary)
            {
                FingerGestureSystem.GetInstance().fingerTouchPress?.Invoke(_fingerGesture._touch0.fingerId, _fingerGesture._touch0.position);
            }
            else if (_fingerGesture._touch0.phase == TouchPhase.Ended || _fingerGesture._touch0.phase == TouchPhase.Canceled)
            {
                FingerGestureSystem.GetInstance().fingerTouchUp?.Invoke(_fingerGesture._touch0.fingerId, _fingerGesture._touch0.position);
                FingerGestureSystem.GetInstance().fingerTouchClick?.Invoke(_fingerGesture._touch0.fingerId, _fingerGesture._touch0.position);
                _stateMachine.ChangeState((int)GestureStateEnum.None);
            }
        }
        else if (_fingerGesture._touchCount == 2)
        {
            _stateMachine.ChangeState((int)GestureStateEnum.Pinch);
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}