using UnityEngine;
using System.Collections;


    public struct Gesture
    {
        public int fingerIndex;

        public int twoFingerIndexA;

        public int twoFingerIndexB;

        public int touchCount;

        public Vector2 startPosition;

        public Vector2 position;

        public Vector2 deltaPosition;

        public float actionTime;

        public float deltaTime;

        public TouchBase.SwipeType swipe;

        public float swipeLength;

        public Vector2 swipeVector;

        public float deltaPinch;

        public float twistAngle;

        public float twoFingerDistance;

        public GameObject pickObject;

        public GameObject otherReceiver;

        public bool isHoverReservedArea;

        public Vector3 GetTouchToWorldPoint(float z, bool worldZ = false)
        {
            if (!worldZ)
            {
                return TouchBase.GetCamera().ScreenToWorldPoint(new Vector3(position.x, position.y, z));
            }
            else
            {
                return TouchBase.GetCamera().ScreenToWorldPoint(new Vector3(position.x, position.y, z - TouchBase.GetCamera().transform.position.z));
            }
        }

        public float GetSwipeOrDragAngle()
        {
            return Mathf.Atan2(swipeVector.normalized.y, swipeVector.normalized.x) * Mathf.Rad2Deg;
        }

        public bool IsInRect(Rect rect, bool guiRect = false)
        {
            if (guiRect)
            {
                rect = new Rect(rect.x, Screen.height - rect.y - rect.height, rect.width, rect.height);
            }

            return rect.Contains(position);
        }

        public Vector2 NormalizedPosition()
        {
            return new Vector2(100f / Screen.width * position.x / 100f, 100f / Screen.height * position.y / 100f);
        }

        public float GetDeltaPositionX()
        {
            return deltaPosition.x;
        }

        public float GetDeltaPositionY()
        {
            return deltaPosition.y;
        }
    }
