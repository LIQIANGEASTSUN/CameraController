using UnityEngine;

public class UnitInfo : MonoBehaviour
{
    [SerializeField]
    private int _unitId;

    public void SetUnitId(int unitId)
    {
        _unitId = unitId;
    }

    public int GetUnitId()
    {
        return _unitId;
    }
}