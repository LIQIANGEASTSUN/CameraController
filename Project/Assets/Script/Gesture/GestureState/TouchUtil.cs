using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchUtil
{
    //是否点击在UI上
    public static bool IsPointerOverGameObject(Vector3 screenPosition)
    {
        try
        {
#if ENABLE_GM || DEBUG
            //GM开启时会禁用EventSystem，容错，正式包不会
            if (EventSystem.current == null)
                return false;
#endif
            if (screenPosition.x < 0 || screenPosition.x > Screen.width || screenPosition.y < 0 || screenPosition.y > Screen.height)
            {
                return true;
            }

            //实例化点击
            PointerEventData eventDataCurrentPosition = new PointerEventData(uiEventSystem);
            //将点击位置的屏幕坐标赋值给点击事件
            eventDataCurrentPosition.position = new Vector2(screenPosition.x, screenPosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            //向点击初发送射线
            uiEventSystem.RaycastAll(eventDataCurrentPosition, results);

            return results.Count > 0;
        }
        catch (Exception ex)
        {
            Debug.LogError($"TouchUtil: {ex.Message}");
            return false;
        }
    }

    private static UnityEngine.EventSystems.EventSystem _uiEventSystem;
    public static UnityEngine.EventSystems.EventSystem uiEventSystem
    {
        get
        {
            if (_uiEventSystem == null)
                _uiEventSystem = UnityEngine.EventSystems.EventSystem.current;
            return _uiEventSystem;
        }
    }
}
