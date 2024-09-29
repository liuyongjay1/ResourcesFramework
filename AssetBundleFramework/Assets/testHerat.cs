using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class testHerat : MonoBehaviour
{
    public Image image;

    public Material mat;
    // Start is called before the first frame update
    void Start()
    {
        image.material = mat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
