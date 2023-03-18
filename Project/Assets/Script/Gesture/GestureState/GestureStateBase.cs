public enum GestureStateEnum
{
    /// <summary>
    /// 空状态
    /// </summary>
    None = 1 << 0,

    /// <summary>
    /// 点击：包含 按下/点击/弹起
    /// </summary>
    Click = 1 << 1,

    /// <summary>
    /// 长按：包含 长时间按下(>= 0.3秒)
    /// </summary>
    Press = 1 << 2,

    /// <summary>
    /// 拖拽：包含 开始拖拽/拖拽/停止拖拽
    /// </summary>
    Drag = 1 << 3,

    /// <summary>
    /// 缩放：包含 开始缩放/缩放/停止缩放
    /// </summary>
    Pinch = 1 << 4,
}

public class GestureStateBase : StateBase
{
    protected FingerGesture _fingerGesture;

    public GestureStateBase(StateMachine stateMachine, int state, FingerGesture fingerGesture) : base(stateMachine, state)
    {
        _stateMachine = stateMachine;
        _state = state;
        _fingerGesture = fingerGesture;
    }

    public override void OnExecute()
    {
        base.OnExecute();

        if (_fingerGesture._touchCount <= 0)
        {
            Touch0Execute();
        }
        else if (_fingerGesture._touchCount == 1)
        {
            Touch1Execute();
        }
        else if (_fingerGesture._touchCount == 2)
        {
            Touch2Execute();
        }
    }

    protected virtual void Touch0Execute()
    {

    }

    protected virtual void Touch1Execute()
    {

    }

    protected virtual void Touch2Execute()
    {

    }
}