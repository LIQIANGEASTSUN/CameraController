using UnityEngine;

public class CameraController
{
    public float m_currentDistance = 8.0f;  // Ĭ�Ͼ���
    private float m_pinchSpeed = 2;         // �����ٶ�

    private float m_fDistanceMin = 6;
    private float m_fDistanceMax = 20;
    protected Transform cameraTr;

    public CameraController()
    {
        cameraTr = Camera.main.transform;
    }

    //�������������λ��
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

        //��������ĵ�������
        Vector3 zero = GetPosition() + Forward * dist;
        // �������������
        Vector3 position = zero - m_currentDistance * Forward;
        SetPosition(position);
    }

    public void UpdateDragPosition(Vector3 currentScreen, Vector3 offsetScreen)
    {
        Vector3 startScreen = currentScreen - offsetScreen;
        Ray rayStart = Camera.main.ScreenPointToRay(startScreen);
        float dist = CameraToLookPositionLength(rayStart.direction);
        Vector3 start = GetPosition() + rayStart.direction * dist;

        Ray rayCurrent = Camera.main.ScreenPointToRay(currentScreen);
        dist = CameraToLookPositionLength(rayCurrent.direction);
        Vector3 current = GetPosition() + rayCurrent.direction * dist;

        Vector3 offset = current - start;
        Vector3 position = GetPosition() - offset;
        SetPosition(position);
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
}