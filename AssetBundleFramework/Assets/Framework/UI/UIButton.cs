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
    //�Զ�����ƵId��soundEnumΪCustomʱ����
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
    /// ����������ť״̬��ͨ��������������
    /// </summary>
    /// <param name="isActive">true����</param>
    public void OnLockBtn(bool isActive)
    {
        //��¼��ť����֮ǰ��״̬
        PreBtnState = btn.interactable;
        if (btn == null)
            LogManager.LogError("Btn is null,btnId: " + buttonID);
        btn.interactable = isActive;
    }

    /// <summary>
    /// ��OnLockBtn��ͬ���ǣ�����ӿڸ����������ð�ť״̬�������¼֮ǰ��ť״̬
    /// </summary>
    /// <param name="isActive"></param>
    public void OnSetBtnActive(bool isActive)
    {
        if (btn == null)
            LogManager.LogError("Btn is null,btnId: " + buttonID);
        btn.interactable = isActive;
    }

    /// <summary>
    /// ������ť
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
