using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIButton : MonoBehaviour
{
    public int buttonID;
    [HideInInspector]
    private UIBase uiBase = null;
    public GameObject redTip = null;
    public UISoundEnum soundEnum = UISoundEnum.COMMON_CLICK;
    //自定义音频Id，soundEnum为Custom时调用
    public string audioId;
    private Button btn;

    private bool PreBtnState;
    public void Awake()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(OnClickBtn);
    }

    void OnClickBtn()
    {
        if (soundEnum == UISoundEnum.COMMON_CLICK)
            AudioManager.Instance.PlayAudio("Audio/click1.mp3");
    }
    public void Init(UIBase ui)
    {
        btn = gameObject.GetComponent<Button>();
        uiBase = ui;
    }

    /// <summary>
    /// 设置锁定按钮状态，通常用于新手引导
    /// </summary>
    /// <param name="isActive">true开启</param>
    public void OnLockBtn(bool isActive)
    {
        //记录按钮锁定之前的状态
        PreBtnState = btn.interactable;
        if (btn == null)
            LogManager.LogError("Btn is null,btnId: " + buttonID);
        btn.interactable = isActive;
    }

    /// <summary>
    /// 与OnLockBtn不同的是，这个接口给开发者设置按钮状态，无需记录之前按钮状态
    /// </summary>
    /// <param name="isActive"></param>
    public void OnSetBtnActive(bool isActive)
    {
        if (btn == null)
            LogManager.LogError("Btn is null,btnId: " + buttonID);
        btn.interactable = isActive;
    }

    /// <summary>
    /// 解锁按钮
    /// </summary>
    public void OnUnlockBtn()
    {
        if (btn == null)
            LogManager.LogError("Btn is null,btnId: " + buttonID);
        btn.interactable = PreBtnState;
    }

    public bool GetBtnActiveState()
    {
        if (btn == null)
            LogManager.LogError("Btn is null,btnId: " + buttonID);
        return btn.interactable;
    }
}
