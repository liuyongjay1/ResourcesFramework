using UnityEngine;
using System.Collections;


    public class VirtualScreen : MonoBehaviour
    {
        #region Delegate Event

        public delegate void On_ScreenResizeHandler();

        public static event On_ScreenResizeHandler On_ScreenResize;

        #endregion

        public enum ScreenResolution { IPhoneTall, IPhoneWide, IPhone4GTall, IPhone4GWide, IPadTall, IPadWide, };

        #region Members

        public static VirtualScreen instance = null;

        public float VirtualWidth { get { return virtualWidth; } set { virtualWidth = value; } }
        public float VirtualHeight { get { return virtualHeight; } set { virtualHeight = value; } }

        private float virtualWidth = 1024;
        private float virtualHeight = 768;

        public static float width = 1024;
        public static float height = 768;

        public static float xRatio = 1;
        public static float yRatio = 1;

        private float realWidth;
        private float realHeight;
        private float oldRealWidth;
        private float oldRealHeight;

        #endregion

        #region Monobehaviors
        void Awake()
        {
            instance = this;
            realWidth = oldRealWidth = Screen.width;
            realHeight = oldRealHeight = Screen.height;
            ComputeScreen();
        }

        void Update()
        {
            realWidth = Screen.width;
            realHeight = Screen.height;

            if (realWidth != oldRealWidth || realHeight != oldRealHeight)
            {
                ComputeScreen();
                if (On_ScreenResize != null)
                {
                    On_ScreenResize();
                }
            }

            oldRealWidth = realWidth;
            oldRealHeight = realHeight;
        }
        #endregion

        /// <summary>
        /// Computes the size of the virtual screen resolution depending on the real screen resolution
        /// </summary>
        public void ComputeScreen()
        {

            width = virtualWidth;
            height = virtualHeight;
            xRatio = 1;
            yRatio = 1;

            float realRatio = 0;
            float tmpLength = 0;
            if (Screen.width > Screen.height)
            {
                realRatio = (float)((float)Screen.width / (float)Screen.height);
                tmpLength = width;
            }
            else
            {
                realRatio = (float)((float)Screen.height / (float)Screen.width);
                tmpLength = height;
            }


            float tmpOtherLength = 0;
            tmpOtherLength = tmpLength / realRatio;


            if (Screen.width > Screen.height)
            {
                height = tmpOtherLength;
                xRatio = (float)Screen.width / width;
                yRatio = (float)Screen.height / height;
            }
            else
            {
                width = tmpOtherLength;
                xRatio = (float)Screen.width / width;
                yRatio = (float)Screen.height / height;
            }

        }

        /// <summary>
        /// Computes the virtual screen.
        /// </summary>
        public static void ComputeVirtualScreen()
        {
            VirtualScreen.instance.ComputeScreen();
        }

        /// <summary>
        /// Sets the GUI scale matrix.
        /// </summary>
        public static void SetGuiScaleMatrix()
        {
            GUI.matrix = Matrix4x4.Scale(new Vector3(xRatio, yRatio, 1f));
        }

        /// <summary>
        /// Gets the real rect.
        /// </summary>
        /// <returns>
        /// The real rect.
        /// </returns>
        /// <param name='rect'>
        /// Rect.
        /// </param>
        public static Rect GetRealRect(Rect rect)
        {
            return new Rect(rect.x * xRatio, rect.y * yRatio, rect.width * xRatio, rect.height * yRatio);
        }
    }
