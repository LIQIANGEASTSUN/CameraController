using UnityEngine;

public class GestureStateClick : StateBase
{
    public GestureStateClick(StateMachine stateMachine, FingerGesture fingerGesture) : base(stateMachine, (int)GestureStateEnum.Click, fingerGesture)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        if (_fingerGesture._touch0.phase == TouchPhase.Began)
        {
            FingerInputController.GetInstance().NotifyTouchDown(_fingerGesture._touch0.fingerId, _fingerGesture._touch0.position);
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
                FingerInputController.GetInstance().NotifyTouchPress(_fingerGesture._touch0.fingerId, _fingerGesture._touch0.position);
            }
            else if (_fingerGesture._touch0.phase == TouchPhase.Ended || _fingerGesture._touch0.phase == TouchPhase.Canceled)
            {
                FingerInputController.GetInstance().NotifyTouchUp(_fingerGesture._touch0.fingerId, _fingerGesture._touch0.position);
                FingerInputController.GetInstance().NotifyTouchClick(_fingerGesture._touch0.fingerId, _fingerGesture._touch0.position);
                _stateMachine.ChangeState((int)GestureStateEnum.None);
            }
        }
        else if (_fingerGesture._touchCount == 2)
        {
            _stateMachine.ChangeState((int)GestureStateEnum.Pinch);
        }
    }
}