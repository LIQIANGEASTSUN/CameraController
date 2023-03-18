public class GestureStateBase : StateBase
{
    protected FingerGesture _fingerGesture;

    public GestureStateBase(StateMachine stateMachine, int state, FingerGesture fingerGesture) : base(stateMachine, state)
    {
        _stateMachine = stateMachine;
        _state = state;
        _fingerGesture = fingerGesture;
    }
}