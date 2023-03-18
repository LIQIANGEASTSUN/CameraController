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

        FingerInputController.GetInstance().AddTouchDown(TouchDown);
        FingerInputController.GetInstance().AddTouchUp(TouchUp);
        FingerInputController.GetInstance().AddTouchClick(TouchClick);
        FingerInputController.GetInstance().AddTouchPress(TouchPress);
        FingerInputController.GetInstance().AddBeginDrag(BeginDrag);
        FingerInputController.GetInstance().AddTouchDrag(Drag);
        FingerInputController.GetInstance().AddDragEnd(EndDrag);
        FingerInputController.GetInstance().AddBeginPinch(BeginPinch);
        FingerInputController.GetInstance().AddTouchPinch(Pinch);
        FingerInputController.GetInstance().AddEndPinch(EndPinch);

        for (int i = 0; i < 10;  i++)
        {
            _msgList.Add("");
        }
    }

    public void Update()
    {
        FingerInputController.GetInstance().Update();
        _cameraController.LateUpdate();
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
            sb.Clear();
        }
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

    private void TouchPress(int fingerId, Vector2 position)
    {
        _msgList[3] = ("TouchPress:" + fingerId + "    " + position);
    }

    private void BeginDrag(int fingerId, Vector2 position)
    {
        _msgList[4] = ("BeginDrag:" + fingerId + "    " + position);
    }

    private void Drag(int fingerId, Vector2 position, Vector2 deltaPosition)
    {
        _cameraController.DragPosition(position, deltaPosition);
        _msgList[5] = ("Drag:" + fingerId + "    " + position);
    }

    private void EndDrag(int fingerId, Vector2 position, Vector2 deltaPosition)
    {
        _msgList[6] = ("EndDrag:" + fingerId + "    " + position + "   " + deltaPosition);
        _cameraController.DragEnd(position, deltaPosition);
    }

    private void BeginPinch(int fingerId1, int fingerId2, float pinch)
    {
        _msgList[7] = ("BeginPinch:" + fingerId1 + "    " + pinch);
    }

    private void Pinch(int fingerId1, int fingerId2, float pinch)
    {
        _cameraController.UpdatePinch(pinch);
        _msgList[8] = ("Pinch:" + fingerId1 + "    " + pinch);
    }

    private void EndPinch(int fingerId1, int fingerId2, float pinch)
    {
        _msgList[9] = ("EndPinch:" + fingerId1 + "    " + pinch);
    }
}