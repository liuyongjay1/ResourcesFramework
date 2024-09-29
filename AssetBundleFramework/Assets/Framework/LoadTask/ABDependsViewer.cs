using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ABDependsViewer :  MonoBehaviour
{
    enum ShowType {
        None = 0,
        Level = 1,
        Focus = 2,
        
    }

    class ABInfo
    {
        public int levelIndex;
        public string abPath;
        public int dependCount;
    }
    public bool ShowGUI;
    private Vector2 scrollPosition;
    private float width;
    private float height;

    private int showLevel = 1;

    private List<ABInfo> showLevels = new List<ABInfo>();
    private List<string> focusRefList = new List<string>();
    private GUIStyle fontStyle = new GUIStyle();
    private GUIStyle levelBtnStyle = new GUIStyle();

    private Color defalutColor;
    private Color levelOneColor = Color.green;
    private Color levelTwoColor = Color.red;
    private Color levelThreeColor = Color.yellow;
    private Color levelFourColor = Color.blue;

    private ShowType curShowType = ShowType.Level;
    private string focusABPath;
    GUIStyle smallFont;
    GUIStyle largeFont;

    private 

    void Start()
    {
        smallFont = new GUIStyle();
        largeFont = new GUIStyle();

        smallFont.fontSize = 10;
        largeFont.fontSize = 32;
        defalutColor = GUI.backgroundColor;
    }

    private void CollectLevels(int levelIndex,List<ABLoadTask> tasks,List<ABInfo> abInfoList)
    {
        levelIndex++;
        for (int i = 0; i < tasks.Count; i++)
        {
            ABInfo info = new ABInfo();
            info.levelIndex = levelIndex;
            info.abPath = tasks[i].GetABPath();
            info.dependCount = tasks[i].GetRefCount();
            abInfoList.Add(info);
            if (tasks[i].GetABDepends().Count > 0)
            {
                CollectLevels(levelIndex, tasks[i].GetABDepends(), abInfoList);
            }
        }
    }

    private void GetFoucusItem(List<ABLoadTask> tasks, List<string> focusDepends)
    {
        for (int i = 0; i < tasks.Count; i++)
        {
            List<ABLoadTask> dependList = tasks[i].GetABDepends();
            for (int j = 0; j < dependList.Count; j++)
            {
                ABLoadTask abTask = dependList[j];
                if (abTask.GetABPath() == focusABPath)
                {
                    focusDepends.Add(tasks[i].GetABPath());
                }
                GetFoucusItem(dependList, focusDepends);
            }
        }
    }

    private void OnGUI()
    {
        if (!ShowGUI)
            return;
        if (!GameSetting.Instance.AssetbundleMode)
            return;
        if (curShowType == ShowType.Level)
            ShowLevels();
        else if (curShowType == ShowType.Focus)
            ShowFocus();
    }

    private void ShowLevels()
    {
        GUILayout.BeginVertical();
        if (GUILayout.Button("刷新", GUILayout.Width(300), GUILayout.Height(100)))
        {
            showLevel = 1;
            GetFirstLevelABList();
        }
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("一级", GUILayout.Width(150), GUILayout.Height(50)))
        {
            showLevel = 1;
        }
        else if (GUILayout.Button("二级", GUILayout.Width(150), GUILayout.Height(50)))
        {
            showLevel = 2;
        }
        else if (GUILayout.Button("三级", GUILayout.Width(150), GUILayout.Height(50)))
        {
            showLevel = 3;
        }
        else if (GUILayout.Button("四级", GUILayout.Width(150), GUILayout.Height(50)))
        {
            showLevel = 4;
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.Space(100);
        GUILayout.Button("ab包路径", GUILayout.Width(700), GUILayout.Height(100));
        GUILayout.Button("被依赖数量", GUILayout.Width(200), GUILayout.Height(100));
        GUILayout.EndHorizontal();

        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(1200), GUILayout.Height(800));
        for (int i = 0; i < showLevels.Count; i++)
        {
            GUI.backgroundColor = levelThreeColor;
            ABInfo info = showLevels[i];
            if (info.levelIndex <= showLevel)
            {
                if (info.levelIndex == 1)
                    GUI.backgroundColor = levelOneColor;
                else if (info.levelIndex == 2)
                    GUI.backgroundColor = levelTwoColor;
                else if (info.levelIndex == 3)
                    GUI.backgroundColor = levelThreeColor;
                else if (info.levelIndex == 4)
                    GUI.backgroundColor = levelFourColor;
                GUILayout.BeginHorizontal();
                int spaceValue = 100 * info.levelIndex;
                GUILayout.Space(spaceValue);
                //
                if (GUILayout.Button(info.abPath, GUILayout.Width(800 - spaceValue), GUILayout.Height(50)))
                {
                    focusRefList.Clear();
                    focusABPath = info.abPath;
                    GetFoucusItem(LoadTaskManager.Instance.GetABDependsList(), focusRefList);
                    curShowType = ShowType.Focus;
                }
                GUILayout.Button(info.dependCount.ToString(), GUILayout.Width(200), GUILayout.Height(50));
                GUILayout.EndHorizontal();
            }
        }
        GUI.backgroundColor = defalutColor;
        GUILayout.EndScrollView();
        GUILayout.EndVertical();
    }

    private void ShowFocus()
    {

        GUILayout.BeginVertical();
        if (GUILayout.Button("回退到层级界面", GUILayout.Width(200), GUILayout.Height(50)))
        {
            curShowType = ShowType.Level;
        }

        GUILayout.Button("当前查看目标: " + focusABPath, GUILayout.Height(50));
        GUILayout.Space(50);
        for (int i = 0; i < focusRefList.Count; i++)
        {
            GUILayout.Button(focusRefList[i], GUILayout.Width(800), GUILayout.Height(50));    
        }
        GUILayout.EndVertical();
    }

    private void GetFirstLevelABList()
    {
        showLevels.Clear();
        List<ABLoadTask> ABList = LoadTaskManager.Instance.GetABDependsList();
        CollectLevels(0, ABList, showLevels);
    }
}
