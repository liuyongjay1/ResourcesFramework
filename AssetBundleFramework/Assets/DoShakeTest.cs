using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class DoShakeTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOShakePosition(10, new Vector3(30,30,0),30);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
