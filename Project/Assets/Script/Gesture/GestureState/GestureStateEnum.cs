using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GestureStateEnum
{
    /// <summary>
    /// 空状态
    /// </summary>
    None = 1 << 0,

    /// <summary>
    /// 点击：包含 按下/点击/弹起
    /// </summary>
    Click = 1 << 1,
    
    /// <summary>
    /// 拖拽：包含 开始拖拽/拖拽/停止拖拽
    /// </summary>
    Drag = 1 << 2,

    /// <summary>
    /// 缩放：包含 开始缩放/缩放/停止缩放
    /// </summary>
    Pinch = 1 << 3,
}
