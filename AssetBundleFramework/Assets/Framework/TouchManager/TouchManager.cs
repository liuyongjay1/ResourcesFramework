using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;

using XLua;

public class TouchMgr:Singleton<TouchMgr>
{
    private TouchBase mTouchBase = null;
    private VirtualScreen mVirtualScreen = null;

    private Action<int, float, Vector2> mCSFunc;
    private int JOYSTICK_INVALID_ID = -100;
    private int mJoystickFingerIndex = -100;
    private bool mPinchFlag = false;
    private bool mSwipeFlag = false;
    private int mSwipeFingerIndex = -1;

    public bool ignoreAllEvents = false;
    public bool enableDebugLog = false;

    //public event Action<Gesture> SimpleTapEvent;
    public event Action<Gesture> TouchStartEvent;
    public event Action<Gesture> DragEvent;
    public event Action<Gesture> SwipeEvent;
    public event Action<Gesture> DoubleTapEvent;


    //public  Action<Gesture> SwipeEvent;
    //public  Action<Gesture> DoubleTapEvent;
    InputManager.KeyCallback FireState;
    public void InitExternal()
    {
        if (mTouchBase != null) return;
        {
            var go = new GameObject("TouchEvent");
            UnityEngine.Object.DontDestroyOnLoad(go);
            mTouchBase = go.AddComponent<TouchBase>();
            mVirtualScreen = go.AddComponent<VirtualScreen>();
            TouchBase.SetUseRealTouch(Application.isMobilePlatform);
        }

        {
            //下面的事件都可监听
            //TouchBase.OnDragStart += OnDragStart;
            //TouchBase.OnDrag += OnDrag;
            //TouchBase.OnDragEnd += OnDragEnd;
            //TouchBase.OnSimpleTap += OnSimpleTap;
            //TouchBase.OnDoubleTap += OnDoubleTap;
            //TouchBase.OnSwipeStart += OnSwipeStart;
            TouchBase.OnSwipe += OnSwipe;
            //TouchBase.OnSwipeEnd += OnSwipeEnd;
            //TouchBase.OnPinchIn += OnPinchIn;
            //TouchBase.OnPinchOut += OnPinchOut;
            //TouchBase.OnPinchEnd += OnPinchEnd;
            TouchBase.OnTouchStart += OnTouchStart;
            //TouchBase.OnTouchDown += OnTouchDown;
            //TouchBase.OnTouchUp += OnTouchUp;
            //TouchBase.OnCancel2Fingers += OnCancelTwoFingers;
        }
    }

    // <param name="type">0按下，1抬起</param>
    
    private void OnDoubleTap(Gesture gesture)
    {
        DoubleTapEvent.Invoke(gesture);
        //OnEvent((int)EventName.On_DoubleTap, gesture);
        //OnEvent(1, gesture);
    }

    private void OnDragStart(Gesture gesture)
    {
        OnEvent((int)EventName.On_DragStart, gesture);
        //OnEvent(1, gesture);
    }

    private void OnDrag(Gesture gesture)
    {
        DragEvent?.Invoke(gesture);
        OnEvent((int)EventName.On_Drag, gesture);
        //OnEvent(1, gesture);
    }

    private void OnDragEnd(Gesture gesture)
    {
        OnEvent((int)EventName.On_DragEnd, gesture);
        //OnEvent(1, gesture);
    }
    public void SetJoystickID(int joystickFingerIndex)
    {
        mJoystickFingerIndex = joystickFingerIndex;
    }

    private void OnCancelTwoFingers(Gesture gesture)
    {
        if (mPinchFlag)
        {
            OnPinchEnd(gesture);
        }
    }

    private void OnPinchIn(Gesture gesture)
    {
        if (enableDebugLog) Debug.LogFormat("TOUCH OnPinchIn {0} {1} {2} {3}", gesture.twoFingerIndexA, gesture.twoFingerIndexB, mJoystickFingerIndex, mPinchFlag);
        //摇杆手指过滤
        if (gesture.twoFingerIndexA == mJoystickFingerIndex) return;
        if (gesture.twoFingerIndexB == mJoystickFingerIndex) return;
        mPinchFlag = true;

        OnEvent((int)EventName.On_PinchIn, gesture);
        //OnEvent(1, gesture);
    }
    private void OnPinchOut(Gesture gesture)
    {
        if (enableDebugLog) Debug.LogFormat("TOUCH OnPinchOut {0} {1} {2} {3}", gesture.twoFingerIndexA, gesture.twoFingerIndexB, mJoystickFingerIndex, mPinchFlag);
        //摇杆手指过滤
        if (gesture.twoFingerIndexA == mJoystickFingerIndex) return;
        if (gesture.twoFingerIndexB == mJoystickFingerIndex) return;
        mPinchFlag = true;
        //OnEvent(2, gesture);
        OnEvent((int)EventName.On_PinchOut, gesture);
    }
    private void OnPinchEnd(Gesture gesture)
    {
        if (enableDebugLog) Debug.LogFormat("TOUCH OnPinchEnd {0} {1} {2} {3}", gesture.twoFingerIndexA, gesture.twoFingerIndexB, mJoystickFingerIndex, mPinchFlag);
        mPinchFlag = false;
        OnEvent((int)EventName.On_PinchEnd, gesture);
        //OnEvent(3, gesture);
    }

    private void OnSwipeStart(Gesture gesture)
    {
        if (enableDebugLog) Debug.LogFormat("TOUCH OnSwipeStart {0} {1} {2} {3} {4}", gesture.fingerIndex, mJoystickFingerIndex, mSwipeFingerIndex, mPinchFlag, mSwipeFlag);
        //摇杆过滤
        if (gesture.fingerIndex == mJoystickFingerIndex) return;
        //缩放与滑动不能同时执行
        if (mPinchFlag || mSwipeFlag) return;
        //修改滑动状态
        mSwipeFlag = true;
        mSwipeFingerIndex = gesture.fingerIndex;
        OnEvent((int)EventName.On_SwipeStart, gesture);
        //OnEvent(4, gesture);
    }
    private void OnSwipe(Gesture gesture)
    {
        if (enableDebugLog) Debug.LogFormat("TOUCH OnSwipe {0} {1} {2} {3} {4}", gesture.fingerIndex, mJoystickFingerIndex, mSwipeFingerIndex, mPinchFlag, mSwipeFlag);
        //摇杆过滤
        if (gesture.fingerIndex == mJoystickFingerIndex) return;
        //缩放与滑动不能同时执行
        //if (mPinchFlag || !mSwipeFlag || gesture.fingerIndex != mSwipeFingerIndex) return;
        //触发滑动事件
        SwipeEvent?.Invoke(gesture);
    }
    private void OnSwipeEnd(Gesture gesture)
    {
        if (enableDebugLog) Debug.LogFormat("TOUCH OnSwipeEnd {0} {1} {2} {3} {4}", gesture.fingerIndex, mJoystickFingerIndex, mSwipeFingerIndex, mPinchFlag, mSwipeFlag);
        if (!mSwipeFlag || gesture.fingerIndex != mSwipeFingerIndex) return;
        //重置滑动状态
        mSwipeFlag = false;
        mSwipeFingerIndex = -1;
      
        OnEvent((int)EventName.On_SwipeEnd, gesture);
        //OnEvent(6, gesture);
    }

    private void OnTouchStart(Gesture gesture) {
        TouchStartEvent?.Invoke(gesture);
        OnEvent((int)EventName.On_TouchStart, gesture); 
    }
    private void OnTouchDown(Gesture gesture) { OnEvent((int)EventName.On_TouchDown, gesture); }
    private void OnTouchUp(Gesture gesture) { OnEvent((int)EventName.On_TouchUp, gesture); }
    private void OnSimpleTap(Gesture gesture) {

        //SimpleTapEvent?.Invoke(gesture);
        OnEvent((int)EventName.On_SimpleTap, gesture); 
    }

    #region 外部接口
    public void SubscribeEvent(int eventId,Action<Gesture> action)
    {
        switch ((EventName)eventId)
        {
            case EventName.On_Swipe:
                SwipeEvent += action;
                break;
            case EventName.On_DoubleTap:
                SwipeEvent += action;
                break;
        }
       

    }
    public void UnsubscribeEvent(int eventId,Action<Gesture> action)
    {
        switch ((EventName)eventId)
        {
            case EventName.On_Swipe:
                SwipeEvent -= action;
                break;
            case EventName.On_DoubleTap:
                SwipeEvent -= action;
                break;
        }
    }

    public void Test(Gesture ges)
    { 
        
    }


    #endregion
    private void OnEvent(int evtID, Gesture ges)
    {
        if (!ignoreAllEvents)
        {
            //OnTouchEvent.Invoke(evtID, ges);
        }
    }
}
