public class StateBase : IState
{
    protected StateMachine _stateMachine;
    protected int _state;
    protected object _transitionData;

    public StateBase(StateMachine stateMachine, int state)
    {
        _stateMachine = stateMachine;
        _state = state;
    }

    public int State
    {
        get
        {
            return _state;
        }
    }

    public void SetTransitionData(object transitionData)
    {
        _transitionData = transitionData;
    }

    public virtual void OnEnter()
    {

    }

    public virtual void OnExecute()
    {

    }

    public virtual void OnExit()
    {

    }
}
