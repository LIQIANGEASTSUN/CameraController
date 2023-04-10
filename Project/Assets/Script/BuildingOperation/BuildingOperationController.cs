using UnityEngine;

public class BuildingOperationController : SingletonObject<BuildingOperationController>
{

    public void Init()
    {
        FingerInputController.GetInstance().fingerTouchClick += TouchClick;
    }
    private void TouchClick(Vector3 position)
    {
        UnitInfo unitInfo = GetUnitInfo(position);
        if (unitInfo != null)
        {
            Debug.LogError("Click:" + unitInfo.GetUnitId());
        }
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
        FingerInputController.GetInstance().fingerTouchClick -= TouchClick;
    }

}