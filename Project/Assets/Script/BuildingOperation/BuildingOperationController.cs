using UnityEngine;

public class BuildingOperationController : SingletonObject<BuildingOperationController>
{

    public void Init()
    {
        FingerInputController.GetInstance().fingerTouchDown += TouchDown;
        FingerInputController.GetInstance().fingerTouchUp += TouchUp;
        FingerInputController.GetInstance().fingerTouchClick += TouchClick;
        FingerInputController.GetInstance().fingerTouchBeginLongPress += TouchBeginLongPress;
        FingerInputController.GetInstance().fingerTouchLongPress += TouchPress;
        FingerInputController.GetInstance().fingerTouchBeginLongPress += TouchEndLongPress;
        FingerInputController.GetInstance().fingerTouchBeginDrag += BeginDrag;
        FingerInputController.GetInstance().fingerTouchDrag += Drag;
        FingerInputController.GetInstance().fingerTouchDragEnd += EndDrag;
    }

    private void TouchDown(int fingerId, Vector2 position)
    {

    }

    private void TouchUp(int fingerId, Vector2 position)
    {

    }

    private void TouchClick(int fingerId, Vector2 position)
    {
        UnitInfo unitInfo = GetUnitInfo(position);
        if (unitInfo != null)
        {
            Debug.LogError("Click:" + unitInfo.GetUnitId());
        }
    }

    private void TouchBeginLongPress(int fingerId, Vector2 position)
    {

    }

    private void TouchPress(int fingerId, Vector2 position, float time)
    {

    }

    private void TouchEndLongPress(int fingerId, Vector2 position)
    {

    }

    private void BeginDrag(int fingerId, Vector2 position)
    {

    }

    private void Drag(int fingerId, Vector2 position, Vector2 deltaPosition)
    {

    }

    private void EndDrag(int fingerId, Vector2 position, Vector2 deltaPosition)
    {

    }

    private RaycastHit[] results = new RaycastHit[5];
    private int layerMask = 1 << LayerMask.NameToLayer("Default");
    public UnitInfo GetUnitInfo(Vector2 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        int count = Physics.RaycastNonAlloc(ray, results, 50, layerMask);
        for (int i = 0; i < count; i++)
        {
            RaycastHit hit = results[i];
            UnitInfo unitInfo = hit.transform.GetComponent<UnitInfo>();
            if (null != unitInfo)
            {
                return unitInfo;
            }
        }
        return null;
    }

    public void Release()
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
    }

}