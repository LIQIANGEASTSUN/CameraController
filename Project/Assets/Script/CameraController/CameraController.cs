using UnityEngine;
using System;

public class CameraController : SingletonObject<CameraController>
{
    private Camera _camera;

    private float m_pinchSpeed = 2;                // �����ٶ�

    private Bounds _rangeBounds = new Bounds();    // ����������

    private Vector3 _smoothVector = Vector3.zero;  // Camera ֹͣ����ƽ���ƶ�����
    private float _smoothTime = 0;                 // Camera ֹͣ����ƽ���ƶ�ʱ��

    private bool _lockDrag = false;                // �� Drag ����
    private bool _lockPinch = false;               // �� pinch ����

    public CameraController()
    {
        Vector3 min = new Vector3(-50, 5, -50);  // �������СX�� -50, ��СY��  5, ��СZ��-50
        Vector3 max = new Vector3(50, 30, 50);   // ��������X��  50, ���Y�� 30, ���Z�� 50
        _rangeBounds.SetMinMax(min, max);
    }

    public static CameraController Instance
    {
        get { return GetInstance(); }
    }

    public Transform Transform
    {
        get { return _camera.transform; }
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

    public void SetRotation(Quaternion rotation)
    {
        Transform.rotation = rotation;
    }

    public Quaternion GetRotation()
    {
        return Transform.rotation;
    }

    public Vector3 Forward
    {
        get { return Transform.forward; }
    }

    public void SetCamera(Camera camera)
    {
        _camera = camera;
    }

    public void SetLockDrag(bool lockDrag)
    {
        _lockDrag = lockDrag;
    }

    public void SetLockPinch(bool lockPinch)
    {
        _lockPinch = lockPinch;
    }

    public void SetRangeBounds(Vector3 min, Vector3 max)
    {
        _rangeBounds.SetMinMax(min, max);
    }

    public Bounds GetRangeBounds()
    {
        return _rangeBounds;
    }

    public void UpdatePinch(float pinch)
    {
        if (_lockPinch)
            return;
        float dist = CameraToDirectionLength(Forward);
        float currentDistance = dist - pinch * m_pinchSpeed;
        currentDistance = Mathf.Clamp(currentDistance, ZoomMin, ZoomMax);

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
        StartSmoothTo(1, offset * 0.5f);
    }

    public Vector3 DragOffset(Vector2 currentScreen, Vector2 offsetScreen)
    {
        Vector3 startScreen = currentScreen - offsetScreen;
        Ray rayStart = ScreenPointToRay(startScreen);
        float dist = CameraToDirectionLength(rayStart.direction);
        Vector3 start = GetPosition() + rayStart.direction * dist;

        Ray rayCurrent = ScreenPointToRay(currentScreen);
        dist = CameraToDirectionLength(rayCurrent.direction);
        Vector3 current = GetPosition() + rayCurrent.direction * dist;

        Vector3 offset = current - start;
        return offset;
    }

    /// <summary>
    /// ������۽��� targetPos λ��
    /// </summary>
    /// <param name="targetPos">(3D����)</param>
    /// <param name="distance">����������� targetPos ��������ľ���Ϊ lockOffset </param>
    public void FocusLookPosition(Vector3 targetPos, float distance = 0, Action callback = null)
    {
        //��Ļ������������
        Vector3 camIntersectionPoint = GetScreenCenterIntersectionPos();
        targetPos.y = camIntersectionPoint.y;
        Vector3 cameraPosition = GetPosition();
        Vector3 offset = cameraPosition - camIntersectionPoint;
        if (Mathf.Abs(distance) <= 1)
        {
            targetPos += offset;
        }
        else
        {
            targetPos += offset.normalized * distance;
        }
        targetPos = GetClampToBoundaries(targetPos);

        SetPosition(targetPos);

        //bool isSmall = Vector3.Distance(cameraPosition, targetPos) < 1;
        //float duration = isSmall ? .03f : 0.5f;
        //Tween tween = DOTween.To(() => cameraPosition, (position) =>
        //{
        //    SetPosition(position);
        //}, targetPos, duration);
        //tween.onComplete += () =>
        //{
        //    tween.Kill();
        //    tween = null;
        //    callback?.Invoke();
        //};
    }

    public void LateUpdate()
    {
        SmoothTo();
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

    /// <summary>
    /// ��ȡ��Ļ���Ķ�Ӧ�ĵ������꣬����Ļ���ķ������ߣ������Ľ���
    /// </summary>
    public Vector3 GetScreenCenterIntersectionPos()
    {
        Vector2 centerPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        return GetScreenPositionIntersectionPos(centerPoint);
    }

    /// <summary>
    /// ��ȡ��Ļ�����Ӧ�ĵ������꣬����Ļ���ķ������ߣ������Ľ���
    /// </summary>
    public Vector3 GetScreenPositionIntersectionPos(Vector2 screenPosition)
    {
        Ray rayCurrent = _camera.ScreenPointToRay(screenPosition);
        float dist = CameraToDirectionLength(rayCurrent.direction);
        Vector3 position = GetPosition() + rayCurrent.direction * dist;
        return position;
    }

    /// <summary>
    /// ����������� direction ���������ߣ�����潻��
    /// </summary>
    public float CameraToDirectionLength(Vector3 direction)
    {
        Vector3 position = GetPosition();
        float dist = position.y / Vector3.Dot(direction, Vector3.down);
        return dist;
    }

    public Ray ScreenPointToRay(Vector2 screenPosition)
    {
        Ray ray = _camera.ScreenPointToRay(screenPosition);
        return ray;
    }

    public Vector3 GetClampToBoundaries(Vector3 position)
    {
        Bounds bounds = GetRangeBounds();
        position.x = Mathf.Clamp(position.x, bounds.min.x, bounds.max.x);
        position.y = Mathf.Clamp(position.y, bounds.min.y, bounds.max.y);
        position.z = Mathf.Clamp(position.z, bounds.min.z, bounds.max.z);
        return (position);
    }

    public float ZoomMin
    {
        get
        {
            return _rangeBounds.min.y;
        }
        set
        {
            Vector3 min = _rangeBounds.min;
            min.y = value;
            _rangeBounds.min = min;
        }
    }

    public float ZoomMax
    {
        get
        {
            return _rangeBounds.max.y;
        }
        set
        {
            Vector3 max = _rangeBounds.max;
            max.y = value;
            _rangeBounds.max = max;
        }
    }

    public float ZoomCurrent
    {
        get
        {
            return GetPosition().y;
        }
    }
}