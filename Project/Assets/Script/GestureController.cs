using UnityEngine;

public class GestureController : MonoBehaviour
{
    public static GestureController Instance;
    private CameraController _cameraController;

    private void Awake()
    {
        Instance = this;
        _cameraController = CameraController.GetInstance();

        BuildingOperationController.GetInstance().Init();

        FingerInputController.GetInstance().fingerTouchDrag += Drag;
        FingerInputController.GetInstance().fingerTouchDragEnd += EndDrag;
        FingerInputController.GetInstance().fingerTouchPinch += Pinch;

        Camera camera = GetComponent<Camera>();
        _cameraController.SetCamera(camera);
    }

    public void SetEnable(bool value)
    {
        enabled = value;
    }

    public void Update()
    {
        if (enabled)
        {
            FingerInputController.GetInstance().Update();
        }
    }

    public void LateUpdate()
    {
        if (enabled)
        {
            _cameraController.LateUpdate();
        }
    }

    private void OnDestroy()
    {
        FingerInputController.GetInstance().fingerTouchDrag -= Drag;
        FingerInputController.GetInstance().fingerTouchDragEnd -= EndDrag;
        FingerInputController.GetInstance().fingerTouchPinch -= Pinch;

        BuildingOperationController.GetInstance().Release();
    }

    private void Drag(Vector3 startPosition, Vector3 position, Vector3 deltaPosition)
    {
        _cameraController.DragPosition(position, deltaPosition);
    }

    private void EndDrag(Vector3 position, Vector3 deltaPosition)
    {
        _cameraController.DragEnd(position, deltaPosition);
    }

    private void Pinch(float pinch)
    {
        _cameraController.UpdatePinch(pinch);
    }
}