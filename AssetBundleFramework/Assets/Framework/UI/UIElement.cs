using System.Collections.Generic;
using UnityEngine;
using XLua;
#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
    [CustomEditor(typeof(UIElement))]
    public class UIElementInspector : Editor
    {
        private UIElement mUIElement;
        private static List<string> mComponentNames = new List<string>();
        private static List<Component> mComponents = new List<Component>();
        private static Dictionary<string, int> mElementNames = new Dictionary<string, int>();
        private void OnEnable()
        {
            mUIElement = target as UIElement;
        }

        public void GetComponents(UnityEngine.Object component, List<Component> components)
        {
            if (components == null || component == null) return;
            var componentGO = component is GameObject ? component as GameObject : (component as Component).gameObject;
            componentGO.GetComponents(components);
        }

        public override void OnInspectorGUI()
        {
            //检查每个名字的数量
            mElementNames.Clear();
            for (int i = 0; i < mUIElement.mElements.Count; i++)
            {
                UIElement.ElementData elementData = mUIElement.mElements[i];
                if (elementData.component != null && !string.IsNullOrEmpty(elementData.componentName))
                {
                    if (!mElementNames.ContainsKey(elementData.componentName)) mElementNames[elementData.componentName] = 0;
                    mElementNames[elementData.componentName] += 1;
                }
            }
            EditorGUILayout.Space();

            //是否为数组
            EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginChangeCheck();
            mUIElement.mIsArray = EditorGUILayout.Toggle(mUIElement.mIsArray, GUILayout.Width(15));
            EditorGUILayout.LabelField("是否以数组形式导出");
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            for (int i = 0; i < mUIElement.mElements.Count; i++)
            {
                UIElement.ElementData elementData = mUIElement.mElements[i];
                EditorGUILayout.BeginHorizontal();
                //移除当前组件
                if (GUILayout.Button("X", GUILayout.Width(30)))
                {
                    mUIElement.mElements.RemoveAt(i--);
                }

                var oldComponent = elementData.component;
                elementData.component = EditorGUILayout.ObjectField(elementData.component, typeof(UnityEngine.Object), true, GUILayout.Width(130));
                if (elementData.component != null)
                {
                    //清理并获取所有组件
                    mComponents.Clear(); mComponentNames.Clear(); GetComponents(elementData.component, mComponents); mComponents.Insert(0, null);
                    //获取所有组件类型名字
                    int oldSelectIndex = 0;
                    for (int j = 0; j < mComponents.Count; j++)
                    {
                        var component = mComponents[j];
                        if (component != null)
                        {
                            mComponentNames.Add(component.GetType().Name);
                            if (elementData.component.GetType() == component.GetType())
                            {
                                oldSelectIndex = j;
                            }
                        }
                        else
                        {
                            mComponentNames.Add("GameObject");
                        }
                    }
                    //检查类型是否发生变化
                    var newSelectIndex = EditorGUILayout.Popup(oldSelectIndex, mComponentNames.ToArray(), GUILayout.Width(80));
                    if (newSelectIndex != oldSelectIndex || oldComponent != elementData.component || string.IsNullOrEmpty(elementData.componentName))
                    {
                        elementData.component = newSelectIndex == 0 ? (UnityEngine.Object)mComponents[1].gameObject : mComponents[newSelectIndex];
                        elementData.componentName = string.Format("{0}{1}", elementData.component.name.ToLower(), elementData.component.GetType().Name);
                    }
                    //显示组件名称
                    var hasEqualName = mElementNames.ContainsKey(elementData.componentName) && mElementNames[elementData.componentName] >= 2;
                    var labelStyle = new GUIStyle(EditorStyles.label); if (hasEqualName) labelStyle.normal.textColor = Color.red;
                    elementData.componentName = EditorGUILayout.TextField(elementData.componentName, labelStyle, GUILayout.Height(20));
                }
                EditorGUILayout.EndHorizontal();

            }
            EditorGUILayout.Space();

            if (GUILayout.Button("添加字段", GUILayout.Height(50)))
            {
                mUIElement.mElements.Add(new UIElement.ElementData());
            }

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(mUIElement);
            }
        }
    }
#endif
    [System.Serializable]
    public class UIElement : MonoBehaviour
    {
        [System.Serializable]
        public class ElementData
        {
            public Object component;
            public string componentName;
        }

        [SerializeField] public bool mIsArray = false;
        [SerializeField] public int mApplyInstanceID = -1;
        [SerializeField] public List<ElementData> mElements = new List<ElementData>();

        public bool applyFinish
        {
            get
            {
                return mApplyInstanceID == GetInstanceID();
            }
            set
            {
                mApplyInstanceID = GetInstanceID();
            }
        }
        ElementData elementData = null;
        public void ApplyElementToLua(LuaTable luaTable)
        {
            if (applyFinish) return; applyFinish = true;
            for (int i = 0; i < mElements.Count; i++)
            {
                 elementData = mElements[i];
                if (elementData.component == null) continue;
                if (string.IsNullOrEmpty(elementData.componentName)) continue;

                if (elementData.component is UIElement)
                {
                    var uiElement = elementData.component as UIElement;
                    if (uiElement.applyFinish) continue;
                    if (!mIsArray)
                    {
                        //luaTable.AddTable(elementData.componentName);
                        //uiElement.ApplyElementToLua(luaTable[elementData.componentName] as LuaTable);
                        luaTable.Set(elementData.componentName,LuaManager.Instance.LuaEnv.NewTable());
                        LuaTable inner;
                        luaTable.Get(elementData.componentName, out inner);
                        uiElement.ApplyElementToLua(inner);

                    }
                    else
                    {
                        //luaTable.AddTable(i + 1);
                        //uiElement.ApplyElementToLua(luaTable[i + 1] as LuaTable);

                        luaTable.Set(i + 1, LuaManager.Instance.LuaEnv.NewTable());
                        LuaTable inner;
                        luaTable.Get(i + 1, out inner);
                        uiElement.ApplyElementToLua(inner);
                    }
                }
                else
                {
                    if (!mIsArray)
                    {
                        luaTable.Set(elementData.componentName, elementData.component);
                    }
                    else
                    {
                        luaTable.Set(i + 1,elementData.component);
                    }
                }
            }
        }
    }
