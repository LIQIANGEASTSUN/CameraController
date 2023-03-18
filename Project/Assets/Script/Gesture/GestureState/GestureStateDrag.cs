using UnityEngine;

public class GestureStateDrag : StateBase
{
    private Vector2 _lastPosition = Vector2.zero;
    public GestureStateDrag(StateMachine stateMachine, FingerGesture fingerGesture) : base(stateMachine, (int)GestureStateEnum.Drag, fingerGesture)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        if (_fingerGesture._touch0.phase == TouchPhase.Moved)
        {
            _lastPosition = _fingerGesture._touch0.position;
            FingerInputController.GetInstance().NotifyBeginDrag(_fingerGesture._touch0.fingerId, _fingerGesture._touch0.position);
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

        if (_fingerGesture._touch0.phase == TouchPhase.Moved)
        {
            Vector2 deltaPosition = _fingerGesture._touch0.position - _lastPosition;
            FingerInputController.GetInstance().NotifyTouchDrag(_fingerGesture._touch0.fingerId, _fingerGesture._touch0.position, deltaPosition);
            _lastPosition = _fingerGesture._touch0.position;
        }
        else if (_fingerGesture._touch0.phase == TouchPhase.Ended || _fingerGesture._touch0.phase == TouchPhase.Canceled)
        {
            Vector2 deltaPosition = _fingerGesture._touch0.position - _lastPosition;
            FingerInputController.GetInstance().NotifyDragEnd(_fingerGesture._touch0.fingerId, _fingerGesture._touch0.position, deltaPosition);
            _stateMachine.ChangeState((int)GestureStateEnum.None);
        }
    }
}