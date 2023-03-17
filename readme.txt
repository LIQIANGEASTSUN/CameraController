
手指按下时在UI 上，则该手指后续的所有操作忽略

单指按下，
if(touch.phase == down)
{
    触发按下
}
else if (touch.phase == standy)
{
    触发长按
}
else if (touch.phase == move)
{
    if（长按中）
        {
               长按结束
        }
    触发拖拽
}
else if (touch.phase == end || touch.phase == cancel)
{
    触发弹起
    if （长按中）
        {
        长按结束
    }
    if (拖拽中)
        {
                拖拽结束
        }
}

if (！第二指操作)
{
    return
}

if (拖拽中)
{
        return
}

触发弹起

if (长按中)
{
    长按结束
}


缩放逻辑



第一指只要是开始拖拽，手指拖拽时停止或一直移动，但是没有弹起，则第二指所有操作无效
单指按下，长按时，第二指按下/托拽，则退出按下，长按，转为缩放












