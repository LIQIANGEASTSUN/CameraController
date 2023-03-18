using UnityEngine;

public class GestureStateDrag : GestureStateBase
{
    private Vector2 _lastPosition = Vector2.zero;
    public GestureStateDrag(StateMachine stateMachine, FingerGesture fingerGesture) : base(stateMachine, (int)GestureStateEnum.Drag, fingerGesture)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _lastPosition = _fingerGesture._touch0.position;
        if(_fingerGesture._touch0.phase == TouchPhase.Moved)
        {
            FingerInputController.GetInstance().fingerTouchBeginDrag?.Invoke(_fingerGesture._touch0.fingerId, _fingerGesture._touch0.position);
        }
    }

    protected override void Touch0Execute()
    {
        _stateMachine.ChangeState((int)GestureStateEnum.None);
    }

    protected override void Touch1Execute()
    {
        Drag();
    }

    protected override void Touch2Execute()
    {
        Drag();
    }

    private void Drag()
    {
        if (_fingerGesture._touch0.phase == TouchPhase.Moved)
        {
            Vector2 deltaPosition = GetDeltaPosition();
            FingerInputController.GetInstance().fingerTouchDrag?.Invoke(_fingerGesture._touch0.fingerId, _fingerGesture._touch0.position, deltaPosition);
        }
        else if (_fingerGesture._touch0.phase == TouchPhase.Ended || _fingerGesture._touch0.phase == TouchPhase.Canceled)
        {
            Vector2 deltaPosition = GetDeltaPosition();
            FingerInputController.GetInstance().fingerTouchDragEnd(_fingerGesture._touch0.fingerId, _fingerGesture._touch0.position, deltaPosition);
            FingerInputController.GetInstance().fingerTouchUp?.Invoke(_fingerGesture._touch0.fingerId, _fingerGesture._touch0.position);
            _stateMachine.ChangeState((int)GestureStateEnum.None);
        }
    }

    private Vector2 GetDeltaPosition()
    {
        Vector2 deltaPosition = _fingerGesture._touch0.position - _lastPosition;
        _lastPosition = _fingerGesture._touch0.position;
        return deltaPosition;
    }
}