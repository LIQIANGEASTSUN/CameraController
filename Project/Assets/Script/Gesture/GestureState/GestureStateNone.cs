public class GestureStateNone : GestureStateBase
{
    public GestureStateNone(StateMachine stateMachine, FingerGesture fingerGesture) : base(stateMachine, (int)GestureStateEnum.None, fingerGesture)
    {
    }

    protected override void Touch1Execute()
    {
        _stateMachine.ChangeState((int)GestureStateEnum.Click);
    }

    protected override void Touch2Execute()
    {
        _stateMachine.ChangeState((int)GestureStateEnum.Pinch);
    }
}