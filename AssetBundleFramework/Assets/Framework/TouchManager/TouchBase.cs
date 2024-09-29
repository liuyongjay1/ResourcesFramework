using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;

public enum EventName
{
    None = 0,
    On_Cancel = 1,
    On_Cancel2Fingers = 2,
    On_TouchStart = 3,
    On_TouchDown = 4,
    On_TouchUp = 5,
    On_SimpleTap = 6,
    On_DoubleTap = 7,
    On_LongTapStart = 8,
    On_LongTap = 9,
    On_LongTapEnd = 10,
    On_DragStart = 11,
    On_Drag = 12,
    On_DragEnd,
    On_SwipeStart,
    On_Swipe,
    On_SwipeEnd,
    On_TouchStart2Fingers,
    On_TouchDown2Fingers,
    On_TouchUp2Fingers,
    On_SimpleTap2Fingers,
    On_DoubleTap2Fingers,
    On_LongTapStart2Fingers,
    On_LongTap2Fingers,
    On_LongTapEnd2Fingers,
    On_Twist,
    On_TwistEnd,
    On_PinchIn,
    On_PinchOut,
    On_PinchEnd,
    On_DragStart2Fingers,
    On_Drag2Fingers,
    On_DragEnd2Fingers,
    On_SwipeStart2Fingers,
    On_Swipe2Fingers,
    On_SwipeEnd2Fingers
}
public class TouchBase : MonoBehaviour
{
    #region Delegate and Events
    public delegate void TouchCancelHandler(Gesture gesture);
    public delegate void Cancel2FingersHandler(Gesture gesture);
    public delegate void TouchStartHandler(Gesture gesture);
    public delegate void TouchDownHandler(Gesture gesture);
    public delegate void TouchUpHandler(Gesture gesture);
    public delegate void SimpleTapHandler(Gesture gesture);
    public delegate void DoubleTapHandler(Gesture gesture);
    public delegate void LongTapStartHandler(Gesture gesture);
    public delegate void LongTapHandler(Gesture gesture);
    public delegate void LongTapEndHandler(Gesture gesture);
    public delegate void DragStartHandler(Gesture gesture);
    public delegate void DragHandler(Gesture gesture);
    public delegate void DragEndHandler(Gesture gesture);
    public delegate void SwipeStartHandler(Gesture gesture);
    public delegate void SwipeHandler(Gesture gesture);
    public delegate void SwipeEndHandler(Gesture gesture);
    public delegate void TouchStart2FingersHandler(Gesture gesture);
    public delegate void TouchDown2FingersHandler(Gesture gesture);
    public delegate void TouchUp2FingersHandler(Gesture gesture);
    public delegate void SimpleTap2FingersHandler(Gesture gesture);
    public delegate void DoubleTap2FingersHandler(Gesture gesture);
    public delegate void LongTapStart2FingersHandler(Gesture gesture);
    public delegate void LongTap2FingersHandler(Gesture gesture);
    public delegate void LongTapEnd2FingersHandler(Gesture gesture);
    public delegate void TwistHandler(Gesture gesture);
    public delegate void TwistEndHandler(Gesture gesture);
    public delegate void PinchInHandler(Gesture gesture);
    public delegate void PinchOutHandler(Gesture gesture);
    public delegate void PinchEndHandler(Gesture gesture);
    public delegate void DragStart2FingersHandler(Gesture gesture);
    public delegate void Drag2FingersHandler(Gesture gesture);
    public delegate void DragEnd2FingersHandler(Gesture gesture);
    public delegate void SwipeStart2FingersHandler(Gesture gesture);
    public delegate void Swipe2FingersHandler(Gesture gesture);
    public delegate void SwipeEnd2FingersHandler(Gesture gesture);

    /// <summary>
    /// Occurs when The system cancelled tracking for the touch, as when (for example) the user puts the device to her face.
    /// </summary>
    public static event TouchCancelHandler OnCancel;
    /// <summary>
    /// Occurs when the touch count is no longer egal to 2 and different to 0, after the begining of a two fingers gesture.
    /// </summary>
    public static event Cancel2FingersHandler OnCancel2Fingers;
    /// <summary>
    /// Occurs when a finger touched the screen.
    /// </summary>
    public static event TouchStartHandler OnTouchStart;
    /// <summary>
    /// Occurs as the touch is active.
    /// </summary>
    public static event TouchDownHandler OnTouchDown;
    /// <summary>
    /// Occurs when a finger was lifted from the screen.
    /// </summary>
    public static event TouchUpHandler OnTouchUp;
    /// <summary>
    /// Occurs when a finger was lifted from the screen, and the time elapsed since the beginning of the touch is less than the time required for the detection of a long tap.
    /// </summary>
    public static event SimpleTapHandler OnSimpleTap;
    /// <summary>
    /// Occurs when the number of taps is egal to 2 in a short time.
    /// </summary>
    public static event DoubleTapHandler OnDoubleTap;
    /// <summary>
    /// Occurs when a finger is touching the screen,  but hasn't moved  since the time required for the detection of a long tap.
    /// </summary>
    public static event LongTapStartHandler OnLongTapStart;
    /// <summary>
    /// Occurs as the touch is active after a LongTapStart
    /// </summary>
    public static event LongTapHandler OnLongTap;
    /// <summary>
    /// Occurs when a finger was lifted from the screen, and the time elapsed since the beginning of the touch is more than the time required for the detection of a long tap.
    /// </summary>
    public static event LongTapEndHandler OnLongTapEnd;
    /// <summary>
    /// Occurs when a drag start. A drag is a swipe on a pickable object
    /// </summary>
    public static event DragStartHandler OnDragStart;
    /// <summary>
    /// Occurs as the drag is active.
    /// </summary>
    public static event DragHandler OnDrag;
    /// <summary>
    /// Occurs when a finger that raise the drag event , is lifted from the screen.
    /// </summary>/
    public static event DragEndHandler OnDragEnd;
    /// <summary>
    /// Occurs when swipe start.
    /// </summary>
    public static event SwipeStartHandler OnSwipeStart;
    /// <summary>
    /// Occurs as the  swipe is active.
    /// </summary>
    public static event SwipeHandler OnSwipe;
    /// <summary>
    /// Occurs when a finger that raise the swipe event , is lifted from the screen.
    /// </summary>
    public static event SwipeEndHandler OnSwipeEnd;
    /// <summary>
    /// Like On_TouchStart but for a 2 fingers gesture.
    /// </summary>
    public static event TouchStart2FingersHandler OnTouchStart2Fingers;
    /// <summary>
    /// Like On_TouchDown but for a 2 fingers gesture.
    /// </summary>
    public static event TouchDown2FingersHandler OnTouchDown2Fingers;
    /// <summary>
    /// Like On_TouchUp but for a 2 fingers gesture.
    /// </summary>
    public static event TouchUp2FingersHandler OnTouchUp2Fingers;
    /// <summary>
    /// Like On_SimpleTap but for a 2 fingers gesture.
    /// </summary>
    public static event SimpleTap2FingersHandler OnSimpleTap2Fingers;
    /// <summary>
    /// Like On_DoubleTap but for a 2 fingers gesture.
    /// </summary>
    public static event DoubleTap2FingersHandler OnDoubleTap2Fingers;
    /// <summary>
    /// Like On_LongTapStart but for a 2 fingers gesture.
    /// </summary>
    public static event LongTapStart2FingersHandler OnLongTapStart2Fingers;
    /// <summary>
    /// Like On_LongTap but for a 2 fingers gesture.
    /// </summary>
    public static event LongTap2FingersHandler OnLongTap2Fingers;
    /// <summary>
    /// Like On_LongTapEnd but for a 2 fingers gesture.
    /// </summary>
    public static event LongTapEnd2FingersHandler OnLongTapEnd2Fingers;
    /// <summary>
    /// Occurs when a twist gesture start
    /// </summary>
    public static event TwistHandler OnTwist;
    /// <summary>
    /// Occurs as the twist gesture is active.
    /// </summary>
    public static event TwistEndHandler OnTwistEnd;
    /// <summary>
    /// Occurs as the twist in gesture is active.
    /// </summary>
    public static event PinchInHandler OnPinchIn;
    /// <summary>
    /// Occurs as the pinch out gesture is active.
    /// </summary>
    public static event PinchOutHandler OnPinchOut;
    /// <summary>
    /// Occurs when the 2 fingers that raise the pinch event , are lifted from the screen.
    /// </summary>
    public static event PinchEndHandler OnPinchEnd;
    /// <summary>
    /// Like On_DragStart but for a 2 fingers gesture.
    /// </summary>
    public static event DragStart2FingersHandler OnDragStart2Fingers;
    /// <summary>
    /// Like On_Drag but for a 2 fingers gesture.
    /// </summary>
    public static event Drag2FingersHandler OnDrag2Fingers;
    /// <summary>
    /// Like On_DragEnd2Fingers but for a 2 fingers gesture.
    /// </summary>
    public static event DragEnd2FingersHandler OnDragEnd2Fingers;
    /// <summary>
    /// Like On_SwipeStart but for a 2 fingers gesture.
    /// </summary>
    public static event SwipeStart2FingersHandler OnSwipeStart2Fingers;
    /// <summary>
    /// Like On_Swipe but for a 2 fingers gesture.
    /// </summary>
    public static event Swipe2FingersHandler OnSwipe2Fingers;
    /// <summary>
    /// Like On_SwipeEnd but for a 2 fingers gesture.
    /// </summary>
    public static event SwipeEnd2FingersHandler OnSwipeEnd2Fingers;
    #endregion

    #region Enumerations
    public enum GestureType { Tap, Drag, Swipe, None, LongTap, Pinch, Twist, Cancel, Acquisition };
    /// <summary>
    /// Represents the different directions for a swipe or drag gesture (Left, Right, Up, Down, Other)
    /// 
    /// The direction is influenced by the swipe Tolerance parameter Look at SetSwipeTolerance( float tolerance)
    /// <br><br>
    /// This enumeration is used on Gesture class
    /// </summary>
    public enum SwipeType { None, Left, Right, Up, Down, Other };


    #endregion

    #region Public members

    //private bool enable = true;				// Enables or disables Easy Touch
    private bool enableRemote = false;          // Enables or disables Unity remote
    private bool useBroadcastMessage = true;    // For javascript developper
    private GameObject receiverObject = null;   // Other object that can receive messages.
    private bool isExtension = false;           // Send message for extension

    private bool enable2FingersGesture = true;  // Enables 2 fingers gesture.
    private bool enableTwist = false;            // Enables or disables recognition of the twist
    private bool enablePinch = true;            // Enables or disables recognition of the Pinch

    private Camera touchCamera;                 // The main camera
    private bool autoSelect = false;            // Enables or disables auto select
    private LayerMask pickableLayers;           // Layer detectable by default

    private float StationnaryTolerance = 25f;   // 
    private float longTapTime = 1000f;             // The time required for the detection of a long tap.
    private float swipeTolerance = 0.85f;       // Determines the accuracy of detecting a drag movement 0 => no precision 1=> high precision.
    private float minPinchLength = 0f;          // The minimum length for a pinch detection.
    private float minTwistAngle = 1f;           // The minimum angle for a twist detection.

    // NGUI
    public bool EnabledNGuiMode
    {
        get { return enabledNGuiMode; }
        set { enabledNGuiMode = value; }
    }


    public LayerMask NGUILayers
    {
        get { return nGUILayers; }
        set { nGUILayers = value; }
    }
    private bool enabledNGuiMode = false;   // True = no events are send when touch is hover an NGui panel
    private LayerMask nGUILayers;
    private List<Camera> nGUICameras = new List<Camera>();
    //private bool isStartHoverUGUI = false;
    private bool isStartHoverUGUI2Fingers = false;

    // Extension (joystick and button)
    private List<Rect> reservedAreas = new List<Rect>();
    private bool enableReservedArea = true;

    // Second Finger
    private KeyCode twistKey = KeyCode.LeftAlt;
    private KeyCode swipeKey = KeyCode.LeftControl;

    // Inspector
    private bool showGeneral = true;
    private bool showSelect = true;
    private bool showGesture = true;
    private bool showTwoFinger = true;
    private bool showSecondFinger = true;
    #endregion

    #region Private members

    private static TouchBase instance;                              // Fake singleton

    private TouchInput input;

    private GestureType complexCurrentGesture = GestureType.None;   // The current gesture 2 fingers
    private GestureType oldGesture = GestureType.None;

    private float startTimeAction;                                  // The time of onset of action.
    private Finger[] fingers = new Finger[10];                      // The informations of the touch for finger 1.
    private List<Finger> tempFingers = new List<Finger>(10);
    private Queue<Finger> fingerPool = new Queue<Finger>();
    private GameObject pickObject2Finger;
    private GameObject oldPickObject2Finger;


    public Texture secondFingerTexture;                             // The texture to display the simulation of the second finger.

    private Vector2 startPosition2Finger;                           // Start position for two fingers gesture
    private int twoFinger0;                                         // finger index
    private int twoFinger1;                                         // finger index
    private Vector2 oldStartPosition2Finger;
    private float oldFingerDistance;
    private bool twoFingerDragStart = false;
    private bool twoFingerSwipeStart = false;
    private int oldTouchCount = 0;


    #endregion

    #region Constructor
    public TouchBase()
    {
        //enable = true;
        useBroadcastMessage = false;
        enable2FingersGesture = true;
        enableTwist = false;
        enablePinch = true;
        autoSelect = false;
        StationnaryTolerance = 25f;
        longTapTime = 1000f;
        swipeTolerance = 0.85f;
        minPinchLength = 0f;
        minTwistAngle = 1f;
    }

    private bool useRealTouch = false;

    public static TouchBase getInstance
    {
        get { return instance; }
    }

    #endregion

    public static void SetUseRealTouch(bool v)
    {
        if (getInstance)
        {
            getInstance.useRealTouch = v;
            if (getInstance.input != null)
                getInstance.input.SetUseRealTouch(v);
        }
    }

    #region MonoBehaviour methods
    void Awake()
    {
        // Assing the fake singleton
        if (TouchBase.instance == null)
            instance = this;
    }

    void OnEnable()
    {
        if (Application.isPlaying && Application.isEditor)
        {
            InitTouch();
        }
    }

    void Start()
    {
        InitTouch();
    }

    // Non comments.
    void Update()
    {
        if (TouchBase.instance == null)
        {
            TouchBase.instance = this;
        }

        if (TouchBase.instance == this)
        {
            int i;

            // 获取手指数量
            int count = input.TouchCount();

            // Reset after two finger gesture;
            if (oldTouchCount == 2 && count != 2 && count > 0)
            {
                CreateGesture2Finger(EventName.On_Cancel2Fingers, Vector2.zero, Vector2.zero, Vector2.zero, 0, SwipeType.None, 0, Vector2.zero, 0, 0, 0);
            }

            UpdateTouches(useRealTouch, count);

            // two fingers gesture
            oldPickObject2Finger = pickObject2Finger;
            if (enable2FingersGesture)
            {
                if (count == 2)
                {
                    TwoFinger();
                }
                else
                {
                    complexCurrentGesture = GestureType.None;
                    pickObject2Finger = null;
                    twoFingerSwipeStart = false;
                    twoFingerDragStart = false;
                }
            }

            // Other fingers gesture
            for (i = 0; i < 10; i++)
            {
                if (fingers[i] != null)
                {
                    OneFinger(i);
                }
            }

            oldTouchCount = count;
        }
    }

    void InitTouch()
    {
        input = new TouchInput();
        input.SetUseRealTouch(useRealTouch);

        // Assing the fake singleton
        if (TouchBase.instance == null)
            instance = this;

        // We search the main camera with the tag MainCamera.
        // For automatic object selection.
        if (touchCamera == null)
        {
            touchCamera = Camera.main;

            if (touchCamera == null && autoSelect)
            {
                Debug.LogWarning("No camera with flag \"MainCam\" was found in the scene, please setup the camera");
            }
        }
    }

    void UpdateTouches(bool realTouch, int touchCount)
    {
        if (realTouch || enableRemote)
        {
            tempFingers.Clear(); tempFingers.AddRange(fingers); Array.Clear(fingers, 0, fingers.Length);
            for (var i = 0; i < touchCount; ++i)
            {
                Touch touch = Input.GetTouch(i);
                //找出上一帧的对应触摸信息
                for (int j = 0; j < tempFingers.Count; j++)
                {
                    if (tempFingers[j] != null && tempFingers[j].fingerIndex == touch.fingerId)
                    {
                        fingers[i] = tempFingers[j];
                        tempFingers.RemoveAt(j);
                        break;
                    }
                }
                //找不到说明上一帧没有触摸事件
                if (fingers[i] == null)
                {
                    fingers[i] = fingerPool.Count > 0 ? fingerPool.Dequeue() : new Finger();
                    fingers[i].fingerIndex = touch.fingerId;
                    fingers[i].gesture = GestureType.None;
                    fingers[i].phase = TouchPhase.Began;
                }
                else
                {
                    fingers[i].phase = touch.phase;
                }

                fingers[i].position = touch.position;
                fingers[i].deltaPosition = touch.deltaPosition;
                fingers[i].tapCount = touch.tapCount;
                fingers[i].deltaTime = touch.deltaTime;

                fingers[i].touchCount = touchCount;
            }
            //清理掉其他无效的触摸信息
            for (int i = touchCount; i < tempFingers.Count; i++)
            {
                if (tempFingers[i] != null)
                {
                    fingerPool.Enqueue(tempFingers[i]);
                }
            }
        }
        else
        {
            int i = 0;
            while (i < touchCount)
            {
                if (fingers[i] == null)
                {
                    fingers[i] = fingerPool.Count > 0 ? fingerPool.Dequeue() : new Finger();
                    fingers[i].gesture = GestureType.None;
                }
                fingers[i] = input.GetMouseTouch(i, fingers[i]) as Finger;
                fingers[i].touchCount = touchCount;
                i++;
            }
            if (touchCount <= 0 && fingers[0] != null)
            {
                //BUG.未检测到对应的事件(编辑模式切换场景时)
                for (i = 0; i < fingers.Length; i++)
                {
                    if (fingers[i] != null)
                    {
                        fingers[i].phase = TouchPhase.Ended;
                    }
                }
            }
        }
    }
    #endregion

    #region One finger Private methods
    private void OneFinger(int fingerIndex)
    {
        //Debug.Log("one finger->" + fingerIndex);
        float timeSinceStartAction = 0;

        // A tap starts ?
        if (fingers[fingerIndex].gesture == GestureType.None)
        {

            startTimeAction = Time.realtimeSinceStartup;

            //fingers[fingerIndex].gesture=GestureType.Tap;
            fingers[fingerIndex].gesture = GestureType.Acquisition;
            fingers[fingerIndex].startPosition = fingers[fingerIndex].position;

            // do we touch a pickable gameobject ?
            if (autoSelect)
                fingers[fingerIndex].pickedObject = GetPickeGameObject(fingers[fingerIndex].startPosition);

            // we notify a touch
            CreateGesture(fingerIndex, EventName.On_TouchStart, fingers[fingerIndex], 0, SwipeType.None, 0, Vector2.zero);
        }

        // Calculates the time since the beginning of the action.
        timeSinceStartAction = Time.realtimeSinceStartup - startTimeAction;

        //Debug.Log("one finger phase->" + fingers[fingerIndex].phase);
        // touch canceled?
        if (fingers[fingerIndex].phase == TouchPhase.Canceled)
        {
            //fingers[fingerIndex].gesture = GestureType.Cancel;
        }

        if (fingers[fingerIndex].phase != TouchPhase.Ended && fingers[fingerIndex].phase != TouchPhase.Canceled)
        {

            // Are we stationary ?
            if (fingers[fingerIndex].phase == TouchPhase.Stationary && timeSinceStartAction >= longTapTime && fingers[fingerIndex].gesture == GestureType.Acquisition)
            {
                fingers[fingerIndex].gesture = GestureType.LongTap;
                CreateGesture(fingerIndex, EventName.On_LongTapStart, fingers[fingerIndex], timeSinceStartAction, SwipeType.None, 0, Vector2.zero);
            }

            // Let's move us?
            if ((fingers[fingerIndex].gesture == GestureType.Acquisition || fingers[fingerIndex].gesture == GestureType.LongTap) && (FingerInTolerance(fingers[fingerIndex]) == false))
            {


                //  long touch => cancel
                if (fingers[fingerIndex].gesture == GestureType.LongTap)
                {
                    fingers[fingerIndex].gesture = GestureType.Cancel;
                    CreateGesture(fingerIndex, EventName.On_LongTapEnd, fingers[fingerIndex], timeSinceStartAction, SwipeType.None, 0, Vector2.zero);
                    // Init the touch to start
                    fingers[fingerIndex].gesture = GestureType.None;

                }
                else
                {
                    // If an object is selected we drag
                    if (fingers[fingerIndex].pickedObject)
                    {
                        fingers[fingerIndex].gesture = GestureType.Drag;
                        CreateGesture(fingerIndex, EventName.On_DragStart, fingers[fingerIndex], timeSinceStartAction, SwipeType.None, 0, Vector2.zero);
                    }
                    // If not swipe
                    else
                    {
                        fingers[fingerIndex].gesture = GestureType.Swipe;
                        CreateGesture(fingerIndex, EventName.On_SwipeStart, fingers[fingerIndex], timeSinceStartAction, SwipeType.None, 0, Vector2.zero);
                    }
                }
            }

            // Gesture update
            EventName message = EventName.None;
            //Debug.Log("one finger gesture->" + fingers[fingerIndex].gesture);
            switch (fingers[fingerIndex].gesture)
            {
                case GestureType.LongTap:
                    message = EventName.On_LongTap;
                    break;
                case GestureType.Drag:
                    message = EventName.On_Drag;
                    break;
                case GestureType.Swipe:
                    message = EventName.On_Swipe;
                    break;
            }

            // Send gesture
            SwipeType currentSwipe = SwipeType.None;
            if (message != EventName.None)
            {
                currentSwipe = GetSwipe(new Vector2(0, 0), fingers[fingerIndex].deltaPosition);
                CreateGesture(fingerIndex, message, fingers[fingerIndex], timeSinceStartAction, currentSwipe, 0, fingers[fingerIndex].deltaPosition);
            }

            // TouchDown
            CreateGesture(fingerIndex, EventName.On_TouchDown, fingers[fingerIndex], timeSinceStartAction, currentSwipe, 0, fingers[fingerIndex].deltaPosition);
        }

        else
        {
            bool realEnd = true;

            // End of the touch		
            switch (fingers[fingerIndex].gesture)
            {
                // tap
                case GestureType.Acquisition:
                    if (FingerInTolerance(fingers[fingerIndex]))
                    {
                        if (fingers[fingerIndex].tapCount < 2)
                        {
                            CreateGesture(fingerIndex, EventName.On_SimpleTap, fingers[fingerIndex], timeSinceStartAction, SwipeType.None, 0, Vector2.zero);
                        }
                        else
                        {
                            CreateGesture(fingerIndex, EventName.On_DoubleTap, fingers[fingerIndex], timeSinceStartAction, SwipeType.None, 0, Vector2.zero);
                        }

                    }
                    else
                    {
                        SwipeType currentSwipe = GetSwipe(new Vector2(0, 0), fingers[fingerIndex].deltaPosition);
                        if (fingers[fingerIndex].pickedObject)
                        {
                            CreateGesture(fingerIndex, EventName.On_DragStart, fingers[fingerIndex], timeSinceStartAction, SwipeType.None, 0, Vector2.zero);
                            CreateGesture(fingerIndex, EventName.On_Drag, fingers[fingerIndex], timeSinceStartAction, currentSwipe, 0, fingers[fingerIndex].deltaPosition);
                            CreateGesture(fingerIndex, EventName.On_DragEnd, fingers[fingerIndex], timeSinceStartAction, GetSwipe(fingers[fingerIndex].startPosition, fingers[fingerIndex].position), (fingers[fingerIndex].startPosition - fingers[fingerIndex].position).magnitude, fingers[fingerIndex].position - fingers[fingerIndex].startPosition);

                        }
                        // If not swipe
                        else
                        {
                            CreateGesture(fingerIndex, EventName.On_SwipeStart, fingers[fingerIndex], timeSinceStartAction, SwipeType.None, 0, Vector2.zero);
                            CreateGesture(fingerIndex, EventName.On_Swipe, fingers[fingerIndex], timeSinceStartAction, currentSwipe, 0, fingers[fingerIndex].deltaPosition);
                            CreateGesture(fingerIndex, EventName.On_SwipeEnd, fingers[fingerIndex], timeSinceStartAction, GetSwipe(fingers[fingerIndex].startPosition, fingers[fingerIndex].position), (fingers[fingerIndex].position - fingers[fingerIndex].startPosition).magnitude, fingers[fingerIndex].position - fingers[fingerIndex].startPosition);

                        }
                    }
                    break;
                // long tap
                case GestureType.LongTap:
                    CreateGesture(fingerIndex, EventName.On_LongTapEnd, fingers[fingerIndex], timeSinceStartAction, SwipeType.None, 0, Vector2.zero);
                    break;
                // drag
                case GestureType.Drag:
                    CreateGesture(fingerIndex, EventName.On_DragEnd, fingers[fingerIndex], timeSinceStartAction, GetSwipe(fingers[fingerIndex].startPosition, fingers[fingerIndex].position), (fingers[fingerIndex].startPosition - fingers[fingerIndex].position).magnitude, fingers[fingerIndex].position - fingers[fingerIndex].startPosition);
                    break;
                // swipe
                case GestureType.Swipe:
                    CreateGesture(fingerIndex, EventName.On_SwipeEnd, fingers[fingerIndex], timeSinceStartAction, GetSwipe(fingers[fingerIndex].startPosition, fingers[fingerIndex].position), (fingers[fingerIndex].position - fingers[fingerIndex].startPosition).magnitude, fingers[fingerIndex].position - fingers[fingerIndex].startPosition);
                    break;
                // cancel
                case GestureType.Cancel:
                    CreateGesture(fingerIndex, EventName.On_Cancel, fingers[fingerIndex], 0, SwipeType.None, 0, Vector2.zero);
                    break;
            }

            if (realEnd)
            {
                CreateGesture(fingerIndex, EventName.On_TouchUp, fingers[fingerIndex], timeSinceStartAction, SwipeType.None, 0, Vector2.zero);
                if (fingers[fingerIndex] != null)
                {
                    fingerPool.Enqueue(fingers[fingerIndex]);
                    fingers[fingerIndex] = null;
                }
            }
        }

    }

    /// <summary>
    /// 检查该手指是否与UGUI互斥，true为互斥
    /// 如果互斥则保存触摸UGUI触摸状态，不互斥UGUI触摸状态设置为false
    /// </summary>
    /// <param name="evt"></param>
    /// <returns></returns>
    public bool EventFilterUGUI(EventName evt)
    {
        switch(evt)
        {
            //case EventName.On_SwipeStart:
            //    return true;
            //case EventName.On_Swipe:
            //    return true;
            //case EventName.On_SwipeEnd:
            //    return true;
            default:
                return false;
        }
    }
    private void CreateGesture(int touchIndex, EventName evt, Finger finger, float actionTime, SwipeType swipe, float swipeLength, Vector2 swipeVector)
    {
        bool isStartHoverUGUI = false;
        //先检查该手指事件是否与UGUI互斥
        if (EventFilterUGUI(evt))
            isStartHoverUGUI = IsTouchHoverUGUI(touchIndex);
        else
            isStartHoverUGUI = false;//不互斥直接通过
        fingers[touchIndex].isTouchHoverUGUI = isStartHoverUGUI;
        //Debug.Log("CreateGesture : " + message);
        if (!isStartHoverUGUI)
        {
            //Creating the structure with the required information
            Gesture gesture = new Gesture();

            gesture.fingerIndex = finger.fingerIndex;
            gesture.twoFingerIndexA = -1;
            gesture.twoFingerIndexB = -1;
            gesture.touchCount = finger.touchCount;
            gesture.startPosition = finger.startPosition;
            gesture.position = finger.position;
            gesture.deltaPosition = finger.deltaPosition;

            gesture.actionTime = actionTime;
            gesture.deltaTime = finger.deltaTime;

            gesture.swipe = swipe;
            gesture.swipeLength = swipeLength;
            gesture.swipeVector = swipeVector;

            gesture.deltaPinch = 0;
            gesture.twistAngle = 0;
            gesture.pickObject = finger.pickedObject;
            gesture.otherReceiver = receiverObject;

            gesture.isHoverReservedArea = IsTouchHoverVirtualControll(touchIndex);


            if (useBroadcastMessage)
            {
                SendGesture(evt, gesture);
            }
            if (!useBroadcastMessage || isExtension)
            {
                RaiseEvent(evt, gesture);
            }
        }

    }
    private void SendGesture(EventName message, Gesture gesture)
    {
        if (useBroadcastMessage)
        {
            // Sent to user GameObject
            if (receiverObject != null)
            {
                if (receiverObject != gesture.pickObject)
                {
                    receiverObject.SendMessage(message.ToString(), gesture, SendMessageOptions.DontRequireReceiver);
                }
            }

            // Sent to the  GameObject who is selected
            if (gesture.pickObject)
            {
                gesture.pickObject.SendMessage(message.ToString(), gesture, SendMessageOptions.DontRequireReceiver);
            }
            // sent to gameobject
            else
            {
                SendMessage(message.ToString(), gesture, SendMessageOptions.DontRequireReceiver);   //函数名 、 参数 、 选项
            }
        }
    }

#endregion

#region Two finger private methods
    private void TwoFinger()
    {

        float timeSinceStartAction = 0;
        bool move = false;
        Vector2 position = Vector2.zero;
        Vector2 deltaPosition = Vector2.zero;
        float fingerDistance = 0;

        // A touch starts
        if (complexCurrentGesture == GestureType.None)
        {
            twoFinger0 = GetTwoFinger(-1);
            twoFinger1 = GetTwoFinger(twoFinger0);

            startTimeAction = Time.realtimeSinceStartup;
            complexCurrentGesture = GestureType.Tap;

            fingers[twoFinger0].complexStartPosition = fingers[twoFinger0].position;
            fingers[twoFinger1].complexStartPosition = fingers[twoFinger1].position;

            fingers[twoFinger0].oldPosition = fingers[twoFinger0].position;
            fingers[twoFinger1].oldPosition = fingers[twoFinger1].position;


            oldFingerDistance = Mathf.Abs(Vector2.Distance(fingers[twoFinger0].position, fingers[twoFinger1].position));
            startPosition2Finger = new Vector2((fingers[twoFinger0].position.x + fingers[twoFinger1].position.x) / 2, (fingers[twoFinger0].position.y + fingers[twoFinger1].position.y) / 2);
            deltaPosition = Vector2.zero;

            // do we touch a pickable gameobject ?
            if (autoSelect)
            {
                pickObject2Finger = GetPickeGameObject(fingers[twoFinger0].complexStartPosition);
                if (pickObject2Finger != GetPickeGameObject(fingers[twoFinger1].complexStartPosition))
                {
                    pickObject2Finger = null;
                }
            }

            // we notify the touch
            CreateGesture2Finger(EventName.On_TouchStart2Fingers, startPosition2Finger, startPosition2Finger, deltaPosition, timeSinceStartAction, SwipeType.None, 0, Vector2.zero, 0, 0, oldFingerDistance);
        }


        // Calculates the time since the beginning of the action.
        timeSinceStartAction = Time.realtimeSinceStartup - startTimeAction;

        // Position & deltaPosition
        position = new Vector2((fingers[twoFinger0].position.x + fingers[twoFinger1].position.x) / 2, (fingers[twoFinger0].position.y + fingers[twoFinger1].position.y) / 2);
        deltaPosition = position - oldStartPosition2Finger;
        fingerDistance = Mathf.Abs(Vector2.Distance(fingers[twoFinger0].position, fingers[twoFinger1].position));

        // Cancel
        if (fingers[twoFinger0].phase == TouchPhase.Canceled || fingers[twoFinger1].phase == TouchPhase.Canceled)
        {
            //complexCurrentGesture = GestureType.Cancel;
        }

        // Let's go
        if (fingers[twoFinger0].phase != TouchPhase.Ended && fingers[twoFinger0].phase != TouchPhase.Canceled &&
            fingers[twoFinger1].phase != TouchPhase.Ended && fingers[twoFinger1].phase != TouchPhase.Canceled)
        {

            // Are we stationary ?
            if (complexCurrentGesture == GestureType.Tap && timeSinceStartAction >= longTapTime && FingerInTolerance(fingers[twoFinger0]) && FingerInTolerance(fingers[twoFinger1]))
            {
                complexCurrentGesture = GestureType.LongTap;
                // we notify the beginning of a longtouch
                CreateGesture2Finger(EventName.On_LongTapStart2Fingers, startPosition2Finger, position, deltaPosition, timeSinceStartAction, SwipeType.None, 0, Vector2.zero, 0, 0, fingerDistance);
            }

            // Let's move us ?
            //if (FingerInTolerance(fingers[twoFinger0])==false ||FingerInTolerance(fingers[twoFinger1])==false){
            move = true;
            //}

            // we move
            if (move)
            {

                float dot = Vector2.Dot(fingers[twoFinger0].deltaPosition.normalized, fingers[twoFinger1].deltaPosition.normalized);

                // Pinch
                if (enablePinch && fingerDistance != oldFingerDistance)
                {
                    // Pinch
                    if (Mathf.Abs(fingerDistance - oldFingerDistance) >= minPinchLength)
                    {
                        complexCurrentGesture = GestureType.Pinch;
                    }

                    // update pinch
                    if (complexCurrentGesture == GestureType.Pinch)
                    {
                        //complexCurrentGesture = GestureType.Acquisition;				
                        if (fingerDistance < oldFingerDistance)
                        {

                            // Send end message
                            if (oldGesture != GestureType.Pinch)
                            {
                                CreateStateEnd2Fingers(oldGesture, startPosition2Finger, position, deltaPosition, timeSinceStartAction, false, fingerDistance);
                                startTimeAction = Time.realtimeSinceStartup;
                            }

                            // Send pinch
                            CreateGesture2Finger(EventName.On_PinchIn, startPosition2Finger, position, deltaPosition, timeSinceStartAction, GetSwipe(fingers[twoFinger0].complexStartPosition, fingers[twoFinger0].position), 0, Vector2.zero, 0, Mathf.Abs(fingerDistance - oldFingerDistance), fingerDistance);
                            complexCurrentGesture = GestureType.Pinch;

                        }
                        else if (fingerDistance > oldFingerDistance)
                        {
                            // Send end message
                            if (oldGesture != GestureType.Pinch)
                            {
                                CreateStateEnd2Fingers(oldGesture, startPosition2Finger, position, deltaPosition, timeSinceStartAction, false, fingerDistance);
                                startTimeAction = Time.realtimeSinceStartup;
                            }

                            // Send pinch
                            CreateGesture2Finger(EventName.On_PinchOut, startPosition2Finger, position, deltaPosition, timeSinceStartAction, GetSwipe(fingers[twoFinger0].complexStartPosition, fingers[twoFinger0].position), 0, Vector2.zero, 0, Mathf.Abs(fingerDistance - oldFingerDistance), fingerDistance);
                            complexCurrentGesture = GestureType.Pinch;
                        }
                    }
                }

                // Twist
                if (enableTwist)
                {

                    if (Mathf.Abs(TwistAngle()) > minTwistAngle)
                    {

                        // Send end message
                        if (complexCurrentGesture != GestureType.Twist)
                        {
                            CreateStateEnd2Fingers(complexCurrentGesture, startPosition2Finger, position, deltaPosition, timeSinceStartAction, false, fingerDistance);
                            startTimeAction = Time.realtimeSinceStartup;
                        }
                        complexCurrentGesture = GestureType.Twist;
                    }

                    // Update Twist
                    if (complexCurrentGesture == GestureType.Twist)
                    {
                        CreateGesture2Finger(EventName.On_Twist, startPosition2Finger, position, deltaPosition, timeSinceStartAction, SwipeType.None, 0, Vector2.zero, TwistAngle(), 0, fingerDistance);
                    }

                    fingers[twoFinger0].oldPosition = fingers[twoFinger0].position;
                    fingers[twoFinger1].oldPosition = fingers[twoFinger1].position;
                }

                // Drag
                if (dot > 0)
                {
                    if (pickObject2Finger && !twoFingerDragStart)
                    {
                        // Send end message
                        if (complexCurrentGesture != GestureType.Tap)
                        {
                            CreateStateEnd2Fingers(complexCurrentGesture, startPosition2Finger, position, deltaPosition, timeSinceStartAction, false, fingerDistance);
                            startTimeAction = Time.realtimeSinceStartup;
                        }
                        //
                        CreateGesture2Finger(EventName.On_DragStart2Fingers, startPosition2Finger, position, deltaPosition, timeSinceStartAction, SwipeType.None, 0, Vector2.zero, 0, 0, fingerDistance);
                        twoFingerDragStart = true;
                    }
                    else if (!pickObject2Finger && !twoFingerSwipeStart)
                    {
                        // Send end message
                        if (complexCurrentGesture != GestureType.Tap)
                        {
                            CreateStateEnd2Fingers(complexCurrentGesture, startPosition2Finger, position, deltaPosition, timeSinceStartAction, false, fingerDistance);
                            startTimeAction = Time.realtimeSinceStartup;
                        }
                        //

                        CreateGesture2Finger(EventName.On_SwipeStart2Fingers, startPosition2Finger, position, deltaPosition, timeSinceStartAction, SwipeType.None, 0, Vector2.zero, 0, 0, fingerDistance);
                        twoFingerSwipeStart = true;
                    }
                }
                else
                {
                    if (dot < 0)
                    {
                        twoFingerDragStart = false;
                        twoFingerSwipeStart = false;
                    }
                }

                //
                if (twoFingerDragStart)
                {
                    CreateGesture2Finger(EventName.On_Drag2Fingers, startPosition2Finger, position, deltaPosition, timeSinceStartAction, GetSwipe(oldStartPosition2Finger, position), 0, deltaPosition, 0, 0, fingerDistance);
                }

                if (twoFingerSwipeStart)
                {
                    CreateGesture2Finger(EventName.On_Swipe2Fingers, startPosition2Finger, position, deltaPosition, timeSinceStartAction, GetSwipe(oldStartPosition2Finger, position), 0, deltaPosition, 0, 0, fingerDistance);
                }

            }
            else
            {
                // Long tap update
                if (complexCurrentGesture == GestureType.LongTap)
                {
                    CreateGesture2Finger(EventName.On_LongTap2Fingers, startPosition2Finger, position, deltaPosition, timeSinceStartAction, SwipeType.None, 0, Vector2.zero, 0, 0, fingerDistance);
                }
            }

            CreateGesture2Finger(EventName.On_TouchDown2Fingers, startPosition2Finger, position, deltaPosition, timeSinceStartAction, GetSwipe(oldStartPosition2Finger, position), 0, deltaPosition, 0, 0, fingerDistance);


            oldFingerDistance = fingerDistance;
            oldStartPosition2Finger = position;
            oldGesture = complexCurrentGesture;
        }
        else
        {
            CreateStateEnd2Fingers(complexCurrentGesture, startPosition2Finger, position, deltaPosition, timeSinceStartAction, true, fingerDistance);
            complexCurrentGesture = GestureType.None;
            pickObject2Finger = null;
            twoFingerSwipeStart = false;
            twoFingerDragStart = false;
        }
    }

    private int GetTwoFinger(int index)
    {

        int i = index + 1;
        bool find = false;

        while (i < 10 && !find)
        {
            if (fingers[i] != null)
            {
                if (i >= index)
                {
                    find = true;
                }
            }
            i++;
        }
        i--;

        return i;
    }

    private void CreateStateEnd2Fingers(GestureType gesture, Vector2 startPosition, Vector2 position, Vector2 deltaPosition, float time, bool realEnd, float fingerDistance)
    {
        switch (gesture)
        {
            // Tap
            case GestureType.Tap:

                if (fingers[twoFinger0].tapCount < 2 && fingers[twoFinger1].tapCount < 2)
                {
                    CreateGesture2Finger(EventName.On_SimpleTap2Fingers, startPosition, position, deltaPosition,
                    time, SwipeType.None, 0, Vector2.zero, 0, 0, fingerDistance);
                }
                else
                {
                    CreateGesture2Finger(EventName.On_DoubleTap2Fingers, startPosition, position, deltaPosition,
                    time, SwipeType.None, 0, Vector2.zero, 0, 0, fingerDistance);
                }
                break;

            // Long tap
            case GestureType.LongTap:
                CreateGesture2Finger(EventName.On_LongTapEnd2Fingers, startPosition, position, deltaPosition,
                time, SwipeType.None, 0, Vector2.zero, 0, 0, fingerDistance);
                break;

            // Pinch 
            case GestureType.Pinch:
                CreateGesture2Finger(EventName.On_PinchEnd, startPosition, position, deltaPosition,
                time, SwipeType.None, 0, Vector2.zero, 0, 0, fingerDistance);
                break;

            // twist
            case GestureType.Twist:
                CreateGesture2Finger(EventName.On_TwistEnd, startPosition, position, deltaPosition,
                time, SwipeType.None, 0, Vector2.zero, 0, 0, fingerDistance);
                break;
        }

        if (realEnd)
        {
            // Drag
            if (twoFingerDragStart)
            {
                CreateGesture2Finger(EventName.On_DragEnd2Fingers, startPosition, position, deltaPosition,
                time, GetSwipe(startPosition, position), (position - startPosition).magnitude, position - startPosition, 0, 0, fingerDistance);
            };

            // Swipe
            if (twoFingerSwipeStart)
            {
                CreateGesture2Finger(EventName.On_SwipeEnd2Fingers, startPosition, position, deltaPosition,
                time, GetSwipe(startPosition, position), (position - startPosition).magnitude, position - startPosition, 0, 0, fingerDistance);
            }

            CreateGesture2Finger(EventName.On_TouchUp2Fingers, startPosition, position, deltaPosition, time, SwipeType.None, 0, Vector2.zero, 0, 0, fingerDistance);
        }
    }

    private void CreateGesture2Finger(EventName message, Vector2 startPosition, Vector2 position, Vector2 deltaPosition, float actionTime, SwipeType swipe, float swipeLength, Vector2 swipeVector, float twist, float pinch, float twoDistance)
    {
        if (message == EventName.On_TouchStart2Fingers)
        {
            isStartHoverUGUI2Fingers = IsTouchHoverUGUI(twoFinger1) & IsTouchHoverUGUI(twoFinger0);
        }

        if (!isStartHoverUGUI2Fingers)
        {
            //Creating the structure with the required information
            Gesture gesture = new Gesture();

            gesture.touchCount = 2;
            gesture.fingerIndex = -1;
            gesture.twoFingerIndexA = fingers[twoFinger0] != null ? fingers[twoFinger0].fingerIndex : -1;
            gesture.twoFingerIndexB = fingers[twoFinger1] != null ? fingers[twoFinger1].fingerIndex : -1;
            gesture.startPosition = startPosition;
            gesture.position = position;
            gesture.deltaPosition = deltaPosition;

            gesture.actionTime = actionTime;

            if (fingers[twoFinger0] != null)
                gesture.deltaTime = fingers[twoFinger0].deltaTime;
            else if (fingers[twoFinger1] != null)
                gesture.deltaTime = fingers[twoFinger1].deltaTime;
            else
                gesture.deltaTime = 0;

            gesture.swipe = swipe;
            gesture.swipeLength = swipeLength;
            gesture.swipeVector = swipeVector;

            gesture.deltaPinch = pinch;
            gesture.twistAngle = twist;
            gesture.twoFingerDistance = twoDistance;


            if (message != EventName.On_Cancel2Fingers)
            {
                gesture.pickObject = pickObject2Finger;
            }
            else
            {
                gesture.pickObject = oldPickObject2Finger;
            }

            gesture.otherReceiver = receiverObject;

            if (useBroadcastMessage)
            {
                SendGesture2Finger(message, gesture);
            }
            else
            {
                RaiseEvent(message, gesture);
            }
        }
    }

    private void SendGesture2Finger(EventName message, Gesture gesture)
    {

        // Sent to user GameObject
        if (receiverObject != null)
        {
            if (receiverObject != gesture.pickObject)
            {
                receiverObject.SendMessage(message.ToString(), gesture, SendMessageOptions.DontRequireReceiver);
            }
        }

        // Sent to the  GameObject who is selected
        if (gesture.pickObject != null)
        {
            gesture.pickObject.SendMessage(message.ToString(), gesture, SendMessageOptions.DontRequireReceiver);
        }
        // sent to gameobject
        else
        {
            SendMessage(message.ToString(), gesture, SendMessageOptions.DontRequireReceiver);
        }
    }
#endregion

#region General private methods
    private void RaiseEvent(EventName evnt, Gesture gesture)
    {
        //Debugger.Log("touch base event->" + evnt);
        switch (evnt)
        {
            case EventName.On_Cancel:
                if (OnCancel != null)
                    OnCancel(gesture);
                break;
            case EventName.On_Cancel2Fingers:
                if (OnCancel2Fingers != null)
                    OnCancel2Fingers(gesture);
                break;
            case EventName.On_TouchStart:
                if (OnTouchStart != null)
                    OnTouchStart(gesture);
                break;
            case EventName.On_TouchDown:
                if (OnTouchDown != null)
                    OnTouchDown(gesture);
                break;
            case EventName.On_TouchUp:
                if (OnTouchUp != null)
                    OnTouchUp(gesture);
                break;
            case EventName.On_SimpleTap:
                if (OnSimpleTap != null)
                    OnSimpleTap(gesture);
                break;
            case EventName.On_DoubleTap:
                if (OnDoubleTap != null)
                    OnDoubleTap(gesture);
                break;
            case EventName.On_LongTapStart:
                if (OnLongTapStart != null)
                    OnLongTapStart(gesture);
                break;
            case EventName.On_LongTap:
                if (OnLongTap != null)
                    OnLongTap(gesture);
                break;
            case EventName.On_LongTapEnd:
                if (OnLongTapEnd != null)
                    OnLongTapEnd(gesture);
                break;
            case EventName.On_DragStart:
                if (OnDragStart != null)
                    OnDragStart(gesture);
                break;
            case EventName.On_Drag:
                if (OnDrag != null)
                    OnDrag(gesture);
                break;
            case EventName.On_DragEnd:
                if (OnDragEnd != null)
                    OnDragEnd(gesture);
                break;
            case EventName.On_SwipeStart:
                if (OnSwipeStart != null)
                    OnSwipeStart(gesture);
                break;
            case EventName.On_Swipe:
                if (OnSwipe != null)
                    OnSwipe(gesture);
                break;
            case EventName.On_SwipeEnd:
                if (OnSwipeEnd != null)
                    OnSwipeEnd(gesture);
                break;
            case EventName.On_TouchStart2Fingers:
                if (OnTouchStart2Fingers != null)
                    OnTouchStart2Fingers(gesture);
                break;
            case EventName.On_TouchDown2Fingers:
                if (OnTouchDown2Fingers != null)
                    OnTouchDown2Fingers(gesture);
                break;
            case EventName.On_TouchUp2Fingers:
                if (OnTouchUp2Fingers != null)
                    OnTouchUp2Fingers(gesture);
                break;
            case EventName.On_SimpleTap2Fingers:
                if (OnSimpleTap2Fingers != null)
                    OnSimpleTap2Fingers(gesture);
                break;
            case EventName.On_DoubleTap2Fingers:
                if (OnDoubleTap2Fingers != null)
                    OnDoubleTap2Fingers(gesture);
                break;
            case EventName.On_LongTapStart2Fingers:
                if (OnLongTapStart2Fingers != null)
                    OnLongTapStart2Fingers(gesture);
                break;
            case EventName.On_LongTap2Fingers:
                if (OnLongTap2Fingers != null)
                    OnLongTap2Fingers(gesture);
                break;
            case EventName.On_LongTapEnd2Fingers:
                if (OnLongTapEnd2Fingers != null)
                    OnLongTapEnd2Fingers(gesture);
                break;
            case EventName.On_Twist:
                if (OnTwist != null)
                    OnTwist(gesture);
                break;
            case EventName.On_TwistEnd:
                if (OnTwistEnd != null)
                    OnTwistEnd(gesture);
                break;
            case EventName.On_PinchIn:
                if (OnPinchIn != null)
                    OnPinchIn(gesture);
                break;
            case EventName.On_PinchOut:
                if (OnPinchOut != null)
                    OnPinchOut(gesture);
                break;
            case EventName.On_PinchEnd:
                if (OnPinchEnd != null)
                    OnPinchEnd(gesture);
                break;
            case EventName.On_DragStart2Fingers:
                if (OnDragStart2Fingers != null)
                    OnDragStart2Fingers(gesture);
                break;
            case EventName.On_Drag2Fingers:
                if (OnDrag2Fingers != null)
                    OnDrag2Fingers(gesture);
                break;
            case EventName.On_DragEnd2Fingers:
                if (OnDragEnd2Fingers != null)
                    OnDragEnd2Fingers(gesture);
                break;
            case EventName.On_SwipeStart2Fingers:
                if (OnSwipeStart2Fingers != null)
                    OnSwipeStart2Fingers(gesture);
                break;
            case EventName.On_Swipe2Fingers:
                if (OnSwipe2Fingers != null)
                    OnSwipe2Fingers(gesture);
                break;
            case EventName.On_SwipeEnd2Fingers:
                if (OnSwipeEnd2Fingers != null)
                    OnSwipeEnd2Fingers(gesture);
                break;
        }
    }

    private GameObject GetPickeGameObject(Vector2 screenPos)
    {

        if (touchCamera != null)
        {
            Ray ray = touchCamera.ScreenPointToRay(screenPos);
            RaycastHit hit;

            LayerMask mask = pickableLayers;

            if (Physics.Raycast(ray, out hit, float.MaxValue, mask))
            {
                return hit.collider.gameObject;
            }
        }
        else
        {
            Debug.LogWarning("No camera is assigned to EasyTouch");
        }

        return null;

    }

    private SwipeType GetSwipe(Vector2 start, Vector2 end)
    {

        Vector2 linear;
        linear = (end - start).normalized;

        if (Mathf.Abs(linear.y) > Mathf.Abs(linear.x))
        {
            if (Vector2.Dot(linear, Vector2.up) >= swipeTolerance)
                return SwipeType.Up;

            if (Vector2.Dot(linear, -Vector2.up) >= swipeTolerance)
                return SwipeType.Down;
        }
        else
        {
            if (Vector2.Dot(linear, Vector2.right) >= swipeTolerance)
                return SwipeType.Right;

            if (Vector2.Dot(linear, -Vector2.right) >= swipeTolerance)
                return SwipeType.Left;
        }

        return SwipeType.Other;
    }

    private bool FingerInTolerance(Finger finger)
    {

        if ((finger.position - finger.startPosition).sqrMagnitude <= (StationnaryTolerance * StationnaryTolerance))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private float DeltaAngle(Vector2 start, Vector2 end)
    {

        var tmp = (start.x * end.y) - (start.y * end.x);
        return Mathf.Atan2(tmp, Vector2.Dot(start, end));

    }

    private float TwistAngle()
    {

        Vector2 dir = (fingers[twoFinger0].position - fingers[twoFinger1].position);
        Vector2 refDir = (fingers[twoFinger0].oldPosition - fingers[twoFinger1].oldPosition);
        float angle = Mathf.Rad2Deg * DeltaAngle(refDir, dir);

        return angle;
    }

    //点击UI检测
    public bool IsTouchHoverUGUI(int touchIndex)
    {
#if UNITY_EDITOR
        //PC只能用该接口,IsPointerOverGameObject(touchIndex)识别不到
        return EventSystem.current.IsPointerOverGameObject();
#else
        return EventSystem.current.IsPointerOverGameObject(touchIndex);
#endif
    }

    private bool IsTouchHoverVirtualControll(int touchIndex)
    {

        bool returnValue = false;

        if (enableReservedArea)
        {
            int i = 0;

            while (!returnValue && i < reservedAreas.Count)
            {
                Rect rectTest = VirtualScreen.GetRealRect(reservedAreas[i]);
                rectTest = new Rect(rectTest.x, Screen.height - rectTest.y - rectTest.height, rectTest.width, rectTest.height);
                returnValue = rectTest.Contains(fingers[touchIndex].position);
                i++;
            }
        }

        return returnValue;
    }

    private Finger GetFinger(int finderId)
    {

        Finger finger = null;

        foreach (Finger fg in fingers)
        {
            if (fg.fingerIndex == finderId)
            {
                finger = fg;
            }
        }

        //int t = 0;
        //while (t < 10 && finger == null)
        //{
        //    if (fingers[t] != null)
        //    {
        //        if (fingers[t].fingerIndex == finderId)
        //        {
        //            finger = fingers[t];
        //        }
        //    }
        //    t++;
        //}

        return finger;
    }
#endregion

#region public static methods
    /// <summary>
    /// Return the current touches count.
    /// </summary>
    /// <returns>
    /// int
    /// </returns>
    public static int GetTouchCount()
    {
        return TouchBase.instance.input.TouchCount();
    }

    /// <summary>
    /// Sets the camera uses by EasyTouch to linePick for auto-selection.
    /// </summary>
    /// <param name='cam'>
    /// The camera
    /// </param>
    public static void SetCamera(Camera cam)
    {
        TouchBase.instance.touchCamera = cam;
    }

    /// <summary>
    /// Return the camera used by EasyTouch for the auto-selection.
    /// </summary>
    /// <returns>
    /// The camera
    /// </returns
    /// >
    public static Camera GetCamera()
    {
        return TouchBase.instance.touchCamera;
    }

    /// <summary>
    /// Enables or disables the recognize of 2 fingers gesture.
    /// </summary>
    /// <param name='enable'>
    /// true = enabled<br>
    /// false = disabled
    /// </param>
    public static void SetEnable2FingersGesture(bool enable)
    {
        TouchBase.instance.enable2FingersGesture = enable;
    }

    /// <summary>
    /// Return if 2 fingers gesture is enabled or disabled
    /// </summary>
    /// <returns>
    /// true = enabled<br>
    /// false = disabled
    /// </returns>
    public static bool GetEnable2FingersGesture()
    {
        return TouchBase.instance.enable2FingersGesture;
    }

    /// <summary>
    /// Enables or disables the recognize of twist gesture
    /// </summary>
    /// <param name='enable'>
    /// true = enabled<br>
    /// false = disabled
    /// </param>
    public static void SetEnableTwist(bool enable)
    {
        TouchBase.instance.enableTwist = enable;
    }

    /// <summary>
    /// Return if 2 twist gesture is enabled or disabled
    /// </summary>
    /// <returns>
    /// true = enabled
    /// false = disables
    /// </returns>
    public static bool GetEnableTwist()
    {
        return TouchBase.instance.enableTwist;
    }

    /// <summary>
    /// Enables or disables the recognize of pinch gesture
    /// </summary>
    /// <param name='enable'>
    /// true = enabled
    /// false = disables
    /// </param>
    public static void SetEnablePinch(bool enable)
    {
        TouchBase.instance.enablePinch = enable;
    }

    /// <summary>
    /// Return if 2 pinch gesture is enabled or disabled
    /// </summary>
    /// <returns>
    /// true = enabled
    /// false = disables
    /// </returns>
    public static bool GetEnablePinch()
    {
        return TouchBase.instance.enablePinch;
    }

    /// <summary>
    /// Enables or disables auto select.
    /// </summary>
    /// <param name='enable'>
    /// true = enabled
    /// false = disables
    /// </param>
    public static void SetEnableAutoSelect(bool enable)
    {
        TouchBase.instance.autoSelect = enable;
    }

    /// <summary>
    /// Return if auto select is enabled or disabled
    /// </summary>
    /// <returns>
    /// true = enabled
    /// false = disables
    /// </returns>
    public static bool GetEnableAutoSelect()
    {
        return TouchBase.instance.autoSelect;
    }

    /// <summary>
    /// Sets the other receiver for EasyTouch event.
    /// </summary>
    /// <param name='receiver'>
    /// GameObject.
    /// </param>
    public static void SetOtherReceiverObject(GameObject receiver)
    {
        TouchBase.instance.receiverObject = receiver;
    }

    /// <summary>
    /// Return the other event receiver.
    /// </summary>
    /// <returns>
    /// GameObject
    /// </returns>
    public static GameObject GetOtherReceiverObject()
    {
        return TouchBase.instance.receiverObject;
    }

    /// <summary>
    /// Sets the stationnary tolerance.
    /// </summary>
    /// <param name='tolerance'>
    /// float Tolerance.
    /// </param>
    public static void SetStationnaryTolerance(float tolerance)
    {
        TouchBase.instance.StationnaryTolerance = tolerance;
    }

    /// <summary>
    /// Return the stationnary tolerance.
    /// </summary>
    /// <returns>
    /// Float
    /// </returns>
    public static float GetStationnaryTolerance()
    {
        return TouchBase.instance.StationnaryTolerance;
    }

    /// <summary>
    /// Set the long tap time in second
    /// </summary>
    /// <param name='time'>
    /// Float
    /// </param>
    public static void SetlongTapTime(float time)
    {
        TouchBase.instance.longTapTime = time;
    }

    /// <summary>
    ///  Return the longs the tap time.
    /// </summary>
    /// <returns>
    /// Float.
    /// </returns>
    public static float GetlongTapTime()
    {
        return TouchBase.instance.longTapTime;
    }

    /// <summary>
    /// Sets the swipe tolerance.
    /// </summary>
    /// <param name='tolerance'>
    /// Float
    /// </param>
    public static void SetSwipeTolerance(float tolerance)
    {
        TouchBase.instance.swipeTolerance = tolerance;
    }

    /// <summary>
    /// Return the swipe tolerance.
    /// </summary>
    /// <returns>
    /// Float.
    /// </returns>
    public static float GetSwipeTolerance()
    {
        return TouchBase.instance.swipeTolerance;
    }

    /// <summary>
    /// Sets the minimum length of the pinch.
    /// </summary>
    /// <param name='length'>
    /// Float.
    /// </param>
    public static void SetMinPinchLength(float length)
    {
        TouchBase.instance.minPinchLength = length;
    }

    /// <summary>
    /// Return the minimum length of the pinch.
    /// </summary>
    /// <returns>
    /// Float
    /// </returns>
    public static float GetMinPinchLength()
    {
        return TouchBase.instance.minPinchLength;
    }

    /// <summary>
    /// Sets the minimum twist angle.
    /// </summary>
    /// <param name='angle'>
    /// Float
    /// </param>
    public static void SetMinTwistAngle(float angle)
    {
        TouchBase.instance.minTwistAngle = angle;
    }

    /// <summary>
    /// Gets the minimum twist angle.
    /// </summary>
    /// <returns>
    /// Float
    /// </returns>
    public static float GetMinTwistAngle()
    {
        return TouchBase.instance.minTwistAngle;
    }

    /// <summary>
    /// Gets the current picked object under a specific touch
    /// </summary>
    /// <returns>
    /// The current picked object.
    /// </returns>
    /// <param name='fingerIndex'>
    /// Finger index.
    /// </param>
    public static GameObject GetCurrentPickedObject(int fingerIndex)
    {
        return TouchBase.instance.GetPickeGameObject(TouchBase.instance.GetFinger(fingerIndex).position);
    }

    /// <summary>
    /// Determines if a touch is under a specified rect guiRect.
    /// </summary>
    /// <returns>
    /// <c>true</c> True; otherwise, <c>false</c>.
    /// </returns>
    /// <param name='rect'>
    /// The Rect <c>true</c> rect.
    /// </param>
    /// <param name='guiRect'>
    /// Determines if the rect is on GUI coordinate
    /// </param>
    public static bool IsRectUnderTouch(Rect rect, bool guiRect = false)
    {

        bool find = false;

        for (int i = 0; i < 10; i++)
        {

            if (TouchBase.instance.fingers[i] != null)
            {
                if (guiRect)
                {
                    rect = new Rect(rect.x, Screen.height - rect.y - rect.height, rect.width, rect.height);
                }
                find = rect.Contains(TouchBase.instance.fingers[i].position);
                if (find)
                    break;

            }
        }

        return find;
    }

    /// <summary>
    /// Gets the a specific finger position.
    /// </summary>
    /// <returns>
    /// The finger position.
    /// </returns>
    /// <param name='fingerIndex'>
    /// Finger index.
    /// </param>
    public static Vector2 GetFingerPosition(int fingerIndex)
    {

        if (TouchBase.instance.fingers[fingerIndex] != null)
        {
            return TouchBase.instance.GetFinger(fingerIndex).position;
        }
        else
        {
            return Vector2.zero;
        }
    }


    /// <summary>
    /// Return if Reserved Area is enable or disable
    /// </summary>
    /// <returns>
    /// true = enable
    /// false = disable
    /// </returns>
    public static bool GetIsReservedArea()
    {
        return TouchBase.instance.enableReservedArea;
    }

    /// <summary>
    /// Sets if Reserved Area is enable or disable
    /// </summary>
    /// <param name='enable'>
    /// Enable.
    /// </param>
    public static void SetIsReservedArea(bool enable)
    {
        TouchBase.instance.enableReservedArea = enable;
    }

    /// <summary>
    /// Adds a reserved area.
    /// </summary>
    /// <param name='rec'>
    /// Rec.
    /// </param>
    public static void AddReservedArea(Rect rec)
    {
        if (TouchBase.instance)
            TouchBase.instance.reservedAreas.Add(rec);
    }

    /// <summary>
    /// Removes a reserved area.
    /// </summary>
    /// <param name='rec'>
    /// Rec.
    /// </param>
    public static void RemoveReservedArea(Rect rec)
    {
        if (TouchBase.instance)
            TouchBase.instance.reservedAreas.Remove(rec);
    }

    /// <summary>
    /// Resets a specific touch.
    /// </summary>
    /// <param name='fingerIndex'>
    /// Finger index.
    /// </param>
    public static void ResetTouch(int fingerIndex)
    {
        if (TouchBase.instance)
            TouchBase.instance.GetFinger(fingerIndex).gesture = GestureType.None;
    }
#endregion

#region class Finger
    public class Finger
    {
        public int fingerIndex;             // 手指索引
        public int touchCount;              // 触摸数量
        public Vector2 startPosition;       // 起始点
        public Vector2 complexStartPosition;// 双点起始点
        public Vector2 position;            // 点击当前位置
        public Vector2 deltaPosition;       // 最后改变位置的增量. 
        public Vector2 oldPosition;         // 上一位置
        public int tapCount;                // tap数量
        public float deltaTime;             // 上次改变的时间戳数量
        public TouchPhase phase;            // 点击的描述
        public TouchBase.GestureType gesture; //gesture类型
        public GameObject pickedObject;     //触摸的对象
        public bool isTouchHoverUGUI;
    }

#endregion

#region class TouchInput
    public class TouchInput
    {
#region private members

        private Vector2[] oldMousePosition = new Vector2[2];
        private int[] tapCount = new int[2];
        private float[] startActionTime = new float[2];
        private float[] deltaTime = new float[2];
        private float[] tapeTime = new float[2];

        // Complexe 2 fingers simulation
        private bool bComplex = false;
        private Vector2 deltaFingerPosition;
        private Vector2 oldFinger2Position;
        private Vector2 complexCenter;

        private bool useRealTouch = false;

#endregion

        public void SetUseRealTouch(bool v)
        {
            useRealTouch = v;
        }

#region Public methods
        // Return the number of touch
        public int TouchCount()
        {
            return getTouchCount(useRealTouch);
        }

        private int getTouchCount(bool realTouch)
        {

            int count = 0;

            if (realTouch || TouchBase.instance.enableRemote)
            {
                count = Input.touchCount;
            }
            else
            {
                if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
                {
                    count = 1;
                    if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(TouchBase.instance.twistKey) || Input.GetKey(KeyCode.LeftControl) || Input.GetKey(TouchBase.instance.swipeKey))
                        count = 2;
                    if (Input.GetKeyUp(KeyCode.LeftAlt) || Input.GetKeyUp(TouchBase.instance.twistKey) || Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(TouchBase.instance.swipeKey))
                        count = 2;
                }
            }

            return count;
        }

        // return in Finger structure all informations on an touch
        public Finger GetMouseTouch(int fingerIndex, Finger myFinger)
        {

            Finger finger;

            if (myFinger != null)
            {
                finger = myFinger;
            }
            else
            {
                finger = new Finger();
                finger.gesture = TouchBase.GestureType.None;
            }


            if (fingerIndex == 1 && (Input.GetKeyUp(KeyCode.LeftAlt) || Input.GetKeyUp(TouchBase.instance.twistKey) || Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(TouchBase.instance.swipeKey)))
            {

                finger.fingerIndex = fingerIndex;
                finger.position = oldFinger2Position;
                finger.deltaPosition = finger.position - oldFinger2Position;
                finger.tapCount = tapCount[fingerIndex];
                finger.deltaTime = Time.realtimeSinceStartup - deltaTime[fingerIndex];
                finger.phase = TouchPhase.Ended;

                return finger;
            }

            if (Input.GetMouseButton(0))
            {

                finger.fingerIndex = fingerIndex;
                finger.position = GetPointerPosition(fingerIndex);

                if (Time.realtimeSinceStartup - tapeTime[fingerIndex] > 0.5)
                {
                    tapCount[fingerIndex] = 0;
                }

                if (Input.GetMouseButtonDown(0) || (fingerIndex == 1 && (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(TouchBase.instance.twistKey) || Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(TouchBase.instance.swipeKey))))
                {

                    // Began						
                    finger.position = GetPointerPosition(fingerIndex);
                    finger.deltaPosition = Vector2.zero;
                    tapCount[fingerIndex] = tapCount[fingerIndex] + 1;
                    finger.tapCount = tapCount[fingerIndex];
                    startActionTime[fingerIndex] = Time.realtimeSinceStartup;
                    deltaTime[fingerIndex] = startActionTime[fingerIndex];
                    finger.deltaTime = 0;
                    finger.phase = TouchPhase.Began;


                    if (fingerIndex == 1)
                    {
                        oldFinger2Position = finger.position;
                    }
                    else
                    {
                        oldMousePosition[fingerIndex] = finger.position;
                    }

                    if (tapCount[fingerIndex] == 1)
                    {
                        tapeTime[fingerIndex] = Time.realtimeSinceStartup;
                    }


                    return finger;
                }


                finger.deltaPosition = finger.position - oldMousePosition[fingerIndex];

                finger.tapCount = tapCount[fingerIndex];
                finger.deltaTime = Time.realtimeSinceStartup - deltaTime[fingerIndex];
                if (finger.deltaPosition.sqrMagnitude < 1)
                {
                    finger.phase = TouchPhase.Stationary;
                }
                else
                {
                    finger.phase = TouchPhase.Moved;
                }

                oldMousePosition[fingerIndex] = finger.position;
                deltaTime[fingerIndex] = Time.realtimeSinceStartup;

                return finger;
            }

            else if (Input.GetMouseButtonUp(0))
            {
                finger.fingerIndex = fingerIndex;
                finger.position = GetPointerPosition(fingerIndex);
                finger.deltaPosition = finger.position - oldMousePosition[fingerIndex];
                finger.tapCount = tapCount[fingerIndex];
                finger.deltaTime = Time.realtimeSinceStartup - deltaTime[fingerIndex];
                finger.phase = TouchPhase.Ended;
                oldMousePosition[fingerIndex] = finger.position;

                return finger;
            }

            return null;
        }

        // Get the position of the simulate second finger
        public Vector2 GetSecondFingerPosition()
        {

            Vector2 pos = new Vector2(-1, -1);

            if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(TouchBase.instance.twistKey)) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(TouchBase.instance.swipeKey)))
            {
                if (!bComplex)
                {
                    bComplex = true;
                    deltaFingerPosition = (Vector2)Input.mousePosition - oldFinger2Position;
                }
                pos = GetComplex2finger();
                return pos;
            }
            else if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(TouchBase.instance.twistKey))
            {
                pos = GetPinchTwist2Finger();
                bComplex = false;
                return pos;
            }
            else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(TouchBase.instance.swipeKey))
            {

                pos = GetComplex2finger();
                bComplex = false;
                return pos;
            }

            return pos;
        }

#endregion

#region Private methods
        // Get the postion of simulate finger
        private Vector2 GetPointerPosition(int index)
        {

            Vector2 pos;

            if (index == 0)
            {
                pos = Input.mousePosition;
                return pos;
            }
            else
            {
                return GetSecondFingerPosition();

            }
        }

        // Simulate for a twist or pinc
        private Vector2 GetPinchTwist2Finger()
        {

            Vector2 position;

            if (complexCenter == Vector2.zero)
            {
                position.x = (Screen.width / 2.0f) - (Input.mousePosition.x - (Screen.width / 2.0f));
                position.y = (Screen.height / 2.0f) - (Input.mousePosition.y - (Screen.height / 2.0f));
            }
            else
            {
                position.x = (complexCenter.x) - (Input.mousePosition.x - (complexCenter.x));
                position.y = (complexCenter.y) - (Input.mousePosition.y - (complexCenter.y));
            }
            oldFinger2Position = position;

            return position;
        }

        // complexe Alt + Ctr
        private Vector2 GetComplex2finger()
        {

            Vector2 position;

            position.x = Input.mousePosition.x - deltaFingerPosition.x;
            position.y = Input.mousePosition.y - deltaFingerPosition.y;

            complexCenter = new Vector2((Input.mousePosition.x + position.x) / 2f, (Input.mousePosition.y + position.y) / 2f);
            oldFinger2Position = position;

            return position;
        }
#endregion
    }

#endregion

}
