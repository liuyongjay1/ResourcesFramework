using UnityEngine;
using UnityEngine.UI;
public class UGUITransform : MonoBehaviour
{
    private RectTransform trans;
    private Image image;
    private Texture texture;
    private Button uiButton;
    InputField input;
    // Start is called before the first frame update
    void Start()
    {
        image.color = Color.black;
        trans = gameObject.GetComponent<RectTransform>();
        trans.offsetMax = Vector2.zero;
        trans.offsetMin = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("anchoredPosition: " + trans.anchoredPosition);
        Debug.Log("anchorMax: " + trans.anchorMax);
        Debug.Log("anchorMin: " + trans.anchorMin);
        Debug.Log("offsetMax: " + trans.offsetMax);
        Debug.Log("offsetMin: " + trans.offsetMin);
        Debug.Log("pivot: " + trans.pivot);
        //Debug.Log("anchorMax: " + trans.anchorMax);
    }
}
