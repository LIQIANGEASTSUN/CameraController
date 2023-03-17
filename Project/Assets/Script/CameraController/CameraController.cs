using UnityEngine;

public class CameraController
{
    public float m_currentDistance = 8.0f;  // 默认距离
    private float m_pinchSpeed = 2;         // 缩放速度

    private float m_fDistanceMin = 6;
    private float m_fDistanceMax = 20;

    private Vector3 _smoothVector = Vector3.zero;
    private float _smoothTime = 0;

    protected Transform cameraTr;

    public CameraController()
    {
        cameraTr = Camera.main.transform;
    }

    //设置主摄像机的位置
    public void SetPosition(Vector3 position)
    {
        cameraTr.position = position;
    }

    public Vector3 GetPosition()
    {
        return cameraTr.position;
    }

    private void SetRotation(Quaternion rotation)
    {
        cameraTr.rotation = rotation;
    }

    public Quaternion GetRotation()
    {
        return cameraTr.rotation;
    }

    private Vector3 Forward
    {
        get { return cameraTr.forward;}
    }

    public void UpdatePinch(float pinch)
    {
        float dist = CameraToLookPositionLength(Forward);
        m_currentDistance = dist - pinch * m_pinchSpeed;
        m_currentDistance = Mathf.Clamp(m_currentDistance, m_fDistanceMin, m_fDistanceMax);

        //摄像机中心地面坐标
        Vector3 zero = GetPosition() + Forward * dist;
        // 计算摄像机坐标
        Vector3 position = zero - m_currentDistance * Forward;
        SetPosition(position);
    }

    public void DragPosition(Vector2 currentScreen, Vector2 offsetScreen)
    {
        Vector3 offset = DragOffset(currentScreen, offsetScreen);
        Vector3 position = GetPosition() - offset;
        SetPosition(position);
    }

    public void DragEnd(Vector2 currentScreen, Vector2 offsetScreen)
    {
        Vector3 offset = DragOffset(currentScreen, offsetScreen);
        _smoothVector = offset * 0.5f;
        _smoothTime = 1;
    }

    public Vector3 DragOffset(Vector2 currentScreen, Vector2 offsetScreen)
    {
        Vector3 startScreen = currentScreen - offsetScreen;
        Ray rayStart = Camera.main.ScreenPointToRay(startScreen);
        float dist = CameraToLookPositionLength(rayStart.direction);
        Vector3 start = GetPosition() + rayStart.direction * dist;

        Ray rayCurrent = Camera.main.ScreenPointToRay(currentScreen);
        dist = CameraToLookPositionLength(rayCurrent.direction);
        Vector3 current = GetPosition() + rayCurrent.direction * dist;

        Vector3 offset = current - start;
        return offset;
    }

    public void Update()
    {
        if(_smoothTime > 0)
        {
            _smoothTime -= Time.deltaTime * 2;
            _smoothTime = Mathf.Clamp(_smoothTime, 0, 1);
            Vector3 position = GetPosition() - _smoothVector * _smoothTime;
            SetPosition(position);
        }
    }

    public void MoveLookPosition(Vector3 position)
    {
        float dist = CameraToLookPositionLength(Forward);

        Vector3 result = position - Forward * dist;
        // 此处加入 Tween
        SetPosition(result);
    }

    private float CameraToLookPositionLength(Vector3 dir)
    {
        Vector3 position = GetPosition();
        float dist = position.y / Vector3.Dot(dir, Vector3.down);
        return dist;
    }
}