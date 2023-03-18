using UnityEngine;
using System.Text;
using System.Collections.Generic;

public class MouseInput : MonoBehaviour
{
    private CameraController _cameraController;

    private List<string> _msgList = new List<string>();

    private void Start()
    {
        _cameraController = CameraController.Instance;
        _cameraController.SetCamera(Camera.main);

        FingerInputController.GetInstance().fingerTouchDown += TouchDown;
        FingerInputController.GetInstance().fingerTouchUp += TouchUp;
        FingerInputController.GetInstance().fingerTouchClick += TouchClick;
        FingerInputController.GetInstance().fingerTouchBeginLongPress += TouchBeginLongPress;
        FingerInputController.GetInstance().fingerTouchLongPress += TouchPress;
        FingerInputController.GetInstance().fingerTouchEndLongPress += TouchEndLongPress;
        FingerInputController.GetInstance().fingerTouchBeginDrag += BeginDrag;
        FingerInputController.GetInstance().fingerTouchDrag += Drag;
        FingerInputController.GetInstance().fingerTouchDragEnd += EndDrag;
        FingerInputController.GetInstance().fingerTouchBeginPinch += BeginPinch;
        FingerInputController.GetInstance().fingerTouchPinch += Pinch;
        FingerInputController.GetInstance().fingerTouchPinchEnd += EndPinch;

        for (int i = 0; i < 12; i++)
        {
            _msgList.Add("");
        }
    }

    public void Update()
    {
        FingerInputController.GetInstance().Update();
        _cameraController.LateUpdate();

        if (Input.GetKeyDown(KeyCode.A))
        {
            _cameraController.SetLockDrag(true);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _cameraController.SetLockDrag(false);
        }
    }

    private void OnGUI()
    {
        StringBuilder sb = new StringBuilder();
        foreach (string str in _msgList)
        {
            sb.AppendLine(str);
        }

        GUI.TextArea(new Rect(100, 10, 500, 160), sb.ToString());
        if (GUI.Button(new Rect(0, 10, 50, 50), "Clean"))
        {
            for (int i = 0; i < _msgList.Count; ++i)
            {
                _msgList[i] = "";
            }
        }
    }

    private void OnDestroy()
    {
        FingerInputController.GetInstance().fingerTouchDown -= TouchDown;
        FingerInputController.GetInstance().fingerTouchUp -= TouchUp;
        FingerInputController.GetInstance().fingerTouchClick -= TouchClick;
        FingerInputController.GetInstance().fingerTouchBeginLongPress -= TouchBeginLongPress;
        FingerInputController.GetInstance().fingerTouchLongPress -= TouchPress;
        FingerInputController.GetInstance().fingerTouchBeginLongPress -= TouchEndLongPress;
        FingerInputController.GetInstance().fingerTouchBeginDrag -= BeginDrag;
        FingerInputController.GetInstance().fingerTouchDrag -= Drag;
        FingerInputController.GetInstance().fingerTouchDragEnd -= EndDrag;
        FingerInputController.GetInstance().fingerTouchBeginPinch -= BeginPinch;
        FingerInputController.GetInstance().fingerTouchPinch -= Pinch;
        FingerInputController.GetInstance().fingerTouchPinchEnd -= EndPinch;
    }

    private void TouchDown(int fingerId, Vector2 position)
    {
        _msgList[0] = "TouchDown:" + fingerId + "    " + position;
    }

    private void TouchUp(int fingerId, Vector2 position)
    {
        _msgList[1] = "TouchUp:" + fingerId + "    " + position;
    }

    private void TouchClick(int fingerId, Vector2 position)
    {
        _msgList[2] = ("TouchClick:" + fingerId + "    " + position);
    }

    private void TouchBeginLongPress(int fingerId, Vector2 position)
    {
        _msgList[3] = ("BeginLongPress:" + fingerId + "    " + position);
    }

    private void TouchPress(int fingerId, Vector2 position, float time)
    {
        _msgList[4] = ("TouchPress:" + fingerId + "    " + position + "  " + time);
    }

    private void TouchEndLongPress(int fingerId, Vector2 position)
    {
        _msgList[5] = ("EndLongPress:" + fingerId + "    " + position);
    }

    private void BeginDrag(int fingerId, Vector2 position)
    {
        _msgList[6] = ("BeginDrag:" + fingerId + "    " + position);
    }

    private void Drag(int fingerId, Vector2 position, Vector2 deltaPosition)
    {
        _cameraController.DragPosition(position, deltaPosition);
        _msgList[7] = ("Drag:" + fingerId + "    " + position);
    }

    private void EndDrag(int fingerId, Vector2 position, Vector2 deltaPosition)
    {
        _msgList[8] = ("EndDrag:" + fingerId + "    " + position + "   " + deltaPosition);
        _cameraController.DragEnd(position, deltaPosition);
    }

    private void BeginPinch(int fingerId1, int fingerId2, float pinch)
    {
        _msgList[9] = ("BeginPinch:" + fingerId1 + "    " + pinch);
    }

    private void Pinch(int fingerId1, int fingerId2, float pinch)
    {
        _cameraController.UpdatePinch(pinch);
        _msgList[10] = ("Pinch:" + fingerId1 + "    " + pinch);
    }

    private void EndPinch(int fingerId1, int fingerId2, float pinch)
    {
        _msgList[11] = ("EndPinch:" + fingerId1 + "    " + pinch);
    }
}