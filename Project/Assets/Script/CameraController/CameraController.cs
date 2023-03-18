using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    private float m_pinchSpeed = 2;                // �����ٶ�

    private float m_fDistanceMin = 6;              // Camera Forward ���� ��������������
    private float m_fDistanceMax = 20;             // Camera Forward ���� ���������Զ����

    private Vector3 _smoothVector = Vector3.zero;  // Camera ֹͣ����ƽ���ƶ�����
    private float _smoothTime = 0;                 // Camera ֹͣ����ƽ���ƶ�ʱ��

    private bool _lockDrag = false;                // �� Drag ����
    private bool _lockPinch = false;               // �� pinch ����

    private Camera _camera;
    public CameraController() {  
    
    }

    public void Awake()
    {
        Instance = this;
        _camera = GetComponent<Camera>();
    }

    public void SetCamera(Camera camera)
    {
        _camera = camera;
    }

    private Transform Transform
    {
        get { return _camera.transform; }
    }

    public void SetLockDrag(bool lockDrag)
    {
        _lockDrag = lockDrag;
    }

    public void SetLockPinch(bool lockPinch)
    {
        _lockPinch = lockPinch;
    }

    //�������������λ��
    public void SetPosition(Vector3 position)
    {
        _camera.transform.position = position;
    }

    public Vector3 GetPosition()
    {
        return Transform.position;
    }

    private void SetRotation(Quaternion rotation)
    {
        Transform.rotation = rotation;
    }

    public Quaternion GetRotation()
    {
        return Transform.rotation;
    }

    private Vector3 Forward
    {
        get { return Transform.forward;}
    }

    public void UpdatePinch(float pinch)
    {
        if (_lockPinch)
            return;
        float dist = CameraToLookPositionLength(Forward);
        float currentDistance = dist - pinch * m_pinchSpeed;
        currentDistance = Mathf.Clamp(currentDistance, m_fDistanceMin, m_fDistanceMax);

        //��������ĵ�������
        Vector3 zero = GetPosition() + Forward * dist;
        // �������������
        Vector3 position = zero - currentDistance * Forward;
        SetPosition(position);
        StopSmooth();
    }

    public void DragPosition(Vector2 currentScreen, Vector2 offsetScreen)
    {
        if (_lockDrag)
            return;
        Vector3 offset = DragOffset(currentScreen, offsetScreen);
        Vector3 position = GetPosition() - offset;
        SetPosition(position);
        StopSmooth();
    }

    public void DragEnd(Vector2 currentScreen, Vector2 offsetScreen)
    {
        if (_lockDrag)
            return;
        Vector3 offset = DragOffset(currentScreen, offsetScreen);
        StartSmoothTo( 1, offset * 0.5f);
    }

    public Vector3 DragOffset(Vector2 currentScreen, Vector2 offsetScreen)
    {
        Vector3 startScreen = currentScreen - offsetScreen;
        Ray rayStart = _camera.ScreenPointToRay(startScreen);
        float dist = CameraToLookPositionLength(rayStart.direction);
        Vector3 start = GetPosition() + rayStart.direction * dist;

        Ray rayCurrent = _camera.ScreenPointToRay(currentScreen);
        dist = CameraToLookPositionLength(rayCurrent.direction);
        Vector3 current = GetPosition() + rayCurrent.direction * dist;

        Vector3 offset = current - start;
        return offset;
    }

    public void LateUpdate()
    {
        SmoothTo();
    }

    public void MoveLookPosition(Vector3 position)
    {
        float dist = CameraToLookPositionLength(Forward);

        Vector3 result = position - Forward * dist;
        // �˴����� Tween
        SetPosition(result);
    }

    private float CameraToLookPositionLength(Vector3 dir)
    {
        Vector3 position = GetPosition();
        float dist = position.y / Vector3.Dot(dir, Vector3.down);
        return dist;
    }

    private void StopSmooth()
    {
        _smoothTime = 0;
        _smoothVector = Vector3.zero;
    }

    private void StartSmoothTo(float smoothTime, Vector3 smoothVector)
    {
        _smoothTime = smoothTime;
        _smoothVector = smoothVector;
    }

    private void SmoothTo()
    {
        if (_smoothTime > 0)
        {
            _smoothTime -= Time.deltaTime * 2;
            _smoothTime = Mathf.Clamp(_smoothTime, 0, 1);
            Vector3 position = GetPosition() - _smoothVector * _smoothTime;
            SetPosition(position);
        }
    }
}