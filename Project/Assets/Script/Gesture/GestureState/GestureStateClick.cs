using UnityEngine;

public class GestureStateClick : GestureStateBase
{
    private float _enterTime;
    private float PRESS_MIN_TIME = 0.3f;
    public GestureStateClick(StateMachine stateMachine, FingerGesture fingerGesture) : base(stateMachine, (int)GestureStateEnum.Click, fingerGesture)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _enterTime = Time.realtimeSinceStartup;
        if (_fingerGesture._touch0.phase == TouchPhase.Began)
        {
            FingerInputController.GetInstance().fingerTouchDown?.Invoke(_fingerGesture._touch0.position);
        }
    }

    protected override void Touch0Execute()
    {
        _stateMachine.ChangeState((int)GestureStateEnum.None);
    }

    protected override void Touch1Execute()
    {
        if (_fingerGesture._touch0.phase == TouchPhase.Moved)
        {
            _stateMachine.ChangeState((int)GestureStateEnum.Drag);
        }
        else if (_fingerGesture._touch0.phase == TouchPhase.Stationary)
        {
            if (Time.realtimeSinceStartup - _enterTime >= PRESS_MIN_TIME)
            {
                _stateMachine.ChangeState((int)GestureStateEnum.Press);
            }
        }
        else if (_fingerGesture._touch0.phase == TouchPhase.Ended || _fingerGesture._touch0.phase == TouchPhase.Canceled)
        {
            FingerInputController.GetInstance().fingerTouchUp?.Invoke(_fingerGesture._touch0.position);
            FingerInputController.GetInstance().fingerTouchClick?.Invoke(_fingerGesture._touch0.position);
            _stateMachine.ChangeState((int)GestureStateEnum.None);
        }
    }

    protected override void Touch2Execute()
    {
        _stateMachine.ChangeState((int)GestureStateEnum.Pinch);
    }
}