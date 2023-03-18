/// <summary>
/// 单例
/// </summary>
public abstract class SingletonObject<T> : System.Object where T : SingletonObject<T>, new() {

    private static T _instance;

    private static readonly object syslock = new object();

    public static T GetInstance() {
        if (null == _instance) {
            lock (syslock)
            {
                if (null == _instance)
                {
                    _instance = new T();
                }
            }
        }
        return _instance;
    }

    public virtual void Destroy() {
        _instance = null;
    }
}