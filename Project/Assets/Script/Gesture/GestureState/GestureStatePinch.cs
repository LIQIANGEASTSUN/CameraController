using UnityEngine;

public class GestureStatePinch : GestureStateBase
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
        FingerInputController.GetInstance().fingerTouchBeginPinch?.Invoke(0);
    }

    protected override void Touch0Execute()
    {
        _stateMachine.ChangeState((int)GestureStateEnum.None);
    }

    protected override void Touch1Execute()
    {
        _stateMachine.ChangeState((int)GestureStateEnum.None);
    }

    protected override void Touch2Execute()
    {
        if (_fingerGesture._touch0.phase == TouchPhase.Moved || _fingerGesture._touch1.phase == TouchPhase.Moved)
        {
            float pinch = Pinch();
            FingerInputController.GetInstance().fingerTouchPinch?.Invoke(pinch);
        }
        else if (_fingerGesture._touch0.phase == TouchPhase.Ended || _fingerGesture._touch0.phase == TouchPhase.Canceled
              || _fingerGesture._touch1.phase == TouchPhase.Ended || _fingerGesture._touch1.phase == TouchPhase.Canceled)
        {
            _stateMachine.ChangeState((int)GestureStateEnum.None);
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        FingerInputController.GetInstance().fingerTouchPinchEnd?.Invoke(0);
    }

    private float Pinch()
    {
        float pinch = Vector2.Distance(_fingerGesture._touch0.position, _fingerGesture._touch1.position);
        float value = (pinch - _lastPinchDistance) * _coefficient;
        _lastPinchDistance = pinch;
        return value;
    }
}