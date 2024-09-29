using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TMP_HeightTest : MonoBehaviour
{
    public Image UI_Image;
    public RectTransform rectTrans;
    public TextMeshProUGUI UI_Text;
    // Start is called before the first frame update
    void Start()
    {
        //rectTrans = GetComponent<RectTransform>();
        rectTrans.sizeDelta = new Vector2(300, 300);
        Debug.Log(string.Format("renderedWidth:  {0} ,renderedHeight: {1} ", UI_Text.renderedWidth, UI_Text.renderedHeight));
        Debug.Log(string.Format("preferredHeight:  {0} ,preferredHeight: {1} ", UI_Text.preferredWidth, UI_Text.preferredHeight));
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("height:  " + rectTrans.rect.height);
        Debug.Log(string.Format("renderedWidth:  {0} ,renderedHeight: {1} ", UI_Text.renderedWidth, UI_Text.renderedHeight));
        Debug.Log(string.Format("preferredHeight:  {0} ,preferredHeight: {1} ", UI_Text.preferredWidth, UI_Text.preferredHeight));
    }
}
