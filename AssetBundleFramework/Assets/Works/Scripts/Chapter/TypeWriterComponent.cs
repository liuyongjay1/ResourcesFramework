using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using XLua;


public class TypeWriterComponent:MonoBehaviour
{

    RectTransform a;
    public TextMeshProUGUI UIText;
    //每个字打印延迟
    private float perWordDelay = 0.005f;
    private WaitForSeconds delayTimers;
    //完整的对话
    private string _Dialog;
    //
    private string _CurText;
    private LuaTable _Caller;
    private LuaFunction _Callback;

    private void Awake()
    {
        delayTimers = new WaitForSeconds(perWordDelay);
    }

    public void StartType(string text)
    {
        _Dialog = text;
        StartCoroutine(ShowText());
    }
    public void ClearDialogText()
    {
        UIText.text = "";
    }

    public void StopType()
    {
        StopCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i <= _Dialog.Length; i++)//遍历插入字符串的长度
        {
            _CurText = _Dialog.Substring(0, i);//看demo1的代码注释
            UIText.text = _CurText;
            if (i == _Dialog.Length)
            {
                LuaTable tab = LuaManager.Instance.LuaEnv.NewTable();
                LuaManager.Instance.CSSendEventToLua("UIEvent_TypeWriterFinish", tab);
                StopType();
            }
            yield return delayTimers;//每次延迟的时间 数值越小 延迟越少
        }
    }
}

