public class GestureStateNone : StateBase
{
    public GestureStateNone(StateMachine stateMachine, FingerGesture fingerGesture) : base(stateMachine, (int)GestureStateEnum.None, fingerGesture)
    {
    }

    public override void OnExecute()
    {
        base.OnExecute();
        if (_fingerGesture._touchCount == 1)
        {
            _stateMachine.ChangeState((int)GestureStateEnum.Click);
        }
        else if (_fingerGesture._touchCount == 2)
        {
            _stateMachine.ChangeState((int)GestureStateEnum.Pinch);
        }
    }
}