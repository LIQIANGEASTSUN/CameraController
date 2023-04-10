using UnityEngine;

public class GestureStatePress : GestureStateBase
{
    private float _enterTime;
    public GestureStatePress(StateMachine stateMachine, FingerGesture fingerGesture) : base(stateMachine, (int)GestureStateEnum.Press, fingerGesture)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _enterTime = Time.realtimeSinceStartup;
        FingerInputController.GetInstance().fingerTouchBeginLongPress?.Invoke(_fingerGesture._touch0.position);
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
            float time = Time.realtimeSinceStartup - _enterTime;
            FingerInputController.GetInstance().fingerTouchLongPress?.Invoke(_fingerGesture._touch0.position, time);
        }
        else if (_fingerGesture._touch0.phase == TouchPhase.Ended || _fingerGesture._touch0.phase == TouchPhase.Canceled)
        {
            FingerInputController.GetInstance().fingerTouchUp?.Invoke(_fingerGesture._touch0.position);
            _stateMachine.ChangeState((int)GestureStateEnum.None);
        }
    }

    protected override void Touch2Execute()
    {
        _stateMachine.ChangeState((int)GestureStateEnum.Pinch);
    }

    public override void OnExit()
    {
        base.OnExit();
        FingerInputController.GetInstance().fingerTouchEndLongPress?.Invoke(_fingerGesture._touch0.position);
    }
}