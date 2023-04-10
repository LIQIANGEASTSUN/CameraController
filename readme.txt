

手势事件

按下
FingerTouchDown(Vector3 position);

弹起
FingerTouchUp(Vector3 position);

点击
FingerTouchClick(Vector3 position);

开始长按
FingerTouchBeginLongPress(Vector3 position);

长按
FingerTouchLongPress(Vector3 position, float time);

长按结束
FingerTouchEndLongPress(Vector3 position);

开始拖拽
FingerTouchBeginDrag(Vector3 position);

拖拽
FingerTouchDrag(Vector3 startPosition, Vector3 position, Vector3 deltaPosition);

结束拖拽
FingerTouchDragEnd(Vector3 pisition, Vector3 deltaPosition);

开始双指缩放
FingerTouchBeginPinch(float pinch);

双指缩放
FingerTouchPinch(float pinch);

结束双指缩放
FingerTouchPinchEnd(float pinch);



当手指按下时如果在UI 上，则忽略该手指后续的所有操作，手指弹起后，再重新按下且不在 UI 上时才开始记录

当单指按下、拖拽、长按等 操作，第二指 按下并移动，则开始 双指缩放

可以用来操作摄像机移动拖拽，点击场景内物体、拖拽物体，长按等

测试场景，进入 SampleScene， 主摄像机上挂 GestureController.ca 脚本运行即可




