using UnityEngine;

public class MouseInput : MonoBehaviour
{
    private float m_wheelSpeed = 50.0f;   // 默认鼠标滚轮速度
    private int m_mouseLeft = 0;         //0对应右键 
    protected Vector3 m_lastMousePos;
    private CameraController _cameraController;

    private void Start()
    {
        _cameraController = new CameraController();
    }

    public void Update()
    {
        UpdateDragPosition();
        UpdatePinch();

        if (Input.GetKeyDown(KeyCode.A))
        {
            GameObject go = GameObject.Find("1");
            _cameraController.MoveLookPosition(go.transform.position);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GameObject go = GameObject.Find("2");
            _cameraController.MoveLookPosition(go.transform.position);
        }
    }

    public void UpdatePinch()
    {
        float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        if (mouseScroll != 0)
        {
            _cameraController.UpdatePinch(mouseScroll * m_wheelSpeed);
        }
    }

    void UpdateDragPosition()
    {
        if (Input.GetMouseButtonDown(m_mouseLeft))  //如果鼠标点击的 1键
        {
            m_lastMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(m_mouseLeft))
        {
            Vector3 offset = (Input.mousePosition - m_lastMousePos);
            _cameraController.UpdateDragPosition(offset, Input.mousePosition);
            m_lastMousePos = Input.mousePosition;
        }
    }
}

/*
 
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    public float m_currentDistance = 8.0f;  // 默认距离
    public float m_wheelSpeed = 2.0f;   // 默认鼠标滚轮速度

    private float m_fDistanceMin = 6;
    private float m_fDistanceMax = 30;

    public int m_mouseLeft = 0;  //0对应右键 

    protected Vector3 m_MovePostion;
    protected Vector3 m_OldMousePos;

    protected Transform cameraTr;

    private CameraController _cameraController = new CameraController();

    void OnEnable()
    {
        cameraTr = Camera.main.transform;
    }

    void Update()
    {
        UpdateDragPosition();
        UpdatePinch();
    }

    public void UpdatePinch()
    {
        float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        if (mouseScroll == 0)
        {
            return;
        }

        m_currentDistance = CameraToLookPositionLength(cameraTr.forward);
        m_currentDistance = m_currentDistance - mouseScroll * m_wheelSpeed;
        m_currentDistance = Mathf.Clamp(m_currentDistance, m_fDistanceMin, m_fDistanceMax);

        float dist = CameraToLookPositionLength(cameraTr.forward);
        Vector3 zero = cameraTr.position + cameraTr.forward * dist;
        Vector3 position = zero - m_currentDistance * cameraTr.forward;
        SetPosition(position);
    }

    void UpdateDragPosition()
    {
        if (Input.GetMouseButtonDown(m_mouseLeft))  //如果鼠标点击的 1键
        {
            m_OldMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(m_mouseLeft))
        {
            Ray rayStart = Camera.main.ScreenPointToRay(m_OldMousePos);
            float dist = CameraToLookPositionLength(rayStart.direction);
            Vector3 start = cameraTr.position + rayStart.direction * dist;

            Vector3 currentMouse = Input.mousePosition;
            Ray rayCurrent = Camera.main.ScreenPointToRay(currentMouse);
            dist = CameraToLookPositionLength(rayCurrent.direction);
            Vector3 current = cameraTr.position + rayCurrent.direction * dist;

            Vector3 offset = current - start;
            cameraTr.position -= offset;
            m_OldMousePos = Input.mousePosition;
        }
    }

    //设置主摄像机的位置
    private void SetPosition(Vector3 position)
    {
        cameraTr.position = position;
    }

    private void SetRotation(Quaternion rotation)
    {
        cameraTr.rotation = rotation;
    }

    private float CameraToLookPositionLength(Vector3 dir)
    {
        float dist = cameraTr.position.y / Vector3.Dot(dir, Vector3.up * -1);
        return dist;
    }

    public float GetWorldPerScreenPixel()
    {
        Camera cam = Camera.main;
        float dist = CameraToLookPositionLength(cameraTr.forward);
        float sample = 100;
        return Vector3.Distance(cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2 - sample / 2, dist)), cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2 + sample / 2, dist))) / sample;
    }
}
*/